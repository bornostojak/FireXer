using System.Net.Mime;
using System.Windows.Forms;

namespace ExtraNoveListe
{
    partial class StartingWindowForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartingWindowForm));
            this.file_list_box = new System.Windows.Forms.ListBox();
            this.start_button = new System.Windows.Forms.Button();
            this.close_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.save_textBox = new System.Windows.Forms.TextBox();
            this.browse_button = new System.Windows.Forms.Button();
            this.choose_button = new System.Windows.Forms.Button();
            this.clear_button = new System.Windows.Forms.Button();
            this.clear_checkBox = new System.Windows.Forms.CheckBox();
            this.up_button = new System.Windows.Forms.Button();
            this.down_button = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.radioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eNTERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXTRAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extraToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.enterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sweeperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sweeperiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sweeperiToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.napraviListuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorSchemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicate_button = new System.Windows.Forms.Button();
            this.checkBox_xml = new System.Windows.Forms.CheckBox();
            this.checkBox_fps = new System.Windows.Forms.CheckBox();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radioSweeperSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // file_list_box
            // 
            this.file_list_box.FormattingEnabled = true;
            this.file_list_box.Location = new System.Drawing.Point(13, 52);
            this.file_list_box.Name = "file_list_box";
            this.file_list_box.Size = new System.Drawing.Size(350, 225);
            this.file_list_box.TabIndex = 0;
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(369, 321);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(75, 23);
            this.start_button.TabIndex = 1;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // close_button
            // 
            this.close_button.Location = new System.Drawing.Point(288, 321);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(75, 23);
            this.close_button.TabIndex = 2;
            this.close_button.Text = "Close";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Files to merge:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Save File:";
            // 
            // save_textBox
            // 
            this.save_textBox.Location = new System.Drawing.Point(75, 292);
            this.save_textBox.Name = "save_textBox";
            this.save_textBox.Size = new System.Drawing.Size(288, 20);
            this.save_textBox.TabIndex = 5;
            // 
            // browse_button
            // 
            this.browse_button.Location = new System.Drawing.Point(369, 292);
            this.browse_button.Name = "browse_button";
            this.browse_button.Size = new System.Drawing.Size(75, 23);
            this.browse_button.TabIndex = 6;
            this.browse_button.Text = "Browse";
            this.browse_button.UseVisualStyleBackColor = true;
            this.browse_button.Click += new System.EventHandler(this.browse_button_Click);
            // 
            // choose_button
            // 
            this.choose_button.Location = new System.Drawing.Point(12, 321);
            this.choose_button.Name = "choose_button";
            this.choose_button.Size = new System.Drawing.Size(75, 23);
            this.choose_button.TabIndex = 7;
            this.choose_button.Text = "Choose Files";
            this.choose_button.UseVisualStyleBackColor = true;
            this.choose_button.Click += new System.EventHandler(this.choose_button_Click);
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(207, 321);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(75, 23);
            this.clear_button.TabIndex = 8;
            this.clear_button.Text = "Clear";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // clear_checkBox
            // 
            this.clear_checkBox.AutoSize = true;
            this.clear_checkBox.Location = new System.Drawing.Point(137, 325);
            this.clear_checkBox.Name = "clear_checkBox";
            this.clear_checkBox.Size = new System.Drawing.Size(64, 17);
            this.clear_checkBox.TabIndex = 9;
            this.clear_checkBox.Text = "Clear All";
            this.clear_checkBox.UseVisualStyleBackColor = true;
            // 
            // up_button
            // 
            this.up_button.Location = new System.Drawing.Point(369, 52);
            this.up_button.Name = "up_button";
            this.up_button.Size = new System.Drawing.Size(75, 23);
            this.up_button.TabIndex = 10;
            this.up_button.Text = "Up";
            this.up_button.UseVisualStyleBackColor = true;
            this.up_button.Click += new System.EventHandler(this.up_button_Click);
            // 
            // down_button
            // 
            this.down_button.Location = new System.Drawing.Point(369, 81);
            this.down_button.Name = "down_button";
            this.down_button.Size = new System.Drawing.Size(75, 23);
            this.down_button.TabIndex = 11;
            this.down_button.Text = "Down";
            this.down_button.UseVisualStyleBackColor = true;
            this.down_button.Click += new System.EventHandler(this.down_button_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.radioToolStripMenuItem,
            this.toSaveToolStripMenuItem,
            this.sweeperToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(456, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // radioToolStripMenuItem
            // 
            this.radioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eNTERToolStripMenuItem,
            this.eXTRAToolStripMenuItem});
            this.radioToolStripMenuItem.Name = "radioToolStripMenuItem";
            this.radioToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.radioToolStripMenuItem.Text = "Radio";
            // 
            // eNTERToolStripMenuItem
            // 
            this.eNTERToolStripMenuItem.Name = "eNTERToolStripMenuItem";
            this.eNTERToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.eNTERToolStripMenuItem.Text = "ENTER";
            this.eNTERToolStripMenuItem.Click += new System.EventHandler(this.eNTERToolStripMenuItem_Click);
            // 
            // eXTRAToolStripMenuItem
            // 
            this.eXTRAToolStripMenuItem.Name = "eXTRAToolStripMenuItem";
            this.eXTRAToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.eXTRAToolStripMenuItem.Text = "EXTRA";
            this.eXTRAToolStripMenuItem.Click += new System.EventHandler(this.eXTRAToolStripMenuItem_Click);
            // 
            // toSaveToolStripMenuItem
            // 
            this.toSaveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extraToolStripMenuItem1,
            this.enterToolStripMenuItem1});
            this.toSaveToolStripMenuItem.Name = "toSaveToolStripMenuItem";
            this.toSaveToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.toSaveToolStripMenuItem.Text = "To Save";
            // 
            // extraToolStripMenuItem1
            // 
            this.extraToolStripMenuItem1.Name = "extraToolStripMenuItem1";
            this.extraToolStripMenuItem1.Size = new System.Drawing.Size(109, 22);
            this.extraToolStripMenuItem1.Text = "ENTER";
            this.extraToolStripMenuItem1.Click += new System.EventHandler(this.extraToolStripMenuItem1_Click);
            // 
            // enterToolStripMenuItem1
            // 
            this.enterToolStripMenuItem1.Name = "enterToolStripMenuItem1";
            this.enterToolStripMenuItem1.Size = new System.Drawing.Size(109, 22);
            this.enterToolStripMenuItem1.Text = "EXTRA";
            this.enterToolStripMenuItem1.Click += new System.EventHandler(this.enterToolStripMenuItem1_Click);
            // 
            // sweeperToolStripMenuItem
            // 
            this.sweeperToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sweeperiToolStripMenuItem,
            this.sweeperiToolStripMenuItem1,
            this.napraviListuToolStripMenuItem,
            this.colorSchemeToolStripMenuItem});
            this.sweeperToolStripMenuItem.Name = "sweeperToolStripMenuItem";
            this.sweeperToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.sweeperToolStripMenuItem.Text = "Dodatno";
            this.sweeperToolStripMenuItem.Click += new System.EventHandler(this.sweeperToolStripMenuItem_Click);
            // 
            // sweeperiToolStripMenuItem
            // 
            this.sweeperiToolStripMenuItem.Name = "sweeperiToolStripMenuItem";
            this.sweeperiToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.sweeperiToolStripMenuItem.Text = "Informacijei";
            this.sweeperiToolStripMenuItem.Click += new System.EventHandler(this.sweeperiToolStripMenuItem_Click);
            // 
            // sweeperiToolStripMenuItem1
            // 
            this.sweeperiToolStripMenuItem1.Name = "sweeperiToolStripMenuItem1";
            this.sweeperiToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.sweeperiToolStripMenuItem1.Text = "Sweeperi";
            this.sweeperiToolStripMenuItem1.Click += new System.EventHandler(this.sweeperiToolStripMenuItem1_Click);
            // 
            // napraviListuToolStripMenuItem
            // 
            this.napraviListuToolStripMenuItem.Name = "napraviListuToolStripMenuItem";
            this.napraviListuToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.napraviListuToolStripMenuItem.Text = "Napravi Listu";
            this.napraviListuToolStripMenuItem.Click += new System.EventHandler(this.napraviListuToolStripMenuItem_Click);
            // 
            // colorSchemeToolStripMenuItem
            // 
            this.colorSchemeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.autoToolStripMenuItem});
            this.colorSchemeToolStripMenuItem.Name = "colorSchemeToolStripMenuItem";
            this.colorSchemeToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.colorSchemeToolStripMenuItem.Text = "ColorScheme";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // autoToolStripMenuItem
            // 
            this.autoToolStripMenuItem.Name = "autoToolStripMenuItem";
            this.autoToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.autoToolStripMenuItem.Text = "Custom";
            this.autoToolStripMenuItem.Click += new System.EventHandler(this.AutoColorChangeDialog);
            // 
            // duplicate_button
            // 
            this.duplicate_button.Location = new System.Drawing.Point(370, 111);
            this.duplicate_button.Name = "duplicate_button";
            this.duplicate_button.Size = new System.Drawing.Size(75, 23);
            this.duplicate_button.TabIndex = 13;
            this.duplicate_button.Text = "Duplicate";
            this.duplicate_button.UseVisualStyleBackColor = true;
            this.duplicate_button.Click += new System.EventHandler(this.duplicate_button_Click);
            // 
            // checkBox_xml
            // 
            this.checkBox_xml.AutoSize = true;
            this.checkBox_xml.Location = new System.Drawing.Point(370, 260);
            this.checkBox_xml.Name = "checkBox_xml";
            this.checkBox_xml.Size = new System.Drawing.Size(48, 17);
            this.checkBox_xml.TabIndex = 14;
            this.checkBox_xml.Text = "XML";
            this.checkBox_xml.UseVisualStyleBackColor = true;
            // 
            // checkBox_fps
            // 
            this.checkBox_fps.AutoSize = true;
            this.checkBox_fps.Location = new System.Drawing.Point(370, 237);
            this.checkBox_fps.Name = "checkBox_fps";
            this.checkBox_fps.Size = new System.Drawing.Size(40, 17);
            this.checkBox_fps.TabIndex = 14;
            this.checkBox_fps.Text = "fps";
            this.checkBox_fps.UseVisualStyleBackColor = true;
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.radioSweeperSettingsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // radioSweeperSettingsToolStripMenuItem
            // 
            this.radioSweeperSettingsToolStripMenuItem.Name = "radioSweeperSettingsToolStripMenuItem";
            this.radioSweeperSettingsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.radioSweeperSettingsToolStripMenuItem.Text = "Radio Sweeper Settings";
            this.radioSweeperSettingsToolStripMenuItem.Click += new System.EventHandler(this.radioSweeperSettingsToolStripMenuItem_Click);
            // 
            // StartingWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 356);
            this.Controls.Add(this.checkBox_fps);
            this.Controls.Add(this.checkBox_xml);
            this.Controls.Add(this.duplicate_button);
            this.Controls.Add(this.down_button);
            this.Controls.Add(this.up_button);
            this.Controls.Add(this.clear_checkBox);
            this.Controls.Add(this.clear_button);
            this.Controls.Add(this.choose_button);
            this.Controls.Add(this.browse_button);
            this.Controls.Add(this.save_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.file_list_box);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "StartingWindowForm";
            this.Text = "Extra Liste";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox file_list_box;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox save_textBox;
        private System.Windows.Forms.Button browse_button;
        private System.Windows.Forms.Button choose_button;
        private System.Windows.Forms.Button clear_button;
        private System.Windows.Forms.CheckBox clear_checkBox;
        private System.Windows.Forms.Button up_button;
        private System.Windows.Forms.Button down_button;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem radioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eNTERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eXTRAToolStripMenuItem;
        private System.Windows.Forms.Button duplicate_button;
        private System.Windows.Forms.ToolStripMenuItem toSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extraToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem enterToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sweeperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sweeperiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sweeperiToolStripMenuItem1;
        private System.Windows.Forms.CheckBox checkBox_xml;
        private System.Windows.Forms.CheckBox checkBox_fps;
        private System.Windows.Forms.ToolStripMenuItem napraviListuToolStripMenuItem;
        private ToolStripMenuItem colorSchemeToolStripMenuItem;
        private ToolStripMenuItem autoToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem radioSweeperSettingsToolStripMenuItem;
    }
}

