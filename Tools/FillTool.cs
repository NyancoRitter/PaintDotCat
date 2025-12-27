using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PaintDotCat
{
	/// <summary>
	/// 塗りつぶし( Flood Fill )
	/// </summary>
	public class FillTool : ITool
	{
		//塗りつぶし色
		private Color m_Color;

		//描画履歴データ
		private List<Point> m_Points = new List<Point>();

		//ドラッグしているボタン
		//	※ 値 None を「描画作業中ではない」という意味合いで用いている 
		private MouseButtons m_DraggingButton = MouseButtons.None;

		//-----------------------------------

		/// <summary>ctor</summary>
		public FillTool()
		{	Setup( Color.Black );	}

		/// <summary>セットアップ</summary>
		/// <param name="DrawColor">描画色</param>
		public void Setup( Color DrawColor )
		{	m_Color = DrawColor;	}

		//-----------------------------------
		#region ITool Impl

		/// <inheritdoc/>
		public ToolType Type => ToolType.Fill;

		/// <inheritdoc/>
		public bool IsBusy(){	return m_DraggingButton != MouseButtons.None;	}

		/// <inheritdoc/>
		public IEdit CreateEdit()
		{
			if( !m_Points.Any() )return null;
			return new DrawPixels( m_Points, m_Color );
		}


		/// <inheritdoc/>
		public void DrawStateToViewImg(Graphics g, int MagRate)
		{	/*NOP*/	}

		/// <inheritdoc/>
		public ToolProcResult OnMouseDown(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( m_DraggingButton == MouseButtons.None )
			{
				if( button.HasFlag( MouseButtons.Left ) )
				{	m_DraggingButton = MouseButtons.Left;	}
				else if( button.HasFlag( MouseButtons.Right ) )
				{	m_DraggingButton = MouseButtons.Right;	}
				else
				{	return ToolProcResult.None;	}

				//ボタン押下タイミングで処理を行う
				return ( FloodFill( pos, BMP )  ?  ToolProcResult.ShouldUpdateView  :  ToolProcResult.None );
			}
			else if( m_DraggingButton != button )
			{//現在と異なるボタンが押された場合，描画をキャンセル
				m_DraggingButton = MouseButtons.None;
				return ToolProcResult.EditShouldBeRejected;
			}

			return ToolProcResult.None;
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseMove(Point pos, Bitmap BMP){	return ToolProcResult.None;	}


		/// <inheritdoc/>
		public ToolProcResult OnMouseUp(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( m_DraggingButton == MouseButtons.None )return ToolProcResult.None;
			if( !button.HasFlag( m_DraggingButton ) )return ToolProcResult.None;
			
			m_DraggingButton = MouseButtons.None;
			return ( m_Points.Any()  ?  ToolProcResult.EditCompleted  :  ToolProcResult.EditShouldBeRejected );
		}

		#endregion
		
		private bool FloodFill( Point FromPos, Bitmap BMP )
		{
			m_Points.Clear();

			//開始点の色が描画色と同じだったら，やることはない
			Color TgtCol = BMP.GetPixel( FromPos.X, FromPos.Y );
			if( TgtCol == m_Color ){	return false;	}

			//
			int BPP = 0;	//Bytes per Pixel
			if( BMP.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ){	BPP=3;	}
			else if( BMP.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb ){	BPP=4;	}
			else {	return false;	}

			System.Drawing.Imaging.BitmapData bmpData = BMP.LockBits( new Rectangle( 0,0, BMP.Width,BMP.Height ), System.Drawing.Imaging.ImageLockMode.ReadWrite, BMP.PixelFormat );
			
			//
			byte TgtR = TgtCol.R;
			byte TgtG = TgtCol.G;
			byte TgtB = TgtCol.B;
			byte R = m_Color.R;
			byte G = m_Color.G;
			byte B = m_Color.B;

			var ST = new Stack<Point>();
			ST.Push( FromPos );
			unsafe
			{
				IntPtr ptr = bmpData.Scan0;

				while( ST.Any() )
				{
					var P = ST.Pop();

					Byte* p = (Byte*)ptr.ToPointer() + bmpData.Stride*P.Y + P.X*BPP;
					if( p[0]!=TgtB || p[1]!=TgtG || p[2]!=TgtR )continue;
					if( p[0]==B && p[1]==G && p[2]==R )continue;

					p[0] = B;
					p[1] = G;
					p[2] = R;
					m_Points.Add( P );

					if( P.X>=1 )ST.Push( new Point(P.X-1, P.Y) );
					if( P.X+1 < BMP.Width )ST.Push( new Point(P.X+1, P.Y) );
					if( P.Y>=1 )ST.Push( new Point(P.X, P.Y-1 ) );
					if( P.Y+1 < BMP.Height )ST.Push( new Point(P.X, P.Y+1) );
				}
			}
			BMP.UnlockBits(bmpData);
			return true;
		}
	}
}
