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

namespace ticket
{
	/// <summary>
	/// Interaction logic for UX.xaml
	/// </summary>
	public partial class UX : UserControl
	{
		public UX()
		{
			InitializeComponent();
			Constants.Log = Log;

			Constants.AddLog("Setting Type of Object");
			Classes.DB.ticket.SetType(typeof(Classes.DB.ticket)); // This is hacky
			Constants.AddLog("Initializing Component");
			Classes.DB.ticket.Initialize();
			Constants.AddLog("Creating Table if not exists");
			Classes.DB.ticket.CreateTable();
			Constants.AddLog("Adding object into Database");
			/*
			Classes.DB.ticket v = Classes.DB.ticket.AddRow();
			v.TicketID = "12345";
			v.ENV = Guid.NewGuid().ToString();
			Classes.DB.ticket.Save(v);
			*/
			Constants.AddLog("Retrieving Rows");

			foreach (Classes.DB.ticket i in Classes.DB.ticket.RefreshRows())
			{
				Constants.AddLog(string.Format("Record: UID {0}", i.UID));
				Constants.AddLog(string.Format("\t TicketID: {0}", i.TicketID));
				Constants.AddLog(string.Format("\t ENV: {0}", i.ENV));
				Constants.AddLog("");
			}
			Constants.AddLog("Finalizing");
		}
	}
}
