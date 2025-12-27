namespace PaintDotCat
{
	partial class ToolsUC
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ToolsUC ) );
			Main_tabControl = new TabControl();
			Pen_tabPage = new TabPage();
			tableLayoutPanel1 = new TableLayoutPanel();
			Pen1Pix_radioButton = new RadioButton();
			Pen2x2_radioButton = new RadioButton();
			label1 = new Label();
			label2 = new Label();
			Line_tabPage = new TabPage();
			tableLayoutPanel2 = new TableLayoutPanel();
			label3 = new Label();
			label4 = new Label();
			RoundEnd_checkBox = new CheckBox();
			label5 = new Label();
			LineWeight_numericUpDown = new NumericUpDown();
			ResetLineWeight_button = new Button();
			Select_tabPage = new TabPage();
			tableLayoutPanel3 = new TableLayoutPanel();
			RectSelMode_radioButton = new RadioButton();
			RectSelMode_pictureBox = new PictureBox();
			FreeFormSelMode_pictureBox = new PictureBox();
			label6 = new Label();
			TransparentMode_pictureBox = new PictureBox();
			FreeFormSelMode_radioButton = new RadioButton();
			TransBackColor_checkBox = new CheckBox();
			pictureBox4 = new PictureBox();
			Eraser_tabPage = new TabPage();
			tableLayoutPanel4 = new TableLayoutPanel();
			Eraser3x3_radioButton = new RadioButton();
			Eraser5x5_radioButton = new RadioButton();
			label7 = new Label();
			label8 = new Label();
			Eraser7x7_radioButton = new RadioButton();
			Eraser9x9_radioButton = new RadioButton();
			Eraser11x11_radioButton = new RadioButton();
			Fill_tabPage = new TabPage();
			tableLayoutPanel5 = new TableLayoutPanel();
			label10 = new Label();
			Icon_imageList = new ImageList( components );
			Main_tabControl.SuspendLayout();
			Pen_tabPage.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			Line_tabPage.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)LineWeight_numericUpDown ).BeginInit();
			Select_tabPage.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)RectSelMode_pictureBox ).BeginInit();
			( (System.ComponentModel.ISupportInitialize)FreeFormSelMode_pictureBox ).BeginInit();
			( (System.ComponentModel.ISupportInitialize)TransparentMode_pictureBox ).BeginInit();
			( (System.ComponentModel.ISupportInitialize)pictureBox4 ).BeginInit();
			Eraser_tabPage.SuspendLayout();
			tableLayoutPanel4.SuspendLayout();
			Fill_tabPage.SuspendLayout();
			tableLayoutPanel5.SuspendLayout();
			SuspendLayout();
			// 
			// Main_tabControl
			// 
			Main_tabControl.Alignment = TabAlignment.Right;
			Main_tabControl.Controls.Add( Pen_tabPage );
			Main_tabControl.Controls.Add( Line_tabPage );
			Main_tabControl.Controls.Add( Select_tabPage );
			Main_tabControl.Controls.Add( Eraser_tabPage );
			Main_tabControl.Controls.Add( Fill_tabPage );
			Main_tabControl.Dock = DockStyle.Fill;
			Main_tabControl.ImageList = Icon_imageList;
			Main_tabControl.Location = new Point( 0, 0 );
			Main_tabControl.Margin = new Padding( 0 );
			Main_tabControl.Multiline = true;
			Main_tabControl.Name = "Main_tabControl";
			Main_tabControl.SelectedIndex = 0;
			Main_tabControl.Size = new Size( 360, 503 );
			Main_tabControl.TabIndex = 0;
			Main_tabControl.SelectedIndexChanged +=  Main_tabControl_SelectedIndexChanged ;
			// 
			// Pen_tabPage
			// 
			Pen_tabPage.Controls.Add( tableLayoutPanel1 );
			Pen_tabPage.ImageIndex = 0;
			Pen_tabPage.Location = new Point( 4, 4 );
			Pen_tabPage.Name = "Pen_tabPage";
			Pen_tabPage.Padding = new Padding( 3 );
			Pen_tabPage.Size = new Size( 317, 495 );
			Pen_tabPage.TabIndex = 0;
			Pen_tabPage.Text = "Pen";
			Pen_tabPage.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel1.Controls.Add( Pen1Pix_radioButton, 0, 0 );
			tableLayoutPanel1.Controls.Add( Pen2x2_radioButton, 0, 1 );
			tableLayoutPanel1.Controls.Add( label1, 0, 2 );
			tableLayoutPanel1.Controls.Add( label2, 0, 3 );
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point( 3, 3 );
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 4;
			tableLayoutPanel1.RowStyles.Add( new RowStyle() );
			tableLayoutPanel1.RowStyles.Add( new RowStyle() );
			tableLayoutPanel1.RowStyles.Add( new RowStyle() );
			tableLayoutPanel1.RowStyles.Add( new RowStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel1.Size = new Size( 311, 489 );
			tableLayoutPanel1.TabIndex = 2;
			// 
			// Pen1Pix_radioButton
			// 
			Pen1Pix_radioButton.AutoSize = true;
			Pen1Pix_radioButton.Location = new Point( 3, 3 );
			Pen1Pix_radioButton.Name = "Pen1Pix_radioButton";
			Pen1Pix_radioButton.Size = new Size( 64, 19 );
			Pen1Pix_radioButton.TabIndex = 0;
			Pen1Pix_radioButton.TabStop = true;
			Pen1Pix_radioButton.Text = "1[Pixel]";
			Pen1Pix_radioButton.UseVisualStyleBackColor = true;
			Pen1Pix_radioButton.CheckedChanged +=  Pen1Pix_radioButton_CheckedChanged ;
			// 
			// Pen2x2_radioButton
			// 
			Pen2x2_radioButton.AutoSize = true;
			Pen2x2_radioButton.Location = new Point( 3, 28 );
			Pen2x2_radioButton.Name = "Pen2x2_radioButton";
			Pen2x2_radioButton.Size = new Size( 43, 19 );
			Pen2x2_radioButton.TabIndex = 1;
			Pen2x2_radioButton.TabStop = true;
			Pen2x2_radioButton.Text = "2x2";
			Pen2x2_radioButton.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			label1.Anchor =  AnchorStyles.Left  |  AnchorStyles.Right ;
			label1.AutoSize = true;
			label1.BackColor = Color.FromArgb( 255, 255, 192 );
			label1.BorderStyle = BorderStyle.FixedSingle;
			label1.Location = new Point( 0, 58 );
			label1.Margin = new Padding( 0, 8, 0, 0 );
			label1.Name = "label1";
			label1.Size = new Size( 311, 32 );
			label1.TabIndex = 2;
			label1.Text = "Start with SHIFT key down, the directiion is constrained to horizontal or vertical.";
			// 
			// label2
			// 
			label2.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
			label2.AutoSize = true;
			label2.BackColor = Color.FromArgb( 255, 255, 192 );
			label2.BorderStyle = BorderStyle.FixedSingle;
			label2.Location = new Point( 0, 90 );
			label2.Margin = new Padding( 0 );
			label2.Name = "label2";
			label2.Size = new Size( 311, 17 );
			label2.TabIndex = 2;
			label2.Text = "CTRL + Click acts as color picker.";
			// 
			// Line_tabPage
			// 
			Line_tabPage.Controls.Add( tableLayoutPanel2 );
			Line_tabPage.ImageIndex = 1;
			Line_tabPage.Location = new Point( 4, 4 );
			Line_tabPage.Name = "Line_tabPage";
			Line_tabPage.Padding = new Padding( 3 );
			Line_tabPage.Size = new Size( 317, 495 );
			Line_tabPage.TabIndex = 1;
			Line_tabPage.Text = "Line";
			Line_tabPage.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 2;
			tableLayoutPanel2.ColumnStyles.Add( new ColumnStyle() );
			tableLayoutPanel2.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel2.Controls.Add( label3, 0, 3 );
			tableLayoutPanel2.Controls.Add( label4, 0, 4 );
			tableLayoutPanel2.Controls.Add( RoundEnd_checkBox, 0, 0 );
			tableLayoutPanel2.Controls.Add( label5, 0, 1 );
			tableLayoutPanel2.Controls.Add( LineWeight_numericUpDown, 0, 2 );
			tableLayoutPanel2.Controls.Add( ResetLineWeight_button, 1, 2 );
			tableLayoutPanel2.Dock = DockStyle.Fill;
			tableLayoutPanel2.Location = new Point( 3, 3 );
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 6;
			tableLayoutPanel2.RowStyles.Add( new RowStyle() );
			tableLayoutPanel2.RowStyles.Add( new RowStyle() );
			tableLayoutPanel2.RowStyles.Add( new RowStyle() );
			tableLayoutPanel2.RowStyles.Add( new RowStyle() );
			tableLayoutPanel2.RowStyles.Add( new RowStyle() );
			tableLayoutPanel2.RowStyles.Add( new RowStyle( SizeType.Absolute, 20F ) );
			tableLayoutPanel2.Size = new Size( 311, 489 );
			tableLayoutPanel2.TabIndex = 3;
			// 
			// label3
			// 
			label3.Anchor =  AnchorStyles.Left  |  AnchorStyles.Right ;
			label3.AutoSize = true;
			label3.BackColor = Color.FromArgb( 255, 255, 192 );
			label3.BorderStyle = BorderStyle.FixedSingle;
			tableLayoutPanel2.SetColumnSpan( label3, 2 );
			label3.Location = new Point( 0, 87 );
			label3.Margin = new Padding( 0, 8, 0, 0 );
			label3.Name = "label3";
			label3.Size = new Size( 311, 32 );
			label3.TabIndex = 2;
			label3.Text = "Pressing SHIFT key constrains the direction to multiples of 45 degrees.";
			// 
			// label4
			// 
			label4.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
			label4.AutoSize = true;
			label4.BackColor = Color.FromArgb( 255, 255, 192 );
			label4.BorderStyle = BorderStyle.FixedSingle;
			tableLayoutPanel2.SetColumnSpan( label4, 2 );
			label4.Location = new Point( 0, 119 );
			label4.Margin = new Padding( 0 );
			label4.Name = "label4";
			label4.Size = new Size( 311, 17 );
			label4.TabIndex = 2;
			label4.Text = "CTRL + Click acts as color picker.";
			// 
			// RoundEnd_checkBox
			// 
			RoundEnd_checkBox.AutoSize = true;
			tableLayoutPanel2.SetColumnSpan( RoundEnd_checkBox, 2 );
			RoundEnd_checkBox.Location = new Point( 3, 3 );
			RoundEnd_checkBox.Name = "RoundEnd_checkBox";
			RoundEnd_checkBox.Size = new Size( 109, 19 );
			RoundEnd_checkBox.TabIndex = 3;
			RoundEnd_checkBox.Text = "Round the ends";
			RoundEnd_checkBox.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			label5.AutoSize = true;
			tableLayoutPanel2.SetColumnSpan( label5, 2 );
			label5.Location = new Point( 3, 33 );
			label5.Margin = new Padding( 3, 8, 3, 0 );
			label5.Name = "label5";
			label5.Size = new Size( 76, 15 );
			label5.TabIndex = 4;
			label5.Text = "Line Weight :";
			// 
			// LineWeight_numericUpDown
			// 
			LineWeight_numericUpDown.Anchor = AnchorStyles.Left;
			LineWeight_numericUpDown.AutoSize = true;
			LineWeight_numericUpDown.Location = new Point( 3, 52 );
			LineWeight_numericUpDown.Minimum = new decimal( new int[] { 1, 0, 0, 0 } );
			LineWeight_numericUpDown.Name = "LineWeight_numericUpDown";
			LineWeight_numericUpDown.Size = new Size( 41, 23 );
			LineWeight_numericUpDown.TabIndex = 5;
			LineWeight_numericUpDown.TextAlign = HorizontalAlignment.Right;
			LineWeight_numericUpDown.Value = new decimal( new int[] { 1, 0, 0, 0 } );
			// 
			// ResetLineWeight_button
			// 
			ResetLineWeight_button.Anchor = AnchorStyles.Left;
			ResetLineWeight_button.AutoSize = true;
			ResetLineWeight_button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			ResetLineWeight_button.Location = new Point( 50, 51 );
			ResetLineWeight_button.Name = "ResetLineWeight_button";
			ResetLineWeight_button.Size = new Size( 68, 25 );
			ResetLineWeight_button.TabIndex = 6;
			ResetLineWeight_button.Text = "Reset to 1";
			ResetLineWeight_button.UseVisualStyleBackColor = true;
			ResetLineWeight_button.Click +=  ResetLineWeight_button_Click ;
			// 
			// Select_tabPage
			// 
			Select_tabPage.Controls.Add( tableLayoutPanel3 );
			Select_tabPage.ImageIndex = 2;
			Select_tabPage.Location = new Point( 4, 4 );
			Select_tabPage.Name = "Select_tabPage";
			Select_tabPage.Size = new Size( 317, 495 );
			Select_tabPage.TabIndex = 2;
			Select_tabPage.Text = "Select";
			Select_tabPage.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel3
			// 
			tableLayoutPanel3.ColumnCount = 2;
			tableLayoutPanel3.ColumnStyles.Add( new ColumnStyle() );
			tableLayoutPanel3.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel3.Controls.Add( RectSelMode_radioButton, 1, 0 );
			tableLayoutPanel3.Controls.Add( RectSelMode_pictureBox, 0, 0 );
			tableLayoutPanel3.Controls.Add( FreeFormSelMode_pictureBox, 0, 1 );
			tableLayoutPanel3.Controls.Add( label6, 0, 4 );
			tableLayoutPanel3.Controls.Add( TransparentMode_pictureBox, 0, 3 );
			tableLayoutPanel3.Controls.Add( FreeFormSelMode_radioButton, 1, 1 );
			tableLayoutPanel3.Controls.Add( TransBackColor_checkBox, 1, 3 );
			tableLayoutPanel3.Controls.Add( pictureBox4, 0, 2 );
			tableLayoutPanel3.Dock = DockStyle.Fill;
			tableLayoutPanel3.Location = new Point( 0, 0 );
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 4;
			tableLayoutPanel3.RowStyles.Add( new RowStyle() );
			tableLayoutPanel3.RowStyles.Add( new RowStyle() );
			tableLayoutPanel3.RowStyles.Add( new RowStyle() );
			tableLayoutPanel3.RowStyles.Add( new RowStyle() );
			tableLayoutPanel3.RowStyles.Add( new RowStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel3.Size = new Size( 317, 495 );
			tableLayoutPanel3.TabIndex = 3;
			// 
			// RectSelMode_radioButton
			// 
			RectSelMode_radioButton.Anchor = AnchorStyles.Left;
			RectSelMode_radioButton.AutoSize = true;
			RectSelMode_radioButton.Location = new Point( 33, 5 );
			RectSelMode_radioButton.Name = "RectSelMode_radioButton";
			RectSelMode_radioButton.Size = new Size( 77, 19 );
			RectSelMode_radioButton.TabIndex = 0;
			RectSelMode_radioButton.TabStop = true;
			RectSelMode_radioButton.Text = "Rectangle";
			RectSelMode_radioButton.TextImageRelation = TextImageRelation.ImageBeforeText;
			RectSelMode_radioButton.UseVisualStyleBackColor = true;
			RectSelMode_radioButton.CheckedChanged +=  SelectToolMode_radioButton_CheckedChanged ;
			// 
			// RectSelMode_pictureBox
			// 
			RectSelMode_pictureBox.Anchor = AnchorStyles.Left;
			RectSelMode_pictureBox.ErrorImage = null;
			RectSelMode_pictureBox.Image = Properties.Resources.RectSel;
			RectSelMode_pictureBox.Location = new Point( 3, 3 );
			RectSelMode_pictureBox.Name = "RectSelMode_pictureBox";
			RectSelMode_pictureBox.Size = new Size( 24, 24 );
			RectSelMode_pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
			RectSelMode_pictureBox.TabIndex = 3;
			RectSelMode_pictureBox.TabStop = false;
			RectSelMode_pictureBox.Click +=  RectSelMode_pictureBox_Click ;
			// 
			// FreeFormSelMode_pictureBox
			// 
			FreeFormSelMode_pictureBox.Anchor = AnchorStyles.Left;
			FreeFormSelMode_pictureBox.ErrorImage = null;
			FreeFormSelMode_pictureBox.Image = Properties.Resources.FreeSel;
			FreeFormSelMode_pictureBox.Location = new Point( 3, 33 );
			FreeFormSelMode_pictureBox.Name = "FreeFormSelMode_pictureBox";
			FreeFormSelMode_pictureBox.Size = new Size( 24, 24 );
			FreeFormSelMode_pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
			FreeFormSelMode_pictureBox.TabIndex = 3;
			FreeFormSelMode_pictureBox.TabStop = false;
			FreeFormSelMode_pictureBox.Click +=  FreeFormSelMode_pictureBox_Click ;
			// 
			// label6
			// 
			label6.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
			label6.AutoSize = true;
			label6.BackColor = Color.FromArgb( 255, 255, 192 );
			label6.BorderStyle = BorderStyle.FixedSingle;
			tableLayoutPanel3.SetColumnSpan( label6, 2 );
			label6.Location = new Point( 0, 118 );
			label6.Margin = new Padding( 0, 8, 0, 0 );
			label6.Name = "label6";
			label6.Size = new Size( 317, 17 );
			label6.TabIndex = 2;
			label6.Text = "Start dragging the selected area with CTRL makes copy.";
			// 
			// TransparentMode_pictureBox
			// 
			TransparentMode_pictureBox.Anchor = AnchorStyles.Left;
			TransparentMode_pictureBox.ErrorImage = null;
			TransparentMode_pictureBox.Image = Properties.Resources.TransBack;
			TransparentMode_pictureBox.Location = new Point( 3, 83 );
			TransparentMode_pictureBox.Name = "TransparentMode_pictureBox";
			TransparentMode_pictureBox.Size = new Size( 24, 24 );
			TransparentMode_pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
			TransparentMode_pictureBox.TabIndex = 3;
			TransparentMode_pictureBox.TabStop = false;
			TransparentMode_pictureBox.Click +=  TransparentMode_pictureBox_Click ;
			// 
			// FreeFormSelMode_radioButton
			// 
			FreeFormSelMode_radioButton.Anchor = AnchorStyles.Left;
			FreeFormSelMode_radioButton.AutoSize = true;
			FreeFormSelMode_radioButton.Location = new Point( 33, 35 );
			FreeFormSelMode_radioButton.Name = "FreeFormSelMode_radioButton";
			FreeFormSelMode_radioButton.Size = new Size( 78, 19 );
			FreeFormSelMode_radioButton.TabIndex = 1;
			FreeFormSelMode_radioButton.TabStop = true;
			FreeFormSelMode_radioButton.Text = "Free-Form";
			FreeFormSelMode_radioButton.UseVisualStyleBackColor = true;
			// 
			// TransBackColor_checkBox
			// 
			TransBackColor_checkBox.Anchor = AnchorStyles.Left;
			TransBackColor_checkBox.AutoSize = true;
			TransBackColor_checkBox.Location = new Point( 33, 85 );
			TransBackColor_checkBox.Name = "TransBackColor_checkBox";
			TransBackColor_checkBox.Size = new Size( 151, 19 );
			TransBackColor_checkBox.TabIndex = 4;
			TransBackColor_checkBox.Text = "Transparent Right-Color";
			TransBackColor_checkBox.UseVisualStyleBackColor = true;
			TransBackColor_checkBox.CheckedChanged +=  TransBackColor_checkBox_CheckedChanged ;
			// 
			// pictureBox4
			// 
			pictureBox4.Anchor =  AnchorStyles.Left  |  AnchorStyles.Right ;
			pictureBox4.BorderStyle = BorderStyle.Fixed3D;
			tableLayoutPanel3.SetColumnSpan( pictureBox4, 2 );
			pictureBox4.Location = new Point( 8, 68 );
			pictureBox4.Margin = new Padding( 8 );
			pictureBox4.Name = "pictureBox4";
			pictureBox4.Size = new Size( 301, 4 );
			pictureBox4.TabIndex = 5;
			pictureBox4.TabStop = false;
			// 
			// Eraser_tabPage
			// 
			Eraser_tabPage.Controls.Add( tableLayoutPanel4 );
			Eraser_tabPage.ImageIndex = 3;
			Eraser_tabPage.Location = new Point( 4, 4 );
			Eraser_tabPage.Name = "Eraser_tabPage";
			Eraser_tabPage.Size = new Size( 317, 495 );
			Eraser_tabPage.TabIndex = 3;
			Eraser_tabPage.Text = "Eraser";
			Eraser_tabPage.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel4
			// 
			tableLayoutPanel4.ColumnCount = 1;
			tableLayoutPanel4.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel4.Controls.Add( Eraser3x3_radioButton, 0, 0 );
			tableLayoutPanel4.Controls.Add( Eraser5x5_radioButton, 0, 1 );
			tableLayoutPanel4.Controls.Add( label7, 0, 5 );
			tableLayoutPanel4.Controls.Add( label8, 0, 6 );
			tableLayoutPanel4.Controls.Add( Eraser7x7_radioButton, 0, 2 );
			tableLayoutPanel4.Controls.Add( Eraser9x9_radioButton, 0, 3 );
			tableLayoutPanel4.Controls.Add( Eraser11x11_radioButton, 0, 4 );
			tableLayoutPanel4.Dock = DockStyle.Fill;
			tableLayoutPanel4.Location = new Point( 0, 0 );
			tableLayoutPanel4.Name = "tableLayoutPanel4";
			tableLayoutPanel4.RowCount = 7;
			tableLayoutPanel4.RowStyles.Add( new RowStyle() );
			tableLayoutPanel4.RowStyles.Add( new RowStyle() );
			tableLayoutPanel4.RowStyles.Add( new RowStyle() );
			tableLayoutPanel4.RowStyles.Add( new RowStyle() );
			tableLayoutPanel4.RowStyles.Add( new RowStyle() );
			tableLayoutPanel4.RowStyles.Add( new RowStyle() );
			tableLayoutPanel4.RowStyles.Add( new RowStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel4.Size = new Size( 317, 495 );
			tableLayoutPanel4.TabIndex = 3;
			// 
			// Eraser3x3_radioButton
			// 
			Eraser3x3_radioButton.AutoSize = true;
			Eraser3x3_radioButton.Location = new Point( 3, 3 );
			Eraser3x3_radioButton.Name = "Eraser3x3_radioButton";
			Eraser3x3_radioButton.Size = new Size( 43, 19 );
			Eraser3x3_radioButton.TabIndex = 0;
			Eraser3x3_radioButton.TabStop = true;
			Eraser3x3_radioButton.Text = "3x3";
			Eraser3x3_radioButton.UseVisualStyleBackColor = true;
			Eraser3x3_radioButton.CheckedChanged +=  EraserSize_radioButton_CheckedChanged ;
			// 
			// Eraser5x5_radioButton
			// 
			Eraser5x5_radioButton.AutoSize = true;
			Eraser5x5_radioButton.Location = new Point( 3, 28 );
			Eraser5x5_radioButton.Name = "Eraser5x5_radioButton";
			Eraser5x5_radioButton.Size = new Size( 43, 19 );
			Eraser5x5_radioButton.TabIndex = 1;
			Eraser5x5_radioButton.TabStop = true;
			Eraser5x5_radioButton.Text = "5x5";
			Eraser5x5_radioButton.UseVisualStyleBackColor = true;
			Eraser5x5_radioButton.CheckedChanged +=  EraserSize_radioButton_CheckedChanged ;
			// 
			// label7
			// 
			label7.Anchor =  AnchorStyles.Left  |  AnchorStyles.Right ;
			label7.AutoSize = true;
			label7.BackColor = Color.FromArgb( 255, 255, 192 );
			label7.BorderStyle = BorderStyle.FixedSingle;
			label7.Location = new Point( 0, 133 );
			label7.Margin = new Padding( 0, 8, 0, 0 );
			label7.Name = "label7";
			label7.Size = new Size( 317, 17 );
			label7.TabIndex = 2;
			label7.Text = "Right button targets only the left color area.";
			// 
			// label8
			// 
			label8.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
			label8.AutoSize = true;
			label8.BackColor = Color.FromArgb( 255, 255, 192 );
			label8.BorderStyle = BorderStyle.FixedSingle;
			label8.Location = new Point( 0, 150 );
			label8.Margin = new Padding( 0 );
			label8.Name = "label8";
			label8.Size = new Size( 317, 17 );
			label8.TabIndex = 2;
			label8.Text = "CTRL + Click acts as color picker.";
			// 
			// Eraser7x7_radioButton
			// 
			Eraser7x7_radioButton.AutoSize = true;
			Eraser7x7_radioButton.Location = new Point( 3, 53 );
			Eraser7x7_radioButton.Name = "Eraser7x7_radioButton";
			Eraser7x7_radioButton.Size = new Size( 43, 19 );
			Eraser7x7_radioButton.TabIndex = 1;
			Eraser7x7_radioButton.TabStop = true;
			Eraser7x7_radioButton.Text = "7x7";
			Eraser7x7_radioButton.UseVisualStyleBackColor = true;
			Eraser7x7_radioButton.CheckedChanged +=  EraserSize_radioButton_CheckedChanged ;
			// 
			// Eraser9x9_radioButton
			// 
			Eraser9x9_radioButton.AutoSize = true;
			Eraser9x9_radioButton.Location = new Point( 3, 78 );
			Eraser9x9_radioButton.Name = "Eraser9x9_radioButton";
			Eraser9x9_radioButton.Size = new Size( 43, 19 );
			Eraser9x9_radioButton.TabIndex = 1;
			Eraser9x9_radioButton.TabStop = true;
			Eraser9x9_radioButton.Text = "9x9";
			Eraser9x9_radioButton.UseVisualStyleBackColor = true;
			Eraser9x9_radioButton.CheckedChanged +=  EraserSize_radioButton_CheckedChanged ;
			// 
			// Eraser11x11_radioButton
			// 
			Eraser11x11_radioButton.AutoSize = true;
			Eraser11x11_radioButton.Location = new Point( 3, 103 );
			Eraser11x11_radioButton.Name = "Eraser11x11_radioButton";
			Eraser11x11_radioButton.Size = new Size( 55, 19 );
			Eraser11x11_radioButton.TabIndex = 1;
			Eraser11x11_radioButton.TabStop = true;
			Eraser11x11_radioButton.Text = "11x11";
			Eraser11x11_radioButton.UseVisualStyleBackColor = true;
			Eraser11x11_radioButton.CheckedChanged +=  EraserSize_radioButton_CheckedChanged ;
			// 
			// Fill_tabPage
			// 
			Fill_tabPage.Controls.Add( tableLayoutPanel5 );
			Fill_tabPage.ImageIndex = 4;
			Fill_tabPage.Location = new Point( 4, 4 );
			Fill_tabPage.Name = "Fill_tabPage";
			Fill_tabPage.Size = new Size( 317, 495 );
			Fill_tabPage.TabIndex = 4;
			Fill_tabPage.Text = "Fill";
			Fill_tabPage.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel5
			// 
			tableLayoutPanel5.ColumnCount = 1;
			tableLayoutPanel5.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel5.Controls.Add( label10, 0, 0 );
			tableLayoutPanel5.Dock = DockStyle.Fill;
			tableLayoutPanel5.Location = new Point( 0, 0 );
			tableLayoutPanel5.Name = "tableLayoutPanel5";
			tableLayoutPanel5.RowCount = 1;
			tableLayoutPanel5.RowStyles.Add( new RowStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel5.RowStyles.Add( new RowStyle( SizeType.Absolute, 20F ) );
			tableLayoutPanel5.RowStyles.Add( new RowStyle( SizeType.Absolute, 20F ) );
			tableLayoutPanel5.RowStyles.Add( new RowStyle( SizeType.Absolute, 20F ) );
			tableLayoutPanel5.Size = new Size( 317, 495 );
			tableLayoutPanel5.TabIndex = 3;
			// 
			// label10
			// 
			label10.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
			label10.AutoSize = true;
			label10.BackColor = Color.FromArgb( 255, 255, 192 );
			label10.BorderStyle = BorderStyle.FixedSingle;
			label10.Location = new Point( 0, 0 );
			label10.Margin = new Padding( 0 );
			label10.Name = "label10";
			label10.Size = new Size( 317, 17 );
			label10.TabIndex = 2;
			label10.Text = "CTRL + Click acts as color picker.";
			// 
			// Icon_imageList
			// 
			Icon_imageList.ColorDepth = ColorDepth.Depth8Bit;
			Icon_imageList.ImageStream = (ImageListStreamer)resources.GetObject( "Icon_imageList.ImageStream" );
			Icon_imageList.TransparentColor = Color.Transparent;
			Icon_imageList.Images.SetKeyName( 0, "Pen.PNG" );
			Icon_imageList.Images.SetKeyName( 1, "Line.PNG" );
			Icon_imageList.Images.SetKeyName( 2, "Select.PNG" );
			Icon_imageList.Images.SetKeyName( 3, "Eraser.png" );
			Icon_imageList.Images.SetKeyName( 4, "Fill.png" );
			// 
			// ToolsUC
			// 
			AutoScaleDimensions = new SizeF( 96F, 96F );
			AutoScaleMode = AutoScaleMode.Dpi;
			Controls.Add( Main_tabControl );
			Name = "ToolsUC";
			Size = new Size( 360, 503 );
			Load +=  ToolsUC_Load ;
			Main_tabControl.ResumeLayout( false );
			Pen_tabPage.ResumeLayout( false );
			tableLayoutPanel1.ResumeLayout( false );
			tableLayoutPanel1.PerformLayout();
			Line_tabPage.ResumeLayout( false );
			tableLayoutPanel2.ResumeLayout( false );
			tableLayoutPanel2.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)LineWeight_numericUpDown ).EndInit();
			Select_tabPage.ResumeLayout( false );
			tableLayoutPanel3.ResumeLayout( false );
			tableLayoutPanel3.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)RectSelMode_pictureBox ).EndInit();
			( (System.ComponentModel.ISupportInitialize)FreeFormSelMode_pictureBox ).EndInit();
			( (System.ComponentModel.ISupportInitialize)TransparentMode_pictureBox ).EndInit();
			( (System.ComponentModel.ISupportInitialize)pictureBox4 ).EndInit();
			Eraser_tabPage.ResumeLayout( false );
			tableLayoutPanel4.ResumeLayout( false );
			tableLayoutPanel4.PerformLayout();
			Fill_tabPage.ResumeLayout( false );
			tableLayoutPanel5.ResumeLayout( false );
			tableLayoutPanel5.PerformLayout();
			ResumeLayout( false );
		}

		#endregion

		private TabControl Main_tabControl;
		private TabPage Pen_tabPage;
		private TabPage Line_tabPage;
		private ImageList Icon_imageList;
		private TabPage Select_tabPage;
		private TabPage Eraser_tabPage;
		private TabPage Fill_tabPage;
		private TableLayoutPanel tableLayoutPanel1;
		private RadioButton Pen1Pix_radioButton;
		private RadioButton Pen2x2_radioButton;
		private Label label1;
		private Label label2;
		private TableLayoutPanel tableLayoutPanel2;
		private Label label3;
		private Label label4;
		private CheckBox RoundEnd_checkBox;
		private Label label5;
		private NumericUpDown LineWeight_numericUpDown;
		private Button ResetLineWeight_button;
		private TableLayoutPanel tableLayoutPanel3;
		private RadioButton RectSelMode_radioButton;
		private RadioButton FreeFormSelMode_radioButton;
		private Label label6;
		private PictureBox RectSelMode_pictureBox;
		private PictureBox FreeFormSelMode_pictureBox;
		private PictureBox TransparentMode_pictureBox;
		private CheckBox TransBackColor_checkBox;
		private PictureBox pictureBox4;
		private TableLayoutPanel tableLayoutPanel4;
		private RadioButton Eraser3x3_radioButton;
		private RadioButton Eraser5x5_radioButton;
		private Label label7;
		private Label label8;
		private RadioButton Eraser7x7_radioButton;
		private RadioButton Eraser9x9_radioButton;
		private RadioButton Eraser11x11_radioButton;
		private TableLayoutPanel tableLayoutPanel5;
		private Label label10;
	}
}
