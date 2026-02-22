using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace XO.PopUtils
{
    public struct Ray
    {
        private float3 _direction;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ray(float3 origin, float3 direction)
        {
            Origin = origin;
            _direction = math.normalize(direction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ray(in float3 origin, in float3 direction)
        {
            Origin = origin;
            _direction = math.normalize(direction);
        }

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
            readonly get => _direction;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _direction = math.normalize(value);
        }
    }
}