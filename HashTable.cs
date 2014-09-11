using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    interface IBucket<K,V>
    {
         bool Contains(K key);
         void Add(K key, V v);
         void Remove(K key);
         V Get(K key);
    }
    class HashTable<K,V,B> where B:IBucket<K,V>,new()
    {
        private int arrSize = 100;
        private B[] buckets;
        public HashTable()
        {
            this.buckets = new B[this.arrSize];
        }
        private int GetKeyHashCode(K key)
        {
            return Math.Abs(key.GetHashCode() % this.arrSize);
        }
        public bool Contains(K key)
        {
            int index = GetKeyHashCode(key);
            if (this.buckets[index] == null)
                return false;
            return this.buckets[index].Contains(key);
        }
        public void Add(K key, V value)
        {
            if (this.Contains(key))
                throw new ArgumentException();
            int index = GetKeyHashCode(key);
            if (this.buckets[index] == null)
                this.buckets[index] = new B();
            this.buckets[index].Add(key, value);
        }
        public void Remove(K key)
        {
            int index = GetKeyHashCode(key);
            if (this.buckets[index] == null)
                return;
            this.buckets[index].Remove(key);
        }
        public V this[K key]
        {
            get
            {
                int index = this.GetKeyHashCode(key);
                if (this.buckets[index] == null)
                    throw new ArgumentException();
                return this.buckets[index].Get(key);
            } 
        }
    }
    class SkipListBucket<K, V>:IBucket<K,V> where K:IComparable
    {
        private SkipList<K, V> skipList= new SkipList<K,V>();
        
        public bool Contains(K key)
        {
            return skipList.Contains(key);
        }
        public void Add(K key, V value)
        {
            skipList.Add(key, value);
        }
        public void Remove(K key)
        {
            skipList.Remove(key);
        }
        public V Get(K key)
        {
            return skipList.Get(key);
        }
    }
    class SkipListHashTable<K, V> : HashTable<K, V, SkipListBucket<K,V>> where K:IComparable
    {

    }
}
