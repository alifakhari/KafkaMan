using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System.Collections;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using KafkaMan.objs;

namespace KafkaMan
{
    public partial class Form1 : Form
    {
        AdminClientConfig adminConfig = new AdminClientConfig()
        {
            BootstrapServers = "192.168.189.128:9092"
        };

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
                        cbotopicConsumer.Items.Add(topicname);
                        cboTopicProducer.Items.Add(topicname);

                        int partitionCount = JTopic[i]["Partitions"].Count;
                        string info = topicname + ", Partitions: " + partitionCount;

                        lstTopicList.Items.Add(info);
                    }

                    lblLastSync.Text = "Last sync: " + DateTime.Now;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //public void func_getMessageCountinTopic()
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
            try
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = "192.168.189.128:9092"
                };

                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync(cboTopicProducer.Text, new Message<Null, string> { Value = "This is a message to topic " + cboTopicProducer.Text + ": " + txtProduce.Text });
                }
                txtProduce.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReferesh_Click_1(object sender, EventArgs e)
        {
            lstTopicList.Items.Clear();
            cbotopicConsumer.Items.Clear();
            cboTopicProducer.Items.Clear();
            
            func_getTopics();
        }

        private void btnConsume_Click(object sender, EventArgs e)
        {
            ConsumerConfig config = new ConsumerConfig
            {
                BootstrapServers = "192.168.189.128:9092",
                GroupId = "1",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            ConsumerBuilder<Null, string> builder = new ConsumerBuilder<Null, string>(config);
            IConsumer<Null, string> consumer = builder.Build();
        }
    }
}
