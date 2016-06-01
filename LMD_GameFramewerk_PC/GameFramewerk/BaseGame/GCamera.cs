using System;

namespace LMD_GameFramewerk_PC.GameFramewerk.BaseGame
{
	public class GCamera : ICamera
	{
		private float x;
		private float y;
		private float scaleX;
		private float scaleY;

		public GCamera()
		{
			x = 0;
			y = 0;
			scaleX = 1f;
			scaleY = 1f;
		}

		public void SetXY(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public void SetX(float value)
		{
			x = value;
		}

		public void SetY(float value)
		{
			y = value;
		}

		public float GetX()
		{
			return x;
		}

		public float GetY()
		{
			return y;
		}

		public void SetScale(float x, float y)
		{
			SetScaleX(x);
			SetScaleY(y);
		}

		public void SetScaleX(float value)
		{
			scaleX = value;
		}

		public void SetScaleY(float value)
		{
			scaleY = value;
		}

		public float GetScaleX()
		{
			return scaleX;
		}

		public float GetScaleY()
		{
			return scaleY;
		}
	}
}