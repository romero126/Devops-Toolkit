using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace DevOps_Toolkit.UI
{
	public class SidebarItem
	{
		public bool isSelected { get; set; }
		public string Name { get; set; }
		public UserControl uiElement { get; set; }
		public BitmapImage Icon { get; set; }
		public ToolTip Tooltip { get; set; }

		public SidebarItem(UserControl element)
		{
			this.uiElement = element;
		}
	}
}
