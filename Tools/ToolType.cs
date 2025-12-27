
namespace PaintDotCat
{
	/// <summary>
	///ツール種類
	/// </summary>
	public enum ToolType
	{
		/// <summary>ペン</summary>
		Pen,
		/// <summary>ブラシ</summary>
		Brush,
		/// <summary>直線</summary>
		Line,
		/// <summary>矩形範囲選択</summary>
		RectAreaSelect,
		/// <summary>自由範囲選択</summary>
		FreeFormAreaSelect,
		/// <summary>消しゴム</summary>
		Eraser,
		/// <summary>塗りつぶし</summary>
		Fill
	}

	///// <summary>
	///// ToolType用拡張メソッド
	///// </summary>
	//public static class ToolTypeExtensions
	//{
	//	/// <summary>範囲選択系のツールか否か</summary>
	//	/// <param name="t"></param>
	//	/// <returns></returns>
	//	public static bool IsAreaSelectTool( this ToolType t )
	//	{	return ( t==ToolType.RectAreaSelect  ||  t==ToolType.FreeFormAreaSelect );	}	
	//}

}