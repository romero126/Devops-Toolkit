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
	/// Interaction logic for Tabs.xaml
	/// </summary>
	public partial class Tabs : UserControl
	{

		public static ObservableCollection<DB.uipwsh_cmdlet> registrar { get; set; }

		public Tabs()
		{
			InitializeComponent();
			registrar = new ObservableCollection<DB.uipwsh_cmdlet>();
			DataContext = this;
		}
		public void Add(DB.uipwsh_cmdlet value)
		{
			foreach (TabItem i in Tab.Items)
			{
				if ((i.DataContext as DB.uipwsh_cmdlet).UID == value.UID)
				{
					i.IsSelected = true;
					return;
				}
			}

			TabItem item = new TabItem();
			item.Header = value;
			UI.IDE ide = new UI.IDE();
			ide.SetCmdlet(value);
			item.Content = ide;
			item.DataContext = value;
			Tab.Items.Add(item);

			registrar.Add(value);
			item.IsSelected = true;
		}
		public void Remove(DB.uipwsh_cmdlet value)
		{
			foreach (TabItem item in Tab.Items)
			{
				if (item.DataContext == value)
				{
					Tab.Items.Remove(item);
					registrar.Remove(item.DataContext as DB.uipwsh_cmdlet);
					break;
				}
			}
		}
		public ObservableCollection<DB.uipwsh_cmdlet> GetAll()
		{
			return registrar;
		}
		private void TabItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Remove((sender as Image).DataContext as DB.uipwsh_cmdlet);
		}
	}
}
