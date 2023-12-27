using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System.Collections;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

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
        }

        public Form1() 
        {
            InitializeComponent();
            
        }


        #region Functions

        //public static List<TopicPartition> GetTopicPartitions(string bootstrapServers, string topicValue)
        public List<TopicPartition> GetTopicPartitions(string topicValue)
        {
            //AdminClientConfig adminConfig = new AdminClientConfig { BootstrapServers = bootstrapServers };
            //AdminClientConfig adminConfig = new AdminClientConfig { BootstrapServers = bootstrapServers };

            using (var adminClient = new AdminClientBuilder(adminConfig).Build())
            {

                var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(20));
                TopicMetadata? topicMetadata = meta.Topics.SingleOrDefault(t => topicValue.Equals(t.Topic));
                if (topicMetadata != null)
                {
                    return topicMetadata.Partitions
                        .Select(x => new TopicPartition(topicMetadata.Topic, x.PartitionId))
                        .ToList();
                }
            } 
            return new List<TopicPartition>();
        }


        public void func_getTopics()
        {
            using (var adminClient = new AdminClientBuilder(adminConfig).Build())
            {
                try
                {
                    var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
                    var dictionary = JsonConvert.DeserializeObject<dynamic>(metadata.ToString());
                    
                    var JTopic = dictionary["Topics"];

                    for (int i = 0; i < JTopic.Count; i++)
                    {
                        var topicname = JTopic[i]["Topic"];
                        
                        var PCount =JTopic[i]["Partitions"].Count;
                        for (int j=0; j< PCount; j++)
                        {
                            Console.WriteLine("");
                        }
                        
                    }
                   
                    

                    var topicsMetadata = metadata.Topics;
                    var topicNames = metadata.Topics.Select(a => a.Topic).ToList();

                    foreach (string topic in topicNames)
                    {
                        var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(20));
                        TopicMetadata? topicMetadata = meta.Topics.SingleOrDefault(t => topic.Equals(t.Topic)) ;

                        #region<Consumer>
                        ConsumerConfig config = new ConsumerConfig
                        {
                            BootstrapServers = "192.168.189.128:9092",
                            GroupId = "1",
                            AutoOffsetReset = AutoOffsetReset.Earliest,
                        };
                        ConsumerBuilder<Null, string> builder = new ConsumerBuilder<Null, string>(config);
                        //builder.SetValueDeserializer(_kafkaConverter);

                        IConsumer<Null, string> consumer = builder.Build();

                        //List<TopicPartition> partitions = GetTopicPartitions(BootstrapServers, topic);
                        List<TopicPartition> partitions = GetTopicPartitions(topic);
                        TopicPartition firstPartition = partitions.First();

                        WatermarkOffsets watermarkOffsets = consumer.QueryWatermarkOffsets(firstPartition, TimeSpan.FromSeconds(10));
                        long total = watermarkOffsets.High - watermarkOffsets.Low;


                        string item = topic + ", Total Document: " + total;
                        lstTopicList.Items.Add(item);
                        #endregion <Consumer>


                        lstTopicList.Items.Add(topic);
                        cboTopicProducer.Items.Add(topic);
                        cbotopicConsumer.Items.Add(topic);

                    }
                    lblLastSync.Text = "Last sync: " + DateTime.Now;

                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"An error occured while fetcing topic list: \r\n {0}", ex.Message.ToString());
                    MessageBox.Show(ex.Message);

                }
            }
        }

        //public void func_getMessageCountinTopic()
        //{

        //    using (var adminClient = new AdminClientBuilder(adminConfig).Build())
        //    {
        //        var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(20));
        //        TopicMetadata? topicMetadata = meta.Topics.SingleOrDefault(t => topicValue.Equals(t.Topic));
        //        if (topicMetadata != null)
        //        {
        //            return topicMetadata.Partitions
        //                .Select(x => new TopicPartition(topicMetadata.Topic, x.PartitionId))
        //                .ToList();
        //        }
        //    }
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
            using (var adminClient = new AdminClientBuilder(adminConfig).Build())
            {
                try
                {
                    adminClient.CreateTopicsAsync(new TopicSpecification[] {
                    new TopicSpecification { Name = txtTopicName.Text, ReplicationFactor =(short)numberReplica.Value, NumPartitions = (short)numberPartition.Value } });

                    MessageBox.Show("New Topic has been created");
                    txtTopicName.Text = string.Empty;
                    txtTopicName.Focus();

                    func_getTopics();
                }
                catch (CreateTopicsException ex)
                {
                    MessageBox.Show(ex.Message);

                    Console.WriteLine($"An error occured creating topic {ex.Results[0].Topic}: {ex.Results[0].Error.Reason}");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            func_getTopics();
            //cboTopicProducer.SelectedIndex = 0;
            //cbotopicConsumer.SelectedIndex = 0;
        }

        private void timer_topicrefresh_Tick(object sender, EventArgs e)
        {
            lstTopicList.Items.Clear();
            
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
                    var result = await producer.ProduceAsync(cboTopicProducer.Text, new Message<Null, string> { Value = "This is a message to topic " + cboTopicProducer.Text });
                }
                txtProduce.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }
    }
}
