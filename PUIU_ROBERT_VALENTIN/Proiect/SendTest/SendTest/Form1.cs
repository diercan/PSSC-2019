using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQ.Client;

namespace SendTest
{
    public partial class Form1 : Form
    {
        string filePath = string.Empty;
        List<string> lines = new List<string>();
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
            textBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://zdolgzsq:NAIaHujVMF-WNTxXa7kZ4z5jU5R-BUdB@dove.rmq.cloudamqp.com/zdolgzsq"),
                UserName = "zdolgzsq",
                Password = "NAIaHujVMF-WNTxXa7kZ4z5jU5R-BUdB",
            };

            using (var connection = factory.CreateConnection())
                foreach (string line in lines)
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "hello",
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);


                        var body = Encoding.UTF8.GetBytes(line);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "hello",
                                             basicProperties: null,
                                             body: body);
                    }
                }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    textBox1.Text = filePath;

                    int counter = 0;
                    string line;

                    System.IO.StreamReader file =
                        new System.IO.StreamReader(filePath);
                    while ((line = file.ReadLine()) != null)
                    {
                        lines.Add(line);
                        counter++;
                    }

                    file.Close();

                    if(counter==0)
                    {
                        button1.Enabled = false;
                    }
                }
            }
        }
    }
}
