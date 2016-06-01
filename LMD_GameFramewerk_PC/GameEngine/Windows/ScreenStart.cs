using System.Drawing;
using LMD_GameFramewerk_PC.GameFramewerk;
using LMD_GameFramewerk_PC.GameFramewerk.UI;
using Tao.OpenGl;

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
		}
		
		public override void Draw()
		{
			DrawElements(dt);
		}

		public override void Resume()
		{
			imgLMD = new GImage(Game);
			imgLMD.SetTexture(GImage.LoadTexture(@"C:\Users\User\Desktop\WarBugs\dot_f_5.png"));
			imgLMD.SetWidth(100);
			imgLMD.SetHeight(100);
			imgLMD.SetX(Game.GetWindowWidth() / 2);
			imgLMD.SetY(Game.GetWindowHeight() / 2);
			AddElement(imgLMD);

			CircleButton but = new CircleButton(Game);
			but.onClick += ScreenStart_onClick;
			but.SetX(50);
			but.SetY(50);
			but.SetWidth(100);
			but.SetHeight(100);
			but.SetRadius(50);
			but.SetImage(GImage.LoadTexture(@"C:\Users\User\Desktop\WarBugs\button_play.png"));

			GameFramewerk.UI.Animations.AnimationScale anim = new GameFramewerk.UI.Animations.AnimationScale();
			anim.Initialize(100, 100, 120, 120, 1, 1, but);

			AddElement(but);
		}

		private void ScreenStart_onClick(GBaseButton button)
		{
			System.Console.WriteLine("click");
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