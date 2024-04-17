namespace Client
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            Mstrip_mainMenu = new MenuStrip();
            menuToolStripMenuItem = new ToolStripMenuItem();
            Mstrip_connectItem = new ToolStripMenuItem();
            Panel_gameField = new Panel();
            timer1 = new System.Windows.Forms.Timer(components);
            timerFire = new System.Windows.Forms.Timer(components);
            Mstrip_mainMenu.SuspendLayout();
            SuspendLayout();
            // 
            // Mstrip_mainMenu
            // 
            Mstrip_mainMenu.BackColor = Color.OliveDrab;
            Mstrip_mainMenu.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Mstrip_mainMenu.ImageScalingSize = new Size(20, 20);
            Mstrip_mainMenu.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem });
            Mstrip_mainMenu.Location = new Point(0, 0);
            Mstrip_mainMenu.Name = "Mstrip_mainMenu";
            Mstrip_mainMenu.Padding = new Padding(8, 2, 0, 2);
            Mstrip_mainMenu.Size = new Size(608, 31);
            Mstrip_mainMenu.TabIndex = 0;
            Mstrip_mainMenu.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { Mstrip_connectItem });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(69, 27);
            menuToolStripMenuItem.Text = "Menu";
            // 
            // Mstrip_connectItem
            // 
            Mstrip_connectItem.Name = "Mstrip_connectItem";
            Mstrip_connectItem.Size = new Size(159, 28);
            Mstrip_connectItem.Text = "Connect";
            Mstrip_connectItem.Click += Mstrip_connectItem_Click;
            // 
            // Panel_gameField
            // 
            Panel_gameField.BackColor = SystemColors.ActiveCaptionText;
            Panel_gameField.Dock = DockStyle.Fill;
            Panel_gameField.Location = new Point(0, 31);
            Panel_gameField.Name = "Panel_gameField";
            Panel_gameField.Size = new Size(608, 568);
            Panel_gameField.TabIndex = 1;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 200;
            timer1.Tick += timer1_Tick;
            // 
            // timerFire
            // 
            timerFire.Enabled = true;
            timerFire.Interval = 800;
            timerFire.Tick += timerFire_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(608, 599);
            Controls.Add(Panel_gameField);
            Controls.Add(Mstrip_mainMenu);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            ForeColor = SystemColors.ActiveCaptionText;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MainMenuStrip = Mstrip_mainMenu;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "Tank Battle";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            Mstrip_mainMenu.ResumeLayout(false);
            Mstrip_mainMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip Mstrip_mainMenu;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem Mstrip_connectItem;
        private Panel Panel_gameField;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerFire;
    }
}
