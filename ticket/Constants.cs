using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ticket
{
	public static class Constants
	{
		public static TextBox Log;
		public static void AddLog(string text) {
			Log.Text += string.Format("\r\nLOG: {0}", text);
		}
	}
}
