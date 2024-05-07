using System.Runtime.CompilerServices;

namespace Otho;

/// <summary>
/// Represents a vector of integers.
/// </summary>
public readonly unsafe struct Vector
{
    readonly int* pool;

    public Vector(int* pool)
        => this.pool = pool;

    public readonly int this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => pool[index];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => *(pool + index) = value;
    }
}