using LotteryReptile.Redis;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryReptile.Models
{
    public static class ReturnRedis
    {
        public static void RemoveKey(List<string> KeyList)
        {
            using (IRedisClient Redis = RedisManager.GetClient())
            {
                foreach (var item in KeyList)
                {
                    Redis.Remove(item);
                }
            }
        }

        public static void RemoveKey(string Key)
        {
            using (IRedisClient Redis = RedisManager.GetClient())
            {

                Redis.Remove(Key);
            }
        }

    }
}
