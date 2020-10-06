using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class PoolMe
{
    public bool isPooled;
}

public abstract class BasePool
{
    public abstract PoolMe GetPooledObject ();
    public abstract void ReturnPooledObject (PoolMe obj);
}

public class SearchPool : BasePool
{
    List<PoolMe> pool;

    public SearchPool (int count)
    {
        pool = new List<PoolMe>(count);
        for (int i = 0; i < count; ++i)
        {
            PoolMe p = new PoolMe();
            p.isPooled = true;
            pool.Add( p );
        }
    }

    public override PoolMe GetPooledObject ()
    {
        for (int i = 0; i < pool.Count; ++i)
        {
            if (pool[i].isPooled)
            {
                pool[i].isPooled = false;
                return pool[i];
            }
        }
        return null;
    }

    public override void ReturnPooledObject (PoolMe obj)
    {
        obj.isPooled = true;
    }
}

public class QueuePool : BasePool
{
    Queue<PoolMe> pool;

    public QueuePool (int count)
    {
        pool = new Queue<PoolMe>(count);
        for (int i = 0; i < count; ++i)
        {
            ReturnPooledObject(new PoolMe());
        }
    }

    public override PoolMe GetPooledObject ()
    {
        if (pool.Count > 0)
        {
            PoolMe retValue = pool.Dequeue();
            retValue.isPooled = false;
            return retValue;
        }
        return null;
    }

    public override void ReturnPooledObject (PoolMe obj)
    {
        obj.isPooled = true;
        pool.Enqueue(obj);
    }
}

public class PoolingComparisonDemo : MonoBehaviour 
{
    const int objCount = 100;
    const int testCount = 1000;
    IEnumerator Start ()
    {
        TestPool(new SearchPool(objCount));
        yield return new WaitForSeconds(1);
        TestPool(new QueuePool(objCount));
    }

    void TestPool (BasePool pool)
    {
        List<PoolMe> activeObjects = new List<PoolMe>( objCount );
        
        Stopwatch watch = new Stopwatch();
        watch.Start();
        
        // Perform a repeating test of getting pooled objects and putting them back
        for (int i = 0; i < testCount; ++i)
        {
            // Get and "use" all items in the pool
            for (int j = 0; j < objCount; ++j)
            {
                activeObjects.Add(pool.GetPooledObject());
            }
        
            // Put all items back in the pool
            for (int j = objCount - 1; j >= 0; --j)
            {
                pool.ReturnPooledObject(activeObjects[j]);
                activeObjects.RemoveAt(j);
            }
        }
    
        watch.Stop();
        UnityEngine.Debug.Log( string.Format("Completed {0} in {1} ms", pool.GetType().Name, watch.Elapsed.Milliseconds) );
    }
}
