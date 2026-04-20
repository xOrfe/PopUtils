using Unity.Burst;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace XO.PopUtils
{   
    public static partial class Pop
    {
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NextPow2(int v)
        {
            v--;
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;
            return v + 1;
        }
        
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 Planar(this float3 v, float3 planeNormal)
        {
            return v - planeNormal * math.dot(v, planeNormal);
        }
        
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 PlanarNormalized(this float3 v, float3 planeNormal)
        {
            float3 planar = v - planeNormal * math.dot(v, planeNormal);
            return math.normalizesafe(planar);
        }
    }
}