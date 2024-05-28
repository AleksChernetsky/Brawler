using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class KillEventView : MonoBehaviour
{
    [SerializeField] private KillEventPanel _panelPrefab;
    [SerializeField] private RectTransform _spawnPosition;
    [SerializeField] private RectTransform[] _targetPosition;

    private List<KillEventPanel> _pooledPanels = new List<KillEventPanel>();
    private int _panelsToPool = 5;
    private float _timeToRelease = 1.1f;
    private bool _canRelease = true;

    private void Awake()
    {
        GlobalEventsManager.OnKillInfo.AddListener(TriggerPanel);
    }
    private void Start()
    {
        while (_pooledPanels.Count <= _panelsToPool)
        {
            PoolPanels();
        }
    }
    private KillEventPanel PoolPanels()
    {
        KillEventPanel newPanel = Instantiate(_panelPrefab, transform);
        _pooledPanels.Add(newPanel);
        return newPanel;
    }
    private KillEventPanel FreePanel()
    {
        for (int i = 0; i < _pooledPanels.Count; i++)
        {
            if (_pooledPanels[i].IsFreeToRelease)
            {
                return _pooledPanels[i];
            }
        }
        throw new Exception("There is no free element in pool");
    }
    private void TriggerPanel(WeaponType weaponIcon, Sprite victimIcon)
    {
        StartCoroutine(ReleasePanel(weaponIcon, victimIcon));
    }
    private IEnumerator ReleasePanel(WeaponType weaponIcon, Sprite victimIcon)
    {
        yield return new WaitUntil(() => _canRelease);
        _canRelease = false;

        FreePanel().SetImages(weaponIcon, victimIcon);
        StartCoroutine(FreePanel().ShowPanel(_spawnPosition, _targetPosition[0]));

        for (int i = 0; i < _pooledPanels.Count; i++)
        {
            if (_pooledPanels[i].IsReleased)
            {
                _pooledPanels[i].PositionPoint++;
                StartCoroutine(_pooledPanels[i].MovePanel(_targetPosition[_pooledPanels[i].PositionPoint]));
            }
        }

        yield return new WaitForSeconds(_timeToRelease);
        _canRelease = true;
    }
}