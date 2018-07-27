using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
namespace pwsh.DB
{
	public class uipwsh
	{
		private pwsh dbo;
		private object console; // Placeholder for Powershell Console Library

		public uipwsh() {
			
		}
		public uipwsh(pwsh obj) {
			dbo = obj;
		}
		public bool UpdateObject()
		{
			foreach (DB.pwsh i in Constants.DB_pwsh)
			{
				if (i.UID == dbo.UID)
				{
					dbo = i;
					return true;
				}
			}
			return false;
		}
		public bool UpdateObject(DB.pwsh obj)
		{
			if (obj.UID == dbo.UID)
			{
				dbo = obj;
				return true;
			}
			return false;
		}

		#region StaticHelperObjects
		
		public static List<object> GetObjects() {
			return GetObjects(Constants.DB_pwsh);
		}
		public static List<object> GetObjects(List<object> list)
		{
			List<object> result = new List<object>();
			foreach (DB.pwsh i in list)
			{
				result.Add(new DB.uipwsh(i));
			}
			return result;
		}
		public static DB.uipwsh FindObjectByUID(string value)
		{
			return FindObjectByUID(value, GetObjects(Constants.DB_pwsh));
		}
		public static DB.uipwsh FindObjectByUID(string value, List<object> list)
		{
			foreach (DB.uipwsh i in list)
			{
				if (i.UID == value)
				{
					return i;
				}
			}
			return null;
		}
		public static List<object> FindObjectsByID(string value)
		{
			return FindObjectsByID(value, GetObjects(Constants.DB_pwsh));
		}
		public static List<object> FindObjectsByID(string value, List<object> list)
		{
			List<object> result = new List<object>();
			foreach (DB.pwsh i in list)
			{
				if (i.ID == value)
				{
					result.Add(new DB.uipwsh(i));
				}
			}
			return result;
		}
		public static List<object> FindObjectsByNAME(string value)
		{
			return FindObjectsByNAME(value, GetObjects(Constants.DB_pwsh));
		}
		public static List<object> FindObjectsByNAME(string value, List<object> list)
		{
			List<object> result = new List<object>();
			foreach (DB.pwsh i in list)
			{
				if (i.NAME == value)
				{
					result.Add(new DB.uipwsh(i));
				}
			}
			return result;
		}
		public static void UpdateObjects() {
			Constants.DB_pwsh = Constants.DB_table_pwsh.RefreshRows();
		}
		public static void RemoveObject(DB.uipwsh obj)
		{
			Constants.DB_table_pwsh.RemoveRow(obj.dbo);
			Constants.DB_pwsh.Remove(obj.dbo);
		}

		#endregion StaticHelperObjects
		public bool Equals(DB.pwsh obj)
		{
			// Todo : Delete me

			if (obj == null)
			{
				return false;
			}
			return (obj.UID == UID);
		}

		public DB.pwsh GetObject()
		{
			return dbo;
		}
		public string UID
		{
			get { return dbo.UID; }
			set { dbo.UID = value; }
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
		public string NAME
		{
			get { return dbo.NAME; }
			set { dbo.NAME = value; }
		}
		public BitmapImage ICON
		{
			get
			{
				if (dbo.ICON == null) {
					BitmapImage bitmap = new BitmapImage();
					bitmap.BeginInit();
					bitmap.UriSource = new Uri("/pwsh;component/Images/White/appbar.console.png", UriKind.RelativeOrAbsolute);
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
