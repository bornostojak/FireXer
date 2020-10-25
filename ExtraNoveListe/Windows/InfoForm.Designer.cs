using System.Windows.Forms;

namespace ExtraNoveListe
{
    partial class InfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
            this.okButon = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.infoListView = new System.Windows.Forms.ListView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButon
            // 
            this.okButon.Location = new System.Drawing.Point(317, 376);
            this.okButon.Name = "okButon";
            this.okButon.Size = new System.Drawing.Size(75, 23);
            this.okButon.TabIndex = 0;
            this.okButon.Text = "Ok";
            this.okButon.UseVisualStyleBackColor = true;
            this.okButon.Click += new System.EventHandler(this.okButon_Click);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(236, 376);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(155, 376);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // infoListView
            // 
            this.infoListView.BackgroundImageTiled = true;
            this.infoListView.Location = new System.Drawing.Point(3, 3);
            this.infoListView.Name = "infoListView";
            this.infoListView.Size = new System.Drawing.Size(389, 338);
            this.infoListView.TabIndex = 3;
            this.infoListView.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.infoListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(395, 344);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(403, 370);
            this.tabControl1.TabIndex = 4;
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 411);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.okButon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InfoForm";
            this.Text = "InfoForm";
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void GridView()
        {
            this.infoListView.View = System.Windows.Forms.View.Details;
            this.infoListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.infoListView_ColumnClick);

            this.infoColumn = new System.Windows.Forms.ColumnHeader();
            this.infoColumn.Text = "Info";
            this.infoColumn.Width = 90;

            this.valueColumn = new System.Windows.Forms.ColumnHeader();
            this.valueColumn.Text = "Value";
            this.valueColumn.Width = 360-90-5;

            this.infoListView.Columns.AddRange(new ColumnHeader[]
            {
                infoColumn,
                valueColumn
            });

            this.infoListView.GridLines = true;
        }

        private void SetUpAnchors()
        {
            applyButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            closeButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            okButon.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            tabControl1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);
            infoListView.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);

            this.MinimumSize = new System.Drawing.Size(400, 450);
        }


        #endregion

        private System.Windows.Forms.Button okButon;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ListView infoListView;

        private System.Windows.Forms.ColumnHeader infoColumn;
        private System.Windows.Forms.ColumnHeader valueColumn;
        private TabPage tabPage1;
        private TabControl tabControl1;
    }
}