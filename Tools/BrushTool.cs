using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PaintDotCat
{
	/// <summary>
	/// ブラシ．
	/// ドラッグされた箇所を特定パターンで描画する
	///		<remarks>
	///		※現実装では 2x2 パターンのみ．
	///		</remarks>
	/// </summary>
	public class BrushTool : ITool
	{
		private SolidBrush m_Brush;	//描画用のブラシ

		//描画履歴データ
		private List<Point> m_Points = new List<Point>();

		//ドラッグしているボタン
		//	※ 値 None を「描画作業中ではない」という意味合いで用いている 
		private MouseButtons m_DraggingButton = MouseButtons.None;

		//水平or垂直 に線を引くモード用
		private bool m_IsLineMode = false;	//水平or垂直の線を引くモードか否か
		private Func<Point,Point> m_PointModifyFunc = null;

		//表示用
		private Point m_CenterPos;

		//-----------------------------------

		/// <summary>ctor</summary>
		public BrushTool()
		{	Setup( Color.Black );	}

		/// <summary>
		/// セットアップ
		/// </summary>
		/// <param name="DrawColor">描画色</param>
		public void Setup( Color DrawColor )
		{
			if( m_Brush==null )
			{	m_Brush = new SolidBrush( DrawColor );	}
			else if( !m_Brush.Color.Equals( DrawColor ) )
			{
				m_Brush.Dispose();
				m_Brush = new SolidBrush( DrawColor );
			}
		}

		//-----------------------------------

		private Point AsHorizontalLine( Point p ){	p.Y = m_Points.Last().Y;	 return p;	}
		private Point AsVerticalLine( Point p ){	p.X = m_Points.Last().X;	return p;	}

		//-----------------------------------
		#region ITool Impl

		/// <inheritdoc/>
		public ToolType Type => ToolType.Brush;

		/// <inheritdoc/>
		public bool IsBusy(){	return m_DraggingButton != MouseButtons.None;	}

		/// <inheritdoc/>
		public IEdit CreateEdit()
		{
			if( !m_Points.Any() )return null;

			if( m_Points.Count==1 )
			{	return new FillRect( new Rectangle(m_Points[0],new Size(2,2)), m_Brush.Color );	}
			else
			{	return new DrawLinesWithSquare( m_Points, m_Brush.Color );	}
		}

		/// <inheritdoc/>
		public void DrawStateToViewImg(Graphics g, int MagRate)
		{
			var Rect = new Rectangle( m_CenterPos, new Size(2,2) );
			Util.DrawRectSelectionState( g, Rect, MagRate, true );
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseDown(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( m_DraggingButton == MouseButtons.None )
			{//描画開始タイミング

				using( var g = Graphics.FromImage( BMP ) )
				{
					g.FillRectangle( m_Brush, new Rectangle(pos, new Size(2,2)) );
				}

				m_Points.Clear();
				m_Points.Add( pos );
				m_DraggingButton = button;
				m_PointModifyFunc=null;
				m_IsLineMode = Control.ModifierKeys.HasFlag( Keys.Shift );
				m_CenterPos = pos;
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
			if( pos.Equals(m_CenterPos) )return ToolProcResult.None;
			m_CenterPos = pos;

			if( m_DraggingButton==MouseButtons.None || !m_Points.Any() )return ToolProcResult.ShouldUpdateView;
			var PrevPos = m_Points.Last();
			if( pos.Equals(m_Points.Last()) )return ToolProcResult.ShouldUpdateView;

			if( m_IsLineMode && m_PointModifyFunc==null )
			{
				if( Math.Abs( pos.X - PrevPos.X ) >= Math.Abs( pos.Y - PrevPos.Y ) )
				{	m_PointModifyFunc = this.AsHorizontalLine;	}
				else
				{	m_PointModifyFunc = this.AsVerticalLine;	}
			}

			if( m_PointModifyFunc != null )
			{	pos = m_PointModifyFunc( pos );	}

			using( var g = Graphics.FromImage( BMP ) )
			{
				Action<int,int> Act = (int x, int y)=>{	g.FillRectangle( m_Brush, x,y, 2, 2 );	};
				Algo.Bresenham( PrevPos.X, PrevPos.Y, pos.X, pos.Y, Act );
			}

			m_Points.Add( pos );
			return ToolProcResult.ShouldUpdateView;
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseUp(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( m_DraggingButton == MouseButtons.None )return ToolProcResult.None;
			m_DraggingButton = MouseButtons.None;
			return ToolProcResult.EditCompleted;
		}

		#endregion
	}
}
