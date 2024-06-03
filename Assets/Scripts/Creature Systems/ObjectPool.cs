using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _projectileContainer;
    [SerializeField] private Transform _damageTextContainer;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private DamageText _damageTextPrefab;

    [SerializeField] private int _amountToPool;

    private List<GameObject> pooledProjectiles = new List<GameObject>();
    private List<GameObject> pooledDamageTexts = new List<GameObject>();
    public static ObjectPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        CreatePool();
    }

    //private void Start() => CreatePool();

    private void CreatePool()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            CreateProjectile();
            CreateDamageText();
        }
    }

    private GameObject CreateProjectile(bool isActiveByDefault = false)
    {
        GameObject projectile = Instantiate(_projectilePrefab.gameObject, _projectileContainer);
        projectile.SetActive(isActiveByDefault);
        pooledProjectiles.Add(projectile);
        return projectile;
    }
    private GameObject CreateDamageText(bool isActiveByDefault = false)
    {
        GameObject damageText = Instantiate(_damageTextPrefab.gameObject, _damageTextContainer);
        damageText.SetActive(isActiveByDefault);
        pooledDamageTexts.Add(damageText);
        return damageText;
    }

    public GameObject GetFreeProjectile()
    {
        for (int i = 0; i < pooledProjectiles.Count; i++)
        {
            if (!pooledProjectiles[i].activeInHierarchy)
            {
                return pooledProjectiles[i];
            }
        }
        throw new Exception("There is no free projectile in pool");
    }
    public GameObject GetFreeDamageText()
    {
        for (int i = 0; i < pooledDamageTexts.Count; i++)
        {
            if (!pooledDamageTexts[i].activeInHierarchy)
            {
                return pooledDamageTexts[i];
            }
        }
        throw new Exception("There is no free damage text in pool");
    }
}