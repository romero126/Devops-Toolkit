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
	/// Interaction logic for Console.xaml
	/// </summary>
	public partial class Console : UserControl
	{
		public static ObservableCollection<DB.uipwsh> registrar { get; set; }

		public Console()
		{
			InitializeComponent();
			registrar = new ObservableCollection<DB.uipwsh>();
			DataContext = this;
			Constants.UX_Console = this;
		}
		public void UpdateTabLayout()
		{
			foreach (TabItem i in Tab.Items)
			{
				DB.uipwsh olddatacontext = i.Header as DB.uipwsh;
				olddatacontext.UpdateObject();
				i.Header = null;
				i.Header = olddatacontext;
			}
		}
		public void Add(DB.uipwsh value)
		{
			foreach (TabItem i in Tab.Items)
			{
				if ((i.DataContext as DB.uipwsh).UID == value.UID)
				{
					i.IsSelected = true;
					return;
				}
			}
			TabItem item = new TabItem();
			item.Header = value;
			//

			//UI.IDE ide = new UI.IDE();
			//ide.SetCmdlet(value);
			//
			//item.Content = ide;
			item.DataContext = value;
			Tab.Items.Add(item);

			registrar.Add(value);
			item.IsSelected = true;
		}
		public void Remove(DB.uipwsh value)
		{
			foreach (TabItem item in Tab.Items)
			{
				if (item.DataContext == value)
				{
					Tab.Items.Remove(item);
					registrar.Remove(item.DataContext as DB.uipwsh);
					break;
				}
			}
		}
		public ObservableCollection<DB.uipwsh> GetAll()
		{
			return registrar;
		}
		private void TabItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Remove((sender as Image).DataContext as DB.uipwsh);
		}
	}
}
