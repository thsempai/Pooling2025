using System.Collections.Generic;
using UnityEngine;
using System;

public class Pool<T>
where T : IPoolClient
{
    private Transform anchor;
    private GameObject prefab;
    private Queue<T> queue = new();

    private int batch;
    public Pool(Transform anchor, GameObject prefab, int batch)
    {
        this.anchor = anchor;
        this.prefab = prefab;
        this.batch = batch;

        CreateBatch();
    }

    private void CreateBatch()
    {
        for (int _ = 0; _ < batch; _++)
        {
            GameObject go = GameObject.Instantiate(prefab);
            if (go.TryGetComponent(out T client))
            {
                Add(client);
            }
            else
            {
                throw new ArgumentException("Prefab doesn't have a IPoolClient as component.");
            }
        }
    }

    public void Add(T client)
    {
        queue.Enqueue(client);
        client.Fall();
    }

    public T Get()
    {
        if (queue.Count == 0) CreateBatch();
        T client = queue.Dequeue();
        client.Arise(anchor.position, anchor.rotation);
        return client;
    }


}
