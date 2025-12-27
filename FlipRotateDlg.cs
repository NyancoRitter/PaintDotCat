using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintDotCat
{
	/// <summary>
	/// 実施したい画像操作（反転や回転処理）を選択するためのダイアログ
	/// </summary>
	public partial class FlipRotateDlg : Form
	{
		public FlipRotateDlg(){	InitializeComponent();	}

		/// <summary>GUI上で選択された動作を取得</summary>
		public RotateFlipType SelectedActionType
		{
			get
			{
				if( FlipHorizontal_radioButton.Checked )return RotateFlipType.RotateNoneFlipX;
				if( FlipVertical_radioButton.Checked )return RotateFlipType.RotateNoneFlipY;
				if( Rot90_radioButton.Checked )return RotateFlipType.Rotate90FlipNone;
				if( Rot180_radioButton.Checked )return RotateFlipType.Rotate180FlipNone;
				return RotateFlipType.Rotate270FlipNone;
			}
		}

		//
		private void FlipRotateDlg_Load(object sender, EventArgs e)
		{
			if( DesignMode )return;

			FlipHorizontal_radioButton.Checked = true;
		}

		private void OK_button_Click(object sender, EventArgs e){	DialogResult = DialogResult.OK;	}
		private void Cancel_button_Click(object sender, EventArgs e){	DialogResult = DialogResult.Cancel;	}
	}
}
