using System;
using System.Drawing;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.DevIl;

namespace LMD_GameFramewerk_PC.GameFramewerk.BaseGame
{
	public class GGraphics : IGraphics
	{
		private const float COLOR_CONVERT = 255;
		private GGame game;

		public GGraphics(GGame game)
		{
			this.game = game;
			this.game.SizeChanged += Game_SizeChanged;
			
			InitOpenGL();
		}

		private void Game_SizeChanged(object sender, EventArgs e)
		{
			this.game.glView.Size = game.Size;
			Gl.glViewport(0, 0, GetSurfaceWidth(), GetSurfaceHeight());
		}

		private void InitOpenGL()
		{
			// Инициализируем OpenGL
			Glut.glutInit();
			Glut.glutInitDisplayMode(Glut.GLUT_RGBA | Glut.GLUT_DOUBLE);
			
			// Инициализируем библиотеку DevIl
			Il.ilInit();
			Il.ilEnable(Il.IL_ORIGIN_SET);

			// Настраиваем режим работы OpenGL
			Gl.glViewport(0, 0, GetSurfaceWidth(), GetSurfaceHeight());

			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();

			Glu.gluOrtho2D(0, GetSurfaceWidth(), 0, GetSurfaceHeight());
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();

			Gl.glClearColor(0.2f, 0.2f, 0.2f, 1f);
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			Gl.glEnable(Gl.GL_BLEND);
			Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
		}

		public int GetSurfaceWidth()
		{
			return game.glView.Width;
		}

		public int GetSurfaceHeight()
		{
			return game.glView.Height;
		}

		public void BeginRender()
		{
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();
			Glu.gluOrtho2D(-game.GetCamera().GetX(), GetSurfaceWidth() + (-game.GetCamera().GetX()),
				-game.GetCamera().GetY(), GetSurfaceHeight() + (-game.GetCamera().GetY()));
			Gl.glScalef(game.GetCamera().GetScaleX(), game.GetCamera().GetScaleY(), 1);
		}

		public void DrawLine(float x1, float y1, float x2, float y2, float width, int R, int G, int B)
		{
			Gl.glLoadIdentity();
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glPushMatrix();
			Gl.glLineWidth(width);
			Gl.glBegin(Gl.GL_LINES);
			Gl.glColor3d(R / COLOR_CONVERT, G / COLOR_CONVERT, B / COLOR_CONVERT);
			Gl.glVertex3f(x1, y1, 0.0f);
			Gl.glVertex3f(x2, y2, 0.0f);
			Gl.glEnd();
			Gl.glPopMatrix();
		}
		public void DrawLines(PointF[] points, float width, int R, int G, int B)
		{
			Gl.glLoadIdentity();
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glPushMatrix();
			Gl.glLineWidth(width);
			Gl.glBegin(Gl.GL_LINES);
			Gl.glColor3d(R / COLOR_CONVERT, G / COLOR_CONVERT, B / COLOR_CONVERT);

			for (int i = 0; i < points.Length; i++)
			{
				Gl.glVertex3f(points[i].X, points[i].Y, 0.0f);
			}
		
			Gl.glEnd();
			Gl.glPopMatrix();
		}

		public void DrawText(string text, float width, int R, int G, int B)
		{
			Gl.glColor3d(R / COLOR_CONVERT, G / COLOR_CONVERT, B / COLOR_CONVERT);
			Gl.glRasterPos3f(0, 0, 0);

			for (int i = 0; i < text.Length; i++)
			{
				Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_TIMES_ROMAN_10, text[i]);
			}
		}

		public void EndRender()
		{
			Gl.glFlush();
			game.glView.Invalidate();
		}
	}
}