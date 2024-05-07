namespace Otho;

using Internal;

public class Board
{
    Vector vector;
    public Board()
    {
        this.vector = Pool.Shared.Rent();
    }
}