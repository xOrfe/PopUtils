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
        public static void Planar(in float3 v, in float3 planeNormal, out float3 result)
        {
            result = v - planeNormal * math.dot(v, planeNormal);
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PlanarNormalized(in float3 v, in float3 planeNormal, out float3 result)
        {
            float3 planar = v - planeNormal * math.dot(v, planeNormal);
            result = math.normalizesafe(planar);
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClosestPointOnSegment(in float3 start, in float3 end, in float3 point, ref float3 result)
        {
            var segmentDir = end - start;
            var lenSq = math.dot(segmentDir, segmentDir);
            if (lenSq < math.EPSILON)
            {
                result = start;
                return;
            }
            var t = math.clamp(math.dot(point - start, segmentDir) / lenSq, 0f, 1f);
            result = start + t * segmentDir;
        }
    }
}