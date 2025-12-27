using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PaintDotCat
{
	/// <summary>
	/// 直線描画ツール
	/// </summary>
	public class LineTool : ITool
	{
		private Pen m_Pen;	//描画用のペン
		private int m_Thickness = 1;
		private bool m_RoundEnd = false;

		//頂点座標
		private Point[] m_Points = new Point[2];

		//ドラッグしているボタン
		//	※ 値 None を「描画作業中ではない」という意味合いで用いている 
		private MouseButtons m_DraggingButton = MouseButtons.None;

		private Bitmap m_Backup;

		//-----------------------------------

		/// <summary>設定</summary>
		public class Settings
		{
			private int m_Thickness = 1;

			/// <summary>太さ（1以上）</summary>
			public int Thickness
			{
				get{	return m_Thickness;	}
				set{	m_Thickness = Math.Max( 1, value );	}
			}

			/// <summary>先端を丸めるか否か（太さが1のときは無視される）</summary>
			public bool RoundEnd{	get;	set;	}
		}

		/// <summary>ctor</summary>
		public LineTool()
		{	Setup( Color.Black, new Settings() );	}

		/// <summary>
		/// セットアップ
		/// </summary>
		/// <param name="DrawColor">描画色</param>
		/// <param name="Setting">設定</param>
		public void Setup( Color DrawColor, Settings Setting )
		{
			m_Thickness = Setting.Thickness;
			m_RoundEnd = Setting.RoundEnd;

			if( m_Pen != null )m_Pen.Dispose();
			m_Pen = new Pen( DrawColor, m_Thickness );
			
			if( m_Thickness>1 && m_RoundEnd )
			{
				m_Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
				m_Pen.EndCap= System.Drawing.Drawing2D.LineCap.Round;
			}
		}

		//-----------------------------------
		#region ITool Impl

		/// <inheritdoc/>
		public ToolType Type => ToolType.Line;

		/// <inheritdoc/>
		public bool IsBusy(){	return m_DraggingButton != MouseButtons.None;	}

		/// <inheritdoc/>
		public IEdit CreateEdit()
		{
			if( m_Points[0].Equals(m_Points[1]) )return null;
			var Editor = new DrawLines( m_Points, m_Pen.Color );
			Editor.Thickness = m_Thickness;
			Editor.RoundEnd = m_RoundEnd;
			return Editor;
		}


		/// <inheritdoc/>
		public void DrawStateToViewImg(Graphics g, int MagRate)
		{	/*NOP*/	}

		/// <inheritdoc/>
		public ToolProcResult OnMouseDown(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( m_DraggingButton == MouseButtons.None )
			{
				//操作開始時点の画像のコピーを得ておく
				Util.DisposeBMP( ref m_Backup );
				m_Backup = new Bitmap( BMP );

				//
				m_Points[0] = pos;
				m_Points[1] = pos;
				m_DraggingButton = button;
				return ToolProcResult.None;
			}
			else if( m_DraggingButton != button )
			{//現在と異なるボタンが押された場合，描画をキャンセル
				m_DraggingButton = MouseButtons.None;
				Util.DisposeBMP( ref m_Backup );
				return ToolProcResult.EditShouldBeRejected;
			}

			return ToolProcResult.None;
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseMove(Point pos, Bitmap BMP)
		{
			if( m_DraggingButton==MouseButtons.None )return ToolProcResult.None;

			if( Control.ModifierKeys.HasFlag( Keys.Shift ) )
			{//SHIFT押下時は 45単位の方向に制限
				int dx = pos.X - m_Points[0].X;
				int dy = pos.Y - m_Points[0].Y;
				double Thresh = Math.Sqrt( dx*dx + dy*dy ) * Math.Cos( 22.5 * Math.PI / 180.0 );
				if( Math.Abs(dx) >= Math.Abs(dy) )
				{
					if( Math.Sign(dx) * dx >= Thresh )
					{	pos.Y = m_Points[0].Y;	}
					else
					{	pos.Y = m_Points[0].Y + Math.Sign(dy)*Math.Abs(dx);	}
				}
				else
				{
					if( Math.Sign(dy) * dy >= Thresh )
					{	pos.X = m_Points[0].X;	}
					else
					{	pos.X = m_Points[0].X + Math.Sign(dx)*Math.Abs(dy);	}
				}
			}

			if( m_Points[1].Equals(pos) )return ToolProcResult.None;
			m_Points[1] = pos;
			if( m_Points[0].Equals(m_Points[1]) )return ToolProcResult.None;

			using( var g = Graphics.FromImage(BMP) )
			{
				var POM = g.PixelOffsetMode;

				g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
				g.DrawImageUnscaled( m_Backup, 0,0 );

				g.PixelOffsetMode = POM;
				g.DrawLine( m_Pen, m_Points[0], m_Points[1] );
			}
			
			return ToolProcResult.ShouldUpdateView;
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseUp(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( m_DraggingButton == MouseButtons.None )return ToolProcResult.None;
			
			m_DraggingButton = MouseButtons.None;
			Util.DisposeBMP( ref m_Backup );

			if( m_Points[0].Equals(m_Points[1]) )return ToolProcResult.EditShouldBeRejected;
			return ToolProcResult.EditCompleted;
		}

		#endregion
	}
}
