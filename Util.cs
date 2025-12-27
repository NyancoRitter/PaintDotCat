using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace PaintDotCat
{
	public static class Util
	{
		/// <summary>
		/// マウスボタンに対応する描画色index (0 or 1) を返す．
		/// </summary>
		/// <param name="button">マウスボタン</param>
		/// <returns>
		/// 描画色index (0 or 1)．
		/// ただし，どちらでもない状況では負の値を返す
		/// </returns>
		public static int DrawColorIndexFor( System.Windows.Forms.MouseButtons button )
		{
			if( button.HasFlag(System.Windows.Forms.MouseButtons.Left) ){	return 0;	}
			if( button.HasFlag(System.Windows.Forms.MouseButtons.Right ) ){	return 1;	}
			else return -1;
		}

		/// <summary>
		/// Bitmapをクリップボードにコピーする．
		/// ただし，Format32bppArgb の場合には他APPにうまくペーストできる形にはならない
		/// </summary>
		/// <param name="bmp">クリップボードにコピーするBitmap</param>
		public static void CopyBMP_To_Clipboard( Bitmap bmp )
		{
			if( bmp.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb )
			{
				//※ClipboardはBtimapの透明度をサポートしないらしいので，
				//アルファCHがある場合,PNGのデータをクリップボードに入れることで対処するとかいう話． 
				//この場合，自分は良いけども他のAPPには意味不明だからペーストできないという問題があるが……
				var DataObj = new System.Windows.Forms.DataObject();
				using( var PngMemStrm = new System.IO.MemoryStream() )
				{
					//てきとーに"PNG" というフォーマット名で突っ込む
					bmp.Save( PngMemStrm, System.Drawing.Imaging.ImageFormat.Png );
					DataObj.SetData( "PNG", false, PngMemStrm );

					//他のAPPへのペーストを考慮して DIB 形式も入れておく．
					//これだと「透明箇所が灰色になった画像」になるようだ．
					DataObj.SetData( System.Windows.Forms.DataFormats.Dib, true, bmp );

					//
					System.Windows.Forms.Clipboard.SetDataObject( DataObj, true );
				}
			}
			else
			{	System.Windows.Forms.Clipboard.SetImage( bmp );	}
		}

		/// <summary>
		/// クリップボードがらBitmapを取り出す．
		///		実装内容は<see cref="CopyBMP_To_Clipboard"/>と対応．
		/// </summary>
		/// <returns>取り出せた画像．失敗時にはnull</returns>
		public static Bitmap GetBMP_from_Clipboard()
		{
			var Data = System.Windows.Forms.Clipboard.GetDataObject();

			//※ここの実装は，CopyBMPToClipboard() と対応している：
			//自身が "PNG" というフォーマットでコピーしてるならそれを取り出す
			if( Data.GetDataPresent( "PNG", false ) )
			{
				var PngMemStrm = Data.GetData( "PNG" ) as System.IO.MemoryStream;
				if( PngMemStrm != null )
				{
					var Bmp = new Bitmap( PngMemStrm );
					PngMemStrm.Dispose();
					return Bmp;
				}
			}
			return Data.GetData(System.Windows.Forms.DataFormats.Bitmap) as Bitmap;
		}

		/// <summary>
		/// bmpをDispose()して且つnullにする
		/// </summary>
		/// <param name="bmp"></param>
		public static void DisposeBMP( ref Bitmap bmp )
		{
			if( bmp != null )
			{
				bmp.Dispose();
				bmp = null;
			}
		}

		/// <summary>
		/// 点群を包括するAABBを返す
		/// </summary>
		/// <param name="Points"></param>
		/// <returns></returns>
		public static Rectangle BoundingRect( IReadOnlyList<Point> Points )
		{
			if( !Points.Any() )return new Rectangle();

			Point TL = Points[0];
			Point RB = Points[0];
			for( int i=1; i<Points.Count; ++i )
			{
				var P = Points[i];
				TL.X = Math.Min( TL.X, P.X );
				TL.Y = Math.Min( TL.Y, P.Y );
				RB.X = Math.Max( RB.X, P.X );
				RB.Y = Math.Max( RB.Y, P.Y );
			}
			return new Rectangle( TL, new Size( RB.X-TL.X+1, RB.Y-TL.Y+1 ) );
		}

		/// <summary>
		/// ２つの画像を比較．
		/// （ハッシュ計算で実施しているので，完全に正当な結果を返すか否かは不明だが）
		/// </summary>
		/// <param name="BMP1"></param>
		/// <param name="BMP2"></param>
		/// <returns>内容が同一と思われるならtrue</returns>
		public static bool IsSame( Bitmap BMP1, Bitmap BMP2 )
		{
			if( !BMP1.Size.Equals( BMP2.Size ) )return false;

			var Cvter = new System.Drawing.ImageConverter();
			byte[] Bytes1 = new byte[1];
			Bytes1 = (byte[])Cvter.ConvertTo( BMP1, Bytes1.GetType() );
			byte[] Bytes2 = new byte[1];
			Bytes2 = (byte[])Cvter.ConvertTo( BMP2, Bytes2.GetType() );

			using( var SHA = SHA256.Create() )
			{
				byte[] Hash1 = SHA.ComputeHash( Bytes1 );
				byte[] Hash2 = SHA.ComputeHash( Bytes2 );
			
				for( int i=0; i<Hash1.Length; ++i )
				{
					if( Hash1[i] != Hash2[i] )return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 画像を指定の拡大率で描画する
		/// </summary>
		/// <param name="g">描画先</param>
		/// <param name="Img">画像</param>
		/// <param name="MagRate">拡大率</param>
		/// <param name="left_x1">描画先座標．ただし，MagRateが乗じられた値が用いられる</param>
		/// <param name="top_x1">描画先座標．ただし，MagRateが乗じられた値が用いられる</param>
		public static void DrawMagnifiedImg( Graphics g, Bitmap Img, int MagRate, int left_x1=0, int top_x1=0 )
		{
			var PreIPMode = g.InterpolationMode;
			var PreOffsetMode = g.PixelOffsetMode;
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
			g.DrawImage( Img, new Rectangle( left_x1*MagRate, top_x1*MagRate, Img.Width*MagRate, Img.Height*MagRate ) );
			g.PixelOffsetMode = PreOffsetMode;
			g.InterpolationMode = PreIPMode;
		}

		/// <summary>
		/// 矩形範囲選択状態の描画
		/// </summary>
		/// <param name="g">描画対象</param>
		/// <param name="Rect">選択範囲</param>
		/// <param name="MagRate">表示拡大率</param>
		/// <param name="DrawWhiteSolidRectInAdvance">破線を描く前に同箇所に白い枠を描く</param>
		public static void DrawRectSelectionState( Graphics g, Rectangle Rect, int MagRate, bool DrawWhiteSolidRectInAdvance=false )
		{
			if( Rect.IsEmpty )return;
			//※実装MEMO:
			//（Graphics の PixelOffsetMode 次第でズレ方が変わるようだが）どう設定しても
			//ごく普通に想定するような描画サイズにならない．
			//サイズを -1 しているのはそのための暫定的な対処．
			var DrawRect = new Rectangle( Rect.X*MagRate, Rect.Y*MagRate, Math.Max(1, Rect.Width*MagRate-1), Math.Max(1, Rect.Height*MagRate-1) );

			if( DrawWhiteSolidRectInAdvance )
			{ g.DrawRectangle( Pens.White, DrawRect ); }

			using( var P = new Pen( Color.Gray ) )
			{
				P.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
				g.DrawRectangle( P, DrawRect );
			}
		}
	}
}
