using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

//線を描く，ドットを描く　系の実装

namespace PaintDotCat
{
	/// <summary>
	/// 指定の画素群を指定の色に変える
	/// </summary>
	public class DrawPixels : IEdit, IDraw
	{
		private readonly Point[] m_Points;
		private readonly Color m_Col;
	
		public DrawPixels( IReadOnlyList<Point> Points, Color Col )
		{
			m_Points = Points.OrderBy( p=>p.X ).OrderBy( p=>p.Y ).ToArray();
			m_Col = Col;
		}

		public EditTypes Edit( Content Cont ){	Cont.Draw(this);	return EditTypes.Draw;	}	
		public void Draw( Bitmap BMP )
		{
			if( m_Points.Length < 10 )
			{//少量なら SetPixel でやる
				foreach( var Pos in m_Points ){	BMP.SetPixel( Pos.X, Pos.Y, m_Col );	}
				return;
			}

			//
			int BPP = 0;	//Bytes per Pixel
			if( BMP.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ){	BPP=3;	}
			else if( BMP.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb ){	BPP=4;	}
			else {	return;	}

			System.Drawing.Imaging.BitmapData bmpData = BMP.LockBits( new Rectangle( 0,0, BMP.Width,BMP.Height ), System.Drawing.Imaging.ImageLockMode.WriteOnly, BMP.PixelFormat );
			
			byte R = m_Col.R;
			byte G = m_Col.G;
			byte B = m_Col.B;
			unsafe
			{
				IntPtr ptr = bmpData.Scan0;
				foreach( var P in m_Points )
				{
					Byte* p = (Byte*)ptr.ToPointer() + bmpData.Stride*P.Y + P.X*BPP;
					p[0] = B;
					p[1] = G;
					p[2] = R;
				}
			}
			BMP.UnlockBits(bmpData);
		}
	}

	/// <summary>
	/// 指定の色で折れ線描画
	/// </summary>
	public class DrawLines : IEdit, IDraw
	{
		private readonly Point[] m_Points;
		private readonly Color m_Col;
		private int m_Thickness = 1;
	
		public DrawLines( IReadOnlyList<Point> Points, Color Col )
		{
			if( Points.Count<2 )throw new ArgumentException( "Invalid Points" );
			m_Points = Points.ToArray();
			m_Col = Col;
		}

		/// <summary>線の太さ（1以上）</summary>
		public int Thickness
		{
			get{	return m_Thickness;	}
			set{	m_Thickness = Math.Max(1,value);	}
		}

		/// <summary>先端を丸くするか（太さが1のときは無効）</summary>
		public bool RoundEnd{	get;	set;	} = false;

		public EditTypes Edit( Content Cont ){	Cont.Draw(this);	return EditTypes.Draw;	}	
		public void Draw( Bitmap BMP )
		{
			using( var g = Graphics.FromImage( BMP ) )
			{
				using( var Pen = new Pen( m_Col, m_Thickness ) )
				{
					if( m_Thickness>1 && RoundEnd )
					{
						Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
						Pen.EndCap= System.Drawing.Drawing2D.LineCap.Round;
					}
					g.DrawLines( Pen, m_Points );
				}
			}
		}
	}

	/// <summary>
	/// 指定の色で塗りつぶし矩形描画
	/// </summary>
	public class FillRect : IEdit, IDraw
	{
		private readonly Rectangle[] m_Rects;
		private readonly Color m_Col;

		public FillRect( Rectangle Rect, Color Col )
		{	m_Rects = new Rectangle[]{ Rect };	m_Col = Col;	}

		public EditTypes Edit( Content Cont ){	Cont.Draw(this);	return EditTypes.Draw;	}	
		public void Draw( Bitmap BMP )
		{
			using( var g = Graphics.FromImage( BMP ) )
			using( var Brush = new SolidBrush(m_Col) )
			{
				foreach( var Rect in m_Rects )
				{	g.FillRectangle( Brush, Rect );	}
			}
		}
	}

	/// <summary>
	/// 指定の色で指定範囲を塗りつぶす
	/// </summary>
	public class FillPath : IEdit, IDraw
	{
		private readonly System.Drawing.Drawing2D.GraphicsPath m_Path;
		private readonly Color m_Col;

		/// <summary>ctor</summary>
		/// <param name="Path">範囲．この参照がそのまま保持されるので注意</param>
		/// <param name="Col">色</param>
		public FillPath( System.Drawing.Drawing2D.GraphicsPath Path, Color Col )
		{	m_Path = Path;	m_Col = Col;	}

		public EditTypes Edit( Content Cont ){	Cont.Draw(this);	return EditTypes.Draw;	}	
		public void Draw( Bitmap BMP )
		{
			using( var g = Graphics.FromImage( BMP ) )
			using( var Brush = new SolidBrush(m_Col) )
			using( var Pen = new Pen( m_Col ) )
			{
				g.FillPath( Brush, m_Path );
				g.DrawPath( Pen, m_Path );
			}
		}
	}

	/// <summary>
	/// 矩形で折れ線描画：
	/// 「太い線」な感じ． 折れ線経路上の各画素位置に塗りつぶし矩形を描画
	/// </summary>
	public class DrawLinesWithSquare : IEdit, IDraw
	{
		private readonly Point[] m_Points;
		private readonly Color m_Col;
		private int m_SquareSize = 2;

		/// <summary>ctor</summary>
		/// <param name="Points">
		/// 折れ線頂点座標群．２点以上であること．
		/// 各点は「矩形の左上座標」を示す．
		/// </param>
		/// <param name="Col">描画色</param>
		/// <exception cref="ArgumentException">頂点数が不正の場合</exception>
		public DrawLinesWithSquare( IReadOnlyList<Point> Points, Color Col )
		{
			if( Points.Count<2 )throw new ArgumentException( "Invalid Points" );
			m_Points = Points.ToArray();
			m_Col = Col;
		}

		/// <summary>矩形サイズ（2以上）</summary>
		public int SquareSize
		{
			get{	return m_SquareSize;	}
			set{	m_SquareSize = Math.Max(2,value);	}
		}

		public EditTypes Edit( Content Cont ){	Cont.Draw(this);	return EditTypes.Draw;	}	
		public void Draw( Bitmap BMP )
		{
			using( var g = Graphics.FromImage( BMP ) )
			{
				using( var Brush = new SolidBrush( m_Col ) )
				{
					Action<int,int> Act = (int x, int y)=>{	g.FillRectangle( Brush, x,y, m_SquareSize, m_SquareSize );	};

					for( int i=0; i+1<m_Points.Length; ++i )
					{
						Algo.Bresenham( m_Points[i].X, m_Points[i].Y, m_Points[i+1].X, m_Points[i+1].Y, Act );
					}
				}
			}
		}
	}
}
