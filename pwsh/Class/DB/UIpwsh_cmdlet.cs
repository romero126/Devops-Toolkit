using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace pwsh.DB
{
	public class uipwsh_cmdlet
	{
		private DB.pwsh_cmdlet dbo;

		public uipwsh_cmdlet(pwsh_cmdlet obj)
		{
			dbo = obj;
			// Initiate Console
			// 
		}
		public bool UpdateObject() {
			foreach (DB.pwsh_cmdlet i in Constants.DB_pwsh_cmdlet)
			{
				if (i.UID == dbo.UID)
				{
					dbo = i;
					return true;
				}
			}
			return false;
		}
		public bool UpdateObject(pwsh_cmdlet obj)
		{
			if (obj.UID == dbo.UID) {
				dbo = obj;
				return true;
			}
			return false;
		}
		public DB.pwsh_cmdlet GetObject() {
			return dbo;
		}
		public void UpdateObjects() {
			// #Todo: Build Function
		}
		public List<object> FindObjectsByUID(string value) {
			List<object> result = new List<object>();
			foreach (DB.pwsh_cmdlet i in Constants.DB_pwsh_cmdlet) {
				if (i.UID == value)
				{
					result.Add(new DB.uipwsh_cmdlet(i));
				}
			}
			return result;
		}
		public List<object> FindObjectsByID(string value)
		{
			List<object> result = new List<object>();
			foreach (DB.pwsh_cmdlet i in Constants.DB_pwsh_cmdlet)
			{
				if (i.ID == value)
				{
					result.Add(new DB.uipwsh_cmdlet(i));
				}
			}
			return result;
		}
		public List<object> FindObjectsByNAME(string value)
		{
			List<object> result = new List<object>();
			foreach (DB.pwsh_cmdlet i in Constants.DB_pwsh_cmdlet)
			{
				if (i.NAME == value)
				{
					result.Add(new DB.uipwsh_cmdlet(i));
				}
			}
			return result;
		}
		public bool Equals(DB.uipwsh_cmdlet obj) {
			if (obj == null) {
				return false;
			}
			return (obj.UID == UID);
		}
		public void RemoveObject() {
			Constants.DB_table_pwsh_cmdlet.RemoveRow(dbo);
			Constants.DB_pwsh_cmdlet.Remove(dbo);
		}

		public string UID
		{
			get { return dbo.UID; }
			set { dbo.UID = value; }
		}
		public string NAME
		{
			get { return dbo.NAME; }
			set { dbo.NAME = value; }
		}
		public string ID
		{
			get { return dbo.ID; }
			set { dbo.ID = value; }
		}
		public string SCRIPT
		{
			get { return dbo.SCRIPT; }
			set { dbo.SCRIPT = value; }
		}
		public BitmapImage ICON
		{
			get
			{
				if (dbo.ICON == null)
				{
					BitmapImage bitmap = new BitmapImage();
					bitmap.BeginInit();
					bitmap.UriSource = new Uri("/pwsh;component/Images/Color/appbar.symbol.braces.png", UriKind.RelativeOrAbsolute);
					bitmap.EndInit();
					return bitmap;
				}
				using (System.IO.MemoryStream ms = new System.IO.MemoryStream(dbo.ICON))
				{
					BitmapImage image = new BitmapImage();
					image.BeginInit();
					image.StreamSource = ms;
					image.EndInit();
					return image;
				}
			}
		}
	}
}
