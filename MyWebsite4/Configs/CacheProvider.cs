using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebsite4.Configs
{
    public class CacheConfig
    {
        public bool RedisEnable { get; set; }

        public string ConnectionString { get; set; }

        public string InstanceName { get; set; }
    }
}
