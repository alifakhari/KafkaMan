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

        public static class KafkaConsumerConfig
        {
            public static ConsumerConfig GetConfig()
            {
                return new ConsumerConfig
                {
                    BootstrapServers = "192.168.189.128:9092",
                    GroupId = "KafkaExampleConsumer",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false,
                };
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
            try
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = "192.168.189.128:9092",
                    EnableDeliveryReports = true,
                    ClientId = Dns.GetHostName(),
                    // Emit debug logs for message writer process, remove this setting in production
                    Debug = "msg",

                    // retry settings:
                    // Receive acknowledgement from all sync replicas
                    Acks = Acks.All,
                    // Number of times to retry before giving up
                    MessageSendMaxRetries = 3,
                    // Duration to retry before next attempt
                    RetryBackoffMs = 1000,
                    // Set to true if you don't want to reorder messages on retry
                    EnableIdempotence = true
                };

                using (var producer = new ProducerBuilder<long, string>(config)
                                    .SetKeySerializer(Serializers.Int64)
                                    .SetValueSerializer(Serializers.Utf8)
                                    .SetLogHandler((_, message) =>
                                    Console.WriteLine($"Facility: {message.Facility}-{message.Level} Message: {message.Message}"))
                                    .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}. Is Fatal: {e.IsFatal}")).Build())
                {
                    var result = producer.ProduceAsync(cboTopicProducer.Text, new Message<long, string> { Value = $"{DateTime.Now},This is a message to topic {cboTopicProducer.Text}: {txtProduce.Text}" }).Result;
                    lblLog.Text = $"Inserting  into {result.Topic} and Partition {result.Partition}: {result.Status}, {(result.Status == PersistenceStatus.Persisted ? "Acked by all brokers" : "Not Acked by all brokers")}";
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
            //CancellationTokenSource cts = new CancellationTokenSource();
            //Run_ManualAssign("test_broker", cbotopicConsumer.Text, cts);
            //Run_MediumConsumer();
            ConsumerConfig config = new ConsumerConfig
            {
                BootstrapServers = "192.168.189.128:9092",
                GroupId = "kafka-dotnet-getting-started",
                EnableAutoOffsetStore = false,
                EnableAutoCommit = true,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true,

                PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky
            };

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            using (var Consumer = new ConsumerBuilder<string, string>(config).Build())
            {
                Consumer.Subscribe(cbotopicConsumer.Text);
                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = Consumer.Consume(cts.Token);
                            if (cr.Message != null)
                            {
                                txtConsume.Text += $"{cr.Message.Value.ToString()} \n";
                                Consumer.Commit(cr);
                                Consumer.StoreOffset(cr);
                            }
                            else
                                break;

                        }
                        catch (ConsumeException ex)
                        {
                            MessageBox.Show($"Error occured: {ex.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ctrl-C was pressed.
                }
                finally
                {

                    Consumer.Close();
                }
            }
        }


        public void Run_ManualAssign(string brokerList, string topic, CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "groupid-not-used-but-mandatory",
                BootstrapServers = "192.168.189.128:9092",
                // partition offsets can be committed to a group even by consumers not
                // subscribed to the group. in this example, auto commit is disabled
                // to prevent this from occurring.
                EnableAutoCommit = false
            };

            using (var consumer =
                new ConsumerBuilder<Ignore, string>(config)
                    .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                    .Build())
            {
                //consumer.Assign(topics.Select(topic => new TopicPartitionOffset(topic, 0, Offset.Beginning)).ToList());

                consumer.Assign(new TopicPartitionOffset(topic, 0, Offset.Beginning));

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(cancellationToken);

                            // Note: End of partition notification has not been enabled, so
                            // it is guaranteed that the ConsumeResult instance corresponds
                            // to a Message, and not a PartitionEOF event.
                            txtConsume.Text = $"Received message at {consumeResult.TopicPartitionOffset}: ${consumeResult.Message.Value}";
                            consumer.Commit(consumeResult);

                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Consume error: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Closing consumer.");
                    consumer.Close();
                }
            }
        }

        public void Run_MediumConsumer()
        {
            var config = KafkaConsumerConfig.GetConfig();
            string topic = cbotopicConsumer.Text;
            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            consumer.Subscribe(topic);
            while (true)
            {
                try
                {
                    var consumeResult = consumer.Consume();
                    txtConsume.Text = $"Received message: {consumeResult.Message.Value} + \n";
                    lblLog.Text = $"Message received from {consumeResult.Topic} : {consumeResult.Partition}";

                    // Process the message here
                    consumer.Commit(consumeResult);
                }
                catch (ConsumeException e)
                {
                    MessageBox.Show($"Error occured: {e.Error.Reason}");

                }
            }
        }

        private void btnNewConsumer_Click(object sender, EventArgs e)
        {

            var _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "192.168.189.128:9092",
                EnableAutoCommit = false,
                EnableAutoOffsetStore = false,
                MaxPollIntervalMs = 300000,
                GroupId = "default",

                // Read messages from start if no commit exists.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<long, string>(_consumerConfig)
                .SetKeyDeserializer(Deserializers.Int64)
                .SetValueDeserializer(Deserializers.Utf8)
                .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                .Build();
            try
            {
                consumer.Subscribe(cbotopicConsumer.Text);
                Console.WriteLine("\nConsumer loop started...\n\n");
                while (true)
                {
                    var result =
                        consumer.Consume(
                            TimeSpan.FromMilliseconds(_consumerConfig.MaxPollIntervalMs - 1000 ?? 250000));
                    var message = result?.Message?.Value;
                    if (message == null)
                    {
                        continue;
                    }

                    Console.WriteLine(
                        $"Received: {result.Message.Key}:{message} from partition: {result.Partition.Value}");

                    consumer.Commit(result);
                    consumer.StoreOffset(result);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
            }
            catch (KafkaException ex)
            {
                Console.WriteLine($"Consume error: {ex.Message}");
                Console.WriteLine("Exiting producer...");
            }
            finally
            {
                consumer.Close();
            }
        }
    }

}
