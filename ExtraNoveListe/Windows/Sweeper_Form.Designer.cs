using System.Drawing;
using System.Windows.Forms;

namespace ExtraNoveListe
{
    partial class Sweeper_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sweeper_Form));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.brisanje_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.names_button = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.export_button = new System.Windows.Forms.Button();
            this.sweeper_listbox = new System.Windows.Forms.ListBox();
            this.radio_listbox = new System.Windows.Forms.ListBox();
            this.view_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.import_button = new System.Windows.Forms.Button();
            this.insert_sw_button = new System.Windows.Forms.Button();
            this.remove_button = new System.Windows.Forms.Button();
            this.duplicate_button = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 460);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.brisanje_button);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.listBox3);
            this.tabPage1.Controls.Add(this.listBox2);
            this.tabPage1.Controls.Add(this.names_button);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 434);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // brisanje_button
            // 
            this.brisanje_button.Location = new System.Drawing.Point(367, 397);
            this.brisanje_button.Name = "brisanje_button";
            this.brisanje_button.Size = new System.Drawing.Size(75, 23);
            this.brisanje_button.TabIndex = 21;
            this.brisanje_button.Text = "Brisanje liste";
            this.brisanje_button.UseVisualStyleBackColor = true;
            this.brisanje_button.Click += new System.EventHandler(this.brisanje_button_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Information:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(367, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Lists:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(367, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Select Information:";
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(367, 140);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(352, 251);
            this.listBox3.TabIndex = 17;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(367, 26);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(352, 95);
            this.listBox2.TabIndex = 16;
            // 
            // names_button
            // 
            this.names_button.Location = new System.Drawing.Point(630, 401);
            this.names_button.Name = "names_button";
            this.names_button.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.names_button.Size = new System.Drawing.Size(89, 23);
            this.names_button.TabIndex = 15;
            this.names_button.Text = "Show Info";
            this.names_button.UseVisualStyleBackColor = true;
            this.names_button.Click += new System.EventHandler(this.names_button_Click_1);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(9, 26);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(352, 394);
            this.listBox1.TabIndex = 14;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.export_button);
            this.tabPage2.Controls.Add(this.sweeper_listbox);
            this.tabPage2.Controls.Add(this.radio_listbox);
            this.tabPage2.Controls.Add(this.view_button);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.import_button);
            this.tabPage2.Controls.Add(this.insert_sw_button);
            this.tabPage2.Controls.Add(this.remove_button);
            this.tabPage2.Controls.Add(this.duplicate_button);
            this.tabPage2.Controls.Add(this.stopButton);
            this.tabPage2.Controls.Add(this.playButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(776, 434);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // export_button
            // 
            this.export_button.Location = new System.Drawing.Point(390, 389);
            this.export_button.Name = "export_button";
            this.export_button.Size = new System.Drawing.Size(75, 23);
            this.export_button.TabIndex = 7;
            this.export_button.Text = "Export";
            this.export_button.UseVisualStyleBackColor = true;
            this.export_button.Click += new System.EventHandler(this.export_button_Click);
            // 
            // sweeper_listbox
            // 
            this.sweeper_listbox.FormattingEnabled = true;
            this.sweeper_listbox.Location = new System.Drawing.Point(471, 213);
            this.sweeper_listbox.Name = "sweeper_listbox";
            this.sweeper_listbox.Size = new System.Drawing.Size(296, 199);
            this.sweeper_listbox.TabIndex = 6;
            // 
            // radio_listbox
            // 
            this.radio_listbox.FormattingEnabled = true;
            this.radio_listbox.Location = new System.Drawing.Point(471, 7);
            this.radio_listbox.Name = "radio_listbox";
            this.radio_listbox.Size = new System.Drawing.Size(296, 199);
            this.radio_listbox.TabIndex = 5;
            // 
            // view_button
            // 
            this.view_button.Location = new System.Drawing.Point(390, 242);
            this.view_button.Name = "view_button";
            this.view_button.Size = new System.Drawing.Size(75, 23);
            this.view_button.TabIndex = 4;
            this.view_button.Text = "Sweepers";
            this.view_button.UseVisualStyleBackColor = true;
            this.view_button.Click += new System.EventHandler(this.view_button_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(8, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(376, 410);
            this.panel1.TabIndex = 3;
            // 
            // import_button
            // 
            this.import_button.Location = new System.Drawing.Point(390, 361);
            this.import_button.Name = "import_button";
            this.import_button.Size = new System.Drawing.Size(75, 23);
            this.import_button.TabIndex = 2;
            this.import_button.Text = "Import List";
            this.import_button.UseVisualStyleBackColor = true;
            this.import_button.Click += new System.EventHandler(this.import_button_Click);
            // 
            // insert_sw_button
            // 
            this.insert_sw_button.Location = new System.Drawing.Point(390, 213);
            this.insert_sw_button.Name = "insert_sw_button";
            this.insert_sw_button.Size = new System.Drawing.Size(75, 23);
            this.insert_sw_button.TabIndex = 1;
            this.insert_sw_button.Text = "<-----";
            this.insert_sw_button.UseVisualStyleBackColor = true;
            this.insert_sw_button.Click += new System.EventHandler(this.insert_sw_button_Click);
            // 
            // remove_button
            // 
            this.remove_button.Location = new System.Drawing.Point(3423, 96);
            this.remove_button.Name = "remove_button";
            this.remove_button.Size = new System.Drawing.Size(75, 23);
            this.remove_button.TabIndex = 1;
            this.remove_button.Text = "Remove";
            this.remove_button.UseVisualStyleBackColor = true;
            this.remove_button.Click += new System.EventHandler(this.remove_button_Click);
            // 
            // duplicate_button
            // 
            this.duplicate_button.Location = new System.Drawing.Point(390, 67);
            this.duplicate_button.Name = "duplicate_button";
            this.duplicate_button.Size = new System.Drawing.Size(75, 23);
            this.duplicate_button.TabIndex = 1;
            this.duplicate_button.Text = "Duplicate";
            this.duplicate_button.UseVisualStyleBackColor = true;
            // 
            // move_down_button1
            // 
            this.stopButton.Location = new System.Drawing.Point(390, 38);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_button1_Click);
            // 
            // move_up_button1
            // 
            this.playButton.Location = new System.Drawing.Point(390, 9);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 1;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButon_button1_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(776, 434);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Sweeper_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Sweeper_Form";
            this.Text = "Informacije o pjesmama";
            this.Load += new System.EventHandler(this.Sweeper_Form_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button brisanje_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button names_button;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button view_button;
        public System.Windows.Forms.ListBox sweeper_listbox;
        public System.Windows.Forms.ListBox radio_listbox;
        private System.Windows.Forms.Button import_button;
        private System.Windows.Forms.Button insert_sw_button;
        private System.Windows.Forms.Button remove_button;
        private System.Windows.Forms.Button duplicate_button;
        private System.Windows.Forms.Button export_button;

        private void ColorSetup()
        {
            this.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Base);

            this.brisanje_button.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side2);
            this.duplicate_button.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side2);
            this.export_button.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side2);
            this.import_button.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side2);
            this.insert_sw_button.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side2);
            this.stopButton.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side2);
            this.playButton.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side2);
            this.names_button.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side2);
            this.remove_button.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side2);
            this.view_button.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side2);
            
            //tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl1.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Base);
            foreach (TabPage tc in tabControl1.TabPages)
            {
                tc.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Base);
                //tc.BorderStyle = BorderStyle.None;
            }

            this.listBox1.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side1);
            this.listBox2.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side1);
            this.listBox3.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side1);

            this.radio_listbox.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side1);
            this.sweeper_listbox.BackColor = WindowColors.GetColor(WindowColors.WindowColor.Side1);


        }

        private void Stylize()
        {
            this.brisanje_button.FlatStyle = FlatStyle.Flat;
            this.brisanje_button.FlatAppearance.BorderColor = Color.Black;
            this.brisanje_button.FlatAppearance.BorderSize = 1;
            
            this.duplicate_button.FlatStyle = FlatStyle.Flat;
            this.duplicate_button.FlatAppearance.BorderColor = Color.Black;
            this.duplicate_button.FlatAppearance.BorderSize = 1;
            
            this.export_button.FlatStyle = FlatStyle.Flat;
            this.export_button.FlatAppearance.BorderColor = Color.Black;
            this.export_button.FlatAppearance.BorderSize = 1;
            
            this.import_button.FlatStyle = FlatStyle.Flat;
            this.import_button.FlatAppearance.BorderColor = Color.Black;
            this.import_button.FlatAppearance.BorderSize = 1;
            
            this.insert_sw_button.FlatStyle = FlatStyle.Flat;
            this.insert_sw_button.FlatAppearance.BorderColor = Color.Black;
            this.insert_sw_button.FlatAppearance.BorderSize = 1;
            
            this.stopButton.FlatStyle = FlatStyle.Flat;
            this.stopButton.FlatAppearance.BorderColor = Color.Black;
            this.stopButton.FlatAppearance.BorderSize = 1;
            
            this.playButton.FlatStyle = FlatStyle.Flat;
            this.playButton.FlatAppearance.BorderColor = Color.Black;
            this.playButton.FlatAppearance.BorderSize = 1;
            
            this.names_button.FlatStyle = FlatStyle.Flat;
            this.names_button.FlatAppearance.BorderColor = Color.Black;
            this.names_button.FlatAppearance.BorderSize = 1;
            
            this.remove_button.FlatStyle = FlatStyle.Flat;
            this.remove_button.FlatAppearance.BorderColor = Color.Black;
            this.remove_button.FlatAppearance.BorderSize = 1;
            
            this.view_button.FlatStyle = FlatStyle.Flat;
            this.view_button.FlatAppearance.BorderColor = Color.Black;
            this.view_button.FlatAppearance.BorderSize = 1;

            //tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            foreach (TabPage tc in tabControl1.TabPages)
            {
                //tc.BorderStyle = BorderStyle.None;
                
            }
            
            this.listBox1.BorderStyle = BorderStyle.FixedSingle;
            this.listBox2.BorderStyle= BorderStyle.FixedSingle;
            this.listBox3.BorderStyle = BorderStyle.FixedSingle;
            
            this.radio_listbox.BorderStyle = BorderStyle.FixedSingle;
            this.sweeper_listbox.BorderStyle = BorderStyle.FixedSingle;

            

            //this.tabPage2.BorderStyle = BorderStyle.FixedSingle;
            //this.tabPage2.BackColor = WindowColors.Side1;
        }
    }
}