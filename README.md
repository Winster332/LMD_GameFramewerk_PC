# LMD_GameFramewerk_PC
Это фреймверк для разработки 2D игр с поддержкой OpenGL, Box2D и прочим функционалом. Данный проект находится в разработке, по этому в некоторых вырсиях определенный функционал может работать не правильно.

## Функционал на данный момент
* Поддержка физики, Box2D
* Вывод графики через OpenGL
* Логгирование
* Система частиц
* Камера
* Собственная реализация и возможность расширение UI

## Пример инициализации
```C#
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
			game.SetStartScreen(new GameEngine.Windows.ScreenGame(game));
			Application.Run(game);
		}
	}
```

## API Класс GGame
/// <summary>
/// Возвращает страртовый экран
/// </summary>
/// <returns></returns>
Screen GetStartScreen();
/// <summary>
/// Возвращает страртовый экран
/// </summary>
/// <returns></returns>
Screen GetCurrentScreen();
/// <summary>
/// Устанавливает активный экран
/// </summary>
/// <param name="screen">Экран который будет установлен</param>
void SetScreen(Screen screen);
/// <summary>
/// Возвращает указатель на объект класса ввода
/// </summary>
/// <returns></returns>
IInput GetInput();
/// <summary>
/// Возвращает указатель на объект класса графики
/// </summary>
/// <returns></returns>
IGraphics GetGraphics();
/// <summary>
/// Возвращает указатель на объект класса отвечающий за I/O данных файловой системы
/// </summary>
/// <returns></returns>
IFileIO GetFileIO();
/// <summary>
/// Возвращает указатель на объект класса по обработки физический процессов
/// </summary>
// <returns></returns>
IPhysics GetPhysics();
/// <summary>
/// Возвращает указатель на объект класса отвечающего за обработку звуковых эффектов
/// </summary>
/// <returns></returns>
IAudio GetAudio();
/// <summary>
/// Возвращает указатель на объект отвечающий за положение элементов на экране
/// </summary>
/// <returns></returns>
ICamera GetCamera();
/// <summary>
/// Возвращает указатель на объект отвечающий за добавление частиц на экран
/// </summary>
/// <returns></returns>
ISystemParticles GetSystemParticles();
/// <summary>
/// Возвращает ширину окна
/// </summary>
/// <returns></returns>
float GetWindowWidth();
/// <summary>
/// Возвращает высоту окна
/// </summary>
/// <returns></returns>
float GetWindowHeight();
/// <summary>
/// Указывает стартовый экран
/// </summary>
/// <param name="screen">Объект экрана</param>
void SetStartScreen(Screen screen);
