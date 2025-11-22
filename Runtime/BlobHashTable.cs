using Unity.Entities;
using Unity.Collections;
using Unity.Burst;
using System;

namespace XO.PopUtils
{
    [BurstCompile]
    public struct BlobHashTable<T> where T : unmanaged
    {
        public int Count;
        public BlobArray<uint> Keys;
        public BlobArray<T> Values;

        public static BlobAssetReference<BlobHashTable<T>> Create(uint[] keys, T[] values)
        {
            if (keys.Length != values.Length)
                throw new ArgumentException("Keys and values length mismatch");

            Array.Sort(keys, values);

            var builder = new BlobBuilder(Allocator.Temp);
            ref var root = ref builder.ConstructRoot<BlobHashTable<T>>();

            root.Count = keys.Length;

            var bKeys = builder.Allocate(ref root.Keys, keys.Length);
            var bValues = builder.Allocate(ref root.Values, values.Length);

            for (var i = 0; i < keys.Length; i++)
            {
                bKeys[i] = keys[i];
                bValues[i] = values[i];
            }

            var blob = builder.CreateBlobAssetReference<BlobHashTable<T>>(Allocator.Persistent);
            builder.Dispose();
            return blob;
        }

        [BurstCompile]
        public bool TryGetValue(uint key, out T value)
        {
            var index = Pop.BinarySearch(ref Keys, key);
            if (index >= 0 && index < Count)
            {
                value = Values[index];
                return true;
            }

            value = default;
            return false;
        }
    }
}