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

namespace core.UX
{
	/// <summary>
	/// Interaction logic for Actionbar.xaml
	/// </summary>
	public partial class Actionbar : UserControl
	{

		public Actionbar()
		{
			InitializeComponent();
		}
		public void Add(UIElement element) {
			if (content.Children.Contains(element)) {
				return;
			}
			content.Children.Add(element);
		}
		public void Remove(UIElement element) {
			if (content.Children.Contains(element))
			{
				content.Children.Remove(element);
			}
		}
		public void HideAll() {
			foreach (UIElement i in content.Children)
			{
				i.Visibility = Visibility.Hidden;
			}
		}

		/*
		public static ObservableCollection<SidebarItem> registrar { get; set; }
		//public static List<UIElement> registrar { get; set; }
		public Actionbar()
		{
			InitializeComponent();
			registrar = new ObservableCollection<SidebarItem>();
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
					break;
				}
			}
			registrar.Add(value);
			value.uiElement.Visibility = Visibility.Hidden;
			content.Children.Add(value.uiElement);
		}
		public void Remove(string name)
		{
			foreach (SidebarItem i in registrar)
			{
				if (i.Name == name)
				{
					registrar.Remove(i);
					content.Children.Remove(i.uiElement);
					break;
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
		*/
	}
}
