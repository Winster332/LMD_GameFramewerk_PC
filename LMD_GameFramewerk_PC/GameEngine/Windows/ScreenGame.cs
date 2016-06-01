using System;
using LMD_GameFramewerk_PC.GameFramewerk;

namespace LMD_GameFramewerk_PC.GameEngine.Windows
{
	public class ScreenGame : Screen
	{
		public ScreenGame(IGame game) : base(game)
		{
		}

		public override void Dispose()
		{
			Game.GetPhysics().Dispose();
		}

		public override void Draw()
		{
			Game.GetPhysics().Draw();
		}

		public override void Pause()
		{
		}

		public override void Resume()
		{
			GResource.LoadTextures();

			Game.GetPhysics().Initialize(0,0, Game.GetWindowWidth(), Game.GetWindowHeight(), 0, -0.2f, false);
			Game.GetPhysics().AddRect(100, 100, 50, 50, 0, 0.4f, 0.4f, 0.4f, GResource.tex_rect);
			Game.GetPhysics().AddRect(130, 200, 50, 50, 0, 0.4f, 0.4f, 0.4f, GResource.tex_rect);
			Game.GetPhysics().AddCircle(130, 200, 20, 0, 0.4f, 0.4f, 0.4f, GResource.tex_circle);
			Game.GetPhysics().AddRect(Game.GetWindowWidth() / 2, 40, Game.GetWindowWidth(), 10, 0, 0, 0.5f, 0, GResource.tex_rect);
		}

		public override void Step(float dt)
		{
			Game.GetPhysics().Step(1f, 20);
		}

		public override void TouchDown(System.Windows.Forms.MouseEventArgs eventArgs)
		{
		}

		public override void TouchMove(System.Windows.Forms.MouseEventArgs eventArgs)
		{
		}

		public override void TouchUp(System.Windows.Forms.MouseEventArgs eventArgs)
		{
		}
	}
}
