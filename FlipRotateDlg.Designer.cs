
namespace PaintDotCat
{
	partial class FlipRotateDlg
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
			tableLayoutPanel1 = new TableLayoutPanel();
			OK_button = new Button();
			Cancel_button = new Button();
			Rot270_radioButton = new RadioButton();
			Rot180_radioButton = new RadioButton();
			Rot90_radioButton = new RadioButton();
			FlipVertical_radioButton = new RadioButton();
			FlipHorizontal_radioButton = new RadioButton();
			label1 = new Label();
			label2 = new Label();
			tableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add( new ColumnStyle() );
			tableLayoutPanel1.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel1.Controls.Add( OK_button, 1, 4 );
			tableLayoutPanel1.Controls.Add( Cancel_button, 0, 4 );
			tableLayoutPanel1.Controls.Add( Rot270_radioButton, 1, 3 );
			tableLayoutPanel1.Controls.Add( Rot180_radioButton, 1, 2 );
			tableLayoutPanel1.Controls.Add( Rot90_radioButton, 1, 1 );
			tableLayoutPanel1.Controls.Add( FlipVertical_radioButton, 0, 2 );
			tableLayoutPanel1.Controls.Add( FlipHorizontal_radioButton, 0, 1 );
			tableLayoutPanel1.Controls.Add( label1, 0, 0 );
			tableLayoutPanel1.Controls.Add( label2, 1, 0 );
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point( 0, 0 );
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.Padding = new Padding( 7, 8, 7, 8 );
			tableLayoutPanel1.RowCount = 5;
			tableLayoutPanel1.RowStyles.Add( new RowStyle() );
			tableLayoutPanel1.RowStyles.Add( new RowStyle() );
			tableLayoutPanel1.RowStyles.Add( new RowStyle() );
			tableLayoutPanel1.RowStyles.Add( new RowStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel1.RowStyles.Add( new RowStyle() );
			tableLayoutPanel1.RowStyles.Add( new RowStyle( SizeType.Absolute, 20F ) );
			tableLayoutPanel1.Size = new Size( 335, 212 );
			tableLayoutPanel1.TabIndex = 0;
			// 
			// OK_button
			// 
			OK_button.Anchor = AnchorStyles.Right;
			OK_button.Location = new Point( 220, 161 );
			OK_button.Name = "OK_button";
			OK_button.Size = new Size( 105, 40 );
			OK_button.TabIndex = 1;
			OK_button.Text = "OK";
			OK_button.UseVisualStyleBackColor = true;
			OK_button.Click +=  OK_button_Click ;
			// 
			// Cancel_button
			// 
			Cancel_button.Anchor = AnchorStyles.Left;
			Cancel_button.DialogResult = DialogResult.Cancel;
			Cancel_button.Location = new Point( 10, 161 );
			Cancel_button.Name = "Cancel_button";
			Cancel_button.Size = new Size( 105, 40 );
			Cancel_button.TabIndex = 2;
			Cancel_button.Text = "Cancel";
			Cancel_button.UseVisualStyleBackColor = true;
			Cancel_button.Click +=  Cancel_button_Click ;
			// 
			// Rot270_radioButton
			// 
			Rot270_radioButton.AutoSize = true;
			Rot270_radioButton.Location = new Point( 125, 91 );
			Rot270_radioButton.Margin = new Padding( 7, 8, 3, 3 );
			Rot270_radioButton.Name = "Rot270_radioButton";
			Rot270_radioButton.Size = new Size( 103, 19 );
			Rot270_radioButton.TabIndex = 0;
			Rot270_radioButton.TabStop = true;
			Rot270_radioButton.Text = "Rotate 270 deg";
			Rot270_radioButton.UseVisualStyleBackColor = true;
			// 
			// Rot180_radioButton
			// 
			Rot180_radioButton.AutoSize = true;
			Rot180_radioButton.Location = new Point( 125, 61 );
			Rot180_radioButton.Margin = new Padding( 7, 8, 3, 3 );
			Rot180_radioButton.Name = "Rot180_radioButton";
			Rot180_radioButton.Size = new Size( 103, 19 );
			Rot180_radioButton.TabIndex = 0;
			Rot180_radioButton.TabStop = true;
			Rot180_radioButton.Text = "Rotate 180 deg";
			Rot180_radioButton.UseVisualStyleBackColor = true;
			// 
			// Rot90_radioButton
			// 
			Rot90_radioButton.AutoSize = true;
			Rot90_radioButton.Location = new Point( 125, 31 );
			Rot90_radioButton.Margin = new Padding( 7, 8, 3, 3 );
			Rot90_radioButton.Name = "Rot90_radioButton";
			Rot90_radioButton.Size = new Size( 97, 19 );
			Rot90_radioButton.TabIndex = 0;
			Rot90_radioButton.TabStop = true;
			Rot90_radioButton.Text = "Rotate 90 deg";
			Rot90_radioButton.UseVisualStyleBackColor = true;
			// 
			// FlipVertical_radioButton
			// 
			FlipVertical_radioButton.AutoSize = true;
			FlipVertical_radioButton.Location = new Point( 14, 61 );
			FlipVertical_radioButton.Margin = new Padding( 7, 8, 3, 3 );
			FlipVertical_radioButton.Name = "FlipVertical_radioButton";
			FlipVertical_radioButton.Size = new Size( 85, 19 );
			FlipVertical_radioButton.TabIndex = 0;
			FlipVertical_radioButton.TabStop = true;
			FlipVertical_radioButton.Text = "Flip Vertical";
			FlipVertical_radioButton.UseVisualStyleBackColor = true;
			// 
			// FlipHorizontal_radioButton
			// 
			FlipHorizontal_radioButton.AutoSize = true;
			FlipHorizontal_radioButton.Location = new Point( 14, 31 );
			FlipHorizontal_radioButton.Margin = new Padding( 7, 8, 3, 3 );
			FlipHorizontal_radioButton.Name = "FlipHorizontal_radioButton";
			FlipHorizontal_radioButton.Size = new Size( 100, 19 );
			FlipHorizontal_radioButton.TabIndex = 0;
			FlipHorizontal_radioButton.TabStop = true;
			FlipHorizontal_radioButton.Text = "Flip horizontal";
			FlipHorizontal_radioButton.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point( 7, 8 );
			label1.Margin = new Padding( 0 );
			label1.Name = "label1";
			label1.Size = new Size( 32, 15 );
			label1.TabIndex = 3;
			label1.Text = "Flip :";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point( 118, 8 );
			label2.Margin = new Padding( 0 );
			label2.Name = "label2";
			label2.Size = new Size( 47, 15 );
			label2.TabIndex = 4;
			label2.Text = "Rotate :";
			// 
			// FlipRotateDlg
			// 
			AcceptButton = OK_button;
			AutoScaleDimensions = new SizeF( 96F, 96F );
			AutoScaleMode = AutoScaleMode.Dpi;
			CancelButton = Cancel_button;
			ClientSize = new Size( 335, 212 );
			Controls.Add( tableLayoutPanel1 );
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FlipRotateDlg";
			ShowIcon = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Flip or Rotate";
			Load +=  FlipRotateDlg_Load ;
			tableLayoutPanel1.ResumeLayout( false );
			tableLayoutPanel1.PerformLayout();
			ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.RadioButton FlipHorizontal_radioButton;
		private System.Windows.Forms.RadioButton FlipVertical_radioButton;
		private System.Windows.Forms.RadioButton Rot90_radioButton;
		private System.Windows.Forms.RadioButton Rot180_radioButton;
		private System.Windows.Forms.RadioButton Rot270_radioButton;
		private System.Windows.Forms.Button OK_button;
		private System.Windows.Forms.Button Cancel_button;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}