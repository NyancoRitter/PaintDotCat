using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PaintDotCat
{
	/// <summary>
	/// 自由形状範囲選択ツール
	/// （範囲をマウスドラッグで選択する処理）
	/// </summary>
	public class FreeFormSelTool : ITool
	{
		public event Action< Point[] > OnFreeFormAreaSelected;

		private bool m_IsDragging = false;
		private List<Point> m_Points = new List<Point>();

		private Bitmap m_Backup;

		//-----------------------------------
		#region ITool Impl

		/// <inheritdoc/>
		public ToolType Type => ToolType.FreeFormAreaSelect;

		/// <inheritdoc/>
		public bool IsBusy(){	return m_IsDragging;	}

		/// <inheritdoc/>
		public IEdit CreateEdit()
		{	return null;	}

		/// <inheritdoc/>
		public void DrawStateToViewImg(Graphics g, int MagRate){	/*NOP*/	}

		/// <inheritdoc/>
		public ToolProcResult OnMouseDown(Point pos, MouseButtons button, Bitmap BMP)
		{
			//左ドラッグ中に他のマウスボタンが押された場合はキャンセル
			if( m_IsDragging && !button.HasFlag(MouseButtons.Left) )
			{
				m_IsDragging = false;
				Util.DisposeBMP( ref m_Backup );
				return ToolProcResult.EditShouldBeRejected;
			}

			//ドラッグできるのは左ボタンのみとする
			if( !button.HasFlag( MouseButtons.Left ) )return ToolProcResult.None;

			//操作開始時点の画像のコピーを得ておく
			Util.DisposeBMP( ref m_Backup );
			m_Backup = new Bitmap( BMP );
			//
			m_Points.Clear();
			m_Points.Add( pos );
			m_IsDragging = true;
			return ToolProcResult.None;
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseMove(Point pos, Bitmap BMP)
		{
			if( !m_IsDragging )return ToolProcResult.None;

			if( pos.X<0 )pos.X=0;
			if( pos.Y<0 )pos.Y=0;
			if( pos.X >= BMP.Width )pos.X = BMP.Width - 1;
			if( pos.Y >= BMP.Height )pos.Y = BMP.Height - 1;

			if( m_Points.Any( p => p.Equals(pos) ) )
			{	return ToolProcResult.None;	}

			m_Points.Add( pos );
			
			//※暫定：ドラッグで範囲を選択している途中の状況というのはどう表示すべき？？
			using( var g = Graphics.FromImage(BMP) )
			{
				var POM = g.PixelOffsetMode;

				g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
				g.DrawImageUnscaled( m_Backup, 0,0 );

				g.PixelOffsetMode = POM;
				g.DrawLines( Pens.Gray, m_Points.ToArray() );
			}

			return ToolProcResult.ShouldUpdateView;
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseUp(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( !m_IsDragging )return ToolProcResult.None;
			if( !button.HasFlag( MouseButtons.Left ) )return ToolProcResult.None;

			m_IsDragging = false;
			Util.DisposeBMP( ref m_Backup );

			//※実装MEMO :
			//点の個数が2個だと，別の箇所で用いている GraphicsPath.AddPolygon() が例外を出すのでここで個数制限して対処．．
			if( m_Points.Count >= 3 )
			{	 OnFreeFormAreaSelected?.Invoke( m_Points.ToArray() );	}

			return ToolProcResult.EditShouldBeRejected;
		}

		#endregion
	}
}
