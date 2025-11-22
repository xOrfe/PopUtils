using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using System.Runtime.CompilerServices;

namespace XO.PopUtils
{
    public static partial class Pop
    {
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int BinarySearch(NativeArray<uint> keys, uint key)
        {
            var left = 0;
            var right = keys.Length - 1;

            while (left <= right)
            {
                var mid = (left + right) >> 1;
                var midKey = keys[mid];
                if (midKey == key)
                    return mid;
                else if (midKey < key)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return -1;
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int BinarySearch(ref BlobArray<uint> keys, uint key)
        {
            var left = 0;
            var right = keys.Length - 1;

            while (left <= right)
            {
                var mid = (left + right) >> 1;
                var midKey = keys[mid];
                if (midKey == key)
                    return mid;
                else if (midKey < key)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return -1;
        }
    }
}