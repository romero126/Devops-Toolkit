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
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace core.UX
{
	/// <summary>
	/// Interaction logic for Sidebar.xaml
	/// </summary>
	public partial class Sidebar : UserControl
	{
		public ObservableCollection<SidebarItem> registrar { get; set; }

		public Sidebar()
		{
			InitializeComponent();
			registrar = new ObservableCollection<SidebarItem>();
			DataContext = this;
		}
		public void Add(SidebarItem value)
		{
			if (value.Name == null)
			{
				throw new ArgumentNullException("Name value expected");
			}
			foreach (SidebarItem i in registrar)
			{
				if (i.Name == value.Name)
				{
					return;
				}
			}
			if (value.Icon == null)
				value.Icon = (BitmapImage)Resources["I_Item_Default"];
			registrar.Add(value);
		}

		public void Remove(string name)
		{
			foreach (SidebarItem i in registrar)
			{
				if (i.Name == name)
				{
					registrar.Remove(i);
					return;
				}
			}
		}
		public SidebarItem Get(string name)
		{
			foreach (SidebarItem i in registrar)
			{
				if (i.Name == name)
				{
					return i;
				}
			}
			return null;
		}
		public ObservableCollection<SidebarItem> GetAll()
		{
			return registrar;
		}

		private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			SidebarItem v = ((Grid)sender).DataContext as SidebarItem;
			foreach (SidebarItem i in registrar)
			{
				if (i.uiElement != null)
				{
					i.uiElement.Visibility = Visibility.Hidden;
					i.isSelected = false;
				}
			}
			if (v.uiElement != null)
				v.uiElement.Visibility = Visibility.Visible;
			v.isSelected = true;
			DataContext = null;
			DataContext = this;
		}
	}
}
