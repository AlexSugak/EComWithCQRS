using ECom.Domain;
using ECom.Utility;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.Infrastructure
{
    public class RedisIdGenerator : IDomainIdentityGenerator
    {
        private readonly string _redisHost;

        public RedisIdGenerator(string redisHost)
        {
            Argument.ExpectNotNullOrWhiteSpace(() => redisHost);

            _redisHost = redisHost;
        }

        public long GenerateNewId()
        {
            using (var client = new PooledRedisClientManager(_redisHost).GetClient())
            {
                return client.As<long>().GetNextSequence();
            }
        }
    }
}
