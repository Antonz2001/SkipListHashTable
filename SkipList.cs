using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class SkipList<K, V> where K : IComparable
    {
        private class Node
        {
            public K Key;
            public V Value;
            public Node[] Next;
            public int Levels;
            public Node(int levels,K key, V value)
            {
                this.Value = value;
                this.Key = key;
                this.Next = new Node[levels+1];
                this.Levels = levels;
            }
        }
        private Node head;
        private int totalLevels = 0;
        private int maxLevels = 31;
        private int[] probabilityMap;
        private Random r=new Random();
        public int Count {get;private set;}
        public SkipList()
        {
            head = new Node(maxLevels, default(K),default(V));
            probabilityMap = new int[maxLevels];
            int d = int.MaxValue;
            for (int i = 0; i < maxLevels; i++)
            {
                probabilityMap[i] = d;
                d /= 2;
            }

        }
        private int RandomLevelCount()
        {
            int j = r.Next();
            int i = 0;
            while (j <= probabilityMap[i])
            {
                i++;
            }
            return i-1 ;
        }
        public bool Contains(K key)
        {
            int i = this.totalLevels;
            Node curr = head;
            while (i >= 0)
            {
                bool f = true;
                while (f)
                {
                    if (curr.Next[i] != null)
                    {
                        if (curr.Next[i].Key.CompareTo(key)==0)
                            return true;
                        if (curr.Next[i].Key.CompareTo(key) == -1)
                        {
                            curr = curr.Next[i];

                        }
                        else break;
                    }
                    else f = false;
                }
                i--;
   
            }
            return false;
        }

        public void  Add(K key,V value)
        {
            Node curr = head;
            
            Node insertNode = new Node(RandomLevelCount(),key, value);
            if (insertNode.Levels > totalLevels)
                totalLevels = insertNode.Levels;
            int i = this.totalLevels;
            while ( i >=0 )
            {
                bool f = true;
                while (f)
                {
                    if (curr.Next[i] != null)
                    {
                        if (curr.Next[i].Key.CompareTo(key) == -1)
                        {
                            curr = curr.Next[i];

                        }
                        else break;
                    }
                    else f = false;
                }
                if (i <= insertNode.Levels)
                {
                    if (curr.Next[i] != null)
                    {
                        insertNode.Next[i] = curr.Next[i];

                     }
                     curr.Next[i] = insertNode;
                 }
                 i--;
                
            }

            this.Count++;
            

        }
        public V Get(K key)
        {
            int i = this.totalLevels;
            Node curr = head;
            while (i >= 0)
            {
                bool f = true;
                while (f)
                {
                    if (curr.Next[i] != null)
                    {
                        if (curr.Next[i].Key.CompareTo(key) == 0)
                            return curr.Next[i].Value;
                        if (curr.Next[i].Key.CompareTo(key) == -1)
                        {
                            curr = curr.Next[i];

                        }
                        else break;
                    }
                    else f = false;
                }
                i--;

            }
            throw new ArgumentException();
        }
        public void Remove(K key)
        {
            int i = this.totalLevels;
            Node curr = head;
            bool removed = false;
            while (i >= 0)
            {

                bool f = true;
                while (f)
                {
                    if (curr.Next[i] != null)
                    {
                        if (curr.Next[i].Key.CompareTo(key) == 0)
                        {
                            removed = true;
                            curr.Next[i] = curr.Next[i].Next[i];
                            break;
                        }
                        if (curr.Next[i].Key.CompareTo(key) == -1)
                        {
                            curr = curr.Next[i];

                        }
                        else break;
                    }
                    else f = false;
                }

                i--;
            }
            if (removed)
                this.Count--;
            
        }
        public IEnumerable<KeyValuePair<K,V>> Enumerate()
        {
            Node cur = this.head.Next[0];
            while (cur != null)
            {
                yield return new KeyValuePair<K,V>(cur.Key,cur.Value);
                cur = cur.Next[0];
            }
        }
    }

}
