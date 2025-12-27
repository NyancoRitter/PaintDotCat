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
	/// サムネイル表示ウィンドウ．
	/// モードレス想定．
	/// </summary>
	public partial class ThumbnailForm : Form
	{
		private readonly Presenter m_Presenter;

		/// <summary>private default ctor</summary>
		private ThumbnailForm(){	InitializeComponent();	}

		/// <summary>
		/// 実用ctor．サムネイルの描画処理者を指定しておく．
		/// </summary>
		/// <param name="presenter">サムネイル描画処理者</param>
		public ThumbnailForm( Presenter presenter )
			: this()
		{	m_Presenter = presenter;	}

		/// <summary>表示更新</summary>
		public void UpdateImg()
		{
			if( m_Presenter.ImgWidth != Thumbnail_pictureBox.ClientSize.Width  ||  m_Presenter.ImgHeight != Thumbnail_pictureBox.ClientSize.Height )
			{	Thumbnail_pictureBox.ClientSize = new Size( m_Presenter.ImgWidth, m_Presenter.ImgHeight );	}

			Thumbnail_pictureBox.Invalidate();
		}

		//-----------------------------------
		#region Event Handler

		private void ThumbnailForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if( e.CloseReason == CloseReason.UserClosing )
			{//※閉じずに非表示にするだけ
				e.Cancel = true;
				this.Visible = false;
			}
		}

		private void Thumbnail_pictureBox_Paint(object sender, PaintEventArgs e)
		{	m_Presenter.DrawThumbnail( e.Graphics );	}

		#endregion
	}
}
