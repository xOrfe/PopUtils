using Unity.Entities;
using Unity.Collections;
using Unity.Burst;
using System;
using UnityEngine;

namespace XO.PopUtils
{
    [BurstCompile]
    public struct BlobHashTable<T> where T : unmanaged
    {
        public int Count;
        private BlobArray<uint> _keys;
        private BlobArray<T> _values;

        public T this[string key] => this[Pop.Hash32(key)];
        public T this[uint key] => GetValue(key);
        public T this[int index] => _values[index];

        [BurstCompile]
        public T GetValue(uint key)
        {
            bool state = TryGetValue(key, out var value);
            Debug.LogError("Key not found: " + key + "");
            return state ? value : default;
        }

        [BurstCompile]
        public bool TryGetValue(uint key, out T value)
        {
            var index = Pop.BinarySearch(ref _keys, key);
            if (index >= 0 && index < Count)
            {
                value = _values[index];
                return true;
            }

            value = default;
            return false;
        }


        public static BlobAssetReference<BlobHashTable<T>> Create(uint[] keys, T[] values)
        {
            if (keys.Length != values.Length)
                throw new ArgumentException("Keys and values length mismatch");

            Array.Sort(keys, values);

            var builder = new BlobBuilder(Allocator.Temp);
            ref var root = ref builder.ConstructRoot<BlobHashTable<T>>();

            root.Count = keys.Length;

            var bKeys = builder.Allocate(ref root._keys, keys.Length);
            var bValues = builder.Allocate(ref root._values, values.Length);

            for (var i = 0; i < keys.Length; i++)
            {
                bKeys[i] = keys[i];
                bValues[i] = values[i];
            }

            var blob = builder.CreateBlobAssetReference<BlobHashTable<T>>(Allocator.Persistent);
            builder.Dispose();
            return blob;
        }
    }
}