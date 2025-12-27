
namespace PaintDotCat
{
	partial class SizeDlg
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			The_tableLayoutPanel = new TableLayoutPanel();
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			Width_numericUpDown = new NumericUpDown();
			Height_numericUpDown = new NumericUpDown();
			OK_button = new Button();
			Cancel_button = new Button();
			SizeSel_comboBox = new ComboBox();
			The_tableLayoutPanel.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)Width_numericUpDown ).BeginInit();
			( (System.ComponentModel.ISupportInitialize)Height_numericUpDown ).BeginInit();
			SuspendLayout();
			// 
			// The_tableLayoutPanel
			// 
			The_tableLayoutPanel.ColumnCount = 2;
			The_tableLayoutPanel.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 50F ) );
			The_tableLayoutPanel.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 50F ) );
			The_tableLayoutPanel.Controls.Add( label1, 0, 0 );
			The_tableLayoutPanel.Controls.Add( label2, 0, 2 );
			The_tableLayoutPanel.Controls.Add( label3, 1, 2 );
			The_tableLayoutPanel.Controls.Add( Width_numericUpDown, 0, 3 );
			The_tableLayoutPanel.Controls.Add( Height_numericUpDown, 1, 3 );
			The_tableLayoutPanel.Controls.Add( OK_button, 1, 4 );
			The_tableLayoutPanel.Controls.Add( Cancel_button, 0, 4 );
			The_tableLayoutPanel.Controls.Add( SizeSel_comboBox, 0, 1 );
			The_tableLayoutPanel.Dock = DockStyle.Fill;
			The_tableLayoutPanel.Location = new Point( 0, 0 );
			The_tableLayoutPanel.Name = "The_tableLayoutPanel";
			The_tableLayoutPanel.Padding = new Padding( 7, 8, 7, 8 );
			The_tableLayoutPanel.RowCount = 5;
			The_tableLayoutPanel.RowStyles.Add( new RowStyle() );
			The_tableLayoutPanel.RowStyles.Add( new RowStyle() );
			The_tableLayoutPanel.RowStyles.Add( new RowStyle() );
			The_tableLayoutPanel.RowStyles.Add( new RowStyle( SizeType.Percent, 100F ) );
			The_tableLayoutPanel.RowStyles.Add( new RowStyle() );
			The_tableLayoutPanel.Size = new Size( 304, 197 );
			The_tableLayoutPanel.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			The_tableLayoutPanel.SetColumnSpan( label1, 2 );
			label1.Font = new Font( "MS UI Gothic", 9F,  FontStyle.Bold  |  FontStyle.Underline , GraphicsUnit.Point, 128 );
			label1.Location = new Point( 10, 8 );
			label1.Margin = new Padding( 3, 0, 3, 12 );
			label1.Name = "label1";
			label1.Size = new Size( 78, 12 );
			label1.TabIndex = 0;
			label1.Text = "Image Size :";
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Top;
			label2.AutoSize = true;
			label2.Location = new Point( 60, 70 );
			label2.Name = "label2";
			label2.Size = new Size( 39, 15 );
			label2.TabIndex = 1;
			label2.Text = "Width";
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Top;
			label3.AutoSize = true;
			label3.Location = new Point( 203, 70 );
			label3.Name = "label3";
			label3.Size = new Size( 43, 15 );
			label3.TabIndex = 2;
			label3.Text = "Height";
			// 
			// Width_numericUpDown
			// 
			Width_numericUpDown.Anchor = AnchorStyles.Top;
			Width_numericUpDown.Location = new Point( 27, 88 );
			Width_numericUpDown.Maximum = new decimal( new int[] { 65535, 0, 0, 0 } );
			Width_numericUpDown.Minimum = new decimal( new int[] { 1, 0, 0, 0 } );
			Width_numericUpDown.Name = "Width_numericUpDown";
			Width_numericUpDown.Size = new Size( 105, 23 );
			Width_numericUpDown.TabIndex = 3;
			Width_numericUpDown.TextAlign = HorizontalAlignment.Right;
			Width_numericUpDown.Value = new decimal( new int[] { 1, 0, 0, 0 } );
			// 
			// Height_numericUpDown
			// 
			Height_numericUpDown.Anchor = AnchorStyles.Top;
			Height_numericUpDown.Location = new Point( 172, 88 );
			Height_numericUpDown.Maximum = new decimal( new int[] { 65535, 0, 0, 0 } );
			Height_numericUpDown.Minimum = new decimal( new int[] { 1, 0, 0, 0 } );
			Height_numericUpDown.Name = "Height_numericUpDown";
			Height_numericUpDown.Size = new Size( 105, 23 );
			Height_numericUpDown.TabIndex = 3;
			Height_numericUpDown.TextAlign = HorizontalAlignment.Right;
			Height_numericUpDown.Value = new decimal( new int[] { 1, 0, 0, 0 } );
			// 
			// OK_button
			// 
			OK_button.Anchor =  AnchorStyles.Bottom  |  AnchorStyles.Right ;
			OK_button.Location = new Point( 189, 146 );
			OK_button.Name = "OK_button";
			OK_button.Size = new Size( 105, 40 );
			OK_button.TabIndex = 4;
			OK_button.Text = "OK";
			OK_button.UseVisualStyleBackColor = true;
			OK_button.Click +=  OK_button_Click ;
			// 
			// Cancel_button
			// 
			Cancel_button.Anchor =  AnchorStyles.Bottom  |  AnchorStyles.Left ;
			Cancel_button.DialogResult = DialogResult.Cancel;
			Cancel_button.Location = new Point( 10, 146 );
			Cancel_button.Name = "Cancel_button";
			Cancel_button.Size = new Size( 105, 40 );
			Cancel_button.TabIndex = 5;
			Cancel_button.Text = "Cancel";
			Cancel_button.UseVisualStyleBackColor = true;
			Cancel_button.Click +=  Cancel_button_Click ;
			// 
			// SizeSel_comboBox
			// 
			SizeSel_comboBox.Anchor = AnchorStyles.Top;
			SizeSel_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			SizeSel_comboBox.FormattingEnabled = true;
			SizeSel_comboBox.Location = new Point( 26, 35 );
			SizeSel_comboBox.Margin = new Padding( 3, 3, 3, 12 );
			SizeSel_comboBox.Name = "SizeSel_comboBox";
			SizeSel_comboBox.Size = new Size( 106, 23 );
			SizeSel_comboBox.TabIndex = 6;
			SizeSel_comboBox.SelectedIndexChanged +=  SizeSel_comboBox_SelectedIndexChanged ;
			// 
			// SizeDlg
			// 
			AcceptButton = OK_button;
			AutoScaleDimensions = new SizeF( 96F, 96F );
			AutoScaleMode = AutoScaleMode.Dpi;
			CancelButton = Cancel_button;
			ClientSize = new Size( 304, 197 );
			Controls.Add( The_tableLayoutPanel );
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "SizeDlg";
			ShowIcon = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "SizeDlg";
			Load +=  SizeDlg_Load ;
			The_tableLayoutPanel.ResumeLayout( false );
			The_tableLayoutPanel.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)Width_numericUpDown ).EndInit();
			( (System.ComponentModel.ISupportInitialize)Height_numericUpDown ).EndInit();
			ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel The_tableLayoutPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown Width_numericUpDown;
		private System.Windows.Forms.NumericUpDown Height_numericUpDown;
		private System.Windows.Forms.Button OK_button;
		private System.Windows.Forms.Button Cancel_button;
		private System.Windows.Forms.ComboBox SizeSel_comboBox;
	}
}