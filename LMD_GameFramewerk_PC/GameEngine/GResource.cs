using System;
using LMD_GameFramewerk_PC.GameFramewerk.UI;

namespace LMD_GameFramewerk_PC.GameEngine
{
	public static class GResource
	{
		public const String PATH_ASSETS = @"Assets\";
		public static uint tex_lmd;

		public static void LoadTextures()
		{
			tex_lmd = GImage.LoadTexture(PATH_ASSETS + "text_lmd.png");
		}
	}
}
