namespace PaintDotCat
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
			Main_toolStripContainer = new ToolStripContainer();
			Main_statusStrip = new StatusStrip();
			Info_toolStripStatusLabel = new ToolStripStatusLabel();
			Zoom_toolStripDropDownButton = new ToolStripDropDownButton();
			Pos_toolStripStatusLabel = new ToolStripStatusLabel();
			Size_toolStripStatusLabel = new ToolStripStatusLabel();
			UD_splitContainer = new SplitContainer();
			LR_splitContainer = new SplitContainer();
			The_ToolsUC = new ToolsUC();
			ScrollSizeDeside_panel = new Panel();
			The_viewControl = new ViewControl();
			ColorPane_tableLayoutPanel = new TableLayoutPanel();
			SwapColor_button = new Button();
			LColor_pictureBox = new PictureBox();
			RColor_pictureBox = new PictureBox();
			The_ColorsUC = new ColorsUC();
			Main_menuStrip = new MenuStrip();
			File_toolStripMenuItem = new ToolStripMenuItem();
			New_ToolStripMenuItem = new ToolStripMenuItem();
			Open_ToolStripMenuItem = new ToolStripMenuItem();
			toolStripSeparator1 = new ToolStripSeparator();
			Save_ToolStripMenuItem = new ToolStripMenuItem();
			SaveAs_ToolStripMenuItem = new ToolStripMenuItem();
			toolStripSeparator2 = new ToolStripSeparator();
			ExportAsMonoBMP_ToolStripMenuItem = new ToolStripMenuItem();
			Edit_toolStripMenuItem = new ToolStripMenuItem();
			Undo_ToolStripMenuItem = new ToolStripMenuItem();
			Redo_ToolStripMenuItem = new ToolStripMenuItem();
			DiscardUndoRedoData_ToolStripMenuItem = new ToolStripMenuItem();
			toolStripSeparator3 = new ToolStripSeparator();
			Copy_ToolStripMenuItem = new ToolStripMenuItem();
			Cut_ToolStripMenuItem = new ToolStripMenuItem();
			Paste_ToolStripMenuItem = new ToolStripMenuItem();
			toolStripSeparator4 = new ToolStripSeparator();
			SelectAll_ToolStripMenuItem = new ToolStripMenuItem();
			ClearSelection_ToolStripMenuItem = new ToolStripMenuItem();
			toolStripSeparator5 = new ToolStripSeparator();
			PasteTo_ToolStripMenuItem = new ToolStripMenuItem();
			Image_toolStripMenuItem = new ToolStripMenuItem();
			FlipRotate_ToolStripMenuItem = new ToolStripMenuItem();
			toolStripSeparator6 = new ToolStripSeparator();
			ChangeSize_ToolStripMenuItem = new ToolStripMenuItem();
			View_toolStripMenuItem = new ToolStripMenuItem();
			ShowThumbnail_ToolStripMenuItem = new ToolStripMenuItem();
			toolStripSeparator7 = new ToolStripSeparator();
			ShowGrid_ToolStripMenuItem = new ToolStripMenuItem();
			GridSettings_ToolStripMenuItem = new ToolStripMenuItem();
			Color_toolStripMenuItem = new ToolStripMenuItem();
			LeftColor_ToolStripMenuItem = new ToolStripMenuItem();
			RightColor_ToolStripMenuItem = new ToolStripMenuItem();
			Settings_toolStripMenuItem = new ToolStripMenuItem();
			ConfirmAtSave_ToolStripMenuItem = new ToolStripMenuItem();
			ShowFullPathOnCaption_ToolStripMenuItem = new ToolStripMenuItem();
			Help_toolStripMenuItem = new ToolStripMenuItem();
			Version_ToolStripMenuItem = new ToolStripMenuItem();
			Main_toolStripContainer.BottomToolStripPanel.SuspendLayout();
			Main_toolStripContainer.ContentPanel.SuspendLayout();
			Main_toolStripContainer.TopToolStripPanel.SuspendLayout();
			Main_toolStripContainer.SuspendLayout();
			Main_statusStrip.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)UD_splitContainer ).BeginInit();
			UD_splitContainer.Panel1.SuspendLayout();
			UD_splitContainer.Panel2.SuspendLayout();
			UD_splitContainer.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)LR_splitContainer ).BeginInit();
			LR_splitContainer.Panel1.SuspendLayout();
			LR_splitContainer.Panel2.SuspendLayout();
			LR_splitContainer.SuspendLayout();
			ScrollSizeDeside_panel.SuspendLayout();
			ColorPane_tableLayoutPanel.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)LColor_pictureBox ).BeginInit();
			( (System.ComponentModel.ISupportInitialize)RColor_pictureBox ).BeginInit();
			Main_menuStrip.SuspendLayout();
			SuspendLayout();
			// 
			// Main_toolStripContainer
			// 
			// 
			// Main_toolStripContainer.BottomToolStripPanel
			// 
			Main_toolStripContainer.BottomToolStripPanel.Controls.Add( Main_statusStrip );
			// 
			// Main_toolStripContainer.ContentPanel
			// 
			Main_toolStripContainer.ContentPanel.Controls.Add( UD_splitContainer );
			Main_toolStripContainer.ContentPanel.Size = new Size( 800, 388 );
			Main_toolStripContainer.Dock = DockStyle.Fill;
			Main_toolStripContainer.Location = new Point( 0, 0 );
			Main_toolStripContainer.Name = "Main_toolStripContainer";
			Main_toolStripContainer.Size = new Size( 800, 450 );
			Main_toolStripContainer.TabIndex = 0;
			Main_toolStripContainer.Text = "toolStripContainer1";
			// 
			// Main_toolStripContainer.TopToolStripPanel
			// 
			Main_toolStripContainer.TopToolStripPanel.Controls.Add( Main_menuStrip );
			// 
			// Main_statusStrip
			// 
			Main_statusStrip.Dock = DockStyle.None;
			Main_statusStrip.ImageScalingSize = new Size( 32, 32 );
			Main_statusStrip.Items.AddRange( new ToolStripItem[] { Info_toolStripStatusLabel, Zoom_toolStripDropDownButton, Pos_toolStripStatusLabel, Size_toolStripStatusLabel } );
			Main_statusStrip.Location = new Point( 0, 0 );
			Main_statusStrip.Name = "Main_statusStrip";
			Main_statusStrip.Size = new Size( 800, 38 );
			Main_statusStrip.TabIndex = 0;
			// 
			// Info_toolStripStatusLabel
			// 
			Info_toolStripStatusLabel.Name = "Info_toolStripStatusLabel";
			Info_toolStripStatusLabel.Size = new Size( 621, 33 );
			Info_toolStripStatusLabel.Spring = true;
			Info_toolStripStatusLabel.Text = "(Info)";
			Info_toolStripStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// Zoom_toolStripDropDownButton
			// 
			Zoom_toolStripDropDownButton.Image = Properties.Resources.Loupe;
			Zoom_toolStripDropDownButton.ImageTransparentColor = Color.Magenta;
			Zoom_toolStripDropDownButton.Name = "Zoom_toolStripDropDownButton";
			Zoom_toolStripDropDownButton.Size = new Size( 87, 36 );
			Zoom_toolStripDropDownButton.Text = "(Scale)";
			Zoom_toolStripDropDownButton.DropDownItemClicked +=  Zoom_toolStripDropDownButton_DropDownItemClicked ;
			// 
			// Pos_toolStripStatusLabel
			// 
			Pos_toolStripStatusLabel.BorderSides =    ToolStripStatusLabelBorderSides.Left  |  ToolStripStatusLabelBorderSides.Top   |  ToolStripStatusLabelBorderSides.Right   |  ToolStripStatusLabelBorderSides.Bottom ;
			Pos_toolStripStatusLabel.Name = "Pos_toolStripStatusLabel";
			Pos_toolStripStatusLabel.Size = new Size( 38, 33 );
			Pos_toolStripStatusLabel.Text = "(Pos)";
			// 
			// Size_toolStripStatusLabel
			// 
			Size_toolStripStatusLabel.BorderSides =    ToolStripStatusLabelBorderSides.Left  |  ToolStripStatusLabelBorderSides.Top   |  ToolStripStatusLabelBorderSides.Right   |  ToolStripStatusLabelBorderSides.Bottom ;
			Size_toolStripStatusLabel.Name = "Size_toolStripStatusLabel";
			Size_toolStripStatusLabel.Size = new Size( 39, 33 );
			Size_toolStripStatusLabel.Text = "(Size)";
			// 
			// UD_splitContainer
			// 
			UD_splitContainer.BorderStyle = BorderStyle.Fixed3D;
			UD_splitContainer.Dock = DockStyle.Fill;
			UD_splitContainer.FixedPanel = FixedPanel.Panel2;
			UD_splitContainer.Location = new Point( 0, 0 );
			UD_splitContainer.Name = "UD_splitContainer";
			UD_splitContainer.Orientation = Orientation.Horizontal;
			// 
			// UD_splitContainer.Panel1
			// 
			UD_splitContainer.Panel1.Controls.Add( LR_splitContainer );
			// 
			// UD_splitContainer.Panel2
			// 
			UD_splitContainer.Panel2.Controls.Add( ColorPane_tableLayoutPanel );
			UD_splitContainer.Size = new Size( 800, 388 );
			UD_splitContainer.SplitterDistance = 304;
			UD_splitContainer.TabIndex = 0;
			// 
			// LR_splitContainer
			// 
			LR_splitContainer.BorderStyle = BorderStyle.Fixed3D;
			LR_splitContainer.Dock = DockStyle.Fill;
			LR_splitContainer.FixedPanel = FixedPanel.Panel1;
			LR_splitContainer.Location = new Point( 0, 0 );
			LR_splitContainer.Name = "LR_splitContainer";
			// 
			// LR_splitContainer.Panel1
			// 
			LR_splitContainer.Panel1.Controls.Add( The_ToolsUC );
			// 
			// LR_splitContainer.Panel2
			// 
			LR_splitContainer.Panel2.AutoScroll = true;
			LR_splitContainer.Panel2.BackColor = SystemColors.ControlDark;
			LR_splitContainer.Panel2.Controls.Add( ScrollSizeDeside_panel );
			LR_splitContainer.Panel2.MouseClick +=  LR_splitContainer_Panel2_MouseClick ;
			LR_splitContainer.Size = new Size( 800, 304 );
			LR_splitContainer.SplitterDistance = 174;
			LR_splitContainer.TabIndex = 0;
			// 
			// The_ToolsUC
			// 
			The_ToolsUC.Dock = DockStyle.Fill;
			The_ToolsUC.Location = new Point( 0, 0 );
			The_ToolsUC.Margin = new Padding( 0 );
			The_ToolsUC.Name = "The_ToolsUC";
			The_ToolsUC.Size = new Size( 170, 300 );
			The_ToolsUC.TabIndex = 0;
			// 
			// ScrollSizeDeside_panel
			// 
			ScrollSizeDeside_panel.AutoSize = true;
			ScrollSizeDeside_panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			ScrollSizeDeside_panel.BackColor = SystemColors.ControlDark;
			ScrollSizeDeside_panel.Controls.Add( The_viewControl );
			ScrollSizeDeside_panel.Location = new Point( 0, 0 );
			ScrollSizeDeside_panel.Margin = new Padding( 0 );
			ScrollSizeDeside_panel.Name = "ScrollSizeDeside_panel";
			ScrollSizeDeside_panel.Padding = new Padding( 8 );
			ScrollSizeDeside_panel.Size = new Size( 28, 29 );
			ScrollSizeDeside_panel.TabIndex = 0;
			ScrollSizeDeside_panel.MouseClick +=  ScrollSizeDeside_panel_MouseClick ;
			// 
			// The_viewControl
			// 
			The_viewControl.Location = new Point( 1, 2 );
			The_viewControl.Name = "The_viewControl";
			The_viewControl.Size = new Size( 16, 16 );
			The_viewControl.TabIndex = 0;
			The_viewControl.Text = "viewControl1";
			The_viewControl.MouseDown +=  The_viewControl_MouseDown ;
			The_viewControl.MouseMove +=  The_viewControl_MouseMove ;
			The_viewControl.MouseUp +=  The_viewControl_MouseUp ;
			// 
			// ColorPane_tableLayoutPanel
			// 
			ColorPane_tableLayoutPanel.ColumnCount = 4;
			ColorPane_tableLayoutPanel.ColumnStyles.Add( new ColumnStyle() );
			ColorPane_tableLayoutPanel.ColumnStyles.Add( new ColumnStyle() );
			ColorPane_tableLayoutPanel.ColumnStyles.Add( new ColumnStyle() );
			ColorPane_tableLayoutPanel.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100F ) );
			ColorPane_tableLayoutPanel.Controls.Add( SwapColor_button, 2, 0 );
			ColorPane_tableLayoutPanel.Controls.Add( LColor_pictureBox, 0, 0 );
			ColorPane_tableLayoutPanel.Controls.Add( RColor_pictureBox, 1, 0 );
			ColorPane_tableLayoutPanel.Controls.Add( The_ColorsUC, 3, 0 );
			ColorPane_tableLayoutPanel.Dock = DockStyle.Fill;
			ColorPane_tableLayoutPanel.Location = new Point( 0, 0 );
			ColorPane_tableLayoutPanel.Name = "ColorPane_tableLayoutPanel";
			ColorPane_tableLayoutPanel.RowCount = 1;
			ColorPane_tableLayoutPanel.RowStyles.Add( new RowStyle( SizeType.Percent, 100F ) );
			ColorPane_tableLayoutPanel.Size = new Size( 796, 76 );
			ColorPane_tableLayoutPanel.TabIndex = 0;
			// 
			// SwapColor_button
			// 
			SwapColor_button.AutoSize = true;
			SwapColor_button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			SwapColor_button.Location = new Point( 115, 3 );
			SwapColor_button.Name = "SwapColor_button";
			SwapColor_button.Size = new Size( 45, 25 );
			SwapColor_button.TabIndex = 0;
			SwapColor_button.Text = "Swap";
			SwapColor_button.UseVisualStyleBackColor = true;
			SwapColor_button.Click +=  SwapColor_button_Click ;
			// 
			// LColor_pictureBox
			// 
			LColor_pictureBox.BorderStyle = BorderStyle.FixedSingle;
			LColor_pictureBox.Location = new Point( 3, 3 );
			LColor_pictureBox.Name = "LColor_pictureBox";
			LColor_pictureBox.Size = new Size( 50, 50 );
			LColor_pictureBox.TabIndex = 1;
			LColor_pictureBox.TabStop = false;
			LColor_pictureBox.DoubleClick +=  LColor_pictureBox_DoubleClick ;
			// 
			// RColor_pictureBox
			// 
			RColor_pictureBox.BorderStyle = BorderStyle.FixedSingle;
			RColor_pictureBox.Location = new Point( 59, 3 );
			RColor_pictureBox.Name = "RColor_pictureBox";
			RColor_pictureBox.Size = new Size( 50, 50 );
			RColor_pictureBox.TabIndex = 1;
			RColor_pictureBox.TabStop = false;
			RColor_pictureBox.DoubleClick +=  RColor_pictureBox_DoubleClick ;
			// 
			// The_ColorsUC
			// 
			The_ColorsUC.Dock = DockStyle.Fill;
			The_ColorsUC.Location = new Point( 163, 0 );
			The_ColorsUC.Margin = new Padding( 0 );
			The_ColorsUC.Name = "The_ColorsUC";
			The_ColorsUC.Size = new Size( 633, 76 );
			The_ColorsUC.TabIndex = 2;
			// 
			// Main_menuStrip
			// 
			Main_menuStrip.Dock = DockStyle.None;
			Main_menuStrip.Items.AddRange( new ToolStripItem[] { File_toolStripMenuItem, Edit_toolStripMenuItem, Image_toolStripMenuItem, View_toolStripMenuItem, Color_toolStripMenuItem, Settings_toolStripMenuItem, Help_toolStripMenuItem } );
			Main_menuStrip.Location = new Point( 0, 0 );
			Main_menuStrip.Name = "Main_menuStrip";
			Main_menuStrip.Size = new Size( 800, 24 );
			Main_menuStrip.TabIndex = 0;
			Main_menuStrip.Text = "menuStrip1";
			Main_menuStrip.MenuActivate +=  Main_menuStrip_MenuActivate ;
			Main_menuStrip.MenuDeactivate +=  Main_menuStrip_MenuDeactivate ;
			// 
			// File_toolStripMenuItem
			// 
			File_toolStripMenuItem.DropDownItems.AddRange( new ToolStripItem[] { New_ToolStripMenuItem, Open_ToolStripMenuItem, toolStripSeparator1, Save_ToolStripMenuItem, SaveAs_ToolStripMenuItem, toolStripSeparator2, ExportAsMonoBMP_ToolStripMenuItem } );
			File_toolStripMenuItem.Name = "File_toolStripMenuItem";
			File_toolStripMenuItem.Overflow = ToolStripItemOverflow.AsNeeded;
			File_toolStripMenuItem.Size = new Size( 51, 20 );
			File_toolStripMenuItem.Text = "File(&F)";
			// 
			// New_ToolStripMenuItem
			// 
			New_ToolStripMenuItem.Name = "New_ToolStripMenuItem";
			New_ToolStripMenuItem.Size = new Size( 215, 22 );
			New_ToolStripMenuItem.Text = "New(&N)...";
			New_ToolStripMenuItem.Click +=  New_ToolStripMenuItem_Click ;
			// 
			// Open_ToolStripMenuItem
			// 
			Open_ToolStripMenuItem.Name = "Open_ToolStripMenuItem";
			Open_ToolStripMenuItem.Size = new Size( 215, 22 );
			Open_ToolStripMenuItem.Text = "Open(&O)...";
			Open_ToolStripMenuItem.Click +=  Open_ToolStripMenuItem_Click ;
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new Size( 212, 6 );
			// 
			// Save_ToolStripMenuItem
			// 
			Save_ToolStripMenuItem.Name = "Save_ToolStripMenuItem";
			Save_ToolStripMenuItem.ShortcutKeys =  Keys.Control  |  Keys.S ;
			Save_ToolStripMenuItem.Size = new Size( 215, 22 );
			Save_ToolStripMenuItem.Text = "Save(&W)";
			Save_ToolStripMenuItem.Click +=  Save_ToolStripMenuItem_Click ;
			// 
			// SaveAs_ToolStripMenuItem
			// 
			SaveAs_ToolStripMenuItem.Name = "SaveAs_ToolStripMenuItem";
			SaveAs_ToolStripMenuItem.Size = new Size( 215, 22 );
			SaveAs_ToolStripMenuItem.Text = "Save As(&S)...";
			SaveAs_ToolStripMenuItem.Click +=  SaveAs_ToolStripMenuItem_Click ;
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new Size( 212, 6 );
			// 
			// ExportAsMonoBMP_ToolStripMenuItem
			// 
			ExportAsMonoBMP_ToolStripMenuItem.Name = "ExportAsMonoBMP_ToolStripMenuItem";
			ExportAsMonoBMP_ToolStripMenuItem.Size = new Size( 215, 22 );
			ExportAsMonoBMP_ToolStripMenuItem.Text = "Export as Mono-BMP(&M)...";
			ExportAsMonoBMP_ToolStripMenuItem.Click +=  ExportAsMonoBMP_ToolStripMenuItem_Click ;
			// 
			// Edit_toolStripMenuItem
			// 
			Edit_toolStripMenuItem.DropDownItems.AddRange( new ToolStripItem[] { Undo_ToolStripMenuItem, Redo_ToolStripMenuItem, DiscardUndoRedoData_ToolStripMenuItem, toolStripSeparator3, Copy_ToolStripMenuItem, Cut_ToolStripMenuItem, Paste_ToolStripMenuItem, toolStripSeparator4, SelectAll_ToolStripMenuItem, ClearSelection_ToolStripMenuItem, toolStripSeparator5, PasteTo_ToolStripMenuItem } );
			Edit_toolStripMenuItem.Name = "Edit_toolStripMenuItem";
			Edit_toolStripMenuItem.Overflow = ToolStripItemOverflow.AsNeeded;
			Edit_toolStripMenuItem.Size = new Size( 53, 20 );
			Edit_toolStripMenuItem.Text = "Edit(&E)";
			// 
			// Undo_ToolStripMenuItem
			// 
			Undo_ToolStripMenuItem.Name = "Undo_ToolStripMenuItem";
			Undo_ToolStripMenuItem.ShortcutKeys =  Keys.Control  |  Keys.Z ;
			Undo_ToolStripMenuItem.Size = new Size( 204, 22 );
			Undo_ToolStripMenuItem.Text = "Undo";
			Undo_ToolStripMenuItem.Click +=  Undo_ToolStripMenuItem_Click ;
			// 
			// Redo_ToolStripMenuItem
			// 
			Redo_ToolStripMenuItem.Name = "Redo_ToolStripMenuItem";
			Redo_ToolStripMenuItem.ShortcutKeys =  Keys.Control  |  Keys.Y ;
			Redo_ToolStripMenuItem.Size = new Size( 204, 22 );
			Redo_ToolStripMenuItem.Text = "Redo";
			Redo_ToolStripMenuItem.Click +=  Redo_ToolStripMenuItem_Click ;
			// 
			// DiscardUndoRedoData_ToolStripMenuItem
			// 
			DiscardUndoRedoData_ToolStripMenuItem.Name = "DiscardUndoRedoData_ToolStripMenuItem";
			DiscardUndoRedoData_ToolStripMenuItem.Size = new Size( 204, 22 );
			DiscardUndoRedoData_ToolStripMenuItem.Text = "Discard Undo/Redo Data";
			DiscardUndoRedoData_ToolStripMenuItem.Click +=  DiscardUndoRedoData_ToolStripMenuItem_Click ;
			// 
			// toolStripSeparator3
			// 
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new Size( 201, 6 );
			// 
			// Copy_ToolStripMenuItem
			// 
			Copy_ToolStripMenuItem.Name = "Copy_ToolStripMenuItem";
			Copy_ToolStripMenuItem.ShortcutKeys =  Keys.Control  |  Keys.C ;
			Copy_ToolStripMenuItem.Size = new Size( 204, 22 );
			Copy_ToolStripMenuItem.Text = "Copy";
			Copy_ToolStripMenuItem.Click +=  Copy_ToolStripMenuItem_Click ;
			// 
			// Cut_ToolStripMenuItem
			// 
			Cut_ToolStripMenuItem.Name = "Cut_ToolStripMenuItem";
			Cut_ToolStripMenuItem.ShortcutKeys =  Keys.Control  |  Keys.X ;
			Cut_ToolStripMenuItem.Size = new Size( 204, 22 );
			Cut_ToolStripMenuItem.Text = "Cut";
			Cut_ToolStripMenuItem.Click +=  Cut_ToolStripMenuItem_Click ;
			// 
			// Paste_ToolStripMenuItem
			// 
			Paste_ToolStripMenuItem.Name = "Paste_ToolStripMenuItem";
			Paste_ToolStripMenuItem.ShortcutKeys =  Keys.Control  |  Keys.V ;
			Paste_ToolStripMenuItem.Size = new Size( 204, 22 );
			Paste_ToolStripMenuItem.Text = "Paste";
			Paste_ToolStripMenuItem.Click +=  Paste_ToolStripMenuItem_Click ;
			// 
			// toolStripSeparator4
			// 
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new Size( 201, 6 );
			// 
			// SelectAll_ToolStripMenuItem
			// 
			SelectAll_ToolStripMenuItem.Name = "SelectAll_ToolStripMenuItem";
			SelectAll_ToolStripMenuItem.ShortcutKeys =  Keys.Control  |  Keys.A ;
			SelectAll_ToolStripMenuItem.Size = new Size( 204, 22 );
			SelectAll_ToolStripMenuItem.Text = "Select All";
			SelectAll_ToolStripMenuItem.Click +=  SelectAll_ToolStripMenuItem_Click ;
			// 
			// ClearSelection_ToolStripMenuItem
			// 
			ClearSelection_ToolStripMenuItem.Name = "ClearSelection_ToolStripMenuItem";
			ClearSelection_ToolStripMenuItem.ShortcutKeys = Keys.Delete;
			ClearSelection_ToolStripMenuItem.Size = new Size( 204, 22 );
			ClearSelection_ToolStripMenuItem.Text = "Clear Selection";
			ClearSelection_ToolStripMenuItem.Click +=  ClearSelection_ToolStripMenuItem_Click ;
			// 
			// toolStripSeparator5
			// 
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new Size( 201, 6 );
			// 
			// PasteTo_ToolStripMenuItem
			// 
			PasteTo_ToolStripMenuItem.Name = "PasteTo_ToolStripMenuItem";
			PasteTo_ToolStripMenuItem.Size = new Size( 204, 22 );
			PasteTo_ToolStripMenuItem.Text = "Paste To(&S)...";
			PasteTo_ToolStripMenuItem.Click +=  PasteTo_ToolStripMenuItem_Click ;
			// 
			// Image_toolStripMenuItem
			// 
			Image_toolStripMenuItem.DropDownItems.AddRange( new ToolStripItem[] { FlipRotate_ToolStripMenuItem, toolStripSeparator6, ChangeSize_ToolStripMenuItem } );
			Image_toolStripMenuItem.Name = "Image_toolStripMenuItem";
			Image_toolStripMenuItem.Overflow = ToolStripItemOverflow.AsNeeded;
			Image_toolStripMenuItem.Size = new Size( 62, 20 );
			Image_toolStripMenuItem.Text = "Image(&I)";
			// 
			// FlipRotate_ToolStripMenuItem
			// 
			FlipRotate_ToolStripMenuItem.Name = "FlipRotate_ToolStripMenuItem";
			FlipRotate_ToolStripMenuItem.Size = new Size( 160, 22 );
			FlipRotate_ToolStripMenuItem.Text = "Flip/Rotate(&F)...";
			FlipRotate_ToolStripMenuItem.Click +=  FlipRotate_ToolStripMenuItem_Click ;
			// 
			// toolStripSeparator6
			// 
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new Size( 157, 6 );
			// 
			// ChangeSize_ToolStripMenuItem
			// 
			ChangeSize_ToolStripMenuItem.Name = "ChangeSize_ToolStripMenuItem";
			ChangeSize_ToolStripMenuItem.Size = new Size( 160, 22 );
			ChangeSize_ToolStripMenuItem.Text = "Change Size(&S)...";
			ChangeSize_ToolStripMenuItem.Click +=  ChangeSize_ToolStripMenuItem_Click ;
			// 
			// View_toolStripMenuItem
			// 
			View_toolStripMenuItem.DropDownItems.AddRange( new ToolStripItem[] { ShowThumbnail_ToolStripMenuItem, toolStripSeparator7, ShowGrid_ToolStripMenuItem, GridSettings_ToolStripMenuItem } );
			View_toolStripMenuItem.Name = "View_toolStripMenuItem";
			View_toolStripMenuItem.Overflow = ToolStripItemOverflow.AsNeeded;
			View_toolStripMenuItem.Size = new Size( 59, 20 );
			View_toolStripMenuItem.Text = "View(&V)";
			// 
			// ShowThumbnail_ToolStripMenuItem
			// 
			ShowThumbnail_ToolStripMenuItem.Name = "ShowThumbnail_ToolStripMenuItem";
			ShowThumbnail_ToolStripMenuItem.ShortcutKeys =  Keys.Control  |  Keys.T ;
			ShowThumbnail_ToolStripMenuItem.Size = new Size( 215, 22 );
			ShowThumbnail_ToolStripMenuItem.Text = "Show Thumbnail(&T)";
			ShowThumbnail_ToolStripMenuItem.Click +=  ShowThumbnail_ToolStripMenuItem_Click ;
			// 
			// toolStripSeparator7
			// 
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new Size( 212, 6 );
			// 
			// ShowGrid_ToolStripMenuItem
			// 
			ShowGrid_ToolStripMenuItem.Name = "ShowGrid_ToolStripMenuItem";
			ShowGrid_ToolStripMenuItem.ShortcutKeys =  Keys.Control  |  Keys.G ;
			ShowGrid_ToolStripMenuItem.Size = new Size( 215, 22 );
			ShowGrid_ToolStripMenuItem.Text = "Show Grid(&G)";
			ShowGrid_ToolStripMenuItem.Click +=  ShowGrid_ToolStripMenuItem_Click ;
			// 
			// GridSettings_ToolStripMenuItem
			// 
			GridSettings_ToolStripMenuItem.Name = "GridSettings_ToolStripMenuItem";
			GridSettings_ToolStripMenuItem.Size = new Size( 215, 22 );
			GridSettings_ToolStripMenuItem.Text = "Grid Settings(&S)...";
			GridSettings_ToolStripMenuItem.Click +=  GridSettings_ToolStripMenuItem_Click ;
			// 
			// Color_toolStripMenuItem
			// 
			Color_toolStripMenuItem.DropDownItems.AddRange( new ToolStripItem[] { LeftColor_ToolStripMenuItem, RightColor_ToolStripMenuItem } );
			Color_toolStripMenuItem.Name = "Color_toolStripMenuItem";
			Color_toolStripMenuItem.Overflow = ToolStripItemOverflow.AsNeeded;
			Color_toolStripMenuItem.Size = new Size( 62, 20 );
			Color_toolStripMenuItem.Text = "Color(&C)";
			// 
			// LeftColor_ToolStripMenuItem
			// 
			LeftColor_ToolStripMenuItem.Name = "LeftColor_ToolStripMenuItem";
			LeftColor_ToolStripMenuItem.Size = new Size( 157, 22 );
			LeftColor_ToolStripMenuItem.Text = "Left Color(&L)...";
			LeftColor_ToolStripMenuItem.Click +=  LeftColor_ToolStripMenuItem_Click ;
			// 
			// RightColor_ToolStripMenuItem
			// 
			RightColor_ToolStripMenuItem.Name = "RightColor_ToolStripMenuItem";
			RightColor_ToolStripMenuItem.Size = new Size( 157, 22 );
			RightColor_ToolStripMenuItem.Text = "Right Color(&R)...";
			RightColor_ToolStripMenuItem.Click +=  RightColor_ToolStripMenuItem_Click ;
			// 
			// Settings_toolStripMenuItem
			// 
			Settings_toolStripMenuItem.DropDownItems.AddRange( new ToolStripItem[] { ConfirmAtSave_ToolStripMenuItem, ShowFullPathOnCaption_ToolStripMenuItem } );
			Settings_toolStripMenuItem.Name = "Settings_toolStripMenuItem";
			Settings_toolStripMenuItem.Overflow = ToolStripItemOverflow.AsNeeded;
			Settings_toolStripMenuItem.Size = new Size( 75, 20 );
			Settings_toolStripMenuItem.Text = "Settings(&S)";
			// 
			// ConfirmAtSave_ToolStripMenuItem
			// 
			ConfirmAtSave_ToolStripMenuItem.Name = "ConfirmAtSave_ToolStripMenuItem";
			ConfirmAtSave_ToolStripMenuItem.Size = new Size( 218, 22 );
			ConfirmAtSave_ToolStripMenuItem.Text = "Confirmation at \"Save\" time";
			ConfirmAtSave_ToolStripMenuItem.Click +=  ConfirmAtSave_ToolStripMenuItem_Click ;
			// 
			// ShowFullPathOnCaption_ToolStripMenuItem
			// 
			ShowFullPathOnCaption_ToolStripMenuItem.Name = "ShowFullPathOnCaption_ToolStripMenuItem";
			ShowFullPathOnCaption_ToolStripMenuItem.Size = new Size( 218, 22 );
			ShowFullPathOnCaption_ToolStripMenuItem.Text = "Show Full Path On Caption";
			ShowFullPathOnCaption_ToolStripMenuItem.Click +=  ShowFullPathOnCaption_ToolStripMenuItem_Click ;
			// 
			// Help_toolStripMenuItem
			// 
			Help_toolStripMenuItem.DropDownItems.AddRange( new ToolStripItem[] { Version_ToolStripMenuItem } );
			Help_toolStripMenuItem.Name = "Help_toolStripMenuItem";
			Help_toolStripMenuItem.Overflow = ToolStripItemOverflow.AsNeeded;
			Help_toolStripMenuItem.Size = new Size( 61, 20 );
			Help_toolStripMenuItem.Text = "Help(&H)";
			// 
			// Version_ToolStripMenuItem
			// 
			Version_ToolStripMenuItem.Name = "Version_ToolStripMenuItem";
			Version_ToolStripMenuItem.Size = new Size( 136, 22 );
			Version_ToolStripMenuItem.Text = "Version(&V)...";
			Version_ToolStripMenuItem.Click +=  Version_ToolStripMenuItem_Click ;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF( 96F, 96F );
			AutoScaleMode = AutoScaleMode.Dpi;
			ClientSize = new Size( 800, 450 );
			Controls.Add( Main_toolStripContainer );
			Icon = (Icon)resources.GetObject( "$this.Icon" );
			MainMenuStrip = Main_menuStrip;
			Name = "MainForm";
			Text = "Paint.CAT";
			FormClosing +=  MainForm_FormClosing ;
			Load +=  MainForm_Load ;
			Main_toolStripContainer.BottomToolStripPanel.ResumeLayout( false );
			Main_toolStripContainer.BottomToolStripPanel.PerformLayout();
			Main_toolStripContainer.ContentPanel.ResumeLayout( false );
			Main_toolStripContainer.TopToolStripPanel.ResumeLayout( false );
			Main_toolStripContainer.TopToolStripPanel.PerformLayout();
			Main_toolStripContainer.ResumeLayout( false );
			Main_toolStripContainer.PerformLayout();
			Main_statusStrip.ResumeLayout( false );
			Main_statusStrip.PerformLayout();
			UD_splitContainer.Panel1.ResumeLayout( false );
			UD_splitContainer.Panel2.ResumeLayout( false );
			( (System.ComponentModel.ISupportInitialize)UD_splitContainer ).EndInit();
			UD_splitContainer.ResumeLayout( false );
			LR_splitContainer.Panel1.ResumeLayout( false );
			LR_splitContainer.Panel2.ResumeLayout( false );
			LR_splitContainer.Panel2.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)LR_splitContainer ).EndInit();
			LR_splitContainer.ResumeLayout( false );
			ScrollSizeDeside_panel.ResumeLayout( false );
			ColorPane_tableLayoutPanel.ResumeLayout( false );
			ColorPane_tableLayoutPanel.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)LColor_pictureBox ).EndInit();
			( (System.ComponentModel.ISupportInitialize)RColor_pictureBox ).EndInit();
			Main_menuStrip.ResumeLayout( false );
			Main_menuStrip.PerformLayout();
			ResumeLayout( false );
		}

		#endregion

		private ToolStripContainer Main_toolStripContainer;
		private StatusStrip Main_statusStrip;
		private MenuStrip Main_menuStrip;
		private ToolStripMenuItem File_toolStripMenuItem;
		private ToolStripMenuItem Edit_toolStripMenuItem;
		private ToolStripMenuItem Image_toolStripMenuItem;
		private ToolStripMenuItem View_toolStripMenuItem;
		private ToolStripMenuItem Color_toolStripMenuItem;
		private ToolStripMenuItem Settings_toolStripMenuItem;
		private ToolStripMenuItem Help_toolStripMenuItem;
		private SplitContainer UD_splitContainer;
		private SplitContainer LR_splitContainer;
		private ToolStripStatusLabel Info_toolStripStatusLabel;
		private ToolStripDropDownButton Zoom_toolStripDropDownButton;
		private ToolStripStatusLabel Pos_toolStripStatusLabel;
		private ToolStripStatusLabel Size_toolStripStatusLabel;
		private ToolStripMenuItem New_ToolStripMenuItem;
		private ToolStripMenuItem Open_ToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem Save_ToolStripMenuItem;
		private ToolStripMenuItem SaveAs_ToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem ExportAsMonoBMP_ToolStripMenuItem;
		private ToolStripMenuItem Undo_ToolStripMenuItem;
		private ToolStripMenuItem Redo_ToolStripMenuItem;
		private ToolStripMenuItem DiscardUndoRedoData_ToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripMenuItem Copy_ToolStripMenuItem;
		private ToolStripMenuItem Cut_ToolStripMenuItem;
		private ToolStripMenuItem Paste_ToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripMenuItem SelectAll_ToolStripMenuItem;
		private ToolStripMenuItem ClearSelection_ToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator5;
		private ToolStripMenuItem PasteTo_ToolStripMenuItem;
		private ToolStripMenuItem ConfirmAtSave_ToolStripMenuItem;
		private ToolStripMenuItem ShowFullPathOnCaption_ToolStripMenuItem;
		private Panel ScrollSizeDeside_panel;
		private TableLayoutPanel ColorPane_tableLayoutPanel;
		private Button SwapColor_button;
		private PictureBox LColor_pictureBox;
		private PictureBox RColor_pictureBox;
		private ToolsUC The_ToolsUC;
		private ViewControl The_viewControl;
		private ToolStripMenuItem Version_ToolStripMenuItem;
		private ToolStripMenuItem FlipRotate_ToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator6;
		private ToolStripMenuItem ChangeSize_ToolStripMenuItem;
		private ToolStripMenuItem ShowThumbnail_ToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator7;
		private ToolStripMenuItem ShowGrid_ToolStripMenuItem;
		private ToolStripMenuItem GridSettings_ToolStripMenuItem;
		private ToolStripMenuItem LeftColor_ToolStripMenuItem;
		private ToolStripMenuItem RightColor_ToolStripMenuItem;
		private ColorsUC The_ColorsUC;
	}
}
