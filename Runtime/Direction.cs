using Unity.Mathematics;
using Unity.Burst;
using System.Runtime.CompilerServices;

namespace XO.PopUtils
{
    public static partial class Pop
    {
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Direction GetDirection(this float3 dir)
        {
            var abs = math.abs(dir);

            if (abs.x >= abs.y && abs.x >= abs.z)
                return dir.x > 0 ? Direction.Right : Direction.Left;

            if (abs.y >= abs.x && abs.y >= abs.z)
                return dir.y > 0 ? Direction.Up : Direction.Down;

            return dir.z > 0 ? Direction.Forward : Direction.Backward;
        }
    }
    [BurstCompile]
    public static class DirectionExtensions
    {
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Direction Mirror(this Direction dir)
        {
            return dir switch
            {
                Direction.Forward => Direction.Backward,
                Direction.Backward => Direction.Forward,
                Direction.Left => Direction.Right,
                Direction.Right => Direction.Left,
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                _ => dir
            };
        }
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsVertical(this Direction dir) => dir is Direction.Up or Direction.Down;
        
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsHorizontal(this Direction dir) => dir is Direction.Left or Direction.Right;
        
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDepth(this Direction dir) => dir is Direction.Forward or Direction.Backward;
    }
        
    public enum Direction
    {
        Forward,
        Backward,
        Left,
        Right,
        Up,
        Down
    }
}