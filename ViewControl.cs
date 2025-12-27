using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintDotCat
{
	/// <summary>
	/// 表示域コントロール．
	/// - BufferedGraphics を使うだけ．
	/// </summary>
	public sealed partial class ViewControl : Control
	{
		private BufferedGraphics m_BG;	//描画用バッファ

		/// <summary>private default ctor</summary>
		public ViewControl(){	InitializeComponent();	}

		/// <summary>表示更新</summary>
		/// <param name="DrawFunc">表示内容描画手段．引数に対して描画を行うこと．</param>
		public void Draw( Action<Graphics> DrawFunc )
		{
			DrawFunc( m_BG.Graphics );
			Invalidate();
			Update();
		}

		protected override void OnClientSizeChanged(EventArgs e)
		{
			m_BG?.Dispose();

			using( var g = CreateGraphics() )
			{	m_BG = BufferedGraphicsManager.Current.Allocate( g,	this.ClientRectangle );		}
		}

		protected override void OnPaintBackground(PaintEventArgs pevent){	/* 領域全体を描画するからNOP */	}
		protected override void OnPaint(PaintEventArgs pe){	m_BG?.Render( pe.Graphics );	}
	}
}
