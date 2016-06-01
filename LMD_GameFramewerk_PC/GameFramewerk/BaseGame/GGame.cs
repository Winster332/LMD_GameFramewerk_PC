using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using LMD_GameFramewerk_PC.GameFramewerk.BaseGame.Physics;
using LMD_GameFramewerk_PC.GameFramewerk.Windows;
using Tao.OpenGl;
using Tao.FreeGlut;
using System;

namespace LMD_GameFramewerk_PC.GameFramewerk.BaseGame
{
	public class GGame : Form, IGame
	{
		#region variables
		private Windows.ManagementScreen MScreen;
		private IAudio audio;
		private IFileIO fileIO;
		private IGraphics graphics;
		private IInput input;
		private IPhysics physics;
		private ICamera camera;
		private ISystemParticles systemParticles;
		private float DeltaTime;
		public Tao.Platform.Windows.SimpleOpenGlControl glView;
		private float PrevDeltaTime;
		#endregion

		/// <summary>
		/// Инициализирует класс GGame
		/// </summary>
		public GGame(string title, int width, int height)
		{
			InitializeComponent();
			glView.InitializeContexts();

			MScreen = new ManagementScreen(this);

			this.Text = title;
			this.Width = width;
			this.Height = height;
			this.glView.Size = this.Size;
		}

		private void GlView_Paint(object sender, PaintEventArgs e)
		{
			DeltaTime = (float)(System.DateTime.Now.Millisecond / PrevDeltaTime); // Вычисляем дельту времени
			PrevDeltaTime = System.DateTime.Now.Millisecond;

			// Обновляем и перерисовываем активный экран
			MScreen.Step(DeltaTime);

			graphics.BeginRender();

			MScreen.Draw();

			graphics.EndRender();
		}

		/// <summary>
		/// Возвращает страртовый экран
		/// </summary>
		/// <returns></returns>
		public Screen GetStartScreen()
		{
			return MScreen.GetMainScreen();
		}

		/// <summary>
		/// Возвращает активный экран
		/// </summary>
		/// <returns></returns>
		public Screen GetCurrentScreen()
		{
			return MScreen.GetCurrentScreen();
		}

		/// <summary>
		/// Устанавливает активный экран
		/// </summary>
		/// <param name="screen">Экран который будет установлен</param>
		public void SetScreen(Screen screen)
		{
			MScreen.SetCurrentScreen(screen);
		}

		/// <summary>
		/// Возвращает указатель на объект класса ввода
		/// </summary>
		/// <returns></returns>
		public IInput GetInput()
		{
			return input;
		}

		/// <summary>
		/// Возвращает указатель на объект класса графики
		/// </summary>
		/// <returns></returns>
		public IGraphics GetGraphics()
		{
			return graphics;
		}

		/// <summary>
		/// Возвращает указатель на объект класса отвечающий за I/O данных файловой системы
		/// </summary>
		/// <returns></returns>
		public IFileIO GetFileIO()
		{
			return fileIO;
		}

		/// <summary>
		/// Возвращает указатель на объект класса по обработки физический процессов
		/// </summary>
		/// <returns></returns>
		public IPhysics GetPhysics()
		{
			return physics;
		}

		/// <summary>
		/// Возвращает указатель на объект класса отвечающего за обработку звуковых эффектов
		/// </summary>
		/// <returns></returns>
		public IAudio GetAudio()
		{
			return audio;
		}

		public ICamera GetCamera()
		{
			return camera;
		}

		public ISystemParticles GetSystemParticles()
		{
			return systemParticles;
		}

		public float GetWindowWidth()
		{
			return this.Width;
		}

		public float GetWindowHeight()
		{
			return this.Height;
		}

		#region Mouse to current screen
		private void GGame_MouseMove(object sender, MouseEventArgs e)
		{
			GetCurrentScreen().TouchMove(e);
		}

		private void GGame_MouseUp(object sender, MouseEventArgs e)
		{
			GetCurrentScreen().TouchUp(e);
		}

		private void GGame_MouseDown(object sender, MouseEventArgs e)
		{
			GetCurrentScreen().TouchDown(e);
		}
		#endregion

		private void InitializeComponent()
		{
			this.glView = new Tao.Platform.Windows.SimpleOpenGlControl();
			this.SuspendLayout();
			// 
			// glView
			// 
			this.glView.AccumBits = ((byte)(0));
			this.glView.AutoCheckErrors = false;
			this.glView.AutoFinish = false;
			this.glView.AutoMakeCurrent = true;
			this.glView.AutoSwapBuffers = true;
			this.glView.BackColor = System.Drawing.Color.Black;
			this.glView.ColorBits = ((byte)(32));
			this.glView.DepthBits = ((byte)(16));
			this.glView.Location = new System.Drawing.Point(0, 0);
			this.glView.Name = "glView";
			this.glView.Size = new System.Drawing.Size(285, 262);
			this.glView.StencilBits = ((byte)(0));
			this.glView.TabIndex = 0;
			this.glView.Load += new System.EventHandler(this.glView_Load);
			// 
			// GGame
			// 
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.glView);
			this.Name = "GGame";
			this.Load += new System.EventHandler(this.GGame_Load);
			this.ResumeLayout(false);

		}

		private void glView_Load(object sender, System.EventArgs e)
		{
			
		}

		private void GGame_Load(object sender, System.EventArgs e)
		{
			audio = new GAudio();
			fileIO = new GFileIO();
			graphics = new GGraphics(this);
			input = new GInput();
			physics = new GPhysics(this);
			camera = new GCamera();
			systemParticles = new GSystemParticles(this);

			MScreen.SetCurrentScreen(GetStartScreen());

			DoubleBuffered = true;
			glView.Paint += GlView_Paint;
			glView.MouseDown += GGame_MouseDown;
			glView.MouseUp += GGame_MouseUp;
			glView.MouseMove += GGame_MouseMove;
			glView.KeyDown += GlView_KeyDown;
			glView.KeyUp += GlView_KeyUp;

			DeltaTime = System.DateTime.Now.Millisecond;
			PrevDeltaTime = System.DateTime.Now.Millisecond;
		}

		private void GlView_KeyUp(object sender, KeyEventArgs e)
		{
			MScreen.GetCurrentScreen().KeyboardUp(e);
		}

		private void GlView_KeyDown(object sender, KeyEventArgs e)
		{
			MScreen.GetCurrentScreen().KeyboardDown(e);
		}

		public void SetStartScreen(Screen screen)
		{
			MScreen.SetMainScreen(screen);
		}
	}
}