using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaMan.objs
{

    public class KafkaClusterInfo
    {
        public string OriginatingBrokerId;
        public string OriginatingBrokerName;

        public Broker? Brokers { get; set; }
        public TopicU? Topics { get; set;}
    }

    public class Broker
    {
        public string BrokerId;
        public string Host;
        public string Prot;
    }
    public class Partition
    {
        public string PartitionId;
        public string Leader;
        public string Replicas;

    }
    public class TopicU
    {
        public string Topic;
        public List<Partition> Partitions = new List<Partition>();
        public string Error;
    }


}
