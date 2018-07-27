﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console
{
	public class init : core.DLL.Initializer
	{
		public override void RegisterComponents()
		{

		}
		public override void InitializeComponents()
		{

		}
		public override UserControl GetUI()
		{
			UserControl control = new UX();
			return control;
		}
		public override BitmapImage GetIcon()
		{
			BitmapImage bitmap = new BitmapImage();
			bitmap.BeginInit();
			bitmap.UriSource = new Uri("/pwkeeper;component/appbar.unlock.keyhole.png", UriKind.RelativeOrAbsolute);
			bitmap.EndInit();
			return bitmap;
		}
	}
}
