using System;
using LMD_GameFramewerk_PC.GameFramewerk;

namespace LMD_GameFramewerk_PC.GameEngine.Windows
{
	public class ScreenGame : Screen
	{
		private Models.Car car;

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

			Game.GetPhysics().Initialize(0,0, Game.GetWindowWidth(), Game.GetWindowHeight(), 0, -1f, true);
		//	GameFramewerk.BaseGame.Physics.InfoBody info1 = Game.GetPhysics().AddRect(200, 250, 50, 50, 0, 0f, 0f, 0f, GResource.tex_rect);
		//	GameFramewerk.BaseGame.Physics.InfoBody info2 = Game.GetPhysics().AddRect(230, 350, 50, 50, 0, 0.4f, 0.4f, 0.4f, GResource.tex_rect);

		//	Game.GetPhysics().AddDistanceJoint(info1.body, info2.body, 200, 250, 230, 350);

	//		Game.GetPhysics().AddCircle(130, 200, 50, 0, 0.4f, 0.4f, 0.4f, GResource.tex_circle);
			Game.GetPhysics().AddRect(Game.GetWindowWidth() / 2, 40, Game.GetWindowWidth(), 10, 0, 0, 0.5f, 0, GResource.tex_rect_green);

			car = new Models.Car(Game, 200, 200);
		}

		public override void Step(float dt)
		{
			Game.GetPhysics().Step(1f, 20);
		}

		public override void TouchDown(System.Windows.Forms.MouseEventArgs eventArgs)
		{
			if (eventArgs.Button == System.Windows.Forms.MouseButtons.Right)
				Game.GetPhysics().AddCircle(eventArgs.X, Game.GetGraphics().GetSurfaceHeight() - eventArgs.Y,
					20, 0, 0.4f, 0.4f, 0.4f, GResource.tex_circle);
			else Game.GetPhysics().AddRect(eventArgs.X, Game.GetGraphics().GetSurfaceHeight() - eventArgs.Y,
					50, 50, 0, 0.4f, 0.4f, 0.4f, GResource.tex_rect);
		}

		public override void TouchMove(System.Windows.Forms.MouseEventArgs eventArgs)
		{
		}

		public override void TouchUp(System.Windows.Forms.MouseEventArgs eventArgs)
		{
		}

		public override void KeyboardDown(System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.W: car.MoveX(-1); break;
				case System.Windows.Forms.Keys.S: car.MoveX(1); break;
			}
		}

		public override void KeyboardUp(System.Windows.Forms.KeyEventArgs e)
		{
		}
	}
}
