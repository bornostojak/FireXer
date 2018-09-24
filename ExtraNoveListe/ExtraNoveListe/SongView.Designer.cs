using System;

namespace ExtraNoveListe
{
    partial class SongView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.checkbox_selected = new System.Windows.Forms.CheckBox();
            this.song_info = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.song_time_info = new System.Windows.Forms.Label();
            this.songViewConntextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.songToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.songViewConntextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkbox_selected
            // 
            this.checkbox_selected.AutoSize = true;
            this.checkbox_selected.Location = new System.Drawing.Point(360, 3);
            this.checkbox_selected.Name = "checkbox_selected";
            this.checkbox_selected.Size = new System.Drawing.Size(15, 14);
            this.checkbox_selected.TabIndex = 0;
            this.checkbox_selected.UseVisualStyleBackColor = true;
            this.checkbox_selected.CheckedChanged += new System.EventHandler(this.checkbox_selected_CheckedChanged);
            // 
            // song_info
            // 
            this.song_info.AutoSize = true;
            this.song_info.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.song_info.Location = new System.Drawing.Point(4, 3);
            this.song_info.Name = "song_info";
            this.song_info.Size = new System.Drawing.Size(0, 18);
            this.song_info.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(314, 22);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(47, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Remove";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // song_time_info
            // 
            this.song_time_info.AutoSize = true;
            this.song_time_info.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.song_time_info.Location = new System.Drawing.Point(7, 24);
            this.song_time_info.Name = "song_time_info";
            this.song_time_info.Size = new System.Drawing.Size(0, 14);
            this.song_time_info.TabIndex = 3;
            // 
            // songViewConntextMenuStrip
            // 
            this.songViewConntextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.playSongToolStripMenuItem,
            this.saveSongToolStripMenuItem});
            this.songViewConntextMenuStrip.Name = "contextMenuStrip1";
            this.songViewConntextMenuStrip.Size = new System.Drawing.Size(153, 136);
            this.songViewConntextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.songViewConntextMenuStrip_Opening);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.songToolStripMenuItem,
            this.listToolStripMenuItem});
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.insertToolStripMenuItem.Text = "Insert";
            // 
            // songToolStripMenuItem
            // 
            this.songToolStripMenuItem.Name = "songToolStripMenuItem";
            this.songToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.songToolStripMenuItem.Text = "Song";
            this.songToolStripMenuItem.Click += new System.EventHandler(this.songToolStripMenuItem_Click);
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.listToolStripMenuItem.Text = "List";
            this.listToolStripMenuItem.Click += new System.EventHandler(this.listToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveSong);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // playSongToolStripMenuItem
            // 
            this.playSongToolStripMenuItem.Name = "playSongToolStripMenuItem";
            this.playSongToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.playSongToolStripMenuItem.Text = "Play Song";
            this.playSongToolStripMenuItem.Click += new System.EventHandler(this.playSongToolStripMenuItem_Click);
            // 
            // saveSongToolStripMenuItem
            // 
            this.saveSongToolStripMenuItem.Name = "saveSongToolStripMenuItem";
            this.saveSongToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveSongToolStripMenuItem.Text = "Save Song";
            this.saveSongToolStripMenuItem.Click += new System.EventHandler(this.saveSongToolStripMenuItem_Click);
            // 
            // SongView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.songViewConntextMenuStrip;
            this.Controls.Add(this.song_time_info);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.song_info);
            this.Controls.Add(this.checkbox_selected);
            this.Name = "SongView";
            this.Size = new System.Drawing.Size(376, 40);
            this.songViewConntextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkbox_selected;
        private System.Windows.Forms.Label song_info;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label song_time_info;
        private System.Windows.Forms.ContextMenuStrip songViewConntextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem songToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playSongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSongToolStripMenuItem;
    }
}
