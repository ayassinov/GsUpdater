using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace UpdateCreator
{
    partial class DetailForm : Form
    {

        private List<string> Files = new List<string>();

        public DetailForm()
        {
            InitializeComponent();
            this.Text = "Update Creator - Choix des fichiers";
        }

        public delegate void FormClosedHandler(List<string> files);

        public event FormClosedHandler OnCloseForm;
 
        private void CloseFormEventHandler()
        {
            if (OnCloseForm != null)
                OnCloseForm(Files);
        }

        public void Init(List<string> files)
        {
            Files = files;
            lstFiles.DataSource = files;
        }

        private void toolStripButton1_Click(object sender, EventArgs ee)
        {
            try
            {
                foreach (var item in lstFiles.SelectedItems)
                {
                    Files.Remove((string) item);
                }

                lstFiles.DataSource = new List<string>();
                lstFiles.DataSource = Files;
            }
            catch (Exception e)
            {
                MessageBox.Show("une erreur s'est produite, détails :" + Environment.NewLine + e.Message);
            }

        }

        private void DetailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseFormEventHandler();
        }
    }
}
