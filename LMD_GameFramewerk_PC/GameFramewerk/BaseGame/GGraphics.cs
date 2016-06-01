using System;
using System.Drawing;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.DevIl;

namespace LMD_GameFramewerk_PC.GameFramewerk.BaseGame
{
	public class GGraphics : IGraphics
	{
		private Graphics graphics;
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

		public void SetGraphics(Graphics g)
		{
			graphics = g;
		}

		public Graphics GetGraphics()
		{
			return graphics;
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
		}

		public void EndRender()
		{
			Gl.glFlush();
			game.glView.Invalidate();
		}
	}
}