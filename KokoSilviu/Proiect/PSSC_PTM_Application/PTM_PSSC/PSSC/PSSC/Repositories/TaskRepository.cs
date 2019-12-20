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
            string author = task.author.name + "(" + task.author.internal_id + ")";
            string dev = task.developer.name + "(" + task.developer.internal_id + ")";
            // Adaugarea datelor
            TableAdapter.Insert(id, nume, description, author, dev, status, prio);
            // Salvarea datelor
            TableAdapterManager.UpdateAll(dbDataSet);
        }

        public System.Collections.IList GetAllTasks()
        {
            return tBinding.List;

        }
        public PSSC.Models.Task GetTask(int taskID)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Silviu\\Desktop\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "SELECT * FROM Tasks WHERE Id=" + taskID + ";";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.SelectCommand = sql_con.CreateCommand();
            da.SelectCommand.CommandText = query;
            da.SelectCommand.ExecuteNonQuery();
            da.Fill(dbDataSet);

            PSSC.Models.Developer author = new PSSC.Models.Developer();
            PSSC.Models.Developer dev = new PSSC.Models.Developer();
            author.internal_id= dbDataSet.Tasks.Rows[0][3].ToString();
            dev.internal_id = dbDataSet.Tasks.Rows[0][4].ToString();

            PSSC.Models.Task t = new PSSC.Models.Task();
            t.id = (int)dbDataSet.Tasks.Rows[0][0];
            t.name = dbDataSet.Tasks.Rows[0][1].ToString();
            t.description = dbDataSet.Tasks.Rows[0][2].ToString();          
            t.author = author;
            t.developer = dev;
            t.status = dbDataSet.Tasks.Rows[0][5].ToString();
            t.priority = dbDataSet.Tasks.Rows[0][6].ToString();

            sql_con.Close();
            return t;
        }
        public async Task UpdateTask(PSSC.Models.Task task)
        {
            SqlConnection sql_con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Silviu\\Desktop\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "Update Tasks SET Status='" + task.status + "' where Id=" + task.id + ";";
            SqlDataAdapter da = new SqlDataAdapter();
            sql_con.Open();
            da.UpdateCommand = sql_con.CreateCommand();
            da.UpdateCommand.CommandText = query;
            await da.UpdateCommand.ExecuteNonQueryAsync();
            sql_con.Close();
        }

        public async Task Delete(PSSC.Models.Task task)
        {
            int id = task.id;
            string nume = task.name;
            string status = task.status;
            string prio = task.priority;
            string author = task.author.name + "(" + task.author.internal_id + ")";
            string dev = task.developer.name + "(" + task.developer.internal_id + ")";
            // Adaugarea datelor
            TableAdapter.Delete(id, nume, author, dev, status, prio);
            // Salvarea datelor
            TableAdapterManager.UpdateAll(dbDataSet);
        }
    }
}
