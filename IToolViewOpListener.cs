using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintDotCat
{
	/// <summary>
	/// <see cref="ToolForm"/>で成された操作に対するリスナ．
	/// 操作に対応した処理の実装側．
	/// </summary>
	public interface IToolViewOpListener
	{
		/// <summary>
		/// ツールの選択操作が成されたとき
		/// </summary>
		/// <param name="SelectedType">選択されたツール種類</param>
		void OnSelectedToolChanged( ToolType SelectedType );

		/// <summary>
		/// Eraserツールのサイズ選択操作が成されたとき
		/// </summary>
		/// <param name="Size">サイズ．（e.g. 3x3が選択されたならば3）</param>
		void OnEraserSizeChanged( int Size );

		/// <summary>
		/// 背景色部分を透過するか否かが変更されたとき
		/// </summary>
		/// <param name="Trans">透過するか否か</param>
		void OnTransBackModeChanged( bool Trans );
	}
}
