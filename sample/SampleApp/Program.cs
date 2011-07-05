﻿using System;
using System.Linq;
using System.Windows.Forms;
using GsUpdater.Framework;

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
            UpdateManager.Instance.Clear(args);

            MessageBox.Show("Nbre de paramètre" + args.Count());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          //  Application.Run(new Form1() { Param1 = p1, Param2 = p2 });
        }
    }
}
