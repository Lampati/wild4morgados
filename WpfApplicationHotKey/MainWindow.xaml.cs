using System;
using System.Windows;
using System.Windows.Input;
using WpfApplicationHotKey.WinApi;

namespace WpfApplicationHotKey
{
	/// <summary>
	/// 	Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private HotKey _hotkey;

		public MainWindow()
		{
			InitializeComponent();
			Loaded += (s, e) =>
			          	{
			          		_hotkey = new HotKey(ModifierKeys.Windows | ModifierKeys.Alt, Keys.Left, this);
			          		_hotkey.HotKeyPressed += (k) => Console.Beep();
			          	};
		}
	}
}