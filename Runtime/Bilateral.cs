using System;
using System.Runtime.CompilerServices;

namespace XO.PopUtils
{
    public enum Side
    {
        Left,
        Right
    }

    public static class SideExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Side Opposite(this Side side) => side == Side.Left ? Side.Right : Side.Left;
    }

    public readonly struct BilateralStruct<T>
    {
        public readonly T Left;
        public readonly T Right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BilateralStruct(T left, T right)
        {
            Left = left;
            Right = right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get(Side side) => side == Side.Left ? Left : Right;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetOpposite(Side side) => side == Side.Left ? Right : Left;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BilateralStruct<TResult> Map<TResult>(Func<T, TResult> selector) =>
            new BilateralStruct<TResult>(selector(Left), selector(Right));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out T left, out T right) => (left, right) = (Left, Right);

        public override string ToString() => $"({Left}, {Right})";
    }

    public interface IBilateral<T>
    {
        public BilateralStruct<T> BilateralVariable { get; set; }
        public Side BilateralSide { get; set; }
    }
}