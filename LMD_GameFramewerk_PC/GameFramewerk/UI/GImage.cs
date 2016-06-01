using System.Drawing;
using Tao.OpenGl;
using Tao.DevIl;

namespace LMD_GameFramewerk_PC.GameFramewerk.UI
{
	public class GImage : BaseUI
	{
		private uint texture;

		public GImage(IGame game) : base(game)
		{
			scaleX = 1f;
			scaleY = 1f;
			IsCamera = true;
		}

		public override void SetX(float value)
		{
			x = value;
		}

		public override void SetY(float value)
		{
			y = value;
		}

		public override float GetX()
		{
			return x;
		}

		public override float GetY()
		{
			return y;
		}

		public override void SetVelocityX(float value)
		{
			velocity_x = value;
		}

		public override void SetVelocityY(float value)
		{
			velocity_y = value;
		}

		public override float GetVelocityX()
		{
			return velocity_x;
		}

		public override float GetVelocityY()
		{
			return velocity_y;
		}

		public override void SetAndle(float value)
		{
			angle = value;
		}

		public override float GetAngle()
		{
			return angle;
		}

		public override void SetAngularVelocity(float value)
		{
			angularVelocity = value;
		}

		public override float GetAngularVelocity()
		{
			return angularVelocity;
		}

		public override void Step(float dt)
		{
			x += velocity_x;
			y += velocity_y;

			angle += angularVelocity;
		}

		public override void Draw()
		{
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();

		//	if (IsCamera)
		//		Gl.glTranslated(x + game.GetCamera().GetX(), y + game.GetCamera().GetY(), 0);
			Gl.glTranslated(x, y, 0);
			
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);

			Gl.glScaled(scaleX, scaleY, 0);
			Gl.glRotated(angle * (180 / (float)System.Math.PI), 0, 0, 1);
			Gl.glColor3f(100, 0, 0);

			Gl.glBegin(Gl.GL_QUADS);

			Gl.glTexCoord2f(0.0f, 0.0f);
			Gl.glVertex2f(-width / 2, -height / 2);
			Gl.glTexCoord2f(1.0f, 0.0f);
			Gl.glVertex2f(width / 2, -height / 2);
			Gl.glTexCoord2f(1.0f, 1.0f);
			Gl.glVertex2f(width / 2, height / 2);
			Gl.glTexCoord2f(0.0f, 1.0f);
			Gl.glVertex2f(-width / 2, height / 2);
			Gl.glEnd();
		}

		public override void Dispose()
		{
		}

		public uint GetTexture()
		{
			return texture;
		}

		public void SetTexture(uint texture)
		{
			this.texture = texture;
		}

		public void SetWidth(float value)
		{
			width = value;
		}

		public void SetHeight(float value)
		{
			height = value;
		}

		public float GetWidth()
		{
			return width;
		}

		public float GetHeight()
		{
			return height;
		}

		public override void SetScaleX(float value)
		{
			scaleX = value;
		}

		public override void SetScaleY(float value)
		{
			scaleY = value;
		}

		public override void SetEnableCamera(bool value)
		{
			IsCamera = value;
		}

		public override float GetScaleX()
		{
			return scaleX;
		}

		public override float GetScaleY()
		{
			return scaleY;
		}

		/// <summary>
		/// Конвертирует Bitmap объект в текстуру.
		/// Не рекомендуется использовать с форматом *png
		/// </summary>
		/// <param name="bitmap_image">Изображение</param>
		/// <returns></returns>
		public static uint ConvertToTexture(Bitmap bitmap_image)
		{
			uint[] textureIds = new uint[1];
	
			Gl.glGenTextures(1, textureIds);
			System.Drawing.Imaging.BitmapData bitmap_data = bitmap_image.LockBits(
				new Rectangle(0, 0, bitmap_image.Width, bitmap_image.Height),
				System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap_image.PixelFormat);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureIds[0]);
			Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, bitmap_image.Width, bitmap_image.Height,
				0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, bitmap_data.Scan0);
			Gl.glTexParameterf(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
			Gl.glTexParameterf(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);
			bitmap_image.Dispose();

			return textureIds[0];
		}

		/// <summary>
		/// Загружает текстуру в память
		/// </summary>
		/// <param name="path">Путь к текстуре</param>
		/// <returns></returns>
		public static uint LoadTexture(string path)
		{
			int imageID;

			Il.ilGenImages(1, out imageID);
			Il.ilBindImage(imageID);

			if (Il.ilLoadImage(path))
			{
				int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
				int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
				int bitsPX = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);

				switch (bitsPX)
				{
					case 24: return MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
					case 32: return MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
				}

				Il.ilDeleteImages(1, ref imageID);
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Класс: GLImage\nФункция: LoadImage()\nОшибка: Ну удалось газгрузить изображение по указоному пути\nПуть: " + path, "Ошибка загрузки текстуры", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
			}

			return 0;
		}

		/// <summary>
		/// Создает текстуру в памяти OpenGL
		/// </summary>
		/// <returns></returns>
		public static uint MakeGlTexture(int Format, System.IntPtr pixels, int w, int h)
		{
			uint texObject;
			Gl.glGenTextures(1, out texObject);
			Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);

			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_LINEAR);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_LINEAR);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);

			switch (Format)
			{
				case Gl.GL_RGB:
					Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
					break;

				case Gl.GL_RGBA:
					Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
					break;
			}
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);

			return texObject;
		}
	}
}