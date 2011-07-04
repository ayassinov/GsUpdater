using System;
using System.Linq;
using System.Windows.Forms;

namespace SampleApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            string p1 = string.Empty, p2 = string.Empty;
            if (args != null && args.Count() > 0)
            {
                var pp = args[0].Split(';');
                p1 = pp[0];
                p2 = pp[1];

            }
            MessageBox.Show("Nbre de paramètre" + args.Count());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1() { Param1 = p1, Param2 = p2 });
        }
    }
}
