using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaintDotCat
{
	/// <summary>
	/// スポイト機能
	/// </summary>
	public class ColorPickTool : ITool
	{
		//ドラッグしているボタンに対応する描画色index．
		//	※ 負の値を「業中ではない」という意味合いで用いている 
		private int m_iDrawColor = -1;
		//描画色変更処理者
		private readonly IColorViewOpListener m_Observer;

		private Action<ToolType> m_OnFinishedCallback;
		private ToolType m_OnFinishedCallbackArg;

		//-----------------------------------

		/// <summary>ctor</summary>
		/// <param name="P">描画色変更処理者</param>
		public ColorPickTool( IColorViewOpListener Observer ){	m_Observer=Observer;	}

		/// <summary>
		/// スポイトツールを終えるべきタイミングで実施する処理を指定する
		/// </summary>
		/// <param name="OnFinishedCallback">処理</param>
		/// <param name="CallbackArg">処理への引数</param>
		public void Setup( Action<ToolType> OnFinishedCallback, ToolType CallbackArg )
		{
			m_OnFinishedCallback = OnFinishedCallback;
			m_OnFinishedCallbackArg = CallbackArg;
		}

		//-----------------------------------
		#region private

		//スポイト処理実施
		private void PickupColor( Bitmap BMP, Point pos )
		{
			if( m_iDrawColor<0 )return;
			if( pos.X<0 || pos.Y<0 || pos.X>=BMP.Width || pos.Y>=BMP.Height )return;

			m_Observer.OnColorSelected( m_iDrawColor, BMP.GetPixel( pos.X, pos.Y ) );
		}

		#endregion
		//-----------------------------------
		#region ITool Impl

		/// <inheritdoc/>
		public ToolType Type => ToolType.Pen;	//※現状，Penとする

		/// <inheritdoc/>
		public bool IsBusy(){	return m_iDrawColor>=0;	}

		/// <inheritdoc/>
		public IEdit CreateEdit(){	return null;	}	//画像の編集は無い

		/// <inheritdoc/>
		public void DrawStateToViewImg(Graphics g, int MagRate)
		{	/*NOP*/	}

		/// <inheritdoc/>
		public ToolProcResult OnMouseDown(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( m_iDrawColor<0 )
			{
				m_iDrawColor = Util.DrawColorIndexFor( button );
				if( m_iDrawColor>=0 )
				{	PickupColor( BMP, pos );	}
			}

			return ToolProcResult.None;
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseMove(Point pos, Bitmap BMP)
		{
			if( m_iDrawColor>=0 )
			{	PickupColor( BMP, pos );	}

			return ToolProcResult.None;
		}

		/// <inheritdoc/>
		public ToolProcResult OnMouseUp(Point pos, MouseButtons button, Bitmap BMP)
		{
			if( m_iDrawColor>=0  &&  m_iDrawColor==Util.DrawColorIndexFor( button ) )
			{
				m_iDrawColor = -1;
				m_OnFinishedCallback?.Invoke( m_OnFinishedCallbackArg );
				return ToolProcResult.ShouldUpdateView;
			}

			return ToolProcResult.None;
		}

		#endregion
	}
}
