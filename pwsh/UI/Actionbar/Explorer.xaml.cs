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

namespace pwsh.UI.Actionbar
{
	/// <summary>
	/// Interaction logic for Explorer.xaml
	/// </summary>
	public partial class Explorer : UserControl
	{
		public Explorer()
		{
			InitializeComponent();
			pwsh.Constants.Actionbar_Explorer_Consoles = Consoles;
			Consoles.CmdletControl = Cmdlets;
			pwsh.Constants.Actionbar_Explorer_Cmdlets = Cmdlets;
		}
	}
}
