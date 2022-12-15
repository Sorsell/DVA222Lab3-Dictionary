using System;
using System.Collections;
using System.Collections.Generic;

namespace HashDictionary
{

    public class HashDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IEquatable<TKey>
    {

        //default constructor 
        public HashDictionary()
        {
        }

        //custom constructor which takes as input the number of chains 
        public HashDictionary(int nchians)
        {
        }


        public bool IsReadOnly
        { 
            get => true;
        }

        public TValue this[TKey key] {
            get 
            {
            } 
            set 
            {
            }
        }

        public int Count 
        {
            get
            {
            } 
        }

        public ICollection<TKey> Keys 
        {
            get
            {
            }
        }

        public ICollection<TValue> Values 
        {
            get
            {
            } 
        }

        public void Add(TKey key, TValue value)
        {
        }

        public void Add(KeyValuePair<TKey, TValue> entry)
        {
        }

        public void Clear()
        {
        }

        public bool ContainsKey(TKey key) 
        {
        }

        public bool Contains(KeyValuePair<TKey, TValue> entry)
        {
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
        }

        public bool Remove(TKey key)
        {
        }

        public bool Remove(KeyValuePair<TKey, TValue> entry)
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
        }

        public bool TryGetValue(TKey key, out TValue value) 
        {
        }
        
    }
    

}
