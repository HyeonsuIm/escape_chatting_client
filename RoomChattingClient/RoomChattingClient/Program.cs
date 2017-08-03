using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomChattingClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(ResolveAssembly);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChatForm());
        }
        private static System.Reflection.Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            System.Reflection.Assembly thisAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            string name = args.Name.Substring(0, args.Name.IndexOf(',')) + ".dll";
            var files = thisAssembly.GetManifestResourceNames().Where(s => s.EndsWith(name));

            if (files.Count() > 0)
            {
                string fileName = files.First();
                using (System.IO.Stream stream = thisAssembly.GetManifestResourceStream(fileName))
                {
                    if (stream != null)
                    {
                        byte[] data = new byte[stream.Length];
                        stream.Read(data, 0, data.Length);
                        return System.Reflection.Assembly.Load(data);
                    }
                }
            }
            return null;
        }
    }
}
