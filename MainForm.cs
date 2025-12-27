using PaintDotCat;
using System.Drawing.Design;

namespace PaintDotCat
{
	/// <summary>APPのメインウィンドウ</summary>
	public partial class MainForm : Form, IView
	{
		private readonly Presenter m_Presenter; //APPロジック
		private ThumbnailForm m_ThumbnailForm;	//サムネイル表示
		private bool m_IsInMenuActiveCondition = false; //メニュー操作状態にあるか否か
		private PaintDotCat.ColorEditor m_ColorEditor = new();

		//-----------------------------------

		/// <summary>default ctor</summary>
		private MainForm() { InitializeComponent(); }

		/// <summary>実用ctor</summary>
		/// <param name="Settings">設定</param>
		public MainForm( UserSettings Settings )
			: this()
		{
			m_Presenter = new Presenter( Settings.ImgWidth, Settings.ImgHeight, this );

			ConfirmAtSave_ToolStripMenuItem.Checked = Settings.ConfirmAtSave;
			ShowFullPathOnCaption_ToolStripMenuItem.Checked = Settings.ShowFullPath;
		}

		/// <summary>
		/// 引数オブジェクトの内容をを現在の設定状態にする
		/// </summary>
		/// <param name="Settings">現在の設定状態受取</param>
		public void ModifySettings( UserSettings Settings )
		{
			Settings.ImgWidth = m_Presenter.ImgWidth;
			Settings.ImgHeight = m_Presenter.ImgHeight;
			Settings.ConfirmAtSave = ConfirmAtSave_ToolStripMenuItem.Checked;
			Settings.ShowFullPath = ShowFullPathOnCaption_ToolStripMenuItem.Checked;
		}

		//-----------------------------------
		#region override

		//まず Presenter にキー処理の機会を与える
		protected override bool ProcessCmdKey( ref System.Windows.Forms.Message Msg, System.Windows.Forms.Keys keys )
		{
			if( !m_IsInMenuActiveCondition )    //メニューが最優先
			{
				if( m_Presenter.ProcessKeyInput( keys ) ) return true;
			}
			return base.ProcessCmdKey( ref Msg, keys );
		}

		#endregion
		//-----------------------------------
		#region private methods

		/// <summary>画像表示域のサイズを現在の状況に合わせて変更</summary>
		private void UpdateImgViewSize()
		{
			int Rate = m_Presenter.ViewMagnificationRate;
			The_viewControl.ClientSize = new Size( m_Presenter.ImgWidth * Rate, m_Presenter.ImgHeight * Rate );
			The_viewControl.Draw( g => m_Presenter.DrawCurrentView( g ) );
			UpdateThumbnail();
		}

		/// <summary>Undo , Redo メニューの状態更新</summary>
		private void UpdateUndoRedoMenuState()
		{
			Undo_ToolStripMenuItem.Enabled = m_Presenter.CanUndo();
			Redo_ToolStripMenuItem.Enabled = m_Presenter.CanRedo();
			DiscardUndoRedoData_ToolStripMenuItem.Enabled = ( m_Presenter.CanUndo() || m_Presenter.CanRedo() );
		}

		/// <summary>画像保存処理</summary>
		/// <param name="SaveFilePath">保存パス</param>
		private void SaveAs( string SaveFilePath )
		{
			try
			{
				if( !m_Presenter.SaveAs( SaveFilePath ) )
				{
					MessageBox.Show( this, "Not possible currently...", "Save was not executed" );
					return;
				}
			}
			catch( Exception ex )
			{
				MessageBox.Show( this, ex.Message + Environment.NewLine + "(" + SaveFilePath + ")", "Save failed" );
				return;
			}

			Info_toolStripStatusLabel.Text = "[" + DateTime.Now.ToLongTimeString() + "] Saved " + System.IO.Path.GetFileName( SaveFilePath );
			UpdateCaption();
		}

		/// <summary>画像読込処理</summary>
		/// <param name="LoadFilePath">読込ファイルパス</param>
		private void LoadFrom( string LoadFilePath )
		{
			if( !m_Presenter.IsImageJustAfterInitialized )
			{
				if(
					MessageBox.Show(
						this,
						"Current data will be discarded." + Environment.NewLine +
						"Are you sure you want to load from the following path?" + Environment.NewLine + Environment.NewLine + LoadFilePath,
						"Confirmation",
						MessageBoxButtons.OKCancel,
						MessageBoxIcon.Exclamation
					)
					!= DialogResult.OK
				)
				{ return; }
			}

			try
			{
				if( !m_Presenter.LoadFrom( LoadFilePath ) )
				{
					MessageBox.Show( this, "Not possible currently...", "Load was not executed" );
					return;
				}
			}
			catch( Exception ex )
			{
				MessageBox.Show( this, ex.Message + Environment.NewLine + "(" + LoadFilePath + ")", "Load failed" );
				return;
			}

			Info_toolStripStatusLabel.Text = "[" + DateTime.Now.ToLongTimeString() + "] Loaded " + System.IO.Path.GetFileName( LoadFilePath );
			UpdateCaption();
		}

		/// <summary>
		/// SaveやLoadを実施した際のタイトル文字列の変更．
		/// タイトルにファイル名を表示する．
		/// ただし，新規作成直後の場合にはAPP名称のみを表示．
		/// </summary>
		private void UpdateCaption()
		{
			string APPName = "Paint.CAT";
			string FilePath = m_Presenter.LastSaveLoadFilePath;
			if( FilePath.Length == 0 ) { this.Text = APPName; }
			else
			{
				if( !ShowFullPathOnCaption_ToolStripMenuItem.Checked )
				{ FilePath = System.IO.Path.GetFileName( FilePath ); }

				this.Text = FilePath + " - " + APPName;
			}
		}

		/// <summary>サムネイルウィンドウの可視性変更時</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnThumbnailFormVisibilityChanged( object sender, EventArgs e )
		{
			ShowThumbnail_ToolStripMenuItem.Checked = m_ThumbnailForm.Visible;
			if( m_ThumbnailForm.Visible )m_ThumbnailForm.UpdateImg();
		}

		/// <summary>サムネイルウィンドウの表示更新</summary>
		private void UpdateThumbnail()
		{
			if( m_ThumbnailForm!=null  &&  m_ThumbnailForm.Visible )
			{	m_ThumbnailForm.UpdateImg();	}
		}

		/// <summary>指定ボタンの色のエディット</summary>
		/// <param name="ButtonIndex">ボタン指定．0 or 1</param>
		private void EditDrawColor( int ButtonIndex )
		{
			if( !m_Presenter.CanShowModalDlg() ) return;

			Color PrevCol = ( ButtonIndex == 0 ? m_Presenter.CurrLColor : m_Presenter.CurrRColor );
			Color EditedCol = new Color();
			if( !m_ColorEditor.Edit( PrevCol, out EditedCol, this ) ) return;
			m_Presenter.OnColorSelected( ButtonIndex, EditedCol );
		}

		#endregion
		//-----------------------------------
		#region IView Impl

		public void OnImgSizeChanged()
		{
			UpdateImgViewSize();
			UpdateSizeInfoView( new Size( m_Presenter.ImgHeight, m_Presenter.ImgHeight ) );
			UpdateUndoRedoMenuState();
		}

		public void OnViewMagRateChanged()
		{
			//画像表示域のサイズを変更
			UpdateImgViewSize();

			//拡大率選択GUIの更新
			int Rate = m_Presenter.ViewMagnificationRate;
			Zoom_toolStripDropDownButton.Text = $"  {Rate * 100}%  ";
			foreach( ToolStripMenuItem Item in Zoom_toolStripDropDownButton.DropDownItems )
			{ Item.Checked = ( (int)Item.Tag == Rate ); }

			//グリッド表示メニューのEnable状態
			ShowGrid_ToolStripMenuItem.Enabled = ( Rate >= m_Presenter.MinViewMagnificationRate_for_GridToBeShown );
		}

		public void OnImgPainted()
		{
			The_viewControl.Draw( g => m_Presenter.DrawCurrentView( g ) );
			UpdateThumbnail();
			UpdateUndoRedoMenuState();
		}

		public void OnSelectedColorChanged( int index, Color col )
		{
			if( index == 0 ) LColor_pictureBox.BackColor = col;
			else RColor_pictureBox.BackColor = col;
		}

		public void UpdateDisplayOfCursorPos( Point ImgPos ) { Pos_toolStripStatusLabel.Text = $"({ImgPos.X}, {ImgPos.Y})"; }
		public void UpdateSizeInfoView( Size size ) { Size_toolStripStatusLabel.Text = $"({size.Width}x{size.Height})"; }
		public void OnToolSelectionChangedTo( ToolType type ) { The_ToolsUC.ChangeCtrlState_WhenCurrSelToolChanged( type ); }

		public void OnSelectionStateChanged( bool Selected )
		{
			The_viewControl.Draw( g => m_Presenter.DrawCurrentView( g ) );
			Copy_ToolStripMenuItem.Enabled = Selected;
			Cut_ToolStripMenuItem.Enabled = Selected;
			ClearSelection_ToolStripMenuItem.Enabled = Selected;
			PasteTo_ToolStripMenuItem.Enabled = Selected;

			if( !Selected )
			{ UpdateSizeInfoView( new Size( m_Presenter.ImgHeight, m_Presenter.ImgHeight ) ); }
		}

		public void ChangeImgViewCursorTo( Cursor NewCursor ) { The_viewControl.Cursor = NewCursor; }
		public LineTool.Settings CreateLineToolSetting() { return The_ToolsUC.CraeteLineToolSetting(); }
		public int GetEraserToolSize() { return The_ToolsUC.EraserToolSize; }
		public bool IsRectModeSelectedForSelectionTool() { return The_ToolsUC.IsRectModeSelectedForSelectionTool(); }

		public Point VisibleTopLeftOfImgView()
		{
			var VisibleTopLeft = LR_splitContainer.Panel2.PointToScreen( new Point( 0, 0 ) );
			var PictureBoxPos = The_viewControl.PointToClient( VisibleTopLeft );

			PictureBoxPos.X = Math.Max( 0, PictureBoxPos.X );
			PictureBoxPos.Y = Math.Max( 0, PictureBoxPos.Y );
			return PictureBoxPos;
		}

		#endregion
		//-----------------------------------
		#region Event Handler (Form)

		//Load
		private void MainForm_Load( object sender, EventArgs e )
		{
			if( DesignMode ) return;

			this.SuspendLayout();

			Main_menuStrip.CanOverflow = true;
			Info_toolStripStatusLabel.Text = Properties.Resources.SoftVerStr;
			UpdateCaption();

			{//拡大率ドロップダウン項目準備
				var Rates = new int[] { 1, 2, 3, 4, 5, 6, 8, 10, 12, 14, 16, 20, 24 };
				var Items = new ToolStripItem[Rates.Length];
				for( int i = 0; i < Rates.Length; ++i )
				{
					Items[i] = new ToolStripMenuItem( $"{Rates[i] * 100}%" );
					Items[i].Tag = Rates[i];    //※拡大倍率を Tag に保持する
				}
				Zoom_toolStripDropDownButton.DropDownItems.AddRange( Items );
			}

			{//レイアウト調整
				LR_splitContainer.SplitterWidth = 8;
				UD_splitContainer.SplitterWidth = 8;

				{
					ScrollSizeDeside_panel.Location = new Point( 0, 0 );
					ScrollSizeDeside_panel.Padding = new Padding( 16 );
					The_viewControl.Left = ScrollSizeDeside_panel.Padding.Left;
					The_viewControl.Top = ScrollSizeDeside_panel.Padding.Top;
				}
				{
					int s = (int)Math.Ceiling( SwapColor_button.Height * 1.5 );
					var S = new Size( s, s );
					LColor_pictureBox.Size = S;
					RColor_pictureBox.Size = S;
				}
			}

			//UCに必要な参照設定
			The_ToolsUC.Observer = m_Presenter;
			The_ColorsUC.Observer = m_Presenter;
			The_ColorsUC.ColEditor = m_ColorEditor;

			{//起動時に開くファイルが指定されている場合
				var CmdLineArgs = Environment.GetCommandLineArgs();
				if( CmdLineArgs.Length >= 2 )
				{ LoadFrom( CmdLineArgs[1] ); }
			}

			//Viewの初期化
			m_Presenter.ViewInitialization();

			this.ResumeLayout();
		}

		//FormClosing
		private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
		{
			if( !m_Presenter.IsImageJustAfterInitialized )
			{
				if( MessageBox.Show( this, "Are you sure you want to exit?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation ) != DialogResult.OK )
				{ e.Cancel = true; }
			}
		}

		//左右ボタン色変更
		private void SwapColor_button_Click( object sender, EventArgs e ) { m_Presenter.SwapLRColor(); }
		private void LColor_pictureBox_DoubleClick( object sender, EventArgs e ) { EditDrawColor( 0 ); }
		private void RColor_pictureBox_DoubleClick( object sender, EventArgs e ) { EditDrawColor( 1 ); }

		//拡大率ドロップダウンリストの選択変更
		private void Zoom_toolStripDropDownButton_DropDownItemClicked( object sender, ToolStripItemClickedEventArgs e )
		{ m_Presenter.ViewMagnificationRate = (int)e.ClickedItem.Tag; }

		//画像エリア内マウス操作
		private void The_viewControl_MouseDown( object sender, MouseEventArgs e ) { m_Presenter.OnMouseDown( e.Location, e.Button ); }
		private void The_viewControl_MouseUp( object sender, MouseEventArgs e ) { m_Presenter.OnMouseUp( e.Location, e.Button ); }
		private void The_viewControl_MouseMove( object sender, MouseEventArgs e ) { m_Presenter.OnMouseMove( e.Location ); }

		//画像エリア外でのマウスクリック
		private void LR_splitContainer_Panel2_MouseClick( object sender, MouseEventArgs e ) { m_Presenter.OnOutsidePosClicked( e.Button ); }
		private void ScrollSizeDeside_panel_MouseClick( object sender, MouseEventArgs e ) { m_Presenter.OnOutsidePosClicked( e.Button ); }

		//メニュー操作状態に 入った/抜けた とき
		private void Main_menuStrip_MenuActivate( object sender, EventArgs e ) { m_IsInMenuActiveCondition = true; }
		private void Main_menuStrip_MenuDeactivate( object sender, EventArgs e ) { m_IsInMenuActiveCondition = false; }

		#endregion
		//-----------------------------------
		#region Event Handler (File Menu)

		//新規
		private void New_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if( !m_Presenter.CanShowModalDlg() ) return;
			using( var Dlg = new SizeDlg() )
			{
				Dlg.ImgWidth = m_Presenter.ImgWidth;
				Dlg.ImgHeight = m_Presenter.ImgHeight;

				if( Dlg.ShowDialog( this ) != DialogResult.OK ) return;
				m_Presenter.StartNew( Dlg.ImgWidth, Dlg.ImgHeight );
			}

			UpdateCaption();
			Info_toolStripStatusLabel.Text = "[" + DateTime.Now.ToLongTimeString() + "] Started New";
		}

		//開く
		private void Open_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if( !m_Presenter.CanShowModalDlg() ) return;
			using( var ofd = new OpenFileDialog() )
			{
				ofd.Filter = "BMP(*.bmp)|*.bmp|PNG(*.png)|*.png|JPG(*jpg)|*.jpg|Img Files(*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg";
				ofd.FilterIndex = 2;
				ofd.CheckFileExists = true;
				ofd.Multiselect = false;
				if( ofd.ShowDialog() != DialogResult.OK ) return;
				LoadFrom( ofd.FileName );
			}

		}

		//上書保存
		private void Save_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if( !m_Presenter.CanShowModalDlg() ) return;    //※処理中だとまずいので「名前をつけて…」と同様の対処

			var OverwirtePath = m_Presenter.LastSaveLoadFilePath;
			if( OverwirtePath.Length == 0 )
			{
				SaveAs_ToolStripMenuItem_Click( sender, e );
				return;
			}

			if( ConfirmAtSave_ToolStripMenuItem.Checked )
			{
				if(
					MessageBox.Show(
						this,
						"Save (overwrite) to the following path?" + Environment.NewLine + Environment.NewLine + OverwirtePath,
						"Confirmation",
						MessageBoxButtons.OKCancel,
						MessageBoxIcon.Exclamation
					) != DialogResult.OK )
				{ return; }
			}

			SaveAs( OverwirtePath );
		}

		//名前を付けて保存
		private void SaveAs_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if( !m_Presenter.CanShowModalDlg() ) return;
			using( var sfd = new SaveFileDialog() )
			{
				sfd.Filter = "BMP(*.bmp)|*.bmp|PNG(*.png)|*.png|JPG(*jpg)|*.jpg";
				sfd.FileName = "untitled";
				sfd.FilterIndex = 2;
				sfd.OverwritePrompt = true;
				if( sfd.ShowDialog() != DialogResult.OK ) return;
				SaveAs( sfd.FileName );
			}
		}

		//モノクロBMPとしてExport
		private void ExportAsMonoBMP_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if( !m_Presenter.CanShowModalDlg() ) return;

			string FilePath = "";
			using( var sfd = new SaveFileDialog() )
			{
				sfd.Filter = "Monochrome BMP(*.bmp)|*.bmp";
				sfd.FileName = "untitled";
				sfd.FilterIndex = 1;
				sfd.OverwritePrompt = true;
				if( sfd.ShowDialog() != DialogResult.OK ) return;

				FilePath = sfd.FileName;
			}

			try
			{
				if( !m_Presenter.ExportAsMonoBMP( FilePath ) )
				{
					MessageBox.Show( this, "Not possible currently...", "Export was not executed" );
					return;
				}
			}
			catch( Exception ex )
			{
				MessageBox.Show( this, ex.Message + Environment.NewLine + "(" + FilePath + ")", "Export failed" );
				return;
			}

			Info_toolStripStatusLabel.Text = "[" + DateTime.Now.ToLongTimeString() + "] Exported " + System.IO.Path.GetFileName( FilePath );
		}

		#endregion
		//-----------------------------------
		#region Event Handler (Edit Menu)

		//Undo / Redo
		private void Undo_ToolStripMenuItem_Click( object sender, EventArgs e ) { m_Presenter.Undo(); }
		private void Redo_ToolStripMenuItem_Click( object sender, EventArgs e ) { m_Presenter.Redo(); }

		//Discard Undo/Redo Data
		private void DiscardUndoRedoData_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if(
				MessageBox.Show(
					this,
					"Are you sure you want to discard edit_history_data?",
					"Confirmation",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Exclamation
				)
				!= DialogResult.OK
			)
			{ return; }
			m_Presenter.DiscardEditHistoryData();
			UpdateUndoRedoMenuState();
		}

		//Copy / Cut / Paste
		private void Copy_ToolStripMenuItem_Click( object sender, EventArgs e ) { m_Presenter.Copy(); }
		private void Cut_ToolStripMenuItem_Click( object sender, EventArgs e ) { m_Presenter.Cut(); }
		private void Paste_ToolStripMenuItem_Click( object sender, EventArgs e ) { m_Presenter.Paste(); }

		//Select All
		private void SelectAll_ToolStripMenuItem_Click( object sender, EventArgs e ) { m_Presenter.SelectAll(); }
		//Clear Selection
		private void ClearSelection_ToolStripMenuItem_Click( object sender, EventArgs e ) { m_Presenter.ClearSelection(); }

		//Paste To
		private void PasteTo_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if( !m_Presenter.CanShowModalDlg() ) return;
			using( var sfd = new SaveFileDialog() )
			{
				sfd.Filter = "BMP(*.bmp)|*.bmp|PNG(*.png)|*.png|JPG(*jpg)|*.jpg";
				sfd.FileName = "untitled";
				sfd.FilterIndex = 2;
				sfd.OverwritePrompt = true;
				if( sfd.ShowDialog() != DialogResult.OK ) return;

				try
				{
					if( !m_Presenter.SaveSelectedAreaAs( sfd.FileName ) )
					{
						MessageBox.Show( this, "Not possible currently...", "Save was not executed" );
						return;
					}
				}
				catch( Exception ex )
				{
					MessageBox.Show( this, ex.Message + Environment.NewLine + "(" + sfd.FileName + ")", "Save failed" );
					return;
				}

				Info_toolStripStatusLabel.Text = "[" + DateTime.Now.ToLongTimeString() + "] Pasted To " + System.IO.Path.GetFileName( sfd.FileName );
			}
		}

		#endregion
		//-----------------------------------
		#region Event Handler (Image Menu)

		//Flip/Rotate
		private void FlipRotate_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if( !m_Presenter.CanShowModalDlg() ) return;
			using( var Dlg = new FlipRotateDlg() )
			{
				if( Dlg.ShowDialog( this ) != DialogResult.OK ) return;
				m_Presenter.RotateFlip( Dlg.SelectedActionType );
			}
		}

		//画像サイズ変更
		private void ChangeSize_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if( !m_Presenter.CanShowModalDlg() ) return;
			using( var Dlg = new SizeDlg() )
			{
				Dlg.ImgWidth = m_Presenter.ImgWidth;
				Dlg.ImgHeight = m_Presenter.ImgHeight;

				if( Dlg.ShowDialog( this ) == DialogResult.OK )
				{ m_Presenter.ChangeImgSizeTo( Dlg.ImgWidth, Dlg.ImgHeight ); }
			}
		}

		#endregion
		//-----------------------------------
		#region Event Handler (View Menu)

		//サムネイル表示
		private void ShowThumbnail_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if( m_ThumbnailForm == null )
			{
				m_ThumbnailForm = new ThumbnailForm( m_Presenter );
				m_ThumbnailForm.VisibleChanged += OnThumbnailFormVisibilityChanged;
				m_ThumbnailForm.Show( this );
				m_ThumbnailForm.UpdateImg();
			}
			else
			{	m_ThumbnailForm.Visible = !m_ThumbnailForm.Visible;	}
		}

		//グリッド表示
		private void ShowGrid_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			ShowGrid_ToolStripMenuItem.Checked = !ShowGrid_ToolStripMenuItem.Checked;
			m_Presenter.GridVisible = ShowGrid_ToolStripMenuItem.Checked;
		}

		//グリッド設定
		private void GridSettings_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if( !m_Presenter.CanShowModalDlg() ) return;

			using( var Dlg = new GridSizeDlg() )
			{
				Dlg.X_Interval = m_Presenter.GridSize.Width;
				Dlg.Y_Interval = m_Presenter.GridSize.Height;
				Dlg.Grid_Color = m_Presenter.GridColor;

				if( Dlg.ShowDialog( this ) != DialogResult.OK ) return;

				m_Presenter.GridSize = new Size( Dlg.X_Interval, Dlg.Y_Interval );
				m_Presenter.GridColor = Dlg.Grid_Color;
			}
		}

		#endregion
		//-----------------------------------
		#region Event Handler (Color Menu)

		//左右ボタンの色変更
		private void LeftColor_ToolStripMenuItem_Click( object sender, EventArgs e ){	EditDrawColor(0);	}
		private void RightColor_ToolStripMenuItem_Click( object sender, EventArgs e ){	EditDrawColor(1);	}

		#endregion
		//-----------------------------------
		#region Event Handler (Settings Menu)

		//上書保存時に確認を取る
		private void ConfirmAtSave_ToolStripMenuItem_Click( object sender, EventArgs e )
		{ ConfirmAtSave_ToolStripMenuItem.Checked = !ConfirmAtSave_ToolStripMenuItem.Checked; }

		//タイトルにファイル名をフルパスで表示する
		private void ShowFullPathOnCaption_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			ShowFullPathOnCaption_ToolStripMenuItem.Checked = !ShowFullPathOnCaption_ToolStripMenuItem.Checked;
			UpdateCaption();
		}

		#endregion
		//-----------------------------------
		#region Event Handler (Help Menu)

		//バージョン情報
		private void Version_ToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if( !m_Presenter.CanShowModalDlg() ) return;
			using( var Dlg = new VersionDlg() )
			{ Dlg.ShowDialog( this ); }
		}

		#endregion

	}
}
