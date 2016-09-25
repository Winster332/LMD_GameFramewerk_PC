using System.Drawing;

namespace LMD_GameFramewerk_PC.GameFramewerk
{
	public interface IGraphics
	{
		void BeginRender();
		void EndRender();
		void DrawLine(float x1, float y1, float x2, float y2, float width, int R, int G, int B);
		void DrawLines(PointF[] points, float width, int R, int G, int B);
		void DrawText(string text, float width, int R, int G, int B);

		int GetSurfaceWidth();
		int GetSurfaceHeight();
	}
}