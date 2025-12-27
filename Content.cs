using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PaintDotCat
{
	/// <summary>
	/// APPのデータ（＝画像データ）を保持
	/// </summary>
	public class Content
	{
		private Bitmap m_BMP;	//画像データ

		//-----------------------------------

		/// <summary>ctor. 初期の画像サイズを指定する</summary>
		/// <param name="w">初期の画像サイズ</param>
		/// <param name="h">初期の画像サイズ</param>
		public Content( int w, int h )
		{	ChangeImgSizeTo(w,h,InitialColor);	}

		//-----------------------------------
		#region public

		/// <summary>初期の画像の色</summary>
		public static Color InitialColor => Color.White;

		/// <summary>現在の画像サイズ</summary>
		public int Width{	get{	return m_BMP.Width;	}	}
		/// <summary>現在の画像サイズ</summary>
		public int Height{	get{	return m_BMP.Height;	}	}
		/// <summary>現在の画像サイズ</summary>
		public Size Size{	get{	return new Size(Width,Height);	}	}

		/// <summary>
		/// 画像内容を引数に差し替える
		/// </summary>
		/// <param name="SrcBMP">
		/// nullであってはならない．
		/// この参照がそのまま保持される点に注意．
		/// </param>
		public void ChangeTo( Bitmap SrcBMP )
		{
			Util.DisposeBMP( ref m_BMP );
			m_BMP = SrcBMP;
		}

		/// <summary>
		/// 画像サイズの変更．
		/// 変更結果には変更前の画像の内容が可能な限り含まれる．
		/// </summary>
		/// <param name="w">新しいサイズ</param>
		/// <param name="h">新しいサイズ</param>
		/// <param name="BaseColor">変更直後の画像全体の色</param>
		/// <returns>
		/// 変更したか否か．
		/// 引数が現在のサイズである場合には何もせずにfalseを返す．
		/// </returns>
		public bool ChangeImgSizeTo( int w, int h, Color BaseColor )
		{
			if( w<0 || h<0 )throw new ArgumentOutOfRangeException( "Invalid Image Size" );

			if( m_BMP!=null && m_BMP.Width==w && m_BMP.Height==h )
			{	return false;	}

			var NewBMP = new Bitmap( w,h, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
			using( var g = Graphics.FromImage( NewBMP ) )
			{
				g.Clear( BaseColor );

				if( m_BMP != null )
				{
					g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
					g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
					g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
					g.DrawImageUnscaled( m_BMP, 0,0 );
					m_BMP.Dispose();
				}
			}
			m_BMP = NewBMP;
			return true;
		}

		/// <summary>
		/// 指定pixelの色を取得
		/// </summary>
		/// <param name="pos">pixel座標</param>
		/// <returns>色</returns>
		public Color GetPixel( Point pos ){	return m_BMP.GetPixel( pos.X, pos.Y );	}

		/// <summary>
		/// 現在の画像を指定の拡大率で描画する
		/// </summary>
		/// <param name="g">描画先</param>
		/// <param name="MagRate">拡大率</param>
		public void DrawMagnifiedImgTo( Graphics g, int MagRate ){	Util.DrawMagnifiedImg( g, m_BMP, MagRate );	}

		/// <summary>
		/// 現在の画像のクローンを生成して返す
		/// </summary>
		/// <returns>クローン</returns>
		public Bitmap CreateCurrImgClone(){	return (Bitmap)m_BMP.Clone();	}

		/// <summary>
		/// 現在の画像の部分画像を生成して返す．
		/// </summary>
		/// <param name="Rect">範囲</param>
		/// <returns>部分画像</returns>
		public Bitmap CreatePartialImg( Rectangle Rect ){	return m_BMP.Clone( Rect, m_BMP.PixelFormat );	}

		/// <summary>
		/// 現在の画像の部分画像を生成して返す．
		/// 画像サイズは引数を包括する矩形のサイズとなる．
		/// フォーマットは Format32bppArgb となり，引数により指定された範囲の外側は透明となる．
		/// </summary>
		/// <param name="Path">範囲指定</param>
		/// <returns></returns>
		public Bitmap CreatePartialImg( System.Drawing.Drawing2D.GraphicsPath Path )
		{
			var AABB = Rectangle.Round( Path.GetBounds() );
			++AABB.Width;
			++AABB.Height;
			AABB.Intersect( new Rectangle( new Point(0,0), m_BMP.Size ) );
			var BMP = m_BMP.Clone( AABB, System.Drawing.Imaging.PixelFormat.Format32bppArgb );

			const int BPP = 4;	//Bytes per Pixel
			System.Drawing.Imaging.BitmapData bmpData = BMP.LockBits( new Rectangle( 0,0, BMP.Width,BMP.Height ), System.Drawing.Imaging.ImageLockMode.ReadWrite, BMP.PixelFormat );

			var Mask = new Bitmap(BMP.Width, BMP.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			using( var g = Graphics.FromImage( Mask ) )
			{
				g.Clear( Color.Black );
				g.TranslateTransform( -AABB.Left, -AABB.Top );
				g.FillPath( Brushes.White, Path );
				g.DrawPath( Pens.White, Path );
			}
			System.Drawing.Imaging.BitmapData MaskData = Mask.LockBits( new Rectangle( 0,0, BMP.Width,BMP.Height ), System.Drawing.Imaging.ImageLockMode.ReadOnly, Mask.PixelFormat );

			unsafe
			{
				IntPtr ptr = bmpData.Scan0;
				IntPtr pMask = MaskData.Scan0;
				for( int y=0; y<BMP.Height; ++y )
				{
					Byte* p = (Byte*)ptr.ToPointer() + bmpData.Stride*y;
					Byte* pM = (Byte*)pMask.ToPointer() + MaskData.Stride*y;
					for( int x=0; x<BMP.Width; ++x )
					{
						if( pM[0]==0 )
						{	p[3] = 0;	}

						p += BPP;
						pM += 3;
					}
				}
			}
			BMP.UnlockBits(bmpData);
			Mask.UnlockBits(MaskData);
			Mask.Dispose();
			return BMP;
		}

		/// <summary>
		/// 描画処理の実施
		/// </summary>
		/// <param name="Painter">描画実施者</param>
		public void Draw( IDraw Painter ){	Painter.Draw( m_BMP );	}

		/// <summary>
		/// 指定ファイル名で画像を保存する．
		/// 失敗時には例外が送出される．
		/// </summary>
		/// <param name="SaveFilePath">保存パス</param>
		/// <param name="format">保存フォーマット</param>
		public void SaveAs( string SaveFilePath, System.Drawing.Imaging.ImageFormat format )
		{
			//24bit化して保存する
			using( Bitmap BMP24 = new Bitmap( m_BMP.Width, m_BMP.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb ) )
			{
				using( Graphics gr = Graphics.FromImage(BMP24) )
				{
					gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
					gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
					gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
					gr.DrawImageUnscaled( m_BMP, 0,0 );
				}

				BMP24.Save( SaveFilePath, format );
			}
		}

		/// <summary>
		/// 現在の画像が引数画像と同一か否かを判定．
		/// （編集操作が結果として何もしていないことを判定する用）
		/// </summary>
		/// <param name="BMP"></param>
		/// <returns>判定結果．同一と思われるならtrue</returns>
		public bool IsSame( Bitmap BMP ){	return Util.IsSame( m_BMP, BMP );	}

		/// <summary>
		/// 指定ファイル名で画像をモノクロBMPとして保存する．
		/// 失敗時には例外が送出される．
		/// </summary>
		/// <param name="ExportFilePath">保存パス</param>
		public void ExportAsMonoBMP( string ExportFilePath )
		{
			using( var ExportImg = CvtToMonoBMP() )
			{	ExportImg.Save( ExportFilePath, System.Drawing.Imaging.ImageFormat.Bmp );	}
		}

		#endregion
		//-----------------------------------
		#region private

		/// <summary>m_BMP をモノクロBMPに変換したものを生成して返す</summary>
		/// <returns>結果のモノクロBMP</returns>
		private Bitmap CvtToMonoBMP()
		{
			Bitmap MonoBmp = new Bitmap( m_BMP.Width, m_BMP.Height, System.Drawing.Imaging.PixelFormat.Format1bppIndexed );
			var OutData = MonoBmp.LockBits( new Rectangle( 0,0, MonoBmp.Width,MonoBmp.Height ), System.Drawing.Imaging.ImageLockMode.WriteOnly, MonoBmp.PixelFormat );

			int SrcBPP = 0;	//Bytes per Pixel
			if( m_BMP.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ){	SrcBPP=3;	}
			else if( m_BMP.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb ){	SrcBPP=4;	}
			else {	throw new InvalidOperationException( "Invalid BMP Format" );	}

			var SrcData = m_BMP.LockBits( new Rectangle( 0,0, m_BMP.Width,m_BMP.Height ), System.Drawing.Imaging.ImageLockMode.ReadOnly, m_BMP.PixelFormat );

			unsafe
			{
				for( int y=0; y<m_BMP.Height; ++y )
				{
					Byte* pSrc = (Byte*)SrcData.Scan0.ToPointer() + SrcData.Stride*y;
					Byte* pOut = (Byte*)OutData.Scan0.ToPointer() + OutData.Stride*y;
					for( int x=0; x<m_BMP.Width; ++x )
					{
						int OutBitPos = ( x & 0x07 );
						if( pSrc[0]+pSrc[1]+pSrc[2] >= 128*3 )  //※閾値は単純な固定実装
						{	*pOut |= (byte)( 0x80 >> OutBitPos );	}

						pSrc += SrcBPP;
						if( OutBitPos == 7 ){	++pOut;	}
					}
				}
			}
			m_BMP.UnlockBits(SrcData);
			MonoBmp.UnlockBits(OutData);

			return MonoBmp;
		}

		#endregion
	}
}
