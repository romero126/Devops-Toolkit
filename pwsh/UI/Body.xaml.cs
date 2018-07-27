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

namespace pwsh.UI
{
	/// <summary>
	/// Interaction logic for Body.xaml
	/// </summary>
	public partial class Body : UserControl
	{
		public Body()
		{
			InitializeComponent();
			Constants.UX_Body = this;
		}
		public void Add(UIElement element) {
			if (items.Children.Contains(element)) {
				return;
			}
			items.Children.Add(element);
		}
		public void Remove(UIElement element) {
			if (items.Children.Contains(element)) {
				items.Children.Remove(element);
			}
		}
		public void HideAll() {
			foreach (UIElement i in items.Children) {
				i.Visibility = Visibility.Hidden;
			}
		}
		public void Collapse(UIElement element)
		{
			element.Visibility = Visibility.Collapsed;
		}
		public void Show(UIElement element) {
			element.Visibility = Visibility.Visible;
		}
	}
}
