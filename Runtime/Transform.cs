using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;

namespace XO.PopUtils
{
    public static partial class Pop
    {
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LocalTransform NewLocalTransform()
        {
            return new LocalTransform()
            {
                Position = float3.zero,
                Rotation = quaternion.identity,
                Scale = 1
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 ToWorldSpace(float4x4 parent, float4x4 local)
        {
            return math.mul(parent, local);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LocalToWorld ToWorldSpace(LocalToWorld parent, LocalToWorld local)
        {
            return new LocalToWorld
            {
                Value = math.mul(parent.Value, local.Value)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 GetRelativeTransform(float4x4 reference, float4x4 target)
        {
            return math.mul(math.inverse(reference), target);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LocalToWorld GetRelativeTransform(LocalToWorld reference, LocalToWorld target)
        {
            return new LocalToWorld
            {
                Value = math.mul(math.inverse(reference.Value), target.Value)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 LocalizedTransform(float4x4 reference, float4x4 target)
        {
            return math.mul(reference, target);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LocalToWorld LocalizedTransform(LocalToWorld reference, LocalToWorld target)
        {
            return new LocalToWorld
            {
                Value = math.mul(reference.Value, target.Value)
            };
        }
        
    }
}