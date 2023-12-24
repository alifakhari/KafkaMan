using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace KafkaMan
{
    public partial class Form1 : Form
    {
        AdminClientConfig adminConfig = new AdminClientConfig()
        {
            BootstrapServers = "192.168.189.128:9092"
        };
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
                    var topicsMetadata = metadata.Topics;
                    var topicNames = metadata.Topics.Select(a => a.Topic).ToList();

                    lstTopicList.Items.Clear();
                    cboTopicProducer.Items.Clear();
                    cbotopicConsumer.Items.Clear();

                    foreach (string topic in topicNames)
                    {
                        var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(20));
                        //var count = 0;
                        TopicMetadata? topicMetadata = meta.Topics.SingleOrDefault(t => topic.Equals(t.Topic));

                        //if (topicMetadata != null)
                        //{
                        //var count =  topicMetadata.Partitions
                        //    .Select(x => new TopicPartition(topicMetadata.Topic, x.PartitionId))
                        //    .ToList();
                        //}

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
                        System.Console.WriteLine(total);

                        lstTopicList.Items.Add(topic);
                        cboTopicProducer.Items.Add(topic);
                        cbotopicConsumer.Items.Add(topic);

                    }
                    lblLastSync.Text = "Last sync: " + DateTime.Now;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occured while fetcing topic list: \r\n {0}", ex.Message.ToString());
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
            cboTopicProducer.SelectedIndex = 0;
            cbotopicConsumer.SelectedIndex = 0;
        }

        private void timer_topicrefresh_Tick(object sender, EventArgs e)
        {
            func_getTopics();
        }
    }
}