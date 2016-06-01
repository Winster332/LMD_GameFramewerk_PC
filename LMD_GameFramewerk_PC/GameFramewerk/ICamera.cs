namespace LMD_GameFramewerk_PC.GameFramewerk
{
	public interface ICamera
	{
		void SetXY(float x, float y);
		void SetX(float value);
		void SetY(float value);
		float GetX();
		float GetY();
		void SetScale(float x, float y);
		void SetScaleX(float value);
		void SetScaleY(float value);
		float GetScaleX();
		float GetScaleY();
	}
}