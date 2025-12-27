using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PaintDotCat
{
	/// <summary>
	/// ITool のメソッド戻り値用．ツールの操作結果を示す．
	/// </summary>
	public enum ToolProcResult
	{
		/// <summary>特に無し</summary>
		None,
		/// <summary>まだ編集作業中であり，表示を更新すべき</summary>
		ShouldUpdateView,
		/// <summary>操作完了であり，一連の操作による画像変更を採用すべき</summary>
		EditCompleted,
		/// <summary>操作完了であり，一連の操作により成された画像変更は棄却されるべき</summary>
		EditShouldBeRejected
	}

	/// <summary>
	/// Tool関係関数
	/// </summary>
	public static class Tool
	{
		/// <summary>
		/// あるツールを選択している状態において
		/// Ctrlキー押下状態ではスポイトとして機能するべきか否か
		/// </summary>
		/// <param name="type">選択中のツール</param>
		/// <returns>スポイト機能となるべきか否か</returns>
		public static bool ShouldActAsColorPicker_when_Ctrl( ToolType type )
		{
			return (type==ToolType.Pen || type==ToolType.Brush || type==ToolType.Line || type==ToolType.Eraser || type==ToolType.Fill );
		}
	}

	/// <summary>
	/// 描画ツールの I/F
	/// 
	/// * マウス操作入力系のメソッドには，「編集中画像バッファ」が渡される．
	///   これは，編集中に表示される画像である．
	///   この画像に対して実施した変更（描画）は，
	///   <see cref="CreateEdit"/>が返すオブジェクトで再現できる必要がある．
	/// </summary>
	public interface ITool
	{
		/// <summary>描画ツール種類</summary>
		ToolType Type{	get;	}

		/// <summary>
		/// ツールによる作業中（：典型的にはマウスボタン押下～解放までの間）か否か．
		/// 
		/// 選択範囲の変更やツールの切替，ペースト等々の操作を棄却するか否かを判断するために用いられる．
		/// </summary>
		/// <returns>作業中状態であればtrue</returns>
		bool IsBusy();

		//----------
		//操作入力
		//（MEMO : 特殊キーの状態については引数ではなく別途 Control.ModifierKeys から得ること）

		/// <summary>マウスボタンが押下されたとき</summary>
		/// <param name="pos">画像上の座標（ただし画像範囲外の値が渡され得る）</param>
		/// <param name="button">対象マウスボタン</param>
		/// <param name="BMP">編集中画像バッファ</param>
		/// <returns>操作結果</returns>
		ToolProcResult OnMouseDown( Point pos, System.Windows.Forms.MouseButtons button, Bitmap BMP );
		
		/// <summary>マウスボタンが離されたとき</summary>
		/// <param name="pos">画像上の座標（ただし画像範囲外の値が渡され得る）</param>
		/// <param name="button">対象マウスボタン</param>
		/// <param name="BMP">編集中画像バッファ</param>
		/// <returns>操作結果</returns>
		ToolProcResult OnMouseUp( Point pos, System.Windows.Forms.MouseButtons button, Bitmap BMP );

		/// <summary>マウスボタンが動いたとき</summary>
		/// <param name="pos">画像上の座標（ただし画像範囲外の値が渡され得る）</param>
		/// <param name="BMP">編集中画像バッファ</param>
		/// <returns>操作結果</returns>
		ToolProcResult OnMouseMove( Point pos, Bitmap BMP );

		//----------
		//表示用

		/// <summary>
		/// 表示域への追加の描画．
		/// ツールの状況等を描画する用．
		/// * 現在の表示倍率を考慮した描画を行うこと
		/// </summary>
		/// <param name="g">表示域に描画するためのGraphics</param>
		/// <param name="MagRate">現在の表示倍率率</param>
		void DrawStateToViewImg( Graphics g, int MagRate );

		//----------
		//画像更新処理の実施手段取得

		/// <summary>
		/// 画像に変更を加える手段を返す．
		/// マウス操作入力系のメソッドで実施した「編集中画像バッファ」への描画を
		/// 完全に再現できるオブジェクトを生成して返す．
		/// </summary>
		/// <returns>
		/// 画像変更手段．
		/// ただし返すべきものが無い（：変更すべき内容が無い）場合には null．
		/// </returns>
		IEdit CreateEdit(); 
	}
}
