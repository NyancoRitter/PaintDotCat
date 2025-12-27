using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace PaintDotCat
{
	/// <summary>
	/// 画像のサイズを入力するダイアログ．
	/// モーダル想定．
	/// </summary>
	public partial class SizeDlg : Form
	{
		//ドロップダウンで選択できるサイズ
		private static readonly Size[] m_CBSizes = new Size[]
		{
			new Size( 16,16 ),
			new Size( 32,32 ),
			new Size( 48,48 ),
			new Size( 64,64 ),
			new Size( 96,96 ),
			new Size( 128,128 ),
			new Size( 256,256 )
		};

		//ctor
		public SizeDlg(){	InitializeComponent();	}

		/// <summary>設定可能な最大幅</summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		/// <summary>設定可能な最大幅</summary>
		public int MaxWidth
		{
			get{	return (int)Width_numericUpDown.Maximum;	}
			set
			{
				if( value < 1 )throw new ArgumentOutOfRangeException( "Invalid MaxWidth" );	
				Width_numericUpDown.Maximum = value;
			}
		}
		
		/// <summary>設定可能な最大高さ</summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int MaxHeight
		{
			get{	return (int)Height_numericUpDown.Maximum;	}
			set
			{
				if( value < 1 )throw new ArgumentOutOfRangeException( "Invalid MaxHeight" );	
				Height_numericUpDown.Maximum = value;
			}
		}

		/// <summary>
		/// 画像幅．
		/// 用途：
		/// * 表示前に現在値を set
		/// * OK で閉じた後に，入力された値を get
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ImgWidth
		{
			get{	return (int)Width_numericUpDown.Value;	}
			set{	Width_numericUpDown.Value = value;	}
		}

		/// <summary>
		/// 画像高さ：
		/// 用途：
		/// * 表示前に現在値を set
		/// * OK で閉じた後に，入力された値を get
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ImgHeight
		{
			get{	return (int)Height_numericUpDown.Value;	}
			set{	Height_numericUpDown.Value = value;	}
		}

		//-----------------------------------
		#region イベントハンドラ

		private void SizeDlg_Load(object sender, EventArgs e)
		{
			if( DesignMode )return;

			foreach( var S in m_CBSizes )
			{	SizeSel_comboBox.Items.Add( $"{S.Width} x {S.Height}" );	}
		}

		private void SizeSel_comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			int iSel = SizeSel_comboBox.SelectedIndex;
			if( iSel < 0 )return;
			ImgWidth = m_CBSizes[iSel].Width;
			ImgHeight = m_CBSizes[iSel].Height;
		}

		private void OK_button_Click(object sender, EventArgs e)
		{	DialogResult = DialogResult.OK;	}

		private void Cancel_button_Click(object sender, EventArgs e)
		{	DialogResult = DialogResult.Cancel;	}


		#endregion

		
	}
}
