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
	/// 矩形範囲選択ツール
	/// （範囲をマウスドラッグで選択する処理）
	/// </summary>
	public class RectSelTool : ITool
	{
		public event Action<Rectangle> OnRectAreaSelected;
		public event Action<Size> ShowDraggingAreaSize;

		private bool m_IsDragging = false;
		private Point[] m_Pos = new Point[2];
		
		/// <summary>
		/// 選択範囲矩形
		/// </summary>
		private Rectangle SelectedRect
		{
			get
			{
				return new Rectangle(
					Math.Min( m_Pos[0].X, m_Pos[1].X ),
					Math.Min( m_Pos[0].Y, m_Pos[1].Y ),
					Math.Abs( m_Pos[0].X - m_Pos[1].X ) + 1,
					Math.Abs( m_Pos[0].Y - m_Pos[1].Y ) + 1
				);
			}
		}

		//-----------------------------------
		#region ITool Impl

		/// <inheritdoc/>
		public ToolType Type => ToolType.RectAreaSelect;

		/// <inheritdoc/>
		public bool IsBusy(){	return m_IsDragging;	}

		/// <inheritdoc/>
		public IEdit CreateEdit()
		{	return null;	}

		/// <inheritdoc/>
		public void DrawStateToViewImg(Graphics g, int MagRate)
		{
			if( !IsBusy() || m_Pos[0].Equals( m_Pos[1] ) )return;

			Util.DrawRectSelectionState( g, SelectedRect, MagRate );
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseDown(Point pos, MouseButtons button, Bitmap BMP)
		{
			//左ドラッグ中に他のマウスボタンが押された場合はキャンセル
			if( m_IsDragging && !button.HasFlag(MouseButtons.Left) )
			{
				m_IsDragging = false;
				return ToolProcResult.EditShouldBeRejected;
			}

			//ドラッグできるのは左ボタンのみとする
			if( !button.HasFlag( MouseButtons.Left ) )return ToolProcResult.None;

			m_Pos[0] = pos;
			m_Pos[1] = pos;
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
			if( m_Pos[1].Equals(pos) )return ToolProcResult.None;

			m_Pos[1] = pos;
			if( ShowDraggingAreaSize != null )
			{	ShowDraggingAreaSize( SelectedRect.Size );	}

			return ToolProcResult.ShouldUpdateView;
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseUp(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( !m_IsDragging )return ToolProcResult.None;
			if( !button.HasFlag( MouseButtons.Left ) )return ToolProcResult.None;
			m_IsDragging = false;
			//1x1 は不可
			if( m_Pos[0].Equals( m_Pos[1] ) )return ToolProcResult.EditShouldBeRejected;

			//
			if( OnRectAreaSelected != null )
			{	OnRectAreaSelected( SelectedRect );	}

			return ToolProcResult.EditShouldBeRejected;
		}

		#endregion
	}
}
