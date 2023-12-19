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
        public void func_getNoofTopics()
        {

            using (var adminClient = new AdminClientBuilder(adminConfig).Build())
            {
                try
                {

                    var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
                    var topicsMetadata = metadata.Topics;
                    var topicNames = metadata.Topics.Select(a => a.Topic).ToList();

                    lstTopicList.Items.Clear();

                    foreach (string topic in topicNames)
                    {
                        lstTopicList.Items.Add(topic);
                    }
                }catch (Exception ex)
                {
                    Console.WriteLine($"An error occured while fetcing topic list: \r\n {0}", ex.Message.ToString() );
                    MessageBox.Show(ex.Message );

                }
            }
        }
        #endregion Functions


        private void btnTopicAdd_Click(object sender, EventArgs e)
        {
            using (var adminClient = new AdminClientBuilder(adminConfig).Build())
            {
                try
                {
                    adminClient.CreateTopicsAsync(new TopicSpecification[] {
                    new TopicSpecification { Name = txtTopicName.Text, ReplicationFactor = 1, NumPartitions = 1 } });

                    MessageBox.Show("New Topic has been created");

                    func_getNoofTopics();
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
            func_getNoofTopics();
        }
    }
}