using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintDotCat
{
	static class Algo
	{
		/// <summary>
		/// ブレゼンハムのアルゴリズム．
		/// (x1,y1) - (x2,y2) の直線経路上の画素位置について Act をコールする
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="y1"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		/// <param name="Act">各画素位置で実施すべき処理．引数は画素位置(x,y)</param>
		public static void Bresenham( int x1, int y1, int x2, int y2, Action<int, int> Act )
		{
			int W = x2 - x1;
			int H = y2 - y1;

			int dx1 = ( W>0 ? 1 : (W<0 ? -1 : 0 ) );
			int dy1 = ( H>0 ? 1 : (H<0 ? -1 : 0 ) );
	
			int dx2 = dx1;
			int dy2 = 0;
			int L = Math.Abs( W );
			int S = Math.Abs( H );
			if( L < S )
			{
				{
					int t = L;
					L = S;
					S = t;
				}
				dy2 = ( H>0 ? 1 : (H<0 ? -1 : 0 ) );
				dx2 = 0;
			}

			int numerator = L/2;
			int x = x1;
			int y = y1;
			for( int i=0; i<=L; i++ )
			{
				Act(x,y);

				numerator += S;
				if( numerator >= L )
				{
					numerator -= L;
					x += dx1;
					y += dy1;
				}
				else
				{
					x += dx2;
					y += dy2;
				}
			}
		}
	}
}

