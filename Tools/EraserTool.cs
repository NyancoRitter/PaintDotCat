using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace PaintDotCat
{
	/// <summary>
	/// 消しゴムツール
	/// </summary>
	public class EraserTool : ITool
	{
		//描画履歴データ
		private List<Point> m_Points = new List<Point>();

		//ドラッグしているボタン
		//	※ 値 None を「描画作業中ではない」という意味合いで用いている 
		private MouseButtons m_DraggingButton = MouseButtons.None;

		//表示用
		private Point m_CenterPos;

		//-----------------------------------

		/// <summary>ctor</summary>
		public EraserTool()
		{	Setup( Color.Black, Color.White, 1 );	}

		/// <summary>セットアップ</summary>
		/// <param name="LeftColor">左ボタンの色</param>
		/// <param name="RightColor">右ボタンの色</param>
		/// <param name="Size">描画正方形のサイズ（一片の長さ）．ただし奇数想定．</param>
		public void Setup( Color LeftColor, Color RightColor, int Size )
		{
			this.LeftColor = LeftColor;
			this.RightColor = RightColor;
			ChangeSize( Size );
		}

		/// <summary>
		/// 描画サイズの変更
		/// </summary>
		/// <param name="Size">描画正方形のサイズ（一片の長さ）．ただし奇数想定．</param>
		public void ChangeSize( int Size )
		{	this.Radius = Math.Max( 1, (Size/2) );	}

		//-----------------------------------

		private Color LeftColor{	get;	set;	}
		private Color RightColor{	get;	set;	}
		private int Radius{	get;	set;	}	//※描画範囲は中央画素から上下左右にこのサイズ分（：一辺が Radius*2+1 な正方形）

		//-----------------------------------
		#region ITool Impl

		/// <inheritdoc/>
		public ToolType Type => ToolType.Eraser;

		/// <inheritdoc/>
		public bool IsBusy(){	return m_DraggingButton != MouseButtons.None;	}

		/// <inheritdoc/>
		public IEdit CreateEdit()
		{
			if( !m_Points.Any() )return null;
			return new DrawPixels( m_Points, RightColor );
		}

		/// <inheritdoc/>
		public void DrawStateToViewImg(Graphics g, int MagRate)
		{
			var Rect = new Rectangle( m_CenterPos, new Size(2*Radius+1,2*Radius+1) );
			Rect.Offset( -Radius, -Radius );
			Util.DrawRectSelectionState( g, Rect, MagRate, true );
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseDown(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( m_DraggingButton == MouseButtons.None )
			{//描画開始タイミング
				if( button.HasFlag( MouseButtons.Left ) )
				{	m_DraggingButton = MouseButtons.Left;	}
				else if( button.HasFlag( MouseButtons.Right ) )
				{	m_DraggingButton = MouseButtons.Right;	}
				else
				{	return ToolProcResult.None;	}

				m_Points.Clear();
				m_CenterPos = pos;
				Erase( pos, BMP );
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
			if( m_DraggingButton!=MouseButtons.None )
			{	Erase( pos, BMP );	}
			return ToolProcResult.ShouldUpdateView;
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseUp(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( m_DraggingButton == MouseButtons.None )return ToolProcResult.None;
			m_DraggingButton = MouseButtons.None;

			return ( m_Points.Any()  ?  ToolProcResult.EditCompleted  :  ToolProcResult.EditShouldBeRejected );
		}

		#endregion

		private bool Erase( Point Center, Bitmap BMP )
		{
			int Left = Math.Max( 0, Center.X - Radius );
			int Right = Math.Min( BMP.Width-1, Center.X + Radius );
			int Top = Math.Max( 0, Center.Y - Radius );
			int Bottom = Math.Min( BMP.Height-1, Center.Y + Radius );
			int W = Right-Left+1;
			int H = Bottom-Top+1;
			if( W<=0 || H<=0 )return false;

			int BPP = 0;	//Bytes per Pixel
			if( BMP.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ){	BPP=3;	}
			else if( BMP.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb ){	BPP=4;	}
			else {	return false;	}

			System.Drawing.Imaging.BitmapData bmpData = BMP.LockBits( new Rectangle( 0,0, BMP.Width,BMP.Height ), System.Drawing.Imaging.ImageLockMode.ReadWrite, BMP.PixelFormat );
			
			
			byte R = RightColor.R;
			byte G = RightColor.G;
			byte B = RightColor.B;
			int Pre_nPoint = m_Points.Count;
			unsafe
			{
				IntPtr ptr = bmpData.Scan0;
				for( int y = Top; y <= Bottom; ++y )
				{
					Byte* p = (Byte*)ptr.ToPointer() + bmpData.Stride * y + Left * BPP;
					for( int x = Left; x <= Right; ++x )
					{
						if( p[0]!=B || p[1]!=G || p[2]!=R )
						{
							//右ボタンの場合は，左カラーと同じ個所のみ
							if( m_DraggingButton!=MouseButtons.Right || ( p[0]==LeftColor.B && p[1]==LeftColor.G && p[2]==LeftColor.R) )
							{
								p[0] = B;
								p[1] = G;
								p[2] = R;
								m_Points.Add( new Point( x, y ) );
							}
						}
						p += BPP;
					}
				}
			}
			BMP.UnlockBits(bmpData);
			return ( Pre_nPoint != m_Points.Count );
		}
		

	}
}
