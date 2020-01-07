namespace lhotelsApp
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.hotelName = new System.Windows.Forms.TextBox();
            this.hotelImg = new System.Windows.Forms.TextBox();
            this.hotelLocation = new System.Windows.Forms.TextBox();
            this.hotelPhone = new System.Windows.Forms.TextBox();
            this.registerHotelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.successLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.hotelRating = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // hotelName
            // 
            this.hotelName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(72)))), ((int)(((byte)(87)))));
            this.hotelName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hotelName.Font = new System.Drawing.Font("Satisfy", 16F);
            this.hotelName.ForeColor = System.Drawing.Color.White;
            this.hotelName.Location = new System.Drawing.Point(477, 182);
            this.hotelName.Margin = new System.Windows.Forms.Padding(5);
            this.hotelName.Name = "hotelName";
            this.hotelName.Size = new System.Drawing.Size(341, 47);
            this.hotelName.TabIndex = 0;
            this.hotelName.TextChanged += new System.EventHandler(this.hotelName_TextChanged);
            // 
            // hotelImg
            // 
            this.hotelImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(72)))), ((int)(((byte)(87)))));
            this.hotelImg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hotelImg.Font = new System.Drawing.Font("Satisfy", 16F);
            this.hotelImg.ForeColor = System.Drawing.Color.White;
            this.hotelImg.Location = new System.Drawing.Point(477, 288);
            this.hotelImg.Name = "hotelImg";
            this.hotelImg.Size = new System.Drawing.Size(341, 47);
            this.hotelImg.TabIndex = 1;
            this.hotelImg.TextChanged += new System.EventHandler(this.hotelImg_TextChanged);
            // 
            // hotelLocation
            // 
            this.hotelLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(72)))), ((int)(((byte)(87)))));
            this.hotelLocation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hotelLocation.Font = new System.Drawing.Font("Satisfy", 16F);
            this.hotelLocation.ForeColor = System.Drawing.Color.White;
            this.hotelLocation.Location = new System.Drawing.Point(476, 485);
            this.hotelLocation.Name = "hotelLocation";
            this.hotelLocation.Size = new System.Drawing.Size(342, 47);
            this.hotelLocation.TabIndex = 2;
            this.hotelLocation.TextChanged += new System.EventHandler(this.hotelRating_TextChanged);
            // 
            // hotelPhone
            // 
            this.hotelPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(72)))), ((int)(((byte)(87)))));
            this.hotelPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hotelPhone.Font = new System.Drawing.Font("Satisfy", 16F);
            this.hotelPhone.ForeColor = System.Drawing.Color.White;
            this.hotelPhone.Location = new System.Drawing.Point(476, 390);
            this.hotelPhone.Name = "hotelPhone";
            this.hotelPhone.Size = new System.Drawing.Size(341, 47);
            this.hotelPhone.TabIndex = 3;
            this.hotelPhone.TextChanged += new System.EventHandler(this.hotelPhone_TextChanged);
            // 
            // registerHotelButton
            // 
            this.registerHotelButton.BackColor = System.Drawing.Color.MistyRose;
            this.registerHotelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registerHotelButton.Font = new System.Drawing.Font("Satisfy", 20F);
            this.registerHotelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(72)))), ((int)(((byte)(87)))));
            this.registerHotelButton.Location = new System.Drawing.Point(440, 639);
            this.registerHotelButton.Name = "registerHotelButton";
            this.registerHotelButton.Size = new System.Drawing.Size(405, 80);
            this.registerHotelButton.TabIndex = 4;
            this.registerHotelButton.Text = "Apply for Registration";
            this.registerHotelButton.UseVisualStyleBackColor = false;
            this.registerHotelButton.Click += new System.EventHandler(this.registerHotelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Satisfy", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MistyRose;
            this.label1.Location = new System.Drawing.Point(997, 366);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 102);
            this.label1.TabIndex = 5;
            this.label1.Text = "LhotelS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Satisfy", 20F);
            this.label2.ForeColor = System.Drawing.Color.MistyRose;
            this.label2.Location = new System.Drawing.Point(940, 453);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(352, 57);
            this.label2.TabIndex = 6;
            this.label2.Text = "- Registration Form -";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Satisfy", 16F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(470, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 46);
            this.label3.TabIndex = 7;
            this.label3.Text = "Hotel Name";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Satisfy", 16F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(470, 245);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(236, 46);
            this.label4.TabIndex = 8;
            this.label4.Text = "Hotel Image Link";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Satisfy", 16F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(470, 349);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(267, 46);
            this.label5.TabIndex = 9;
            this.label5.Text = "Hotel Phone Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Satisfy", 16F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(470, 444);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 46);
            this.label6.TabIndex = 10;
            this.label6.Text = "Hotel City";
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 0;
            this.toolTip1.BackColor = System.Drawing.Color.Transparent;
            this.toolTip1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            // 
            // successLabel
            // 
            this.successLabel.AutoSize = true;
            this.successLabel.BackColor = System.Drawing.Color.Transparent;
            this.successLabel.Font = new System.Drawing.Font("Satisfy", 25F);
            this.successLabel.ForeColor = System.Drawing.Color.White;
            this.successLabel.Location = new System.Drawing.Point(632, 731);
            this.successLabel.Name = "successLabel";
            this.successLabel.Size = new System.Drawing.Size(0, 71);
            this.successLabel.TabIndex = 11;
            this.successLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Satisfy", 16F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(472, 524);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(271, 46);
            this.label7.TabIndex = 13;
            this.label7.Text = "Hotel Official Rating";
            // 
            // hotelRating
            // 
            this.hotelRating.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(72)))), ((int)(((byte)(87)))));
            this.hotelRating.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hotelRating.Font = new System.Drawing.Font("Satisfy", 16F);
            this.hotelRating.ForeColor = System.Drawing.Color.White;
            this.hotelRating.Location = new System.Drawing.Point(478, 565);
            this.hotelRating.Name = "hotelRating";
            this.hotelRating.Size = new System.Drawing.Size(342, 47);
            this.hotelRating.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::lhotelsApp.Properties.Resources.main_bk_full_hd;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1335, 823);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.hotelRating);
            this.Controls.Add(this.successLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.registerHotelButton);
            this.Controls.Add(this.hotelPhone);
            this.Controls.Add(this.hotelLocation);
            this.Controls.Add(this.hotelImg);
            this.Controls.Add(this.hotelName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1357, 879);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1357, 879);
            this.Name = "Form1";
            this.Text = "Hotel Registration for LHotelS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox hotelName;
        private System.Windows.Forms.TextBox hotelImg;
        private System.Windows.Forms.TextBox hotelLocation;
        private System.Windows.Forms.TextBox hotelPhone;
        private System.Windows.Forms.Button registerHotelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label successLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox hotelRating;
    }
}

