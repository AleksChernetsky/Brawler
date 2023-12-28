using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] private int _amountToPool;
    [SerializeField] private bool autoExpand;

    private List<GameObject> pooledObjects = new List<GameObject>();
    public static ObjectPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < _amountToPool; i++)
            CreateObject();
    }

    private GameObject CreateObject(bool isActiveByDefault = false)
    {
        GameObject projectile = Instantiate(_projectilePrefab, container);
        projectile.SetActive(isActiveByDefault);
        pooledObjects.Add(projectile);
        return projectile;
    }

    public GameObject GetFreeElement()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        for (int i = 0; i < 21; i++)
        {
            if (autoExpand)
                return CreateObject(true);
        }

        throw new Exception("There is no free element in pool");
    }
}