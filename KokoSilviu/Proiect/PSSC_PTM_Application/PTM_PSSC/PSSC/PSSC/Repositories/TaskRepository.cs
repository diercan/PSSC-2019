using PSSC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSSC;
using System.Data.SqlClient;


namespace PSSC.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public PSSC.PsscdbDataSetTableAdapters.TasksTableAdapter TableAdapter;
        public PSSC.PsscdbDataSetTableAdapters.TableAdapterManager TableAdapterManager;
        public PSSC.PsscdbDataSet dbDataSet;
        public System.Windows.Forms.BindingSource tBinding;

        public TaskRepository(PSSC.PsscdbDataSetTableAdapters.TasksTableAdapter tta,
                              PSSC.PsscdbDataSetTableAdapters.TableAdapterManager tam,
                              PSSC.PsscdbDataSet dbset,
                              System.Windows.Forms.BindingSource bsource)
        {
            TableAdapter = tta;
            TableAdapterManager = tam;
            dbDataSet = dbset;
            tBinding = bsource;
        }

        public void Create(PSSC.Models.Task task)
        {
            int id = task.id;
            string nume = task.name;
            string description = task.description;
            string status = task.status;
            string prio = task.priority;
           // string author = task.author.name + "(" + task.author.internal_id + ")";
            //string dev = task.developer.name + "(" + task.developer.internal_id + ")";
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "INSERT INTO Tasks VALUES('" + id + "','" + nume + "','" + description + "','" + task.author.internal_id + "','" + task.developer.internal_id + "','" + status + "','" + prio + "');";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.InsertCommand = sql_con.CreateCommand();
            da.InsertCommand.CommandText = query;
            da.InsertCommand.ExecuteNonQuery();
            sql_con.Close();
            tBinding.EndEdit();
            TableAdapterManager.UpdateAll(dbDataSet);
        }

        public System.Collections.IList GetAllTasks()
        {
            return tBinding.List;

        }
        public PSSC.Models.Task GetTask(int taskID)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "SELECT * FROM Tasks WHERE Id=" + taskID + ";";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.SelectCommand = sql_con.CreateCommand();
            da.SelectCommand.CommandText = query;
            da.SelectCommand.ExecuteNonQuery();
            da.Fill(dbDataSet);

            PSSC.Models.Developer author = new PSSC.Models.Developer();
            PSSC.Models.Developer dev = new PSSC.Models.Developer();
            int cnt = 0, i = 0;
            foreach (PsscdbDataSet.TasksRow row in dbDataSet.Tasks.Rows)
            {
                if (row.Id == taskID)
                {
                    cnt=i;
                }
               i++;
            }
            author.UpdateInternalID(dbDataSet.Tasks.Rows[cnt][3].ToString());
            dev.UpdateInternalID(dbDataSet.Tasks.Rows[cnt][4].ToString());

            PSSC.Models.Task t = new PSSC.Models.Task();
            t.ChangeID((int)dbDataSet.Tasks.Rows[cnt][0]);
            t.ChangeName(dbDataSet.Tasks.Rows[cnt][1].ToString());
            t.ChangeDescription(dbDataSet.Tasks.Rows[cnt][2].ToString());          
            t.ChangeAuthor(author);
            t.Assign(dev);
            t.ChangeStatus(dbDataSet.Tasks.Rows[cnt][5].ToString());
            t.ChangePrio(dbDataSet.Tasks.Rows[cnt][6].ToString());

            sql_con.Close();
            return t;
        }
        public void UpdateTaskStatus(PSSC.Models.Task task)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "Update Tasks SET Status='" + task.status + "' where Id=" + task.id + ";";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.UpdateCommand = sql_con.CreateCommand();
            da.UpdateCommand.CommandText = query;
            da.UpdateCommand.ExecuteNonQuery();
            sql_con.Close();
            
            TableAdapterManager.UpdateAll(dbDataSet);
            tBinding.EndEdit();
        }

        public void UpdateTask(PSSC.Models.Task task)
        {

            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");           
            string query = "UPDATE Tasks SET Name='" + task.name + "',Description='" + task.description + "',Developer_uid='"  + task.developer.internal_id + "',Status='" + task.status + "',Priority='" + task.priority + "' WHERE id="+task.id+";";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.UpdateCommand = sql_con.CreateCommand();
            da.UpdateCommand.CommandText = query;
            da.UpdateCommand.ExecuteNonQuery();
            sql_con.Close();
            TableAdapterManager.UpdateAll(dbDataSet);
            tBinding.EndEdit();
        }

        public void Delete(string taskID)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "DELETE FROM Tasks where Id='" + taskID + "';";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.DeleteCommand = sql_con.CreateCommand();
            da.DeleteCommand.CommandText = query;
            da.DeleteCommand.ExecuteNonQuery();
            sql_con.Close();           
            tBinding.EndEdit();
            TableAdapterManager.UpdateAll(dbDataSet);
        }

        public int GetPlannedNr(string uid, bool pm_user)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = null;
            if (pm_user)
            {
                 query = "SELECT * FROM Tasks WHERE Status='Planned';";
            }
            else
            {
                 query = "SELECT * FROM Tasks WHERE Status='Planned' AND Developer_uid='" + uid + "';";
            }
             SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.SelectCommand = sql_con.CreateCommand();
            da.SelectCommand.CommandText = query;
            da.SelectCommand.ExecuteNonQuery();
            da.Fill(dbDataSet);
            int cnt = 0;
            foreach (PsscdbDataSet.TasksRow row in dbDataSet.Tasks.Rows)
            {
                if (row.Status == "Planned")
                {
                    if (row.Developer_uid == uid && !pm_user)
                    {
                        cnt++;
                    }
                    else if(pm_user)
                    {
                        cnt++;
                    }
                }
            }

            return cnt;
        }
        public int GetInWorkN(string uid, bool pm_user)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = null;
            if (pm_user)
            {
                query = "SELECT * FROM Tasks WHERE Status='InWork';";
            }
            else
            {
                query = "SELECT * FROM Tasks WHERE Status='InWork' AND Developer_uid='" + uid + "';";
            }
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.SelectCommand = sql_con.CreateCommand();
            da.SelectCommand.CommandText = query;
            da.SelectCommand.ExecuteNonQuery();
            da.Fill(dbDataSet);
            int cnt = 0;
            foreach(PsscdbDataSet.TasksRow row in  dbDataSet.Tasks.Rows)
            {
                if (row.Status == "InWork")
                {
                    if (row.Developer_uid == uid && !pm_user)
                    {
                        cnt++;
                    }
                    else if (pm_user)
                    {
                        cnt++;
                    }
                }
            }

            return cnt;
        }
        public int GetRealizedNr(string uid, bool pm_user)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = null;
            if (pm_user)
            {
                query = "SELECT * FROM Tasks WHERE Status='Realized';";
            }
            else
            {
                query = "SELECT * FROM Tasks WHERE Status='Realized' AND Developer_uid='" + uid + "';";
            }
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.SelectCommand = sql_con.CreateCommand();
            da.SelectCommand.CommandText = query;
            da.SelectCommand.ExecuteNonQuery();
            da.Fill(dbDataSet);
            int cnt = 0;
            foreach (PsscdbDataSet.TasksRow row in dbDataSet.Tasks.Rows)
            {
                if (row.Status == "Realized")
                {
                    if (row.Developer_uid == uid && !pm_user)
                    {
                        cnt++;
                    }
                    else if (pm_user)
                    {
                        cnt++;
                    }
                }
            }

            return cnt;
        }
        public int GetCanceledNr(string uid, bool pm_user)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = null;
            if (pm_user)
            {
                query = "SELECT * FROM Tasks WHERE Status='Canceled';";
            }
            else
            {
                query = "SELECT * FROM Tasks WHERE Status='Canceled' AND Developer_uid='" + uid + "';";
            }
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.SelectCommand = sql_con.CreateCommand();
            da.SelectCommand.CommandText = query;
            da.SelectCommand.ExecuteNonQuery();
            da.Fill(dbDataSet);
            int cnt = 0;
            foreach (PsscdbDataSet.TasksRow row in dbDataSet.Tasks.Rows)
            {
                if (row.Status == "Canceled")
                {
                    if (row.Developer_uid == uid && !pm_user)
                    {
                        cnt++;
                    }
                    else if (pm_user)
                    {
                        cnt++;
                    }
                }
            }
            return cnt;
        }
        public void AddDeveloper(PSSC.Models.Developer d)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "INSERT INTO UserLogIn VALUES('" + d.internal_id + "','123456789','developer');";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.InsertCommand = sql_con.CreateCommand();
            da.InsertCommand.CommandText = query;
            da.InsertCommand.ExecuteNonQuery();
            sql_con.Close();
            tBinding.EndEdit();
            TableAdapterManager.UpdateAll(dbDataSet);
        }
        public void DeleteDeveloper(PSSC.Models.Developer d)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "DELETE FROM UserLogIn where uid='" + d.internal_id + "';";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.DeleteCommand = sql_con.CreateCommand();
            da.DeleteCommand.CommandText = query;
            da.DeleteCommand.ExecuteNonQuery();
            sql_con.Close();
            tBinding.EndEdit();
            TableAdapterManager.UpdateAll(dbDataSet);
        }
    }
}
