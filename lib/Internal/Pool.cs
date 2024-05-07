using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace Otho.Internal;

internal unsafe class Pool : IDisposable
{
    public static int DefaultSize { get; set; } = 16;
    
    private static Pool shared;
    public static Pool Shared
    {
        get
        {
            if (shared is null)
                shared = Create();
            
            return shared;
        }
    }
    
    public static void Clear()
    {
        if (shared is null)
            return;
        
        shared.Dispose();
        shared = null;
    }
    
    public static Pool Create(int vectorSize = -1, int poolSize = 65536)
        => new(vectorSize == -1 ? DefaultSize : vectorSize, poolSize);


    readonly int poolSize;
    readonly int vectorSize;
    readonly List<IntPtr> pool;
    readonly ConcurrentBag<Vector> bag;
    
    Pool(int vectorSize, int poolSize)
    {
        this.poolSize = poolSize;
        this.vectorSize = vectorSize;
        this.pool = new();
        this.bag = new();
        extend();
    }

    void extend()
    {
        IntPtr dataPointer = Marshal.AllocHGlobal(4 * poolSize);
        this.pool.Add(dataPointer);

        int bucketSize = poolSize / vectorSize / 16;
        Parallel.For(0, 16, i => {
            int start = vectorSize * bucketSize * i;
            int* p = (int*)dataPointer + start;
            int* end = p + bucketSize * vectorSize;

            while (p < end)
            {
                bag.Add(new Vector(p));
                p += bucketSize;
            }
        });
    }

    public Vector Rent()
    {
        lock (pool)
        {
            if (bag.IsEmpty)
                extend();
        }

        bag.TryTake(out var vector);
        return vector;
    }

    public void Return(Vector vector)
        => bag.Add(vector);

    public void Dispose()
    {
        foreach (var data in pool)
            Marshal.FreeHGlobal(data);
    }
}