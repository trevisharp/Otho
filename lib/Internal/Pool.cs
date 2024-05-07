using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Otho.Internal;

internal unsafe class Pool : IDisposable
{
    readonly int poolSize;
    readonly int vectorSize;
    readonly List<int[]> pool;
    readonly ConcurrentBag<Vector> bag;
    Pool(int vectorSize, int poolSize)
    {
        this.poolSize = poolSize;
        this.vectorSize = vectorSize;
        this.pool = new();
        this.bag = new();
    }
    private void extend() {
        int[] array = new int[poolSize];
        this.pool.Add(array);

        int buckets = vectorSize / poolSize / 16;
        Parallel.For(0, buckets, i => {

        });

        throw new NotImplementedException();
    }

    public Vector Rent()
    {
        lock (pool)
        {
            if (bag.Count == 0)
                extend();
        }

        bag.TryTake(out var vector);
        return vector;
    }

    public static Pool Create(int vectorSize, int poolSize = 65536)
        => new(vectorSize, poolSize);

    public void Dispose()
    {
        
    }
}