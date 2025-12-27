using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

//画像サイズを変えるような処理の実装

namespace PaintDotCat
{
	public class ChangeImgSize : IEdit
	{
		private readonly Size m_Size;
		private readonly Color m_BaseColor;

		public ChangeImgSize( Size To, Color BaseColor ){	m_Size=To;	m_BaseColor=BaseColor;	}
		public EditTypes Edit( Content Cont )
		{
			if( Cont.ChangeImgSizeTo( m_Size.Width, m_Size.Height, m_BaseColor ) )
			{	return EditTypes.ImgSizeChange;	}

			return EditTypes.None;
		}
	}
}
