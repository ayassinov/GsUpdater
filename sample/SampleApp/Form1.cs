using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GsUpdater.Framework;

namespace SampleApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            btnInstall.Enabled = false;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
           var updateExist = UpdateManager.Instance.CheckForUpdate("http://localhost/TestFeed.xml");
            if (updateExist)
            {
                btnInstall.Enabled = true;
                lblNewVersion.Text = UpdateManager.Instance.CurrentUpdate.FileVersion.ToString();
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            UpdateManager.Instance.DoUpdate();
        }
    }
}
