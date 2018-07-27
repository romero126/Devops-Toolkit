using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DllDependencies
{
    public abstract class init
    {
		public abstract void start();
		public abstract UserControl GetUI();
		public abstract BitmapImage GetIcon();
	}
}
