
namespace PaintDotCat
{
	partial class ThumbnailForm
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
			Thumbnail_pictureBox = new PictureBox();
			( (System.ComponentModel.ISupportInitialize)Thumbnail_pictureBox ).BeginInit();
			SuspendLayout();
			// 
			// Thumbnail_pictureBox
			// 
			Thumbnail_pictureBox.Location = new Point( 0, 0 );
			Thumbnail_pictureBox.Margin = new Padding( 0 );
			Thumbnail_pictureBox.Name = "Thumbnail_pictureBox";
			Thumbnail_pictureBox.Size = new Size( 88, 50 );
			Thumbnail_pictureBox.TabIndex = 0;
			Thumbnail_pictureBox.TabStop = false;
			Thumbnail_pictureBox.Paint +=  Thumbnail_pictureBox_Paint ;
			// 
			// ThumbnailForm
			// 
			AutoScaleDimensions = new SizeF( 96F, 96F );
			AutoScaleMode = AutoScaleMode.Dpi;
			AutoScroll = true;
			ClientSize = new Size( 175, 151 );
			Controls.Add( Thumbnail_pictureBox );
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "ThumbnailForm";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Thumbnail";
			FormClosing +=  ThumbnailForm_FormClosing ;
			( (System.ComponentModel.ISupportInitialize)Thumbnail_pictureBox ).EndInit();
			ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.PictureBox Thumbnail_pictureBox;
	}
}