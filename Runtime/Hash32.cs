using Unity.Burst;
using Unity.Collections;
using System.Runtime.CompilerServices;

namespace XO.PopUtils
{
    [BurstCompile]
    public static partial class Pop
    {
        private const uint FnvOffsetBasis = 2166136261u;
        private const uint FnvPrime = 16777619u;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash32(string s)
        {
            unchecked
            {
                uint hash = FnvOffsetBasis;
                foreach (var t in s)
                {
                    hash ^= t;
                    hash *= FnvPrime;
                }

                return hash;
            }
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash32<T>(T fs) where T : unmanaged, INativeList<byte>
        {
            unchecked
            {
                uint hash = FnvOffsetBasis;
                for (int i = 0; i < fs.Length; i++)
                {
                    hash ^= fs[i];
                    hash *= FnvPrime;
                }

                return hash;
            }
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hash32(NativeArray<byte> bytes)
        {
            unchecked
            {
                uint hash = FnvOffsetBasis;
                foreach (var t in bytes)
                {
                    hash ^= t;
                    hash *= FnvPrime;
                }

                return hash;
            }
        }
    }
}