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
using System.Data.SqlClient;

namespace PSSC
{
    public partial class MainForm : Form
    {
        private ITaskRepository taskRepository;
        private string user_uid = null;
        private bool pm_user;
        FormLogIn fl = new FormLogIn();

        public MainForm()
        {
            this.WindowState = FormWindowState.Minimized;
            InitializeComponent();
            fl.ShowDialog();
            TaskChart.Visible = false;
            taskRepository = new TaskRepository(this.tasksTableAdapter, this.tableAdapterManager, this.psscdbDataSet, tasksBindingSource);   
            panelPower.Visible = false;          
            user_uid = fl.uid;
            pm_user = !fl.dev_user;
            panelDashboard.Visible = false;
            kryptonDataGridView1.Visible = false;
            userLogInBindingSource.Filter = "Type='developer'";
            if (fl.dev_user)
            {
                tasksBindingSource.Filter = "Developer_uid='" +fl. uid + "'";
                panelTasks.Visible = true;
                buttonDashboard.Enabled = false;
                //this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                tasksBindingSource.Filter = "Author_uid='" + fl.uid + "'";
                panelTasks.Visible = true;
                buttonDashboard.Enabled = true;
                
            }
            if(fl.uid!="" && fl.uid!=null)
            {
                
                this.WindowState = FormWindowState.Normal;
            }

        }

        private void buttonTask_Click(object sender, EventArgs e)
        {
            panelindex.Location = new Point(panelindex.Location.X, 117);
            panelTasks.Visible = true;
            TaskChart.Visible = false;
            kryptonDataGridView1.Visible = false;
            panelDashboard.Visible = false;
        }

        private void buttonDashboard_Click(object sender, EventArgs e)
        {
            panelindex.Location = new Point(panelindex.Location.X, 182);
            panelTasks.Visible = false;
            TaskChart.Visible = false;
            kryptonDataGridView1.Visible = false;
            panelDashboard.Visible = true;
        }

        private void buttonStatistic_Click(object sender, EventArgs e)
        {            
            panelDashboard.Visible = false;
            if (pm_user)
            {
                kryptonDataGridView1.Visible = true;
            }
            TaskChart.Series["Series2"].Points.Clear();
            var taskPlannedNr = taskRepository.GetPlannedNr(user_uid,pm_user);
            var taskInWorkNr = taskRepository.GetInWorkN(user_uid,pm_user);
            var taskRealizedNr = taskRepository.GetRealizedNr(user_uid,pm_user);
            var taskCanceledNr = taskRepository.GetCanceledNr(user_uid,pm_user);

            if (taskPlannedNr > 0)
            {
                TaskChart.Series["Series2"].Points.AddXY("Planned(" + taskPlannedNr.ToString() + ")", taskPlannedNr);            
            }

            if (taskInWorkNr > 0)
            {
                TaskChart.Series["Series2"].Points.AddXY("InWork(" + taskInWorkNr.ToString() + ")", taskInWorkNr);
            }

            if (taskRealizedNr > 0)
            {
                TaskChart.Series["Series2"].Points.AddXY("Realized(" + taskRealizedNr.ToString() + ")", taskRealizedNr);
            }

            if (taskCanceledNr > 0)
            {
                TaskChart.Series["Series2"].Points.AddXY("Canceled(" + taskCanceledNr.ToString()+")", taskCanceledNr);
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
            this.tableAdapterManager.UpdateAll(this.psscdbDataSet);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {            
            // TODO: This line of code loads data into the 'psscdbDataSet.UserLogIn' table. You can move, or remove it, as needed.
            this.userLogInTableAdapter.Fill(this.psscdbDataSet.UserLogIn);
            // TODO: This line of code loads data into the 'psscdbDataSet.Tasks' table. You can move, or remove it, as needed.
           // this.tasksTableAdapter.Fill(this.psscdbDataSet.Tasks);
            // TODO: This line of code loads data into the 'psscdbDataSet.UserLogIn' table. You can move, or remove it, as needed.
            //this.userLogInTableAdapter.Fill(this.psscdbDataSet.UserLogIn);
            // TODO: This line of code loads data into the 'psscdbDataSet.Tasks' table. You can move, or remove it, as needed.
            this.tasksTableAdapter.Fill(this.psscdbDataSet.Tasks);
            // TODO: This line of code loads data into the 'psscdbDataSet1.User' table. You can move, or remove it, as needed.
            //this.userTableAdapter.Fill(this.psscdbDataSet.User);
            // TODO: This line of code loads data into the 'psscdbDataSet1.Tasks' table. You can move, or remove it, as needed.
            //this.tasksTableAdapter.Fill(this.psscdbDataSet.Tasks);

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
                taskRepository.UpdateTaskStatus(task);

                this.tasksBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.psscdbDataSet);
                //kryptonDataGridViewTasks.DataSource=psscdbDataSet1.Tasks;
               // kryptonDataGridViewTasks.Update();
               // kryptonDataGridViewTasks.Refresh();

                
                
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

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string taskId = null;
            if (textBox1.Text!= null && textBox1.Text!="")
            {
                taskId = textBox1.Text.ToString();
            }
            //get aggregate instance
            if (taskId != "" && taskId != null)
            {
                //delete the task
                taskRepository.Delete(taskId);
                //refresh datagridview to observe the changes
                this.tasksBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.psscdbDataSet);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string taskId = null;
            string taskName = null;
            string taskDesc = null;
            string taskDev = null;
            string taskStatus = null;
            string taskPrio = null;
            if (textBoxId.Text != null && textBoxId.Text != "")
            {
                taskId = textBoxId.Text.ToString();
            }

            if (textBoxName.Text != null && textBoxName.Text != "")
            {
                taskName = textBoxName.Text.ToString();
            }

            if (textBoxDesc.Text != null && textBoxDesc.Text != "")
            {
                taskDesc = textBoxDesc.Text.ToString();
            }

            if (textBoxDev.Text != null && textBoxDev.Text != "")
            {
                taskDev = textBoxDev.Text.ToString();
            }

            if (textBoxStatus.Text != null && textBoxStatus.Text != "")
            {
                taskStatus = textBoxStatus.Text.ToString();
            }

            if (textBoxPrio.Text != null && textBoxPrio.Text != "")
            {
                taskPrio = textBoxPrio.Text.ToString();
            }
            //get aggregate instance
            if (taskId != null && taskName != null && taskDesc != null && taskDev!=null && taskStatus!=null && taskPrio!=null)
            {
                Developer ath = new Developer(user_uid);
                Developer dev = new Developer(taskDev);
                PSSC.Models.Task tsk = new PSSC.Models.Task(int.Parse(taskId), taskName, ath, dev, taskDesc, taskStatus, taskPrio);
                //delete the task
                taskRepository.Create(tsk);
                //refresh datagridview to observe the changes
                this.tasksBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.psscdbDataSet);
            }
        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            if (textBoxId.Text != "" && textBoxId.Text != null)
            {
                var task = taskRepository.GetTask(int.Parse(textBoxId.Text));
                //change properties of the task
                if(task.status!=textBoxStatus.Text && textBoxStatus.Text!="" && textBoxStatus.Text!=null)
                {
                    task.ChangeStatus(textBoxStatus.Text);
                }

                if (task.priority != textBoxPrio.Text && textBoxPrio.Text != "" && textBoxPrio.Text != null)
                {
                    task.ChangePrio(textBoxPrio.Text);
                }

                if (task.description != textBoxDesc.Text && textBoxDesc.Text != "" && textBoxDesc.Text != null)
                {
                    task.ChangeDescription(textBoxDesc.Text);
                }

                if (task.name != textBoxName.Text && textBoxName.Text != "" && textBoxName.Text != null)
                {
                    task.ChangeName(textBoxName.Text);
                }

                if (task.developer.internal_id != textBoxDev.Text && textBoxDev.Text != "" && textBoxDev.Text != null)
                {
                    Developer d = new Developer(textBoxDev.Text);
                    task.Assign(d);
                }

                //update the db with the changes
                taskRepository.UpdateTask(task);

                //refresh datagridview to observe the changes
                this.tasksBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.psscdbDataSet);
            }
        }

        private void kryptonButtonAddDev_Click(object sender, EventArgs e)
        {
            if (textBoxAddId.Text != "" &&  textBoxAddId.Text != null )
            {
                Developer d = new Developer(textBoxAddId.Text);
                taskRepository.AddDeveloper(d);
            }
        }

        private void kryptonButtonDeleteDev_Click(object sender, EventArgs e)
        {
            if (textBoxAddId.Text != "" && textBoxAddId.Text != null)
            {
                Developer d = new Developer(textBoxAddId.Text);
                taskRepository.DeleteDeveloper(d);
            }
        }
    }
}
