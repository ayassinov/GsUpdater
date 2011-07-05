using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using GsUpdater.Framework.Utils;

namespace UpdateCreator
{
    partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = String.Format("UpdateCreator {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        private void btnOuvrirSource_Click(object sender, EventArgs e)
        {

            if (fldSource.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = fldSource.SelectedPath;
            }
        }

        private void btnOuvrirDestination_Click(object sender, EventArgs e)
        {
            if (fldDestination.ShowDialog() == DialogResult.OK)
            {
                txtDestination.Text = fldSource.SelectedPath;
            }
        }

        private void BtnConfimer_Click(object sender, EventArgs e)
        {

            //validate folder

            //read from source

            //compress zip file

            //save it in destination folder

            //create xml file


            //save it in destination folder

            //open destination folder on explorer

            var filePath = string.Empty;

            string argument = @"/select, " + filePath;

            System.Diagnostics.Process.Start("explorer.exe", argument);

        }

        private bool ValidateFolders()
        {
            if (string.IsNullOrEmpty(txtSource.Text))
            {
                MessageBox.Show("Vous devez spécifier un dossier source");
                return false;
            }

            if (!Directory.Exists(txtSource.Text))
            {
                MessageBox.Show("Le dossier de source choisie n'est pas valide");
                return false;
            }

            if (string.IsNullOrEmpty(txtDestination.Text))
            {
                MessageBox.Show("Vous devez spécifier un dossier de destination");
                return false;
            }

            if (!Directory.Exists(txtDestination.Text))
            {
                MessageBox.Show("Le dossier de destination choisie n'est pas valide");
                return false;
            }


            if (string.IsNullOrEmpty(txtVersion.Text))
            {
                MessageBox.Show("Vous devez spécifier une version");
                return false;
            }

            try
            {
                new Version(txtVersion.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("La version que vous avez enter n'est pas valide");
                return false;
            }

            if (string.IsNullOrEmpty(txtNom.Text))
            {
                MessageBox.Show("Vous devez spécifier un nom pour l'application");
                return false;
            }


            return true;
        }

        private void ExportFiles()
        {
            if (!ValidateFolders()) // not valid
                return;

            try
            {
                var patch = new Patch();

                patch.LocalFile = Path.Combine(txtDestination.Text, "application.zip");  //prepare path
                if (!Directory.Exists(txtDestination.Text))
                    Directory.CreateDirectory(txtDestination.Text);


                Utils.ZipFileUil.Compress(Directory.GetFiles(txtSource.Text), patch.LocalFile);

                //checksum
                patch.Checksum = FileChecksum.GetSHA256Checksum(patch.LocalFile);

                patch.Description = txtDescription.Text;

                patch.FileLength = new FileInfo(patch.LocalFile).Length.ToString();


            }
            catch (Exception)
            {
                throw;
            }
        }


        public void CreateXmlDocument(Patch patch)
        {
            var doc = new XmlDocument();

            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", null));

            ////XNamespace appCast = "http://www.adobe.com/xml-namespaces/appcast/1.0";
            //XElement rss = new XElement("rss", 
            //    new XAttribute("version", "2.0",
            //    new XAttribute(XNamespace.Xmlns + "appcast", "http://www.adobe.com/xml-namespaces/appcast/1.0"),
            //   )
            //);


            

            //XmlNode node = doc.CreateElement("rss");
            //node.Attributes.Append(doc.CreateAttribute(XNamespace.Xmlns.ToString() + "appcast", "http://www.adobe.com/xml-namespaces/appcast/1.0"));

            //doc.AppendChild(node);



        }



    }

    public class Patch
    {
        public string NomApplication { get; set; }
        public string Version { get; set; }
        public string Url { get; set; }
        public string Checksum { get; set; }
        public string LocalFile { get; set; }
        public string Description { get; set; }
        public string DateTime { get { return System.DateTime.Now.ToShortDateString(); } }
        public string FileLength { get; set; }
    }
}
