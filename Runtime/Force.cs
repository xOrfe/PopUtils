using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace XO.PopUtils
{
    public struct Force
    {
        public float3 Origin
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set;
        }

        public float3 Direction
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Force(float3 origin, float3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Force(in float3 origin, in float3 direction)
        {
            Origin = origin;
            Direction = direction;
        }
    }
}