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
        private string user_uid = null;
        public MainForm(string uid,bool dev_user)
        {
            InitializeComponent();
            TaskChart.Visible = false;
            taskRepository = new TaskRepository(this.tasksTableAdapter, this.tableAdapterManager, this.psscdbDataSet1, tasksBindingSource);   
            panelPower.Visible = false;          
            user_uid = uid;
            panelDashboard.Visible = false;
            if (dev_user)
            {
                tasksBindingSource.Filter = "Developer_uid='" + uid + "'";
                panelTasks.Visible = true;
                buttonDashboard.Enabled = false;
            }
            else
            {
                tasksBindingSource.Filter = "Author_uid='" + uid + "'";
                panelTasks.Visible = true;
                buttonDashboard.Enabled = true;
            }

        }

        private void buttonTask_Click(object sender, EventArgs e)
        {
            panelindex.Location = new Point(panelindex.Location.X, 117);
            panelTasks.Visible = true;
            TaskChart.Visible = false;
            panelDashboard.Visible = false;
        }

        private void buttonDashboard_Click(object sender, EventArgs e)
        {
            panelindex.Location = new Point(panelindex.Location.X, 182);
            panelDashboard.Visible = true;
        }

        private void buttonStatistic_Click(object sender, EventArgs e)
        {
            panelDashboard.Visible = false;
            var taskPlannedNr = taskRepository.GetPlannedNr(user_uid);
            var taskInWorkNr = taskRepository.GetInWorkN(user_uid);
            var taskRealizedNr = taskRepository.GetRealizedNr(user_uid);
            var taskCanceledNr = taskRepository.GetCanceledNr(user_uid);

            if (taskPlannedNr > 0)
            {
                TaskChart.Series["Series2"].Points.AddXY("Planned", taskPlannedNr);            
            }

            if (taskInWorkNr > 0)
            {
                TaskChart.Series["Series2"].Points.AddXY("InWork", taskInWorkNr);
            }

            if (taskRealizedNr > 0)
            {
                TaskChart.Series["Series2"].Points.AddXY("Realized", taskRealizedNr);
            }

            if (taskCanceledNr > 0)
            {
                TaskChart.Series["Series2"].Points.AddXY("Canceled", taskCanceledNr);
            }

            panelindex.Location = new Point(panelindex.Location.X, 248);
            panelTasks.Visible = false;
            TaskChart.Visible = true;
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
            string status = null;
            if (comboBoxStatus.SelectedItem!= null)
            {
              status = comboBoxStatus.SelectedItem.ToString();
            }

            //update database with new status value;
            //get aggregate instance
            if (status != "" && status != null)
            {
                var task = taskRepository.GetTask(taskID);
                //change status of the task
                task.ChangeStatus(status);
                //update the db with the changes
                taskRepository.UpdateTask(task);

                //refresh datagridview to observe the changes
                this.tasksBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.psscdbDataSet1);
            }
        }

        int taskID = 0;
        private void kryptonDataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (kryptonDataGridViewTasks.SelectedRows.Count>0)
            {
                DataGridViewRow row = kryptonDataGridViewTasks.SelectedRows[0];
                if ((int)row.Cells[6].Value > 0)
                {
                    taskID = (int)row.Cells[6].Value;
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
