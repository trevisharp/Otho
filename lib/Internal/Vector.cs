using System.Runtime.CompilerServices;

namespace Otho.Internal;

internal readonly unsafe struct Vector
{
    readonly int* pool;

    internal Vector(int* pool)
        => this.pool = pool;

    internal readonly int this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => pool[index];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => *(pool + index) = value;
    }
}