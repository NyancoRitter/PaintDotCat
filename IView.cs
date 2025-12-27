using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PaintDotCat
{
	/// <summary>
	/// Presenter -> View 方向の情報伝達用 I/F
	/// 
	/// 主に表示更新の指示．
	/// </summary>
	public interface IView
	{
		//-----------------------------------
		//表示更新の指示

		/// <summary>画像サイズが変更された際の表示更新</summary>
		void OnImgSizeChanged();

		/// <summary>表示拡大率が変更された際の表示更新</summary>
		void OnViewMagRateChanged();

		/// <summary>
		/// 画像に何かが描画された際の表示更新
		/// （というか，画像表示を更新すべきとき）
		/// </summary>
		void OnImgPainted();

		/// <summary>
		/// 描画色の選択が変更された際の表示更新
		/// </summary>
		/// <param name="index">描画色index．0 or 1</param>
		/// <param name="col">選択された色</param>
		void OnSelectedColorChanged( int index, Color col );

		/// <summary>マウスカーソル位置に対応した画像データ座標位置の表示を更新する</summary>
		/// <param name="ImgPos">画像データ座標系での位置</param>
		void UpdateDisplayOfCursorPos( Point ImgPos );

		/// <summary>選択範囲サイズ情報表示の更新</summary>
		/// <param name="size">表示すべきサイズ</param>
		void UpdateSizeInfoView( Size size );

		/// <summary>
		/// ツールの選択状態が変わった際の表示更新
		/// </summary>
		/// <param name="type">選択されたツール</param>
		void OnToolSelectionChangedTo( ToolType type );

		/// <summary>
		/// 範囲選択状態の変更時の表示更新
		/// </summary>
		/// <param name="Selected">範囲が選択されている状況か否か</param>
		void OnSelectionStateChanged( bool Selected );

		/// <summary>
		/// 画像表示域のマウスカーソルを変更
		/// </summary>
		/// <param name="NewCursor">カーソル</param>
		void ChangeImgViewCursorTo( System.Windows.Forms.Cursor NewCursor );

		//-----------------------------------
		//ツールの設定の取得

		/// <summary>現在のGUI状態から直線描画ツールの設定を生成して返す</summary>
		/// <returns>設定</returns>
		LineTool.Settings CreateLineToolSetting();

		/// <summary>現在のGUI状態から消しゴムツールの描画サイズを返す</summary>
		/// <returns>正方形の一片の長さ</returns>
		int GetEraserToolSize();

		/// <summary>
		/// 現在のGUI状態では
		/// 範囲選択ツールとして「矩形」側が選択されているか否か．
		/// </summary>
		/// <returns>GUI上での選択状態が矩形側ならtrue, 自由形状側ならfalse</returns>
		bool IsRectModeSelectedForSelectionTool();

		//-----------------------------------
		//表示状態の取得

		/// <summary>
		/// 画像表示域のうち，現在のスクロール状況で見えている範囲の左上の座標．
		/// </summary>
		/// <returns>表示域のpixel座標系での値</returns>
		Point VisibleTopLeftOfImgView();
	}
}
