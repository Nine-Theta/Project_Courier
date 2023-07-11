using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

//TODO: test which queues get used most, and reorganizes queue population based on that

public class PriorityPoolManager<T> where T : IPriorizable
{
    private Queue<T> _criticalPool = new Queue<T>();
    private Queue<T> _importantPool = new Queue<T>();
    private Queue<T> _preferredPool = new Queue<T>();
    private Queue<T> _normalPool = new Queue<T>();
    private Queue<T> _lowPool = new Queue<T>();

    private List<T> _backlogPool = new List<T>();

    public T GetHighestPriority(bool pStopTracking = false)
    {
        T item = ReturnTopPriority();

        if (!pStopTracking) _backlogPool.Add(item);

        return item;
    }

    public void AddItem(T pItem)
    {
        switch (pItem.Priority)
        {
            case PriorityLevel.Normal:
                _normalPool.Enqueue(pItem);
                break;
            case PriorityLevel.Preferred:
                _preferredPool.Enqueue(pItem);
                break;
            case PriorityLevel.Important:
                _importantPool.Enqueue(pItem);
                break;
            case PriorityLevel.Critical:
                _criticalPool.Enqueue(pItem);
                break;
            case PriorityLevel.Low:
                _lowPool.Enqueue(pItem);
                break;
        }
    }

    public void AddBacklogItem(T pItem)
    {
        _backlogPool.Add(pItem);
    }

    public bool TryRemoveItem(T pItem)
    {
        if (_backlogPool.Contains(pItem))
        {
            _backlogPool.Remove(pItem);
            return true;
        }

        switch (pItem.Priority)
        {
            case PriorityLevel.Normal:
                return RemoveFromQueue(_normalPool, pItem);
            case PriorityLevel.Preferred:
                return RemoveFromQueue(_preferredPool, pItem);
            case PriorityLevel.Important:
                return RemoveFromQueue(_importantPool, pItem);
            case PriorityLevel.Critical:
                return RemoveFromQueue(_criticalPool, pItem);
            case PriorityLevel.Low:
                return RemoveFromQueue(_lowPool, pItem);
        }
        return false;
    }

    //Debug
    public List<T> GetPoolAsList(int pPool)
    {
        switch (pPool)
        {
            case 3:
                return _normalPool.ToList<T>();
            case 2:
                return _preferredPool.ToList<T>();
            case 1:
                return _importantPool.ToList<T>();
            case 0:
                return _criticalPool.ToList<T>();
            case 4:
                return _lowPool.ToList<T>();
            case 5:
                return _backlogPool;
        }

        return _backlogPool;
    }

    /////////////////////////////////////////////////////////

    private T ReturnTopPriority()
    {
        if (_criticalPool.Count > 0) return _criticalPool.Dequeue();
        if (_importantPool.Count > 0) return _importantPool.Dequeue();
        if (_preferredPool.Count > 0) return _preferredPool.Dequeue();
        if (_normalPool.Count > 0) return _normalPool.Dequeue();
        if (_lowPool.Count > 0) return _lowPool.Dequeue();

        if (_backlogPool.Count > 0)
        {
            RepopulateQueues();
            return ReturnTopPriority();
        }

        return default;
    }

    private bool RemoveFromQueue(Queue<T> pQueue, T pItem)
    {
        if (!pQueue.Contains(pItem)) return false;

        pQueue = new Queue<T>(pQueue.Where(item => !item.Equals(pItem)));
        return true;
    }

    private void RepopulateQueues()
    {
        for (int i = 0; i < _backlogPool.Count; i++)
        {
            switch (_backlogPool[i].Priority)
            {
                case PriorityLevel.Normal:
                    _normalPool.Enqueue(_backlogPool[i]);
                    break;
                case PriorityLevel.Preferred:
                    _preferredPool.Enqueue(_backlogPool[i]);
                    break;
                case PriorityLevel.Important:
                    _importantPool.Enqueue(_backlogPool[i]);
                    break;
                case PriorityLevel.Critical:
                    _criticalPool.Enqueue(_backlogPool[i]);
                    break;
                case PriorityLevel.Low:
                    _lowPool.Enqueue(_backlogPool[i]);
                    break;
            }
        }
        _backlogPool.Clear();
    }
}
