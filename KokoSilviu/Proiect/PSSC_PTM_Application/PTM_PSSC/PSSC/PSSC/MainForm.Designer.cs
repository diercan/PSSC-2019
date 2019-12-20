namespace PSSC
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonTask = new System.Windows.Forms.Button();
            this.panelindex = new System.Windows.Forms.Panel();
            this.panelStatistic = new System.Windows.Forms.Panel();
            this.panelTasks = new System.Windows.Forms.Panel();
            this.kryptonButtonDeleteTasks = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonStatusChange = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.kryptonDataGridViewTasks = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.authoruidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.developeruidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priorityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tasksBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.psscdbDataSet1 = new PSSC.PsscdbDataSet1();
            this.buttonDashboard = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelPower = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tasksTableAdapter = new PSSC.PsscdbDataSet1TableAdapters.TasksTableAdapter();
            this.tableAdapterManager = new PSSC.PsscdbDataSet1TableAdapters.TableAdapterManager();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.buttonStatistic = new System.Windows.Forms.Button();
            this.panelStatistic.SuspendLayout();
            this.panelTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridViewTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tasksBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.psscdbDataSet1)).BeginInit();
            this.panelPower.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Location = new System.Drawing.Point(-3, -16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(204, 88);
            this.panel1.TabIndex = 1;
            // 
            // buttonTask
            // 
            this.buttonTask.FlatAppearance.BorderSize = 0;
            this.buttonTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTask.ForeColor = System.Drawing.Color.White;
            this.buttonTask.Location = new System.Drawing.Point(-3, 117);
            this.buttonTask.Name = "buttonTask";
            this.buttonTask.Size = new System.Drawing.Size(204, 65);
            this.buttonTask.TabIndex = 0;
            this.buttonTask.Text = "Tasks";
            this.buttonTask.UseVisualStyleBackColor = true;
            this.buttonTask.Click += new System.EventHandler(this.buttonTask_Click);
            // 
            // panelindex
            // 
            this.panelindex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelindex.BackgroundImage")));
            this.panelindex.Location = new System.Drawing.Point(191, 117);
            this.panelindex.Name = "panelindex";
            this.panelindex.Size = new System.Drawing.Size(10, 65);
            this.panelindex.TabIndex = 5;
            // 
            // panelStatistic
            // 
            this.panelStatistic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(35)))));
            this.panelStatistic.Controls.Add(this.panelTasks);
            this.panelStatistic.Location = new System.Drawing.Point(207, 73);
            this.panelStatistic.Name = "panelStatistic";
            this.panelStatistic.Size = new System.Drawing.Size(809, 540);
            this.panelStatistic.TabIndex = 6;
            // 
            // panelTasks
            // 
            this.panelTasks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(20)))));
            this.panelTasks.Controls.Add(this.kryptonButtonDeleteTasks);
            this.panelTasks.Controls.Add(this.kryptonButtonStatusChange);
            this.panelTasks.Controls.Add(this.comboBoxStatus);
            this.panelTasks.Controls.Add(this.kryptonDataGridViewTasks);
            this.panelTasks.Location = new System.Drawing.Point(16, 15);
            this.panelTasks.Name = "panelTasks";
            this.panelTasks.Size = new System.Drawing.Size(742, 378);
            this.panelTasks.TabIndex = 0;
            // 
            // kryptonButtonDeleteTasks
            // 
            this.kryptonButtonDeleteTasks.Location = new System.Drawing.Point(263, 272);
            this.kryptonButtonDeleteTasks.Name = "kryptonButtonDeleteTasks";
            this.kryptonButtonDeleteTasks.Size = new System.Drawing.Size(102, 25);
            this.kryptonButtonDeleteTasks.TabIndex = 3;
            this.kryptonButtonDeleteTasks.Values.Text = "Delete Tasks";
            this.kryptonButtonDeleteTasks.Click += new System.EventHandler(this.kryptonButtonDeleteTasks_Click);
            // 
            // kryptonButtonStatusChange
            // 
            this.kryptonButtonStatusChange.Location = new System.Drawing.Point(155, 271);
            this.kryptonButtonStatusChange.Name = "kryptonButtonStatusChange";
            this.kryptonButtonStatusChange.Size = new System.Drawing.Size(90, 26);
            this.kryptonButtonStatusChange.TabIndex = 2;
            this.kryptonButtonStatusChange.Values.Text = "Change Status";
            this.kryptonButtonStatusChange.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "Planned",
            "InWork",
            "Realized",
            "Canceled"});
            this.comboBoxStatus.Location = new System.Drawing.Point(3, 276);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(145, 21);
            this.comboBoxStatus.TabIndex = 1;
            // 
            // kryptonDataGridViewTasks
            // 
            this.kryptonDataGridViewTasks.AutoGenerateColumns = false;
            this.kryptonDataGridViewTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kryptonDataGridViewTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.authoruidDataGridViewTextBoxColumn,
            this.developeruidDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.priorityDataGridViewTextBoxColumn});
            this.kryptonDataGridViewTasks.DataSource = this.tasksBindingSource;
            this.kryptonDataGridViewTasks.Location = new System.Drawing.Point(3, 7);
            this.kryptonDataGridViewTasks.Name = "kryptonDataGridViewTasks";
            this.kryptonDataGridViewTasks.ReadOnly = true;
            this.kryptonDataGridViewTasks.Size = new System.Drawing.Size(741, 259);
            this.kryptonDataGridViewTasks.TabIndex = 0;
            this.kryptonDataGridViewTasks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.kryptonDataGridViewTasks_CellContentClick);
            this.kryptonDataGridViewTasks.SelectionChanged += new System.EventHandler(this.kryptonDataGridView1_SelectionChanged);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            this.descriptionDataGridViewTextBoxColumn.Width = 240;
            // 
            // authoruidDataGridViewTextBoxColumn
            // 
            this.authoruidDataGridViewTextBoxColumn.DataPropertyName = "Author_uid";
            this.authoruidDataGridViewTextBoxColumn.HeaderText = "Author_uid";
            this.authoruidDataGridViewTextBoxColumn.Name = "authoruidDataGridViewTextBoxColumn";
            this.authoruidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // developeruidDataGridViewTextBoxColumn
            // 
            this.developeruidDataGridViewTextBoxColumn.DataPropertyName = "Developer_uid";
            this.developeruidDataGridViewTextBoxColumn.HeaderText = "Developer_uid";
            this.developeruidDataGridViewTextBoxColumn.Name = "developeruidDataGridViewTextBoxColumn";
            this.developeruidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            this.statusDataGridViewTextBoxColumn.Width = 90;
            // 
            // priorityDataGridViewTextBoxColumn
            // 
            this.priorityDataGridViewTextBoxColumn.DataPropertyName = "Priority";
            this.priorityDataGridViewTextBoxColumn.HeaderText = "Priority";
            this.priorityDataGridViewTextBoxColumn.Name = "priorityDataGridViewTextBoxColumn";
            this.priorityDataGridViewTextBoxColumn.ReadOnly = true;
            this.priorityDataGridViewTextBoxColumn.Width = 70;
            // 
            // tasksBindingSource
            // 
            this.tasksBindingSource.DataMember = "Tasks";
            this.tasksBindingSource.DataSource = this.psscdbDataSet1;
            // 
            // psscdbDataSet1
            // 
            this.psscdbDataSet1.DataSetName = "PsscdbDataSet1";
            this.psscdbDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // buttonDashboard
            // 
            this.buttonDashboard.FlatAppearance.BorderSize = 0;
            this.buttonDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDashboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDashboard.ForeColor = System.Drawing.Color.White;
            this.buttonDashboard.Location = new System.Drawing.Point(-2, 182);
            this.buttonDashboard.Name = "buttonDashboard";
            this.buttonDashboard.Size = new System.Drawing.Size(204, 65);
            this.buttonDashboard.TabIndex = 9;
            this.buttonDashboard.Text = "Dashboard";
            this.buttonDashboard.UseVisualStyleBackColor = true;
            this.buttonDashboard.Click += new System.EventHandler(this.buttonDashboard_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(12, 696);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(41, 34);
            this.button2.TabIndex = 10;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.MouseEnter += new System.EventHandler(this.button2_MouseEnter);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(13, 731);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(165, 19);
            this.panel2.TabIndex = 11;
            this.panel2.MouseEnter += new System.EventHandler(this.panel2_MouseEnter);
            // 
            // panelPower
            // 
            this.panelPower.Controls.Add(this.button4);
            this.panelPower.Controls.Add(this.button3);
            this.panelPower.Location = new System.Drawing.Point(54, 690);
            this.panelPower.Name = "panelPower";
            this.panelPower.Size = new System.Drawing.Size(86, 44);
            this.panelPower.TabIndex = 12;
            // 
            // button4
            // 
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(43, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(41, 34);
            this.button4.TabIndex = 12;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(-1, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(41, 34);
            this.button3.TabIndex = 11;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(-5, 690);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(17, 61);
            this.panel4.TabIndex = 13;
            this.panel4.MouseEnter += new System.EventHandler(this.panel4_MouseEnter);
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(153, 690);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(48, 61);
            this.panel5.TabIndex = 14;
            this.panel5.MouseEnter += new System.EventHandler(this.panel5_MouseEnter);
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(-2, 657);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(203, 32);
            this.panel6.TabIndex = 15;
            this.panel6.MouseEnter += new System.EventHandler(this.panel6_MouseEnter);
            // 
            // tasksTableAdapter
            // 
            this.tasksTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.TasksTableAdapter = this.tasksTableAdapter;
            this.tableAdapterManager.UpdateOrder = PSSC.PsscdbDataSet1TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UserTableAdapter = null;
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2010Black;
            // 
            // buttonStatistic
            // 
            this.buttonStatistic.FlatAppearance.BorderSize = 0;
            this.buttonStatistic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStatistic.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStatistic.ForeColor = System.Drawing.Color.White;
            this.buttonStatistic.Location = new System.Drawing.Point(-2, 248);
            this.buttonStatistic.Name = "buttonStatistic";
            this.buttonStatistic.Size = new System.Drawing.Size(204, 65);
            this.buttonStatistic.TabIndex = 16;
            this.buttonStatistic.Text = "Statistic";
            this.buttonStatistic.UseVisualStyleBackColor = true;
            this.buttonStatistic.Click += new System.EventHandler(this.buttonStatistic_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1028, 751);
            this.Controls.Add(this.panelindex);
            this.Controls.Add(this.buttonStatistic);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelPower);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buttonDashboard);
            this.Controls.Add(this.buttonTask);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelStatistic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelStatistic.ResumeLayout(false);
            this.panelTasks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridViewTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tasksBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.psscdbDataSet1)).EndInit();
            this.panelPower.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonTask;
        private System.Windows.Forms.Panel panelindex;
        private System.Windows.Forms.Panel panelStatistic;
        private System.Windows.Forms.Button buttonDashboard;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelPower;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panelTasks;
        private PsscdbDataSet1 psscdbDataSet1;
        private System.Windows.Forms.BindingSource tasksBindingSource;
        private PsscdbDataSet1TableAdapters.TableAdapterManager tableAdapterManager;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView kryptonDataGridViewTasks;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonStatusChange;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonDeleteTasks;
        public PsscdbDataSet1TableAdapters.TasksTableAdapter tasksTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn authoruidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn developeruidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priorityDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button buttonStatistic;
    }
}