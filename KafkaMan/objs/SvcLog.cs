using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaMan.objs
{
    public class SvcLog
    {
        public DateTime Timestamp {  get; set; }
        public string AppId { get; set; }
        public Guid MsgGuid { get; set; }
        public string TopicName { get; set; } 
        public SvcLog(string t, string app) 
        {
            AppId = app;
            TopicName = t;
            Timestamp = DateTime.Now;
            MsgGuid = Guid.NewGuid();
        }
    }
}
