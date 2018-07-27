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

namespace pwsh.UI.Actionbar
{
	/// <summary>
	/// Interaction logic for Explorer_Snippets.xaml
	/// </summary>
	public partial class Explorer_Cmdlets : UserControl
	{
		public static ObservableCollection<DB.uipwsh_cmdlet> registrar { get; set; }
		
		public DB.uipwsh Console { get; set; }
		private bool IsEditing { get; set; }
		public Explorer_Cmdlets()
		{
			InitializeComponent();

			registrar = new ObservableCollection<DB.uipwsh_cmdlet>();
			//pwsh.Constants.Actionbar_Explorer_Cmdlets = this;

			DataContext = this;
		}
		public void UpdateDataContext()
		{
			List<DB.uipwsh_cmdlet> listToRemove = new List<DB.uipwsh_cmdlet>();

			// Update Each Table
			foreach (DB.uipwsh_cmdlet i in registrar) {
				if (! i.UpdateObject()) {
					listToRemove.Add(i);
				}
			}
			// Remove Each object in table
			foreach (DB.uipwsh_cmdlet i in listToRemove) {
				registrar.Remove(i);
			}
			Constants.UX_Editor.UpdateTabLayout();
		}
		public void SetDataContext(DB.uipwsh uipwsh) {
			Constants.DB_pwsh_cmdlet = Constants.DB_table_pwsh_cmdlet.RefreshRows();

			List<Object> cmdletlist = new List<object>();
			foreach (DB.pwsh_cmdlet i in Constants.DB_pwsh_cmdlet) {
				if (i.ID == uipwsh.UID) {
					cmdletlist.Add(i);
				}
			}
			SetDataContext(uipwsh, cmdletlist);
		}

		public void SetDataContext(DB.uipwsh uipwsh, List<object> pwsh_cmdletList)
		{
			// 
			Console = uipwsh;
			registrar = new ObservableCollection<DB.uipwsh_cmdlet>();
			foreach (DB.pwsh_cmdlet i in pwsh_cmdletList) {
				registrar.Add(new DB.uipwsh_cmdlet(i));
			}
			IsEditing = false;
			DataContext = null;
			DataContext = this;
		}
		private void Cmdlets_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Grid grid = (Grid)sender;
			if ((e.ClickCount == 2) & (IsEditing == false))
			{
				// Marks object as Editable.
				
				DockPanel dockPanel = (DockPanel)grid.Children[0];
				dockPanel.Children[1].Visibility = Visibility.Collapsed; // Sets the Label to Hidden
				dockPanel.Children[2].Visibility = Visibility.Visible;   // Sets the Textbox to Visible
				IsEditing = true;
				return;
			}
			if (IsEditing == true)
			{
				
				return;
			}

			if (Constants.UX_Console.Visibility == Visibility.Visible) {

				// Change to something better. 
				Constants.UX_Console.Add(Console);
				// Todo: Add Console Invoke
				MessageBox.Show("Todo: Add Console Invoke");

				return; // Return to stop default 
			}
			if (Constants.UX_Editor.Visibility == Visibility.Visible) {
				// Change to something better. 
				DB.uipwsh_cmdlet v = grid.DataContext as DB.uipwsh_cmdlet;
				Constants.UX_Editor.Add(v);
			}
		}

		private void Create_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (Console == null)
				return;

			DB.pwsh_cmdlet pwshCmdlet = Constants.DB_table_pwsh_cmdlet.AddRow();
			pwshCmdlet.ID = Console.UID;
			pwshCmdlet.NAME = "New Script";
			registrar.Add(new DB.uipwsh_cmdlet(pwshCmdlet));
			Constants.DB_table_pwsh_cmdlet.Save(pwshCmdlet);
		}
		private void Refresh_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			SetDataContext(Console);
		}

		private void Cmdlets_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			// Rename Cmdlet
			TextBox Sender = (TextBox)sender;
			if (e.Key == Key.Return) {
				DockPanel dockPanel = (DockPanel)Sender.Parent;
				dockPanel.Children[1].Visibility = Visibility.Visible;
				dockPanel.Children[2].Visibility = Visibility.Collapsed;
				IsEditing = false;

				DB.uipwsh_cmdlet cmdlet = Sender.DataContext as DB.uipwsh_cmdlet;
				cmdlet.NAME = Sender.Text;
				Constants.DB_table_pwsh_cmdlet.Save(cmdlet.GetObject());
				UpdateDataContext();
				e.Handled = true;
				return;
			}
		}
		private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
		{
			MenuItem Sender = (MenuItem)sender;
			DB.uipwsh_cmdlet cmdlet = Sender.DataContext as DB.uipwsh_cmdlet;
			Constants.DB_table_pwsh_cmdlet.RemoveRow(cmdlet.GetObject());
			
			SetDataContext(Console);
			Constants.UX_Editor.Remove(cmdlet);
			return;
		}
	}
}
