using Dtwo.API.View;
using Dtwo.Plugins.MultiAccount.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Dtwo.Plugins.MultiAccount.View
{
	public static class MultiAccount_ViewPlugin_Manager
	{
        public static MultiAccountController? ShowedOptionsAccount { get; private set; }
		public static API.View.Window FloatingWindow { get; private set; }

		private static bool m_floatingWindowIsminimized = false;

        public static async void ShowFloatingWindow()
        {
            if (FloatingWindow != null)
                return;

			DEVMODE devMode = default;
			devMode.dmSize = (short)Marshal.SizeOf(devMode);
			EnumDisplaySettings(null, -1, ref devMode);

			double screenWidth = devMode.dmPelsWidth;
			double screenHeight = devMode.dmPelsHeight;

			double marginHeight = screenHeight / 8;

            var windowOptions = new API.View.Window.WindowOptions()
            {
				Width = 80,
				Height = screenHeight - (marginHeight * 2.0f),
                PositionX = screenWidth - 80,
				PositionY = marginHeight,

				HideTitleBar = true,
				Resizable = false,
				TopMost = true,

				Type=typeof(FloatingWindow),
				RenderPath = "plugins/MultiAccount.View/wwwroot/rightpanel.html",
				RenderSelectorId = "right-panel-window"
			};

            FloatingWindow = API.View.Window.CreateWindow(windowOptions, (hwnd) =>
			{
				RefreshFloatingWindowSize();
			});
        }

        public static async void ShowWindowOptions(MultiAccountController controller)
        {
            //if (m_windowOptions == null)
            //{
            //    ShowedOptionsAccount = controller;

            //    Console.WriteLine("show window " + GetOptionsURL);
            //    int index = MultiAccountManager.IndexOfAccount(controller);
            //    m_windowOptions = await ElectronNET.API.Electron.WindowManager.CreateWindowAsync(await GetOptionsWindowOptions(index), GetOptionsURL);

            //    m_windowOptions.SetContentProtection(true);
            //}
        }


		public static void Init()
		{
			MultiAccountManager.OnAddAccount += OnAddAccount;
			MultiAccountManager.OnRemoveAccount += OnRemoveAccount;
		}

        public static void CloseWindow()
		{
            //if (m_window != null)
            //{
            //    ShowedOptionsAccount = null;

            //    m_window.Destroy();
            //    m_window = null;
            //}
        }

		public static void MinimizeWindow()
		{
			m_floatingWindowIsminimized = true;
            RefreshFloatingWindowSize();
            // todo : disable overflow (scroll)
        }

		public static void MaximizeWindow()
		{
            //API.View.Window.ResizeWindow(FloatingWindow, new API.View.Window.ResizeWindowOptions() { Width = FloatingWindow.Options.Width, Height = FloatingWindow.Options.Height });
            m_floatingWindowIsminimized = false;
            RefreshFloatingWindowSize();
        }

        public static void CloseWindowOptions()
        {
            //if (m_windowOptions != null)
            //{
            //    m_windowOptions.Destroy();
            //    m_windowOptions = null;
            //}
        }

		private static void OnAddAccount()
		{
			if (m_floatingWindowIsminimized)
				return;

			RefreshFloatingWindowSize();
        }

		private static void OnRemoveAccount()
		{
            if (m_floatingWindowIsminimized)
                return;

            RefreshFloatingWindowSize();
        }

		private static void RefreshFloatingWindowSize()
		{
            FloatingWindow.Resize(new Window.ResizeWindowOptions()
            {
                Height = m_floatingWindowIsminimized ? 30 : (MultiAccountManager.Accounts.Count * FloatingWindow.Options.Width) + 30,
                Width = FloatingWindow.Options.Width
            });
        }

		[DllImport("user32.dll")]
		static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

		[StructLayout(LayoutKind.Sequential)]
		struct DEVMODE
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
			public string dmDeviceName;
			public short dmSpecVersion;
			public short dmDriverVersion;
			public short dmSize;
			public short dmDriverExtra;
			public int dmFields;
			public int dmPositionX;
			public int dmPositionY;
			public int dmDisplayOrientation;
			public int dmDisplayFixedOutput;
			public short dmColor;
			public short dmDuplex;
			public short dmYResolution;
			public short dmTTOption;
			public short dmCollate;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
			public string dmFormName;
			public short dmLogPixels;
			public int dmBitsPerPel;
			public int dmPelsWidth;
			public int dmPelsHeight;
			public int dmDisplayFlags;
			public int dmDisplayFrequency;
			public int dmICMMethod;
			public int dmICMIntent;
			public int dmMediaType;
			public int dmDitherType;
			public int dmReserved1;
			public int dmReserved2;
			public int dmPanningWidth;
			public int dmPanningHeight;
		}
	}
}
