
namespace Snake
{
    partial class SnakeForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._fileNewGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._newGame10MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._newGame15MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._newGame20MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._fileQuitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._pauseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this._eggCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._eggCountDataLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._gridPanel = new System.Windows.Forms.Panel();
            this._menuStrip.SuspendLayout();
            this._statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileMenuItem,
            this._pauseMenuItem});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(779, 24);
            this._menuStrip.TabIndex = 0;
            this._menuStrip.Text = "---";
            // 
            // _fileMenuItem
            // 
            this._fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileNewGameMenuItem,
            this._fileQuitMenuItem});
            this._fileMenuItem.Name = "_fileMenuItem";
            this._fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this._fileMenuItem.Text = "File";
            // 
            // _fileNewGameMenuItem
            // 
            this._fileNewGameMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newGame10MenuItem,
            this._newGame15MenuItem,
            this._newGame20MenuItem});
            this._fileNewGameMenuItem.Name = "_fileNewGameMenuItem";
            this._fileNewGameMenuItem.Size = new System.Drawing.Size(132, 22);
            this._fileNewGameMenuItem.Text = "New Game";
            // 
            // _newGame10MenuItem
            // 
            this._newGame10MenuItem.Name = "_newGame10MenuItem";
            this._newGame10MenuItem.Size = new System.Drawing.Size(110, 22);
            this._newGame10MenuItem.Text = "10 x 10";
            // 
            // _newGame15MenuItem
            // 
            this._newGame15MenuItem.Name = "_newGame15MenuItem";
            this._newGame15MenuItem.Size = new System.Drawing.Size(110, 22);
            this._newGame15MenuItem.Text = "15 x 15";
            // 
            // _newGame20MenuItem
            // 
            this._newGame20MenuItem.Name = "_newGame20MenuItem";
            this._newGame20MenuItem.Size = new System.Drawing.Size(110, 22);
            this._newGame20MenuItem.Text = "20 x 20";
            // 
            // _fileQuitMenuItem
            // 
            this._fileQuitMenuItem.Name = "_fileQuitMenuItem";
            this._fileQuitMenuItem.Size = new System.Drawing.Size(132, 22);
            this._fileQuitMenuItem.Text = "Quit Game";
            // 
            // _pauseMenuItem
            // 
            this._pauseMenuItem.Name = "_pauseMenuItem";
            this._pauseMenuItem.Size = new System.Drawing.Size(43, 20);
            this._pauseMenuItem.Text = "Start";
            // 
            // _statusStrip
            // 
            this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._eggCountLabel,
            this._eggCountDataLabel});
            this._statusStrip.Location = new System.Drawing.Point(0, 454);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Size = new System.Drawing.Size(779, 24);
            this._statusStrip.TabIndex = 2;
            this._statusStrip.Text = "---";
            // 
            // _eggCountLabel
            // 
            this._eggCountLabel.Name = "_eggCountLabel";
            this._eggCountLabel.Size = new System.Drawing.Size(70, 19);
            this._eggCountLabel.Text = "Eggs eaten: ";
            // 
            // _eggCountDataLabel
            // 
            this._eggCountDataLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this._eggCountDataLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this._eggCountDataLabel.Name = "_eggCountDataLabel";
            this._eggCountDataLabel.Size = new System.Drawing.Size(17, 19);
            this._eggCountDataLabel.Text = "0";
            // 
            // _gridPanel
            // 
            this._gridPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this._gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridPanel.Location = new System.Drawing.Point(0, 24);
            this._gridPanel.Name = "_gridPanel";
            this._gridPanel.Size = new System.Drawing.Size(779, 430);
            this._gridPanel.TabIndex = 3;
            // 
            // SnakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 478);
            this.Controls.Add(this._gridPanel);
            this.Controls.Add(this._statusStrip);
            this.Controls.Add(this._menuStrip);
            this.Name = "SnakeForm";
            this.Text = "Snake!";
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this._statusStrip.ResumeLayout(false);
            this._statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem _fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _fileNewGameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _newGame10MenuItem;
        private System.Windows.Forms.ToolStripMenuItem _newGame15MenuItem;
        private System.Windows.Forms.ToolStripMenuItem _newGame20MenuItem;
        private System.Windows.Forms.ToolStripMenuItem _fileQuitMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel _eggCountLabel;
        private System.Windows.Forms.ToolStripStatusLabel _snakeLengthDataLabel;
        private System.Windows.Forms.Panel _gridPanel;
        private System.Windows.Forms.ToolStripMenuItem _pauseMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel _eggCountDataLabel;
    }
}

