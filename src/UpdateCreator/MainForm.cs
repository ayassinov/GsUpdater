using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using GsUpdater.Framework.Utils;

namespace UpdateCreator
{
    partial class MainForm : Form
    {
        private const string XmlFileName = "UpdateFeed.xml";

        private List<string> Files = new List<string>();

        public MainForm()
        {
            InitializeComponent();
            this.Text = String.Format("Update Creator {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            this.txtUrl.Text = "https://github.com/ayassinov/GsCommande/downloads/";
        }

        private void btnOuvrirSource_Click(object sender, EventArgs e)
        {

            btnDetail.Enabled = (!string.IsNullOrEmpty(txtSource.Text) && Directory.Exists(txtSource.Text));
            if (fldSource.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = fldSource.SelectedPath;
                btnDetail.Enabled = true;
                LoadFiles();
            }
        }

        private void LoadFiles()
        {
            foreach (var file in Directory.GetFiles(fldSource.SelectedPath))
            {
                Files.Add(Path.GetFileName(file));
            }
        }

        private void btnOuvrirDestination_Click(object sender, EventArgs e)
        {
            if (fldDestination.ShowDialog() == DialogResult.OK)
            {
                txtDestination.Text = fldDestination.SelectedPath;
            }
        }

        private void BtnConfimer_Click(object sender, EventArgs e)
        {
            ExportFiles();
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

            if (Directory.GetFiles(txtSource.Text).Length == 0)
            {
                MessageBox.Show("Le dossier source est vide");
                return false;
            }

            if (Files.Count == 0)
            {
                MessageBox.Show("Vous avez ignorer tous les fichiers du répertoire source. Aucun fichier a exporté");
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
            try
            {
                if (!ValidateFolders()) // not valid
                    return;

                var patch = CreatePatch(); // create patch information and export file to dist directory

                CreateXmlDocument(patch); //create xml and export to dist

                var filePath = txtDestination.Text;  //open dist folder

                string argument = @"/select, " + filePath;

                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
            catch (Exception e)
            {
                MessageBox.Show("une erreur s'est produite lors de la préparation du patch"
                    + Environment.NewLine
                    + "Detail de l'erreur :"
                    + Environment.NewLine
                    + e.Message);
            }
        }


        public void CreateXmlDocument(Patch patch)
        {
            var doc = new XmlDocument();
            doc.InnerXml = patch.GetXml();
            doc.Save(Path.Combine(txtDestination.Text, XmlFileName));

        }

        public Patch CreatePatch()
        {
            var patch = new Patch();

            var zipfileName = string.Format("{0}-{1}.zip", Regex.Replace(txtNom.Text, @"\s", string.Empty), txtVersion.Text);

            patch.LocalFile = Path.Combine(txtDestination.Text, zipfileName);  //prepare path
            if (!Directory.Exists(txtDestination.Text))
                Directory.CreateDirectory(txtDestination.Text);


            if (Files.Count == 0)
                Utils.ZipFileUil.Compress(Directory.GetFiles(txtSource.Text), patch.LocalFile);
            else
                Utils.ZipFileUil.Compress(txtSource.Text, Files, patch.LocalFile);

            patch.NomApplication = txtNom.Text;

            //checksum
            patch.Checksum = FileChecksum.GetSHA256Checksum(patch.LocalFile);

            patch.Description = txtDescription.Text;

            patch.FileLength = new FileInfo(patch.LocalFile).Length.ToString();

            txtUrl.Text = txtUrl.Text + Path.GetFileName(patch.LocalFile);

            patch.Url = txtUrl.Text;

            patch.Version = txtVersion.Text;

            return patch;
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            var formdetail = new DetailForm();
            formdetail.OnCloseForm += new DetailForm.FormClosedHandler(formdetail_OnCloseForm);
            formdetail.Init(Files);
            formdetail.ShowDialog();
        }

        void formdetail_OnCloseForm(List<string> files)
        {
            Files = files;
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSource_Leave(object sender, EventArgs e)
        {
            btnDetail.Enabled = (!string.IsNullOrEmpty(txtSource.Text) && Directory.Exists(txtSource.Text));
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
        public string DateCreation { get { return DateTime.Now.ToShortDateString(); } }
        public string FileLength { get; set; }

        #region XMLDATA
        private const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<rss version=""2.0"" xmlns:appcast=""http://www.adobe.com/xml-namespaces/appcast/1.0"">
  <channel>
    <title>{0}</title>
  
    <item>
      <title>{0}</title>
      
      <description><![CDATA[ {1} ]]></description>
      <pubDate>{2}</pubDate>
      <enclosure 
	      url=""{3}"" 
	      length=""{4}"" 
	      type=""application/octet-stream"" 
		  version=""{5}""
          checksum=""{6}""/>
    </item>
  </channel>
</rss>";
        #endregion



        public string GetXml()
        {
            var s = string.Format(xml, NomApplication, Description, DateCreation, Url, FileLength, Version, Checksum);
            return s;
        }
    }
}
