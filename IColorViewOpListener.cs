using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintDotCat
{
	/// <summary>
	/// <see cref="ColorForm"/>で成された操作に対するリスナ．
	/// 操作に対応した処理の実装側．
	/// </summary>
	public interface IColorViewOpListener
	{
		/// <summary>
		/// 描画色選択変更時
		/// </summary>
		/// <param name="iDrawColor">描画色index (0 or 1)．</param>
		/// <param name="Col">色</param>
		void OnColorSelected( int iDrawColor, System.Drawing.Color Col );
	}
}
