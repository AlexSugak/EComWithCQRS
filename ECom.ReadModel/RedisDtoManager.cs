using ECom.Utility;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.ReadModel
{
    public class RedisDtoManager : IDtoManager
    {
        private readonly string _redisHost;

        public RedisDtoManager(string redisHost)
        {
            Argument.ExpectNotNullOrWhiteSpace(() => redisHost);

            _redisHost = redisHost;
        }

        public T Get<T>(Messages.IIdentity id) where T : Dto, new()
        {
            Argument.ExpectNotNull(() => id);

            return Get<T>(id.GetId());
        }

        public T Get<T>(string id) where T : Dto, new()
        {
            Argument.ExpectNotNullOrWhiteSpace(() => id);

            using (var client = new PooledRedisClientManager(_redisHost).GetClient().As<T>())
            {
                return client[DtoId<T>(id)];
            }
        }

        public void Add<T>(Messages.IIdentity id, T dto) where T : Dto, new()
        {
            Argument.ExpectNotNull(() => id);

            Add<T>(id.GetId(), dto);
        }

        public void Add<T>(string id, T dto) where T : Dto, new()
        {
            Argument.ExpectNotNullOrWhiteSpace(() => id);
            Argument.ExpectNotNull(() => dto);

            using (var client = new PooledRedisClientManager(_redisHost).GetClient().As<T>())
            {
                client.SetEntry(DtoId<T>(id), dto);
            }
        }

        public void Delete<T>(Messages.IIdentity id) where T : Dto, new()
        {
            Argument.ExpectNotNull(() => id);

            Delete<T>(id.GetId());
        }

        public void Delete<T>(string id) where T : Dto, new()
        {
            using (var client = new PooledRedisClientManager(_redisHost).GetClient().As<T>())
            {
                client.RemoveEntry(DtoId<T>(id));
            }
        }

        public void DeleteAll<T>() where T : Dto, new()
        {
            using (var client = new PooledRedisClientManager(_redisHost).GetClient())
            {
                var keys = client.SearchKeys("urn:" + typeof(T).Name + ":*");
                keys.ForEach(k => client.RemoveEntry(k));
                client.RemoveEntry("ids:" + typeof(T).Name);
            }
        }

        public void Update<T>(Messages.IIdentity id, Action<T> updateAction) where T : Dto, new()
        {
            Argument.ExpectNotNull(() => id);

            Update<T>(id.GetId(), updateAction);
        }

        public void Update<T>(string id, Action<T> updateAction) where T : Dto, new()
        {
            using (var client = new PooledRedisClientManager(_redisHost).GetClient().As<T>())
            {
                T dto = client[DtoId<T>(id)];
                updateAction(dto);
                client.SetEntry(DtoId<T>(id), dto);
            }
        }

        private static string DtoId<T>(string id) where T : Dto
        {
            return "urn:" + typeof(T).Name + ":" + id;
        }
    }
}
