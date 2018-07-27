using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace core.DLL
{
	public abstract class Initializer
	{
		public abstract void RegisterComponents();
		public abstract void InitializeComponents();

		public abstract UserControl GetUI();
		public abstract BitmapImage GetIcon();
	}
}
