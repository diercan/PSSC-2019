using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using MqttPusher.Client;
using Rn.Logging.Interfaces;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace lhotelsApp
{



    public partial class Form1 : Form
    {
        MqttClient client = new MqttClient("farmer.cloudmqtt.com", 10472, false, null, null, new MqttSslProtocols());
        public bool connectedToMQTT = false;
        public Form1()
        {
            InitializeComponent();

        }



        private async void registerHotelButton_Click(object sender, EventArgs e)
        {
            if (!connectedToMQTT)
            {

                Console.Write(client.Settings.Port);
                byte code = client.Connect(Guid.NewGuid().ToString(), "giruzqus", "0WSzZTInshqj");
                connectedToMQTT = true;
            }

            if (hotelName.Text.Length > 0 && hotelImg.Text.Length > 0 && hotelPhone.Text.Length > 0 && hotelLocation.Text.Length > 0 && hotelRating.Text.Length>0)
            {
                ushort msgId = client.Publish("api/mqtt/test",
             Encoding.UTF8.GetBytes("{\"name\":\"" + hotelName.Text + "\",\"img\":\"" + hotelImg.Text + "\",\"phone\":\"" + hotelPhone.Text + "\",\"city\":\"" + hotelLocation.Text + "\",\"rating\":\"" + hotelRating.Text + "\"}"),
             MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
             false); // retained
                Console.WriteLine("Sent Hotel to MQTT");
                successLabel.Text = "Hotel registered!";
                registerHotelButton.Enabled = false;
            }
            else
            {
                toolTip1.SetToolTip(registerHotelButton, "All fields are required");
            }

        }

        private void hotelRating_TextChanged(object sender, EventArgs e)
        {

        }

        private void hotelPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void hotelImg_TextChanged(object sender, EventArgs e)
        {

        }

        private void hotelName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void transparentTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
