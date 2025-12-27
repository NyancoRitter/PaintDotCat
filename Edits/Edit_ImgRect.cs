using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

//画像を貼りつけるような描画の実装

namespace PaintDotCat
{
	/// <summary>
	/// 単純に画像を丸ごと差し替える
	/// </summary>
	public class ChangeImg : IEdit
	{
		private readonly Bitmap m_BMP;
		
		/// <summary>ctor</summary>
		/// <param name="BMP">※このインスタンスはこの引数（参照）をそのまま保持する</param>
		public ChangeImg( Bitmap BMP ){	m_BMP=BMP;	}

		public EditTypes Edit(Content Cont)
		{
			var Ret = EditTypes.Draw;
			if( Cont.Width!=m_BMP.Width || Cont.Height!=m_BMP.Height )
			{	Ret |= EditTypes.ImgSizeChange;	}

			Cont.ChangeTo( (Bitmap)m_BMP.Clone() );
			return Ret;
		}
	}

	/// <summary>
	/// 画像データを特定の位置に描画する
	/// </summary>
	public class DrawImg : IEdit, IDraw
	{
		private readonly Bitmap m_WriteImg;
		private readonly Point m_WriteTopLeft;

		/// <summary>
		/// ctor
		/// <see cref="Exec"/> の処理は「WriteImg 全体を，Contの指定位置に描画する」となる．
		/// </summary>
		/// <param name="WriteImg">※このインスタンスはこの引数（参照）をそのまま保持する</param>
		/// <param name="WriteTopLeft">描画位置左上座標</param>
		public DrawImg( Bitmap WriteImg, Point WriteTopLeft )
		{
			m_WriteImg = WriteImg;
			m_WriteTopLeft = WriteTopLeft;
		}

		public EditTypes Edit( Content Cont )
		{
			using( var Prev = Cont.CreateCurrImgClone() )
			{
				Cont.Draw( this );
				return ( Cont.IsSame(Prev)  ?  EditTypes.None  :  EditTypes.Draw );
			}
		}

		public void Draw( Bitmap BMP )
		{
			using( var g = Graphics.FromImage( BMP ) )
			{
				g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				//g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
				g.DrawImageUnscaled( m_WriteImg, m_WriteTopLeft );
			}
		}
	}
}
