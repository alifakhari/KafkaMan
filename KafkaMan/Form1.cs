using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System.Collections;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using KafkaMan.objs;
using static Confluent.Kafka.ConfigPropertyNames;
using System.Threading;
using System.Net;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using static MassTransit.Logging.OperationName;
using System.Configuration;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using static MassTransit.ValidationResultExtensions;

namespace KafkaMan
{
    public partial class Form1 : Form
    {

        AdminClientConfig adminConfig = new AdminClientConfig()
        {
            BootstrapServers = "192.168.189.128:9092"
        };
        List<CustomTopic> CustomTopics = new List<CustomTopic>();

        class CustomTopic
        {
            public string TopicName { get; set; }
            public int PartitionsNumber { get; set; }

            public override string ToString()
            {
                return TopicName + ", Prtition Count: " + PartitionsNumber;
            }
        }


        public Form1()
        {
            InitializeComponent();
        }


        #region Functions

        public void func_getTopics()
        {
            using (var adminClient = new AdminClientBuilder(adminConfig).Build())
            {
                try
                {
                    //var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(20));
                    //var topicsMetadata = meta.Topics;
                    //var topicNames = meta.Topics.Select(a => a.Topic).ToList();

                    var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
                    var Dictionary = JsonConvert.DeserializeObject<dynamic>(metadata.ToString());

                    var JTopic = Dictionary["Topics"];

                    for (int i = 0; i < JTopic.Count; i++)
                    {
                        string topicname = JTopic[i]["Topic"];
                        cboTopicProducer.Items.Add(topicname);
                        int partitionCount = JTopic[i]["Partitions"].Count;

                        CustomTopic ctopic = new CustomTopic()
                        {
                            TopicName = topicname,
                            PartitionsNumber = partitionCount
                        };

                        CustomTopics.Add(ctopic);

                        lstTopicList.Items.Add(ctopic.ToString());
                    }

                    lblLastSync.Text = "Last sync: " + DateTime.Now;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //public void func_getMessageCountingTopic()
        //{

        //    using (var adminClient = new AdminClientBuilder(adminConfig).Build())
        //    {
        //          var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(20));
        //          var topicsMetadata = metadata.Topics;
        //          var topicNames = metadata.Topics.Select(a => a.Topic).ToList();

        //          foreach (string topic in topicNames)
        //          {
        //          TopicMetadata? topicMetadata = meta.Topics.SingleOrDefault(t => topicValue.Equals(t.Topic));
        //          if (topicMetadata != null)
        //          {
        //            return topicMetadata.Partitions
        //                .Select(x => new TopicPartition(topicMetadata.Topic, x.PartitionId))
        //                .ToList();
        //          }
        //      }
        //    return new List<TopicPartition>();


        //ConsumerConfig config = new ConsumerConfig
        //{
        //    //BootstrapServers = _bootstrapServers,
        //    //GroupId = _groupId,
        //    AutoOffsetReset = AutoOffsetReset.Earliest,
        //};
        //ConsumerBuilder<Null, string> builder = new ConsumerBuilder<Null, string>(config);
        ////builder.SetValueDeserializer(_kafkaConverter);

        //IConsumer<Null, string> consumer = builder.Build();

        //List<TopicPartition> partitions = KafkaAdmin.GetTopicPartitions(, _topic);
        //TopicPartition firstPartition = partitions.First();

        //WatermarkOffsets watermarkOffsets = consumer.QueryWatermarkOffsets(firstPartition, TimeSpan.FromSeconds(10));
        //long total = watermarkOffsets.High - watermarkOffsets.Low;
        //System.Console.WriteLine(total);

        //}
        #endregion Functions


        private void btnTopicAdd_Click(object sender, EventArgs e)
        {
            if (txtTopicName.Text != string.Empty)
            {
                using (var adminClient = new AdminClientBuilder(adminConfig).Build())
                {
                    try
                    {
                        adminClient.CreateTopicsAsync(
                            new[]{
                    new TopicSpecification { Name = txtTopicName.Text, ReplicationFactor =(short)numberReplica.Value, NumPartitions = (short)numberPartition.Value } });

                        txtTopicName.Text = string.Empty;
                        txtTopicName.Focus();
                        lstTopicList.Items.Clear();
                        func_getTopics();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            func_getTopics();
        }

        private async void btnProduce_Click(object sender, EventArgs e)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "192.168.189.128:9092",
                Acks = Acks.Leader,
                //ClientId = Dns.GetHostName(),
                //Debug = "msg",
                //MessageSendMaxRetries = 3,
                //RetryBackoffMs = 1000,
                //EnableIdempotence = true
            };

            using var prod = new ProducerBuilder<Null, string>(config)
                                    //.SetKeySerializer(Serializers.Int64) // to consider a key instead of Null
                                    //.SetValueSerializer(Serializers.Utf8)
                                    //.SetLogHandler((_, message) =>
                                    //Console.WriteLine($"Facility: {message.Facility}-{message.Level} Message: {message.Message}"))
                                    //.SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}. Is Fatal: {e.IsFatal}")))
                                    .Build();
            try
            {

                var result = prod.ProduceAsync(
                            cboTopicProducer.Text,
                            //new Message<Null, string> { Value =$"{DateTime.Now},Topic:{cboTopicProducer.Text},{new Guid().ToString()}" }).Result;
                            new Message<Null, string> { Value = JsonConvert.SerializeObject(new SvcLog(cboTopicProducer.Text, txtProduce.Text)) }
                            ).Result;

                txtProduce.Clear();

                txtLog.Text = $"Inserting  into {result.Topic} and Partition {result.Partition}: {result.Status}, {result.Value}, {(result.Status == PersistenceStatus.Persisted ? "Acked by the leader" : "Not Acked")}\n";
            }
            catch (ProduceException<Null, string> exp)
            {
                txtLog.Text += exp.Message + "\n";
            }
            finally
            {

            }
        }

        private void btnReferesh_Click_1(object sender, EventArgs e)
        {
            lstTopicList.Items.Clear();
            cboTopicProducer.Items.Clear();
            txtLog.Clear();
            txtProduce.Clear();
            func_getTopics();
        }

    }
}
