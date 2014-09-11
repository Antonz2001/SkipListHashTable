using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
namespace Test
{
    class SLHTTest
    {
        private static Random r = new Random();
        private static string RandomString() //40-120
        {
            int i = r.Next(1, 12);
            StringBuilder sb=new StringBuilder();
            for(int j=0;j<i;j++)
                sb.Append(Convert.ToChar(r.Next(40,120)));
            return sb.ToString();
        }

        public static void Test()
        {
            SkipListHashTable<string, string> test = new SkipListHashTable<string, string>();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < 10000; i++)
            {
                List<string> GeneratedKeys = new List<string>();
                string value = RandomString(),key = RandomString();
                GeneratedKeys.Add(key);
                if (!dic.ContainsKey(key))
                {
                    dic.Add(key, value);
                    test.Add(key, value);
                }
                if (r.Next(0, 3) == 1)
                {
                    if (test.Contains(key))
                    {
                        int j = r.Next(0,GeneratedKeys.Count);
                        test.Remove(GeneratedKeys[j]);
                        dic.Remove(GeneratedKeys[j]);
                    }
                }
            }
            foreach (KeyValuePair<string, string> kvp in dic)
            {
                Debug.Assert(dic[kvp.Key] == test[kvp.Key]  );
            }
            
        }
    }
}
