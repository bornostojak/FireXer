namespace ExtraNoveListe
{
    partial class SweeperSelectSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SweeperSelectSettingsForm));
            this.radio_listbox = new System.Windows.Forms.ListBox();
            this.folder_listbox = new System.Windows.Forms.ListBox();
            this.add_radio_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.remove_radio_button = new System.Windows.Forms.Button();
            this.add_radio_button = new System.Windows.Forms.Button();
            this.remove_location_button = new System.Windows.Forms.Button();
            this.add_location_button = new System.Windows.Forms.Button();
            this.location_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.add_location_dialog_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.update_button = new System.Windows.Forms.Button();
            this.import_button = new System.Windows.Forms.Button();
            this.export_button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.sweeper_listbox = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.radio_letter_combo_box = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // radio_listbox
            // 
            this.radio_listbox.FormattingEnabled = true;
            this.radio_listbox.Location = new System.Drawing.Point(12, 12);
            this.radio_listbox.Name = "radio_listbox";
            this.radio_listbox.Size = new System.Drawing.Size(250, 95);
            this.radio_listbox.TabIndex = 0;
            // 
            // folder_listbox
            // 
            this.folder_listbox.FormattingEnabled = true;
            this.folder_listbox.Location = new System.Drawing.Point(268, 12);
            this.folder_listbox.Name = "folder_listbox";
            this.folder_listbox.Size = new System.Drawing.Size(304, 147);
            this.folder_listbox.TabIndex = 0;
            this.folder_listbox.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // add_radio_textbox
            // 
            this.add_radio_textbox.Location = new System.Drawing.Point(79, 142);
            this.add_radio_textbox.Name = "add_radio_textbox";
            this.add_radio_textbox.Size = new System.Drawing.Size(183, 20);
            this.add_radio_textbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Add Radio:";
            // 
            // remove_radio_button
            // 
            this.remove_radio_button.Location = new System.Drawing.Point(57, 113);
            this.remove_radio_button.Name = "remove_radio_button";
            this.remove_radio_button.Size = new System.Drawing.Size(61, 23);
            this.remove_radio_button.TabIndex = 3;
            this.remove_radio_button.Text = "Remove";
            this.remove_radio_button.UseVisualStyleBackColor = true;
            this.remove_radio_button.Click += new System.EventHandler(this.remove_radio_button_Click);
            // 
            // add_radio_button
            // 
            this.add_radio_button.Location = new System.Drawing.Point(12, 113);
            this.add_radio_button.Name = "add_radio_button";
            this.add_radio_button.Size = new System.Drawing.Size(39, 23);
            this.add_radio_button.TabIndex = 3;
            this.add_radio_button.Text = "Add";
            this.add_radio_button.UseVisualStyleBackColor = true;
            this.add_radio_button.Click += new System.EventHandler(this.add_radio_button_Click);
            // 
            // remove_location_button
            // 
            this.remove_location_button.Location = new System.Drawing.Point(349, 165);
            this.remove_location_button.Name = "remove_location_button";
            this.remove_location_button.Size = new System.Drawing.Size(75, 23);
            this.remove_location_button.TabIndex = 3;
            this.remove_location_button.Text = "Remove";
            this.remove_location_button.UseVisualStyleBackColor = true;
            this.remove_location_button.Click += new System.EventHandler(this.remove_location_button_Click);
            // 
            // add_location_button
            // 
            this.add_location_button.Location = new System.Drawing.Point(268, 165);
            this.add_location_button.Name = "add_location_button";
            this.add_location_button.Size = new System.Drawing.Size(75, 23);
            this.add_location_button.TabIndex = 3;
            this.add_location_button.Text = "Add";
            this.add_location_button.UseVisualStyleBackColor = true;
            this.add_location_button.Click += new System.EventHandler(this.add_location_button_Click);
            // 
            // location_textbox
            // 
            this.location_textbox.Location = new System.Drawing.Point(341, 194);
            this.location_textbox.Name = "location_textbox";
            this.location_textbox.Size = new System.Drawing.Size(198, 20);
            this.location_textbox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Add Location";
            // 
            // add_location_dialog_button
            // 
            this.add_location_dialog_button.Location = new System.Drawing.Point(545, 191);
            this.add_location_dialog_button.Name = "add_location_dialog_button";
            this.add_location_dialog_button.Size = new System.Drawing.Size(27, 23);
            this.add_location_dialog_button.TabIndex = 4;
            this.add_location_dialog_button.Text = "...";
            this.add_location_dialog_button.UseVisualStyleBackColor = true;
            this.add_location_dialog_button.Click += new System.EventHandler(this.add_location_dialog_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(16, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 111);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sweeper settings";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(146, 64);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(91, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 43);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(163, 17);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "1 sweeper every half an hour";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Click += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(6, 66);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(133, 17);
            this.checkBox3.TabIndex = 0;
            this.checkBox3.Text = "n sweepers every hour";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.Click += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(6, 88);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(96, 17);
            this.checkBox4.TabIndex = 0;
            this.checkBox4.Text = "Normal sweeer";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.Click += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(128, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "1 sweeper every hour";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // update_button
            // 
            this.update_button.Location = new System.Drawing.Point(23, 296);
            this.update_button.Name = "update_button";
            this.update_button.Size = new System.Drawing.Size(155, 49);
            this.update_button.TabIndex = 6;
            this.update_button.Text = "Update";
            this.update_button.UseVisualStyleBackColor = true;
            this.update_button.Click += new System.EventHandler(this.update_button_Click);
            // 
            // import_button
            // 
            this.import_button.Location = new System.Drawing.Point(184, 296);
            this.import_button.Name = "import_button";
            this.import_button.Size = new System.Drawing.Size(75, 23);
            this.import_button.TabIndex = 3;
            this.import_button.Text = "Import";
            this.import_button.UseVisualStyleBackColor = true;
            this.import_button.Click += new System.EventHandler(this.add_location_button_Click);
            // 
            // export_button
            // 
            this.export_button.Location = new System.Drawing.Point(184, 322);
            this.export_button.Name = "export_button";
            this.export_button.Size = new System.Drawing.Size(75, 23);
            this.export_button.TabIndex = 3;
            this.export_button.Text = "Export";
            this.export_button.UseVisualStyleBackColor = true;
            this.export_button.Click += new System.EventHandler(this.add_location_button_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.sweeper_listbox);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(268, 221);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 128);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sweepers in the folder";
            // 
            // sweeper_listbox
            // 
            this.sweeper_listbox.Location = new System.Drawing.Point(6, 18);
            this.sweeper_listbox.Name = "sweeper_listbox";
            this.sweeper_listbox.Size = new System.Drawing.Size(292, 69);
            this.sweeper_listbox.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(223, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Exeption";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Letter:";
            // 
            // radio_letter_combo_box
            // 
            this.radio_letter_combo_box.FormattingEnabled = true;
            this.radio_letter_combo_box.Location = new System.Drawing.Point(164, 115);
            this.radio_letter_combo_box.Name = "radio_letter_combo_box";
            this.radio_letter_combo_box.Size = new System.Drawing.Size(95, 21);
            this.radio_letter_combo_box.TabIndex = 2;
            // 
            // SweeperSelectSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.radio_letter_combo_box);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.update_button);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.add_location_dialog_button);
            this.Controls.Add(this.export_button);
            this.Controls.Add(this.import_button);
            this.Controls.Add(this.add_location_button);
            this.Controls.Add(this.remove_location_button);
            this.Controls.Add(this.add_radio_button);
            this.Controls.Add(this.remove_radio_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.location_textbox);
            this.Controls.Add(this.add_radio_textbox);
            this.Controls.Add(this.folder_listbox);
            this.Controls.Add(this.radio_listbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SweeperSelectSettingsForm";
            this.Text = "Sweeper Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox radio_listbox;
        private System.Windows.Forms.ListBox folder_listbox;
        private System.Windows.Forms.TextBox add_radio_textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button remove_radio_button;
        private System.Windows.Forms.Button add_radio_button;
        private System.Windows.Forms.Button remove_location_button;
        private System.Windows.Forms.Button add_location_button;
        private System.Windows.Forms.TextBox location_textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button add_location_dialog_button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button update_button;
        private System.Windows.Forms.Button import_button;
        private System.Windows.Forms.Button export_button;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox sweeper_listbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox radio_letter_combo_box;
    }
}