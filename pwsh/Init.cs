using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using core;
using System.Windows;
namespace pwsh
{
	public class Init : core.DLL.Initializer
	{
		public override void RegisterComponents()
		{
			Constants.RegisterComponents();
		}
		public override void InitializeComponents() {
			Constants.InitializeComponents();
		}
		public override UserControl GetUI()
		{
			UserControl control = new UX();
			return control;
		}
		public override BitmapImage GetIcon()
		{
			BitmapImage bitmap = new BitmapImage();
			bitmap.BeginInit();
			bitmap.UriSource = new Uri("/pwsh;component/Images/White/appbar.console.png", UriKind.RelativeOrAbsolute);
			bitmap.EndInit();
			return bitmap;
		}
	}
}
