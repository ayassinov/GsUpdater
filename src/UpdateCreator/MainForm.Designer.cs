namespace UpdateCreator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.lblSource = new System.Windows.Forms.Label();
            this.lblDestionation = new System.Windows.Forms.Label();
            this.BtnConfimer = new System.Windows.Forms.Button();
            this.btnFermer = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOuvrirDestination = new System.Windows.Forms.Button();
            this.btnOuvrirSource = new System.Windows.Forms.Button();
            this.fldSource = new System.Windows.Forms.FolderBrowserDialog();
            this.fldDestination = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(13, 41);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(343, 20);
            this.txtSource.TabIndex = 0;
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(13, 85);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(343, 20);
            this.txtDestination.TabIndex = 1;
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(13, 25);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(83, 13);
            this.lblSource.TabIndex = 2;
            this.lblSource.Text = "Dossier source :";
            // 
            // lblDestionation
            // 
            this.lblDestionation.AutoSize = true;
            this.lblDestionation.Location = new System.Drawing.Point(13, 69);
            this.lblDestionation.Name = "lblDestionation";
            this.lblDestionation.Size = new System.Drawing.Size(117, 13);
            this.lblDestionation.TabIndex = 3;
            this.lblDestionation.Text = "Dossier de destination :";
            // 
            // BtnConfimer
            // 
            this.BtnConfimer.Location = new System.Drawing.Point(338, 356);
            this.BtnConfimer.Name = "BtnConfimer";
            this.BtnConfimer.Size = new System.Drawing.Size(75, 23);
            this.BtnConfimer.TabIndex = 4;
            this.BtnConfimer.Text = "Confimer";
            this.BtnConfimer.UseVisualStyleBackColor = true;
            this.BtnConfimer.Click += new System.EventHandler(this.BtnConfimer_Click);
            // 
            // btnFermer
            // 
            this.btnFermer.Location = new System.Drawing.Point(257, 356);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(75, 23);
            this.btnFermer.TabIndex = 5;
            this.btnFermer.Text = "Quitter";
            this.btnFermer.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOuvrirDestination);
            this.groupBox1.Controls.Add(this.btnOuvrirSource);
            this.groupBox1.Controls.Add(this.txtSource);
            this.groupBox1.Controls.Add(this.txtDestination);
            this.groupBox1.Controls.Add(this.lblSource);
            this.groupBox1.Controls.Add(this.lblDestionation);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 119);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // btnOuvrirDestination
            // 
            this.btnOuvrirDestination.Location = new System.Drawing.Point(362, 85);
            this.btnOuvrirDestination.Name = "btnOuvrirDestination";
            this.btnOuvrirDestination.Size = new System.Drawing.Size(39, 20);
            this.btnOuvrirDestination.TabIndex = 5;
            this.btnOuvrirDestination.Text = "...";
            this.btnOuvrirDestination.UseVisualStyleBackColor = true;
            this.btnOuvrirDestination.Click += new System.EventHandler(this.btnOuvrirDestination_Click);
            // 
            // btnOuvrirSource
            // 
            this.btnOuvrirSource.Location = new System.Drawing.Point(362, 41);
            this.btnOuvrirSource.Name = "btnOuvrirSource";
            this.btnOuvrirSource.Size = new System.Drawing.Size(39, 20);
            this.btnOuvrirSource.TabIndex = 4;
            this.btnOuvrirSource.Text = "...";
            this.btnOuvrirSource.UseVisualStyleBackColor = true;
            this.btnOuvrirSource.Click += new System.EventHandler(this.btnOuvrirSource_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Controls.Add(this.txtUrl);
            this.groupBox2.Controls.Add(this.txtNom);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtVersion);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(13, 137);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 213);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Informations :";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(12, 140);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(385, 67);
            this.txtDescription.TabIndex = 7;
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(140, 39);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(257, 20);
            this.txtNom.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nom de l\'application";
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(12, 39);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(114, 20);
            this.txtVersion.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Version ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Description";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "URL :";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(12, 87);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(385, 20);
            this.txtUrl.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 389);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.BtnConfimer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MainForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.Label lblDestionation;
        private System.Windows.Forms.Button BtnConfimer;
        private System.Windows.Forms.Button btnFermer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOuvrirDestination;
        private System.Windows.Forms.Button btnOuvrirSource;
        private System.Windows.Forms.FolderBrowserDialog fldSource;
        private System.Windows.Forms.FolderBrowserDialog fldDestination;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label4;

    }
}
