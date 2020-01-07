namespace PSSC
{
    partial class FormLogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogIn));
            this.labelUserIcon = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.kryptonButtonLogIn = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonPower = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelUserIcon
            // 
            this.labelUserIcon.BackColor = System.Drawing.Color.Transparent;
            this.labelUserIcon.Image = ((System.Drawing.Image)(resources.GetObject("labelUserIcon.Image")));
            this.labelUserIcon.Location = new System.Drawing.Point(45, 100);
            this.labelUserIcon.Name = "labelUserIcon";
            this.labelUserIcon.Size = new System.Drawing.Size(43, 33);
            this.labelUserIcon.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.Location = new System.Drawing.Point(46, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 33);
            this.label3.TabIndex = 3;
            // 
            // textBoxUser
            // 
            this.textBoxUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxUser.Enabled = false;
            this.textBoxUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.textBoxUser.Location = new System.Drawing.Point(91, 105);
            this.textBoxUser.MaxLength = 25;
            this.textBoxUser.Multiline = true;
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(204, 22);
            this.textBoxUser.TabIndex = 5;
            this.textBoxUser.TextChanged += new System.EventHandler(this.textBoxUser_TextChanged);
            this.textBoxUser.Leave += new System.EventHandler(this.textBoxUser_Leave);
            // 
            // labelUser
            // 
            this.labelUser.BackColor = System.Drawing.Color.Transparent;
            this.labelUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelUser.ForeColor = System.Drawing.Color.White;
            this.labelUser.Location = new System.Drawing.Point(91, 105);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(204, 22);
            this.labelUser.TabIndex = 7;
            this.labelUser.Text = "Username";
            this.labelUser.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Enabled = false;
            this.textBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBoxPassword.Location = new System.Drawing.Point(91, 146);
            this.textBoxPassword.MaxLength = 25;
            this.textBoxPassword.Multiline = true;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(204, 22);
            this.textBoxPassword.TabIndex = 8;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
            this.textBoxPassword.Leave += new System.EventHandler(this.textBoxPassword_Leave);
            // 
            // labelPassword
            // 
            this.labelPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelPassword.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPassword.ForeColor = System.Drawing.Color.White;
            this.labelPassword.Location = new System.Drawing.Point(91, 146);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(204, 22);
            this.labelPassword.TabIndex = 9;
            this.labelPassword.Text = "Password";
            this.labelPassword.Click += new System.EventHandler(this.labelPassword_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(-5, -6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 102);
            this.panel1.TabIndex = 10;
            // 
            // kryptonButtonLogIn
            // 
            this.kryptonButtonLogIn.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Custom3;
            this.kryptonButtonLogIn.Location = new System.Drawing.Point(130, 203);
            this.kryptonButtonLogIn.Name = "kryptonButtonLogIn";
            this.kryptonButtonLogIn.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonButtonLogIn.Size = new System.Drawing.Size(110, 26);
            this.kryptonButtonLogIn.TabIndex = 12;
            this.kryptonButtonLogIn.Values.Text = "Log In";
            this.kryptonButtonLogIn.Click += new System.EventHandler(this.kryptonButtonLogIn_Click);
            // 
            // buttonPower
            // 
            this.buttonPower.BackColor = System.Drawing.Color.Transparent;
            this.buttonPower.FlatAppearance.BorderSize = 0;
            this.buttonPower.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonPower.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(80)))), ((int)(((byte)(97)))));
            this.buttonPower.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPower.ForeColor = System.Drawing.Color.White;
            this.buttonPower.Image = ((System.Drawing.Image)(resources.GetObject("buttonPower.Image")));
            this.buttonPower.Location = new System.Drawing.Point(84, 3);
            this.buttonPower.Name = "buttonPower";
            this.buttonPower.Size = new System.Drawing.Size(38, 37);
            this.buttonPower.TabIndex = 14;
            this.buttonPower.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonPower.UseVisualStyleBackColor = false;
            this.buttonPower.Click += new System.EventHandler(this.button1_Click);
            this.buttonPower.MouseEnter += new System.EventHandler(this.buttonPower_MouseEnter);
            // 
            // buttonSettings
            // 
            this.buttonSettings.BackColor = System.Drawing.Color.Transparent;
            this.buttonSettings.FlatAppearance.BorderSize = 0;
            this.buttonSettings.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(80)))), ((int)(((byte)(97)))));
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.ForeColor = System.Drawing.Color.White;
            this.buttonSettings.Image = ((System.Drawing.Image)(resources.GetObject("buttonSettings.Image")));
            this.buttonSettings.Location = new System.Drawing.Point(37, 3);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(38, 37);
            this.buttonSettings.TabIndex = 15;
            this.buttonSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonSettings.UseVisualStyleBackColor = false;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonInfo
            // 
            this.buttonInfo.BackColor = System.Drawing.Color.Transparent;
            this.buttonInfo.FlatAppearance.BorderSize = 0;
            this.buttonInfo.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(80)))), ((int)(((byte)(97)))));
            this.buttonInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonInfo.ForeColor = System.Drawing.Color.White;
            this.buttonInfo.Image = ((System.Drawing.Image)(resources.GetObject("buttonInfo.Image")));
            this.buttonInfo.Location = new System.Drawing.Point(-1, 3);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(38, 37);
            this.buttonInfo.TabIndex = 16;
            this.buttonInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonInfo.UseVisualStyleBackColor = false;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.buttonInfo);
            this.panel2.Controls.Add(this.buttonSettings);
            this.panel2.Location = new System.Drawing.Point(7, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(79, 42);
            this.panel2.TabIndex = 17;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.Transparent;
            this.panelMenu.Controls.Add(this.panel2);
            this.panelMenu.Controls.Add(this.buttonPower);
            this.panelMenu.Location = new System.Drawing.Point(208, 405);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(126, 44);
            this.panelMenu.TabIndex = 18;
            this.panelMenu.MouseLeave += new System.EventHandler(this.panelMenu_MouseLeave);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.kryptonButtonLogIn);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.labelUser);
            this.panel3.Controls.Add(this.labelPassword);
            this.panel3.Controls.Add(this.textBoxUser);
            this.panel3.Controls.Add(this.textBoxPassword);
            this.panel3.Controls.Add(this.labelUserIcon);
            this.panel3.Location = new System.Drawing.Point(-5, 96);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(381, 308);
            this.panel3.TabIndex = 19;
            this.panel3.MouseEnter += new System.EventHandler(this.panel3_MouseEnter);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Location = new System.Drawing.Point(157, 405);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(54, 100);
            this.panel4.TabIndex = 20;
            this.panel4.MouseEnter += new System.EventHandler(this.panel4_MouseEnter);
            // 
            // FormLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(341, 453);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Click += new System.EventHandler(this.Form1_Click);
            this.panel2.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelUserIcon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonLogIn;
        private System.Windows.Forms.Button buttonPower;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}

