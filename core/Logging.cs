using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public static class Logging
    {
        private static Dictionary<string, object> LogTypes = new Dictionary<string, object>();
        private static Dictionary<string, string> LogData = new Dictionary<string, string>();
        public static void AddPath(string name, string path)
        {
            Logging.AddPath(name, path);
        }
        public static void AddPath(string name, object path)
        {
            if (LogTypes.ContainsKey(name))
                LogTypes[name] = path;
            else
                LogTypes.Add(name, path);
        }
        public static void Log(string name, string format, params object[] args)
        {
            StackTrace stack = new StackTrace();
            Logging.Log(stack, name, string.Format(format, args));
        }
        public static void Log(string name, string value)
        {
            StackTrace stack = new StackTrace();
            Logging.Log(stack, name, value);
        }
        public static void Log(StackTrace stack, string name, string value)
        {
            if (!LogTypes.ContainsKey(name))
                return;
            System.Reflection.MethodBase method = stack.GetFrame(1).GetMethod();
            StringBuilder parameters = new StringBuilder();
            System.Reflection.ParameterInfo[] param = method.GetParameters();
            foreach (System.Reflection.ParameterInfo i in param)
            {
                parameters.AppendFormat("{0} {1}, ", i.ParameterType, i.Name);
            }
            if (parameters.Length > 2)
                parameters.Remove(parameters.Length - 2, 2);
			string outline = String.Format("{0}.{1}({2})> {3}", method.DeclaringType, method.Name, parameters, value);
			LogData.Add(name, outline);

            Console.WriteLine("{0}: {1}",
                name,
				outline
			);
        }
    }
}
