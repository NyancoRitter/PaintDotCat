using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PaintDotCat
{
	/// <summary>
	/// BMPに何かを描画する
	/// </summary>
	public interface IDraw
	{
		/// <summary>描画する</summary>
		/// <param name="BMP">描画対象画像</param>
		void Draw( Bitmap BMP );
	}

	[Flags]
	public enum EditTypes
	{
		None = 0,
		ImgSizeChange = 1,
		Draw = 2
	}

	/// <summary>
	/// <see cref="Content"/>の編集処理
	/// </summary>
	public interface IEdit
	{
		/// <summary>編集を適用する</summary>
		/// <param name="Cont">編集対象データ</param>
		/// <returns>行った編集内容</returns>
		EditTypes Edit( Content Cont );
	}

	/// <summary>
	/// <see cref="IEdit"/>のシーケンスを単一の「編集」として扱う
	/// </summary>
	public class EditSeq : IEdit
	{
		private List<IEdit> m_Edits = new List<IEdit>();

		public bool IsEmpty => !m_Edits.Any();
		public EditSeq Add( IEdit e ){	m_Edits.Add(e);	return this;	}
		public EditTypes Edit( Content Cont )
		{
			EditTypes ret = EditTypes.None;
			foreach( var e in m_Edits ){	ret |= e.Edit( Cont );	}
			return ret;
		}
	}
}
