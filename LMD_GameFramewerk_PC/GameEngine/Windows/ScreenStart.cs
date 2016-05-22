using System.Drawing;
using LMD_GameFramewerk_PC.GameFramewerk;
using LMD_GameFramewerk_PC.GameFramewerk.UI;

namespace LMD_GameFramewerk_PC.GameEngine.Windows
{
	public class ScreenStart : Screen
	{
		private GImage imgLMD;
		private float dt;

		public ScreenStart(IGame game) : base(game)
		{

		}

		public override void Step(float dt)
		{
			this.dt = dt;

			imgLMD.SetWidth(250);
			imgLMD.SetHeight(200);
		}

		public override void Draw()
		{
			Game.GetGraphics().GetGraphics().Clear(Color.FromArgb(50, 50, 50));

			DrawElements(dt);
		}

		public override void Resume()
		{
			imgLMD = new GImage(Game);
			imgLMD.SetImage(ResourceGame.text_lmd);
			imgLMD.SetWidth(50);
			imgLMD.SetHeight(1);
			imgLMD.SetX(Game.GetWindowWidth() / 2 + 90);
			imgLMD.SetY(200);
			AddElement(imgLMD);
		}

		public override void Pause()
		{
		}

		public override void Dispose()
		{
			RemoveElements();
		}

		public override void TouchDown(System.Windows.Forms.MouseEventArgs eventArgs)
		{
			TouchElements(eventArgs.X, eventArgs.Y, TypeTouch.Down);
		}

		public override void TouchMove(System.Windows.Forms.MouseEventArgs eventArgs)
		{
			TouchElements(eventArgs.X, eventArgs.Y, TypeTouch.Move);
		}

		public override void TouchUp(System.Windows.Forms.MouseEventArgs eventArgs)
		{
			TouchElements(eventArgs.X, eventArgs.Y, TypeTouch.Up);
		}
	}
}