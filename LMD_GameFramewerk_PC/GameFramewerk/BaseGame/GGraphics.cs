using System.Drawing;

namespace LMD_GameFramewerk_PC.GameFramewerk.BaseGame
{
	public class GGraphics : IGraphics
	{
		private Graphics graphics;

		public void SetGraphics(Graphics g)
		{
			graphics = g;
		}

		public Graphics GetGraphics()
		{
			return graphics;
		}
	}
}