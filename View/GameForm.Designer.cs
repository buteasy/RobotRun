namespace RobotRun.View
{
    partial class GameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGameItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGameItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeSmall = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeMedium = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeLarge = new System.Windows.Forms.ToolStripMenuItem();
            this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.timeLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.RoyalBlue;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.SettingsMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(692, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "File";
            // 
            // FileMenu
            // 
            this.FileMenu.BackColor = System.Drawing.Color.CornflowerBlue;
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameItem,
            this.saveGameItem,
            this.loadGameItem,
            this.pauseItem});
            this.FileMenu.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(56, 26);
            this.FileMenu.Text = "File";
            // 
            // newGameItem
            // 
            this.newGameItem.Name = "newGameItem";
            this.newGameItem.Size = new System.Drawing.Size(200, 34);
            this.newGameItem.Text = "New Game";
            this.newGameItem.Click += new System.EventHandler(this.NewGameClicked);
            // 
            // saveGameItem
            // 
            this.saveGameItem.Name = "saveGameItem";
            this.saveGameItem.Size = new System.Drawing.Size(200, 34);
            this.saveGameItem.Text = "Save Game";
            this.saveGameItem.Click += new System.EventHandler(this.SaveGame_Clicked);
            // 
            // loadGameItem
            // 
            this.loadGameItem.Name = "loadGameItem";
            this.loadGameItem.Size = new System.Drawing.Size(200, 34);
            this.loadGameItem.Text = "Load Game";
            this.loadGameItem.Click += new System.EventHandler(this.LoadGame_Clicked);
            // 
            // pauseItem
            // 
            this.pauseItem.Name = "pauseItem";
            this.pauseItem.Size = new System.Drawing.Size(200, 34);
            this.pauseItem.Text = "Pause";
            this.pauseItem.Click += new System.EventHandler(this.PauseGameClicked);
            // 
            // SettingsMenu
            // 
            this.SettingsMenu.BackColor = System.Drawing.Color.CornflowerBlue;
            this.SettingsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeSmall,
            this.sizeMedium,
            this.sizeLarge});
            this.SettingsMenu.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SettingsMenu.Name = "SettingsMenu";
            this.SettingsMenu.Size = new System.Drawing.Size(96, 26);
            this.SettingsMenu.Text = "Settings";
            // 
            // sizeSmall
            // 
            this.sizeSmall.Name = "sizeSmall";
            this.sizeSmall.Size = new System.Drawing.Size(159, 34);
            this.sizeSmall.Text = "7x7";
            this.sizeSmall.Click += new System.EventHandler(this.SmallGameClicked);
            // 
            // sizeMedium
            // 
            this.sizeMedium.Name = "sizeMedium";
            this.sizeMedium.Size = new System.Drawing.Size(159, 34);
            this.sizeMedium.Text = "11x11";
            this.sizeMedium.Click += new System.EventHandler(this.MediumGameClicked);
            // 
            // sizeLarge
            // 
            this.sizeLarge.Name = "sizeLarge";
            this.sizeLarge.Size = new System.Drawing.Size(159, 34);
            this.sizeLarge.Text = "15x15";
            this.sizeLarge.Click += new System.EventHandler(this.LargeGameClicked);
            // 
            // _openFileDialog
            // 
            this._openFileDialog.Filter = "Sudoku tábla (*.stl)|*.stl";
            this._openFileDialog.Title = "Sudoku játék betöltése";
            // 
            // _saveFileDialog
            // 
            this._saveFileDialog.Filter = "Sudoku tábla (*.stl)|*.stl";
            this._saveFileDialog.Title = "Sudoku játék mentése";
            // 
            // statusBar
            // 
            this.statusBar.AutoSize = false;
            this.statusBar.BackColor = System.Drawing.Color.RoyalBlue;
            this.statusBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timeLabel1});
            this.statusBar.Location = new System.Drawing.Point(0, 532);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(692, 30);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusBar";
            // 
            // timeLabel1
            // 
            this.timeLabel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.timeLabel1.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.timeLabel1.Name = "timeLabel1";
            this.timeLabel1.Size = new System.Drawing.Size(71, 23);
            this.timeLabel1.Text = "Time: 0";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(692, 562);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Robot Run";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem SettingsMenu;
        private System.Windows.Forms.ToolStripStatusLabel timeLabel1;
        private System.Windows.Forms.ToolStripMenuItem newGameItem;
        private System.Windows.Forms.ToolStripMenuItem sizeSmall;
        private System.Windows.Forms.ToolStripMenuItem sizeMedium;
        private System.Windows.Forms.ToolStripMenuItem sizeLarge;
        private System.Windows.Forms.ToolStripMenuItem pauseItem;
        private System.Windows.Forms.ToolStripMenuItem saveGameItem;
        private System.Windows.Forms.ToolStripMenuItem loadGameItem;
        private System.Windows.Forms.OpenFileDialog _openFileDialog;
        private System.Windows.Forms.SaveFileDialog _saveFileDialog;
    }
}

