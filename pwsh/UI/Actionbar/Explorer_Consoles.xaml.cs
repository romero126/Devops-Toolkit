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
using System.Collections;
using System.Collections.ObjectModel;
using pwsh;

namespace pwsh.UI.Actionbar
{
	/// <summary>
	/// Interaction logic for Explorer_Consoles.xaml
	/// </summary>
	public partial class Explorer_Consoles : UserControl
	{
		public static ObservableCollection<DB.uipwsh> registrar { get; set; }
		private bool IsEditing { get; set; }
		public Explorer_Cmdlets CmdletControl { get; set; }

		public Explorer_Consoles()
		{
			InitializeComponent();
			registrar = new ObservableCollection<DB.uipwsh>();
			//pwsh.Constants.Actionbar_Explorer_Consoles = this;

			DataContext = this;
		}
		public void UpdateDataContext()
		{
			// Refreshes all data in helper objects
			List<object> querydata = DB.uipwsh.GetObjects();
			List<object> registrar_cache = registrar.ToList<object>();
			registrar = new ObservableCollection<DB.uipwsh>();

			foreach (DB.uipwsh i in querydata)
			{
				DB.uipwsh nvalue = DB.uipwsh.FindObjectByUID(i.UID, registrar_cache);
				if (nvalue == null) {
					registrar.Add(i);
				} else {
					registrar.Add(nvalue);
				}
			}

			DataContext = null;
			DataContext = this;
		}

		private void Console_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Grid grid = (Grid)sender;
			if ((e.ClickCount == 2) & (IsEditing == false))
			{
				// Set from Not Editing, to Editing Mode. <Swap object visibility>
				DockPanel dockPanel = (DockPanel)grid.Children[0];
				dockPanel.Children[1].Visibility = Visibility.Collapsed;
				dockPanel.Children[2].Visibility = Visibility.Visible;
				IsEditing = true;
				return;
			}
			if (IsEditing == true)
			{
				// Check if Item is editing.
				return;
			}
			if (Constants.UX_Console.Visibility == Visibility.Visible)
			{
				// Loads the Console Object and creates a TAB. 
				DB.uipwsh v = grid.DataContext as DB.uipwsh;
				Constants.UX_Console.Add(v);

			}
			DB.uipwsh uipwsh = ((Grid)sender).DataContext as DB.uipwsh;
			CmdletControl.SetDataContext(uipwsh);
		}
		private void Refresh_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			// Refresh Objects in DataModel 
			DB.uipwsh.UpdateObjects();
			UpdateDataContext();
		}

		private void Create_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			// Create a New Entry.
			DB.pwsh pwsh = Constants.DB_table_pwsh.AddRow();
			pwsh.NAME = "New Console";
			registrar.Add(new DB.uipwsh(pwsh));
			Constants.DB_table_pwsh.Save(pwsh);
		}
		private void Console_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			// Rename Object
			TextBox Sender = (TextBox)sender;
			if (e.Key == Key.Return)
			{
				// When you hit return it saves, and swaps from Textbox to Label.
				DockPanel dockPanel = (DockPanel)Sender.Parent;
				dockPanel.Children[1].Visibility = Visibility.Visible;
				dockPanel.Children[2].Visibility = Visibility.Collapsed;
				IsEditing = false;
				DB.uipwsh pwsh = Sender.DataContext as DB.uipwsh;
				pwsh.NAME = Sender.Text;
				Constants.DB_table_pwsh.Save(pwsh.GetObject());
				
				Constants.DB_pwsh = Constants.DB_table_pwsh.RefreshRows();
				UpdateDataContext();
				e.Handled = true;
				return;
			}

		}
		private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
		{
			MenuItem Sender = (MenuItem)sender;
			DB.uipwsh pwsh = Sender.DataContext as DB.uipwsh;

			// Drops all tables inside the commandlet
			// <Keeps SQLite Database Clean>
			List<DB.pwsh_cmdlet> cmdlets = new List<DB.pwsh_cmdlet>();
			foreach (DB.pwsh_cmdlet i in Constants.DB_pwsh_cmdlet)
			{
				if (pwsh.UID == i.ID)
				{
					cmdlets.Add(i);
				}
			}
			foreach (DB.pwsh_cmdlet i in cmdlets)
			{
				Constants.DB_table_pwsh_cmdlet.RemoveRow(i);
			}
			// Finally drop table
			DB.uipwsh.RemoveObject(pwsh);
			//Constants.DB_table_pwsh.RemoveRow(pwsh.GetObject());
			Constants.DB_pwsh = Constants.DB_table_pwsh.RefreshRows();

			//Update DataContext
			UpdateDataContext();
			CmdletControl.UpdateDataContext();
			//Constants.UX_Console.Remove(pwsh);

			return;
		}
	}
}
