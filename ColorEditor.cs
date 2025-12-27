using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PaintDotCat
{
	/// <summary>
	/// ColorDialog での色の編集作業者．
	///		<remarks>
	///		ColorDialog上で作成した色を保持するために，
	///		このclassの同一のインスタンスを使い続ける必要がある．
	///		</remarks>
	/// </summary>
	public class ColorEditor
	{
		//ColorDialog.CustomColorsの内容を覚えておく用
		private int[] m_CreatedCols;

		/// <summary>
		/// ColorDialog による色の編集作業
		/// </summary>
		/// <param name="PrevColor">編集前の色</param>
		/// <param name="EditedColor">
		/// 編集結果の色を受け取る．
		/// ただしfalseが返された場合，値はPrevColorになっている．
		/// </param>
		/// <param name="OwnerWnd">ColorDialog の親にするウィンドウ</param>
		/// <returns>
		/// 編集したか否か：キャンセル操作がなされた場合にはfalse．
		/// </returns>
		public bool Edit( Color PrevColor, out Color EditedColor, IWin32Window OwnerWnd )
		{
			EditedColor = PrevColor;
			using( var Dlg = new ColorDialog() )
			{
				Dlg.FullOpen = true;
				Dlg.Color = PrevColor;
				if( m_CreatedCols != null )
				{ Dlg.CustomColors = m_CreatedCols; }

				if( Dlg.ShowDialog( OwnerWnd ) != DialogResult.OK )return false;

				m_CreatedCols = Dlg.CustomColors;
				EditedColor = Dlg.Color;
			}
			return true;
		}
	}
}
