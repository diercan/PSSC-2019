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
        public PSSC.PsscdbDataSet1TableAdapters.TasksTableAdapter TableAdapter;
        public PSSC.PsscdbDataSet1TableAdapters.TableAdapterManager TableAdapterManager;
        public PSSC.PsscdbDataSet1 dbDataSet;
        public System.Windows.Forms.BindingSource tBinding;

        public TaskRepository(PSSC.PsscdbDataSet1TableAdapters.TasksTableAdapter tta,
                              PSSC.PsscdbDataSet1TableAdapters.TableAdapterManager tam,
                              PSSC.PsscdbDataSet1 dbset,
                              System.Windows.Forms.BindingSource bsource)
        {
            TableAdapter = tta;
            TableAdapterManager = tam;
            dbDataSet = dbset;
            tBinding = bsource;
        }

        public async Task Create(PSSC.Models.Task task)
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
            foreach (PsscdbDataSet1.TasksRow row in dbDataSet.Tasks.Rows)
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
        public async Task UpdateTaskStatus(PSSC.Models.Task task)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "Update Tasks SET Status='" + task.status + "' where Id=" + task.id + ";";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.UpdateCommand = sql_con.CreateCommand();
            da.UpdateCommand.CommandText = query;
            await da.UpdateCommand.ExecuteNonQueryAsync();
            sql_con.Close();
            tBinding.EndEdit();
            TableAdapterManager.UpdateAll(dbDataSet);
        }

        public async Task UpdateTask(PSSC.Models.Task task)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "Update Tasks SET Status='" + task.status + "' where Id=" + task.id + ";";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.UpdateCommand = sql_con.CreateCommand();
            da.UpdateCommand.CommandText = query;
         //   await da.UpdateCommand.ExecuteNonQueryAsync();
            sql_con.Close();
            tBinding.EndEdit();
            TableAdapterManager.UpdateAll(dbDataSet);
        }

        public async Task Delete(string taskID)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "DELETE FROM Tasks where Id='" + taskID + "';";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.DeleteCommand = sql_con.CreateCommand();
            da.DeleteCommand.CommandText = query;
            await da.DeleteCommand.ExecuteNonQueryAsync();
            sql_con.Close();
            tBinding.EndEdit();
            TableAdapterManager.UpdateAll(dbDataSet);
        }

        public int GetPlannedNr(string uid)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "SELECT * FROM Tasks WHERE Status='Planned' AND Developer_uid='"+uid+"';";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.SelectCommand = sql_con.CreateCommand();
            da.SelectCommand.CommandText = query;
            da.SelectCommand.ExecuteNonQuery();
            da.Fill(dbDataSet);
            int cnt = 0;
            foreach (PsscdbDataSet1.TasksRow row in dbDataSet.Tasks.Rows)
            {
                if (row.Developer_uid == uid && row.Status == "Planned")
                {
                    cnt++;
                }
            }

            return cnt;
        }
        public int GetInWorkN(string uid)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "SELECT * FROM Tasks WHERE Status='InWork' AND Developer_uid='" + uid + "';";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.SelectCommand = sql_con.CreateCommand();
            da.SelectCommand.CommandText = query;
            da.SelectCommand.ExecuteNonQuery();
            da.Fill(dbDataSet);
            int cnt = 0;
            foreach(PsscdbDataSet1.TasksRow row in  dbDataSet.Tasks.Rows)
            {
                if (row.Developer_uid == uid && row.Status=="InWork") 
                {
                    cnt++;
                }
            }

            return cnt;
        }
        public int GetRealizedNr(string uid)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "SELECT * FROM Tasks WHERE Status='Realized' AND Developer_uid='" + uid + "';";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.SelectCommand = sql_con.CreateCommand();
            da.SelectCommand.CommandText = query;
            da.SelectCommand.ExecuteNonQuery();
            da.Fill(dbDataSet);
            int cnt = 0;
            foreach (PsscdbDataSet1.TasksRow row in dbDataSet.Tasks.Rows)
            {
                if (row.Developer_uid == uid && row.Status == "Realized")
                {
                    cnt++;
                }
            }

            return cnt;
        }
        public int GetCanceledNr(string uid)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "SELECT * FROM Tasks WHERE Status='Canceled' AND Developer_uid='" + uid + "';";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.SelectCommand = sql_con.CreateCommand();
            da.SelectCommand.CommandText = query;
            da.SelectCommand.ExecuteNonQuery();
            da.Fill(dbDataSet);
            int cnt = 0;
            foreach (PsscdbDataSet1.TasksRow row in dbDataSet.Tasks.Rows)
            {
                if (row.Developer_uid == uid && row.Status == "Canceled")
                {
                    cnt++;
                }
            }

            return cnt;
        }
    }
}
