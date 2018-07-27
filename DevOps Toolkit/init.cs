using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using core;
using pwsh;
using ticket;
using notesdb;

namespace DevOps_Toolkit
{
	public static class Init
	{
		public static void Start() {
			core.DB.Core2.Init();
			MainWindow winMain = (MainWindow)core.globals.Get("UI:Window");
			//Register("shell", new shell.init());
			Register("shell", new shell.Init());
			//Register("Teams");
			// Loads the PowerShell Module used in executing scripts
			Register("pwsh", new pwsh.Init());
			
			// Loads a Note Taking Database used for taking copius ammounts of notes.
			Register("NotesDB", new notesdb.Init());
			// Used in keeping passwords safe
			Register("PWKeeper", new pwkeeper.Init());

			// Used in keeping changelog notes, for server entities.
			Register("TicketA", new ticket.Init());
			Register("TicketB", new ticket.Init());
			Register("TicketC", new ticket.Init());
			Register("TicketD", new ticket.Init());
			// Notes Search?
		}
		public static void Register(string Name, core.DLL.Initializer obj) {
			MainWindow winMain = (MainWindow)core.globals.Get("UI:Window");
			obj.RegisterComponents();
			var v = new UI.SidebarItem(obj.GetUI());
			v.isSelected = false;
			v.Name = Name;
			v.Icon = obj.GetIcon();
			winMain.Sidebar.Add(v);
			winMain.Body.Add(v);
			core.globals.Add(String.Format("UI:Sidebar:{0}", Name), winMain.Sidebar.Get(Name));
			core.globals.Add(String.Format("UI:Body:{0}", Name), winMain.Body.Get(Name));
		}
	}
}
