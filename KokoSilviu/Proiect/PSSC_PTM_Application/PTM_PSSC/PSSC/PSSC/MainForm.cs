using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PSSC.Models;
using PSSC.Repositories;


namespace PSSC
{
    public partial class MainForm : Form
    {
        private ITaskRepository taskRepository;

        public MainForm(string uid,bool dev_user)
        {
            InitializeComponent();
            taskRepository = new TaskRepository(this.tasksTableAdapter, this.tableAdapterManager, this.psscdbDataSet1, tasksBindingSource);   
            panelPower.Visible = false;
            if (dev_user)
            {
                tasksBindingSource.Filter = "Developer_uid='" + uid + "'";
                panelTasks.Visible = true;
            }
            else
            {
                tasksBindingSource.Filter = "Author_uid='" + uid + "'";
                panelTasks.Visible = false;
            }

        }

        private void buttonTask_Click(object sender, EventArgs e)
        {
            panelindex.Location = new Point(panelindex.Location.X, 117);
        }

        private void buttonDashboard_Click(object sender, EventArgs e)
        {
            panelindex.Location = new Point(panelindex.Location.X, 182);
        }

        private void buttonStatistic_Click(object sender, EventArgs e)
        {
            panelindex.Location = new Point(panelindex.Location.X, 248);
        }

        private void panel6_MouseEnter(object sender, EventArgs e)
        {
            panelPower.Visible = false;
        }

        private void panel5_MouseEnter(object sender, EventArgs e)
        {
            panelPower.Visible = false;
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            panelPower.Visible = false;
        }

        private void panel4_MouseEnter(object sender, EventArgs e)
        {
            panelPower.Visible = false;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            panelPower.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void tasksBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tasksBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.psscdbDataSet1);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'psscdbDataSet1.Tasks' table. You can move, or remove it, as needed.
            this.tasksTableAdapter.Fill(this.psscdbDataSet1.Tasks);

        }

        //ChangeStatus button event
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            string status = comboBoxStatus.SelectedItem.ToString();
            //update database with new status value;
            //get aggregate instance
           var task = taskRepository.GetTask(taskID);
            //change status of the task
           task.ChangeStatus(status);
            //update the db with the changes
           taskRepository.UpdateTask(task);

            //refresh datagridview to observe the changes
            BindingSource bds = new BindingSource();
            tasksBindingSource.ResetBindings(true);
            bds = tasksBindingSource;
           kryptonDataGridViewTasks.DataSource = null;
           kryptonDataGridViewTasks.DataSource = bds;
           kryptonDataGridViewTasks.Refresh();
           kryptonDataGridViewTasks.Update();
        }

        private void kryptonButtonDeleteTasks_Click(object sender, EventArgs e)
        {
            //Delete all selected tasks
        }

        int taskID = 0;
        private void kryptonDataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (kryptonDataGridViewTasks.SelectedRows.Count>0)
            {
                DataGridViewRow row = kryptonDataGridViewTasks.SelectedRows[0];
                if ((int)row.Cells[0].Value > 0)
                {
                    taskID = (int)row.Cells[0].Value;
                }
                else
                {
                    taskID = 0;
                }
            }
        }

        private void kryptonDataGridViewTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string s = sender.ToString();
        }

    }
}
