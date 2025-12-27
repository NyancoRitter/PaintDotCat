
namespace PaintDotCat
{
	partial class ColorsUC
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if( disposing && (components != null) )
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
			Palette_flowLayoutPanel = new FlowLayoutPanel();
			Main_tableLayoutPanel = new TableLayoutPanel();
			LineWrap_checkBox = new CheckBox();
			Main_tableLayoutPanel.SuspendLayout();
			SuspendLayout();
			// 
			// Palette_flowLayoutPanel
			// 
			Palette_flowLayoutPanel.AutoScroll = true;
			Palette_flowLayoutPanel.Dock = DockStyle.Fill;
			Palette_flowLayoutPanel.Location = new Point( 63, 3 );
			Palette_flowLayoutPanel.Name = "Palette_flowLayoutPanel";
			Palette_flowLayoutPanel.Size = new Size( 244, 115 );
			Palette_flowLayoutPanel.TabIndex = 1;
			// 
			// Main_tableLayoutPanel
			// 
			Main_tableLayoutPanel.ColumnCount = 2;
			Main_tableLayoutPanel.ColumnStyles.Add( new ColumnStyle() );
			Main_tableLayoutPanel.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100F ) );
			Main_tableLayoutPanel.Controls.Add( Palette_flowLayoutPanel, 1, 0 );
			Main_tableLayoutPanel.Controls.Add( LineWrap_checkBox, 0, 0 );
			Main_tableLayoutPanel.Dock = DockStyle.Fill;
			Main_tableLayoutPanel.Location = new Point( 0, 0 );
			Main_tableLayoutPanel.Margin = new Padding( 0 );
			Main_tableLayoutPanel.Name = "Main_tableLayoutPanel";
			Main_tableLayoutPanel.RowCount = 1;
			Main_tableLayoutPanel.RowStyles.Add( new RowStyle( SizeType.Percent, 100F ) );
			Main_tableLayoutPanel.Size = new Size( 310, 121 );
			Main_tableLayoutPanel.TabIndex = 2;
			// 
			// LineWrap_checkBox
			// 
			LineWrap_checkBox.AutoSize = true;
			LineWrap_checkBox.Location = new Point( 3, 3 );
			LineWrap_checkBox.Name = "LineWrap_checkBox";
			LineWrap_checkBox.Size = new Size( 54, 19 );
			LineWrap_checkBox.TabIndex = 2;
			LineWrap_checkBox.Text = "Wrap";
			LineWrap_checkBox.UseVisualStyleBackColor = true;
			LineWrap_checkBox.CheckedChanged +=  LineWrap_checkBox_CheckedChanged ;
			// 
			// ColorsUC
			// 
			AutoScaleDimensions = new SizeF( 96F, 96F );
			AutoScaleMode = AutoScaleMode.Dpi;
			BorderStyle = BorderStyle.FixedSingle;
			Controls.Add( Main_tableLayoutPanel );
			Name = "ColorsUC";
			Size = new Size( 310, 121 );
			Load +=  ColorsUC_Load ;
			Main_tableLayoutPanel.ResumeLayout( false );
			Main_tableLayoutPanel.PerformLayout();
			ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel Palette_flowLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel Main_tableLayoutPanel;
		private System.Windows.Forms.CheckBox LineWrap_checkBox;
	}
}
