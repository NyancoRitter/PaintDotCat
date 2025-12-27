using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PaintDotCat
{
	/// <summary>
	/// ペン．
	/// ドラッグされた箇所を幅1Pixelで描画する
	/// </summary>
	public class PenTool : ITool
	{
		private Pen m_Pen;	//描画用のペン

		//描画履歴データ
		private List<Point> m_Points = new List<Point>();
		private bool m_Is1stPointSkipped;

		//ドラッグしているボタン
		//	※ 値 None を「描画作業中ではない」という意味合いで用いている 
		private MouseButtons m_DraggingButton = MouseButtons.None;

		//水平or垂直 に線を引くモード用
		private bool m_IsLineMode = false;	//水平or垂直の線を引くモードか否か
		private Func<Point,Point> m_PointModifyFunc = null;

		//-----------------------------------

		/// <summary>ctor</summary>
		public PenTool()
		{	Setup( Color.Black );	}

		/// <summary>
		/// セットアップ
		/// </summary>
		/// <param name="DrawColor">描画色</param>
		public void Setup( Color DrawColor )
		{
			if( m_Pen==null )
			{	m_Pen = new Pen( DrawColor );	}
			else if( !m_Pen.Color.Equals( DrawColor ) )
			{
				m_Pen.Dispose();
				m_Pen = new Pen( DrawColor );
			}
		}

		//-----------------------------------

		private Point AsHorizontalLine( Point p ){	p.Y = m_Points.Last().Y;	 return p;	}
		private Point AsVerticalLine( Point p ){	p.X = m_Points.Last().X;	return p;	}

		//-----------------------------------
		#region ITool Impl

		/// <inheritdoc/>
		public ToolType Type => ToolType.Pen;

		/// <inheritdoc/>
		public bool IsBusy(){	return m_DraggingButton != MouseButtons.None;	}

		/// <inheritdoc/>
		public IEdit CreateEdit()
		{
			if( !m_Points.Any() )return null;

			if( m_Points.Count == 1 )
			{
				if( m_Is1stPointSkipped )return null;
				return new DrawPixels( m_Points, m_Pen.Color );
			}
			return new DrawLines( m_Points, m_Pen.Color );
		}


		/// <inheritdoc/>
		public void DrawStateToViewImg(Graphics g, int MagRate)
		{	/*NOP*/	}

		/// <inheritdoc/>
		public ToolProcResult OnMouseDown(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( m_DraggingButton == MouseButtons.None )
			{//描画開始タイミング
				{//最初の点を描画
					//※「同じ色の箇所を無意味にクリックしただけ」という操作はありがちなので
					//　描画開始位置が描画色と同じだったらそのことを記憶しておく
					var CurrPixCol = BMP.GetPixel( pos.X, pos.Y );
					if( CurrPixCol.ToArgb().Equals( m_Pen.Color.ToArgb() ) )
					{	m_Is1stPointSkipped=true;	}
					else
					{
						BMP.SetPixel( pos.X, pos.Y, m_Pen.Color );
						m_Is1stPointSkipped = false;
					}
				}
				m_Points.Clear();
				m_Points.Add( pos );
				m_DraggingButton = button;
				m_PointModifyFunc=null;
				m_IsLineMode = Control.ModifierKeys.HasFlag( Keys.Shift );

				return ToolProcResult.ShouldUpdateView;
			}
			else if( m_DraggingButton != button )
			{//現在と異なるボタンが押された場合，描画をキャンセル
				m_DraggingButton = MouseButtons.None;
				return ToolProcResult.EditShouldBeRejected;
			}

			return ToolProcResult.None;
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseMove(Point pos, Bitmap BMP)
		{
			if( m_DraggingButton==MouseButtons.None || !m_Points.Any() )return ToolProcResult.None;

			var PrevPos = m_Points.Last();
			if( pos.Equals(m_Points.Last()) )return ToolProcResult.None;

			if( m_IsLineMode && m_PointModifyFunc==null )
			{
				if( Math.Abs( pos.X - PrevPos.X ) >= Math.Abs( pos.Y - PrevPos.Y ) )
				{	m_PointModifyFunc = this.AsHorizontalLine;	}
				else
				{	m_PointModifyFunc = this.AsVerticalLine;	}
			}

			if( m_PointModifyFunc != null )
			{	pos = m_PointModifyFunc( pos );	}

			using( var g = Graphics.FromImage(BMP) )
			{	g.DrawLine( m_Pen, PrevPos, pos );	}
			
			m_Points.Add( pos );
			return ToolProcResult.ShouldUpdateView;
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseUp(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( m_DraggingButton == MouseButtons.None )return ToolProcResult.None;
			m_DraggingButton = MouseButtons.None;

			//描画の必要がなかったことの判定
			//現時点では，
			//「１箇所をクリックしただけで，且つ，そこの色は描画色と同じだった」
			//という状況のみを判定している．
			if( m_Is1stPointSkipped && m_Points.Count<=1 )
			{	return ToolProcResult.EditShouldBeRejected;	}

			return ToolProcResult.EditCompleted;
		}

		#endregion
	}
}
