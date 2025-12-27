
namespace PaintDotCat
{
	partial class VersionDlg
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
			pictureBox1 = new PictureBox();
			label1 = new Label();
			Version_label = new Label();
			OK_button = new Button();
			label2 = new Label();
			panel1 = new Panel();
			tableLayoutPanel1.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)pictureBox1 ).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.AutoSize = true;
			tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel1.Controls.Add( pictureBox1, 0, 0 );
			tableLayoutPanel1.Controls.Add( label1, 0, 1 );
			tableLayoutPanel1.Controls.Add( Version_label, 0, 2 );
			tableLayoutPanel1.Controls.Add( OK_button, 0, 5 );
			tableLayoutPanel1.Controls.Add( label2, 0, 3 );
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point( 0, 0 );
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.Padding = new Padding( 0, 8, 0, 8 );
			tableLayoutPanel1.RowCount = 6;
			tableLayoutPanel1.RowStyles.Add( new RowStyle() );
			tableLayoutPanel1.RowStyles.Add( new RowStyle() );
			tableLayoutPanel1.RowStyles.Add( new RowStyle() );
			tableLayoutPanel1.RowStyles.Add( new RowStyle() );
			tableLayoutPanel1.RowStyles.Add( new RowStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel1.RowStyles.Add( new RowStyle() );
			tableLayoutPanel1.Size = new Size( 313, 218 );
			tableLayoutPanel1.TabIndex = 0;
			// 
			// pictureBox1
			// 
			pictureBox1.Anchor = AnchorStyles.None;
			pictureBox1.Image = Properties.Resources.DotCat32;
			pictureBox1.Location = new Point( 140, 11 );
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size( 32, 32 );
			pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Top;
			label1.AutoSize = true;
			label1.Font = new Font( "MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 128 );
			label1.Location = new Point( 114, 54 );
			label1.Margin = new Padding( 3, 8, 3, 0 );
			label1.Name = "label1";
			label1.Size = new Size( 84, 16 );
			label1.TabIndex = 1;
			label1.Text = "Paint.CAT";
			// 
			// Version_label
			// 
			Version_label.Anchor = AnchorStyles.Top;
			Version_label.AutoSize = true;
			Version_label.Location = new Point( 130, 82 );
			Version_label.Margin = new Padding( 3, 12, 3, 0 );
			Version_label.Name = "Version_label";
			Version_label.Size = new Size( 53, 15 );
			Version_label.TabIndex = 2;
			Version_label.Text = "(Version)";
			Version_label.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// OK_button
			// 
			OK_button.Anchor = AnchorStyles.Top;
			OK_button.Location = new Point( 104, 167 );
			OK_button.Name = "OK_button";
			OK_button.Size = new Size( 105, 40 );
			OK_button.TabIndex = 3;
			OK_button.Text = "OK";
			OK_button.UseVisualStyleBackColor = true;
			OK_button.Click +=  OK_button_Click ;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Top;
			label2.AutoSize = true;
			label2.Location = new Point( 118, 113 );
			label2.Margin = new Padding( 3, 16, 3, 0 );
			label2.Name = "label2";
			label2.Size = new Size( 76, 30 );
			label2.TabIndex = 2;
			label2.Text = "By\r\nNyancoRitter";
			label2.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// panel1
			// 
			panel1.BorderStyle = BorderStyle.Fixed3D;
			panel1.Controls.Add( tableLayoutPanel1 );
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point( 7, 8 );
			panel1.Name = "panel1";
			panel1.Size = new Size( 317, 222 );
			panel1.TabIndex = 1;
			// 
			// VersionDlg
			// 
			AcceptButton = OK_button;
			AutoScaleDimensions = new SizeF( 96F, 96F );
			AutoScaleMode = AutoScaleMode.Dpi;
			ClientSize = new Size( 331, 238 );
			Controls.Add( panel1 );
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "VersionDlg";
			Padding = new Padding( 7, 8, 7, 8 );
			ShowIcon = false;
			SizeGripStyle = SizeGripStyle.Hide;
			StartPosition = FormStartPosition.CenterParent;
			Text = "About Paint.CAT";
			Load +=  VersionDlg_Load ;
			tableLayoutPanel1.ResumeLayout( false );
			tableLayoutPanel1.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)pictureBox1 ).EndInit();
			panel1.ResumeLayout( false );
			panel1.PerformLayout();
			ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label Version_label;
		private System.Windows.Forms.Button OK_button;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel1;
	}
}