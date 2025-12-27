
namespace PaintDotCat
{
	partial class GridSizeDlg
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
			X_numericUpDown = new NumericUpDown();
			Cancel_button = new Button();
			GridCol_label = new Label();
			Y_numericUpDown = new NumericUpDown();
			label3 = new Label();
			OK_button = new Button();
			The_tableLayoutPanel.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)X_numericUpDown ).BeginInit();
			( (System.ComponentModel.ISupportInitialize)Y_numericUpDown ).BeginInit();
			SuspendLayout();
			// 
			// The_tableLayoutPanel
			// 
			The_tableLayoutPanel.ColumnCount = 3;
			The_tableLayoutPanel.ColumnStyles.Add( new ColumnStyle() );
			The_tableLayoutPanel.ColumnStyles.Add( new ColumnStyle() );
			The_tableLayoutPanel.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100F ) );
			The_tableLayoutPanel.Controls.Add( label1, 0, 0 );
			The_tableLayoutPanel.Controls.Add( label2, 0, 1 );
			The_tableLayoutPanel.Controls.Add( X_numericUpDown, 0, 2 );
			The_tableLayoutPanel.Controls.Add( Cancel_button, 0, 5 );
			The_tableLayoutPanel.Controls.Add( GridCol_label, 0, 3 );
			The_tableLayoutPanel.Controls.Add( Y_numericUpDown, 1, 2 );
			The_tableLayoutPanel.Controls.Add( label3, 1, 1 );
			The_tableLayoutPanel.Controls.Add( OK_button, 1, 5 );
			The_tableLayoutPanel.Dock = DockStyle.Fill;
			The_tableLayoutPanel.Location = new Point( 0, 0 );
			The_tableLayoutPanel.Name = "The_tableLayoutPanel";
			The_tableLayoutPanel.Padding = new Padding( 7, 8, 7, 8 );
			The_tableLayoutPanel.RowCount = 6;
			The_tableLayoutPanel.RowStyles.Add( new RowStyle() );
			The_tableLayoutPanel.RowStyles.Add( new RowStyle() );
			The_tableLayoutPanel.RowStyles.Add( new RowStyle() );
			The_tableLayoutPanel.RowStyles.Add( new RowStyle() );
			The_tableLayoutPanel.RowStyles.Add( new RowStyle( SizeType.Percent, 100F ) );
			The_tableLayoutPanel.RowStyles.Add( new RowStyle() );
			The_tableLayoutPanel.Size = new Size( 313, 200 );
			The_tableLayoutPanel.TabIndex = 1;
			// 
			// label1
			// 
			label1.AutoSize = true;
			The_tableLayoutPanel.SetColumnSpan( label1, 3 );
			label1.Font = new Font( "MS UI Gothic", 9F,  FontStyle.Bold  |  FontStyle.Underline , GraphicsUnit.Point, 128 );
			label1.Location = new Point( 10, 8 );
			label1.Margin = new Padding( 3, 0, 3, 12 );
			label1.Name = "label1";
			label1.Size = new Size( 89, 12 );
			label1.TabIndex = 0;
			label1.Text = "Grid Interval :";
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Top;
			label2.AutoSize = true;
			label2.Location = new Point( 55, 32 );
			label2.Name = "label2";
			label2.Size = new Size( 14, 15 );
			label2.TabIndex = 1;
			label2.Text = "X";
			// 
			// X_numericUpDown
			// 
			X_numericUpDown.Anchor = AnchorStyles.Top;
			X_numericUpDown.AutoSize = true;
			X_numericUpDown.Location = new Point( 36, 50 );
			X_numericUpDown.Maximum = new decimal( new int[] { 65535, 0, 0, 0 } );
			X_numericUpDown.Minimum = new decimal( new int[] { 1, 0, 0, 0 } );
			X_numericUpDown.Name = "X_numericUpDown";
			X_numericUpDown.Size = new Size( 53, 23 );
			X_numericUpDown.TabIndex = 3;
			X_numericUpDown.TextAlign = HorizontalAlignment.Right;
			X_numericUpDown.Value = new decimal( new int[] { 1, 0, 0, 0 } );
			// 
			// Cancel_button
			// 
			Cancel_button.Anchor =  AnchorStyles.Bottom  |  AnchorStyles.Left ;
			Cancel_button.DialogResult = DialogResult.Cancel;
			Cancel_button.Location = new Point( 10, 149 );
			Cancel_button.Name = "Cancel_button";
			Cancel_button.Size = new Size( 105, 40 );
			Cancel_button.TabIndex = 5;
			Cancel_button.Text = "Cancel";
			Cancel_button.UseVisualStyleBackColor = true;
			Cancel_button.Click +=  Cancel_button_Click ;
			// 
			// GridCol_label
			// 
			GridCol_label.AutoSize = true;
			GridCol_label.BorderStyle = BorderStyle.FixedSingle;
			The_tableLayoutPanel.SetColumnSpan( GridCol_label, 3 );
			GridCol_label.Font = new Font( "MS UI Gothic", 9F,  FontStyle.Bold  |  FontStyle.Underline , GraphicsUnit.Point, 128 );
			GridCol_label.Location = new Point( 10, 88 );
			GridCol_label.Margin = new Padding( 3, 12, 3, 0 );
			GridCol_label.Name = "GridCol_label";
			GridCol_label.Padding = new Padding( 4 );
			GridCol_label.Size = new Size( 170, 34 );
			GridCol_label.TabIndex = 0;
			GridCol_label.Text = "Grid Color\r\n(Double-Click to Change)";
			GridCol_label.DoubleClick +=  GridCol_label_DoubleClick ;
			// 
			// Y_numericUpDown
			// 
			Y_numericUpDown.Anchor = AnchorStyles.Top;
			Y_numericUpDown.AutoSize = true;
			Y_numericUpDown.Location = new Point( 121, 50 );
			Y_numericUpDown.Maximum = new decimal( new int[] { 65535, 0, 0, 0 } );
			Y_numericUpDown.Minimum = new decimal( new int[] { 1, 0, 0, 0 } );
			Y_numericUpDown.Name = "Y_numericUpDown";
			Y_numericUpDown.Size = new Size( 53, 23 );
			Y_numericUpDown.TabIndex = 3;
			Y_numericUpDown.TextAlign = HorizontalAlignment.Right;
			Y_numericUpDown.Value = new decimal( new int[] { 1, 0, 0, 0 } );
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Top;
			label3.AutoSize = true;
			label3.Location = new Point( 140, 32 );
			label3.Name = "label3";
			label3.Size = new Size( 14, 15 );
			label3.TabIndex = 2;
			label3.Text = "Y";
			// 
			// OK_button
			// 
			OK_button.Anchor =  AnchorStyles.Bottom  |  AnchorStyles.Right ;
			The_tableLayoutPanel.SetColumnSpan( OK_button, 2 );
			OK_button.Location = new Point( 198, 149 );
			OK_button.Name = "OK_button";
			OK_button.Size = new Size( 105, 40 );
			OK_button.TabIndex = 4;
			OK_button.Text = "OK";
			OK_button.UseVisualStyleBackColor = true;
			OK_button.Click +=  OK_button_Click ;
			// 
			// GridSizeDlg
			// 
			AcceptButton = OK_button;
			AutoScaleDimensions = new SizeF( 96F, 96F );
			AutoScaleMode = AutoScaleMode.Dpi;
			CancelButton = Cancel_button;
			ClientSize = new Size( 313, 200 );
			Controls.Add( The_tableLayoutPanel );
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "GridSizeDlg";
			ShowIcon = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Grid Settings";
			The_tableLayoutPanel.ResumeLayout( false );
			The_tableLayoutPanel.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)X_numericUpDown ).EndInit();
			( (System.ComponentModel.ISupportInitialize)Y_numericUpDown ).EndInit();
			ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel The_tableLayoutPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown X_numericUpDown;
		private System.Windows.Forms.NumericUpDown Y_numericUpDown;
		private System.Windows.Forms.Button OK_button;
		private System.Windows.Forms.Button Cancel_button;
		private Label GridCol_label;
	}
}