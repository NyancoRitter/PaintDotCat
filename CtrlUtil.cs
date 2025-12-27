using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace PaintDotCat
{
	/// <summary>Control関係</summary>
	public static class CtrlUtil
	{
		/// <summary>
		/// DPI に合わせた PictureBox のサイズ調整
		/// 
		/// - PictureBoxのサイズを（設定されている画像サイズ * DPI/96 ）にする．
		/// - また，SizeMode を StretchImage にする．
		/// </summary>
		/// <param name="PB">対象．Imageが指定されていない場合には何もしない．</param>
		public static void AdjustPictureBoxSize_for_DevDPI( PictureBox PB )
		{
			if( PB.Image == null )return;

			var Scale = PB.DeviceDpi / 96.0f;
			int NewW = (int)Math.Round( PB.Image.Width * Scale );
			int NewH = (int)Math.Round( PB.Image.Height * Scale );
			PB.Size = new Size( NewW, NewH );
			PB.SizeMode = PictureBoxSizeMode.StretchImage;
		}
	}
}
