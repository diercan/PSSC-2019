using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;

namespace lhotelsApp
{
    public class MQTTBroker
    {
        public bool connected = false;
        public IManagedMqttClient client = new MqttFactory().CreateManagedMqttClient();

        public async Task ConnectAsync()
        {
            string clientId = Guid.NewGuid().ToString();
            string mqttURI = "mqtt://giruzqus:0WSzZTInshqj@farmer.cloudmqtt.com:10472";
            string mqttUser = "giruzqus";
            string mqttPassword = "0WSzZTInshqj";
            int mqttPort = 10472;
            bool mqttSecure = false;
            var messageBuilder = new MqttClientOptionsBuilder()
    .WithClientId(clientId)
                .WithCredentials(mqttUser, mqttPassword)
                .WithTcpServer(mqttURI, mqttPort)
    .WithCleanSession();
            var options = mqttSecure
                ? messageBuilder
                .WithTls()
                .Build()
                : messageBuilder
                .Build();
            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(options)
                .Build();
            await client.StartAsync(managedOptions);
        }

        public async Task PublishAsync(string topic, string payload, bool retainFlag = true, int qos = 1) =>
        await client.PublishAsync(new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)qos)
            .WithRetainFlag(retainFlag)
            .Build());


    }
}
