using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pwsh
{
	/// <summary>
	/// Interaction logic for UX.xaml
	/// </summary>
	public partial class UX : UserControl
	{
		public UX()
		{
			InitializeComponent();
			//core.globals.Add("pwsh:IDE", this.IDE);
			core.globals.Add("pwsh:SIDEBAR", this.SIDEBAR);

			ACTIONBAR.Add(new UI.Actionbar.Explorer());
			{
				// Instantiate Editor Button
				var v = new core.UX.SidebarItem(new UI.Editor());
				v.isSelected = false;
				v.Name = "Editor";
				BitmapImage bitmap = new BitmapImage();
				bitmap.BeginInit();
				bitmap.UriSource = new Uri("/pwsh;component/Images/Color/appbar.page.text.png", UriKind.RelativeOrAbsolute);
				bitmap.EndInit();
				v.Icon = bitmap;
				SIDEBAR.Add(v);
				Body.Add(v.uiElement);
			}
			{
				// Instantiate Editor Button
				var v = new core.UX.SidebarItem(new UI.Console());
				v.isSelected = false;
				v.Name = "Console";
				BitmapImage bitmap = new BitmapImage();
				bitmap.BeginInit();
				bitmap.UriSource = new Uri("/pwsh;component/Images/Color/appbar.powershell.png", UriKind.RelativeOrAbsolute);
				bitmap.EndInit();
				v.Icon = bitmap;
				SIDEBAR.Add(v);
				Body.Add(v.uiElement);
			}
			Body.HideAll();
			Body.Show(Constants.UX_Editor);
		}

		private void UX_Loaded(object sender, RoutedEventArgs e)
		{
			Constants.InitializeComponents();
		}
	}
}
