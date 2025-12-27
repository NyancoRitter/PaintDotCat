using PaintDotCat;
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
	public partial class ToolsUC : UserControl
	{
		public ToolsUC()
		{
			InitializeComponent();
		}

		/// <summary>操作に対する処理の実施者</summary>
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		/// <summary>操作に対する処理の実施者</summary>
		public IToolViewOpListener Observer { get; set; }

		/// <summary>
		/// APPの内部状態にCtrlの状態の方を合わせる際に
		/// Ctrl関係のエベントが余計なことをしないように黙らせる用のフラグ
		/// </summary>
		private bool DisableCtrlEventHandler { get; set; }

		/// <summary>
		/// GUIの状態を，指定のツールが選択されている状態に変更する
		/// </summary>
		/// <param name="type"></param>
		public void ChangeCtrlState_WhenCurrSelToolChanged( ToolType type )
		{
			try
			{
				DisableCtrlEventHandler = true;

				TabPage TabToBeSelected = Main_tabControl.SelectedTab;

				switch( type )
				{
				case ToolType.Pen:
					TabToBeSelected = Pen_tabPage;
					Pen1Pix_radioButton.Checked = true;
					break;
				case ToolType.Brush:
					TabToBeSelected = Pen_tabPage;
					Pen2x2_radioButton.Checked = true;
					break;

				case ToolType.RectAreaSelect:
					TabToBeSelected = Select_tabPage;
					RectSelMode_radioButton.Checked = true;
					break;
				case ToolType.FreeFormAreaSelect:
					TabToBeSelected = Select_tabPage;
					FreeFormSelMode_radioButton.Checked = true;
					break;

				case ToolType.Line: TabToBeSelected = Line_tabPage; break;
				case ToolType.Eraser: TabToBeSelected = Eraser_tabPage; break;
				case ToolType.Fill: TabToBeSelected = Fill_tabPage; break;
				default: break;
				}

				if( Main_tabControl.SelectedTab != TabToBeSelected )
				{ Main_tabControl.SelectedTab = TabToBeSelected; }
			}
			finally
			{ DisableCtrlEventHandler = false; }
		}

		/// <summary>
		/// 現在のGUI状態から Line ツールの設定を生成して返す
		/// </summary>
		/// <returns></returns>
		public LineTool.Settings CraeteLineToolSetting()
		{
			var Ret = new LineTool.Settings();
			Ret.Thickness = (int)LineWeight_numericUpDown.Value;
			Ret.RoundEnd = RoundEnd_checkBox.Checked;
			return Ret;
		}

		/// <summary>
		/// 現在GUI上で選択されている消しゴムツールのサイズ
		/// </summary>
		public int EraserToolSize
		{
			get
			{
				if( Eraser3x3_radioButton.Checked ) return 3;
				if( Eraser7x7_radioButton.Checked ) return 7;
				if( Eraser9x9_radioButton.Checked ) return 9;
				if( Eraser11x11_radioButton.Checked ) return 11;
				return 5;
			}
		}

		/// <summary>
		/// GUI上で選択ツールのモードとして「矩形」側が選択されているか否か
		/// </summary>
		/// <returns></returns>
		public bool IsRectModeSelectedForSelectionTool()
		{
			return RectSelMode_radioButton.Checked;
		}

		//-----------------------------------

		/// <summary>
		/// タブのテキスト更新：選択中のタブにだけテキストを表示する
		/// </summary>
		private void UpdateTabText()
		{
			var SelectedTab = Main_tabControl.SelectedTab;

			Pen_tabPage.Text = ( Pen_tabPage == SelectedTab ? "Pen" : "" );
			Line_tabPage.Text = ( Line_tabPage == SelectedTab ? "Line" : "" );
			Select_tabPage.Text = ( Select_tabPage == SelectedTab ? "Select" : "" );
			Eraser_tabPage.Text = ( Eraser_tabPage == SelectedTab ? "Eraser" : "" );
			Fill_tabPage.Text = ( Fill_tabPage == SelectedTab ? "Fill" : "" );
		}

		//-----------------------------------
		#region UC

		//Load
		private void ToolsUC_Load( object sender, EventArgs e )
		{
			if( DesignMode ) return;

			//
			UpdateTabText();

			//
			RectSelMode_radioButton.Checked = true;
			Eraser5x5_radioButton.Checked = true;
			RectSelMode_radioButton.Checked = true;

			CtrlUtil.AdjustPictureBoxSize_for_DevDPI( RectSelMode_pictureBox );
			CtrlUtil.AdjustPictureBoxSize_for_DevDPI( FreeFormSelMode_pictureBox );
			CtrlUtil.AdjustPictureBoxSize_for_DevDPI( TransparentMode_pictureBox );
		}

		//タブ選択変更時
		private void Main_tabControl_SelectedIndexChanged( object sender, EventArgs e )
		{
			UpdateTabText();

			if( DisableCtrlEventHandler ) return;

			if( Main_tabControl.SelectedIndex < 0 ) return;

			{//Observerへの通知
				var SelectedToolType = ToolType.Pen;

				if( Main_tabControl.SelectedTab == Pen_tabPage )
				{ SelectedToolType = ( Pen1Pix_radioButton.Checked ? ToolType.Pen : ToolType.Brush ); }
				else if( Main_tabControl.SelectedTab == Select_tabPage )
				{ SelectedToolType = ( FreeFormSelMode_radioButton.Checked ? ToolType.FreeFormAreaSelect : ToolType.RectAreaSelect ); }
				else if( Main_tabControl.SelectedTab == Line_tabPage ) { SelectedToolType = ToolType.Line; }
				else if( Main_tabControl.SelectedTab == Eraser_tabPage ) { SelectedToolType = ToolType.Eraser; }
				else if( Main_tabControl.SelectedTab == Fill_tabPage ) { SelectedToolType = ToolType.Fill; }
				else { return; }    //※ここにはこないハズ

				Observer?.OnSelectedToolChanged( SelectedToolType );
			}
		}

		#endregion
		//-----------------------------------
		#region Pen Tool

		private void Pen1Pix_radioButton_CheckedChanged( object sender, EventArgs e )
		{
			var Tool = ( Pen1Pix_radioButton.Checked ? ToolType.Pen : ToolType.Brush );
			Observer?.OnSelectedToolChanged( Tool );
		}

		#endregion
		//-----------------------------------
		#region Line Tool

		private void ResetLineWeight_button_Click( object sender, EventArgs e )
		{ LineWeight_numericUpDown.Value = 1; }

		#endregion
		//-----------------------------------
		#region Select Tool

		private void SelectToolMode_radioButton_CheckedChanged( object sender, EventArgs e )
		{
			if( DisableCtrlEventHandler ) return;

			if( FreeFormSelMode_radioButton.Checked )
			{ Observer?.OnSelectedToolChanged( ToolType.FreeFormAreaSelect ); }
			else
			{ Observer?.OnSelectedToolChanged( ToolType.RectAreaSelect ); }
		}

		private void TransBackColor_checkBox_CheckedChanged( object sender, EventArgs e )
		{
			if( DisableCtrlEventHandler ) return;
			Observer?.OnTransBackModeChanged( TransBackColor_checkBox.Checked );
		}

		private void RectSelMode_pictureBox_Click( object sender, EventArgs e ){	RectSelMode_radioButton.Checked = true;	}
		private void FreeFormSelMode_pictureBox_Click( object sender, EventArgs e ){	FreeFormSelMode_radioButton.Checked = true;	}
		private void TransparentMode_pictureBox_Click( object sender, EventArgs e ){	TransBackColor_checkBox.Checked = !TransBackColor_checkBox.Checked;	}

		#endregion
		//-----------------------------------
		#region Eraser Tool

		private void EraserSize_radioButton_CheckedChanged( object sender, EventArgs e )
		{
			if( DisableCtrlEventHandler ) return;

			if( Eraser3x3_radioButton.Checked ) { Observer?.OnEraserSizeChanged( 3 ); return; }
			if( Eraser5x5_radioButton.Checked ) { Observer?.OnEraserSizeChanged( 5 ); return; }
			if( Eraser7x7_radioButton.Checked ) { Observer?.OnEraserSizeChanged( 7 ); return; }
			if( Eraser9x9_radioButton.Checked ) { Observer?.OnEraserSizeChanged( 9 ); return; }
			if( Eraser11x11_radioButton.Checked ) { Observer?.OnEraserSizeChanged( 11 ); return; }
		}

		#endregion
	}
}
