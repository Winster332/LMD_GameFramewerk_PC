using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LMD_GameFramewerk_PC.GameFramewerk.BaseGame;

namespace LMD_GameFramewerk_PC
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			GGame game = new GGame("LMD_GF", 500, 500);
			game.SetStartScreen(new GameEngine.Windows.ScreenStart(game));

			Application.Run(game);
		}
	}
}
