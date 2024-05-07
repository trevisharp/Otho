using System.Collections.Generic;

namespace Otho;

using System;
using Internal;

/*******************************************************************************
 * Board representation:
 * 8 bytes  = bitboard to white piece presence
 * 8 bytes  = bitboard to black piece presence
 * 8 bytes  = bitboard to white valid move
 * 8 bytes  = bitboard to black valid move
 * 4 bytes  = position avaliation
 */

/// <summary>
/// Represents a Board of Othelo.
/// </summary>
public readonly struct Board
{
    readonly Vector vector;
    public Board()
        => this.vector = Pool.Shared.Rent();

    public readonly IEnumerable<Board> Next
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public readonly int Avaliation
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    private static Board? emptyReversi = null;
    private static Board? empty = null;

    /// <summary>
    /// Get the board fully empty.
    /// </summary>
    public static Board EmptyReversi
    {
        get
        {
            emptyReversi ??= new Board();
            return emptyReversi.Value;
        }
    }

    /// <summary>
    /// Get the board in initial position of Othelo.
    /// </summary>
    public static Board Empty
    {
        get
        {
            empty ??= constructEmpty();
            return empty.Value;
        }
    }

    private static Board constructEmpty()
    {
        throw new NotImplementedException();
    }
}