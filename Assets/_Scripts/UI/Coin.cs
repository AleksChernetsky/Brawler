using TMPro;

using UnityEngine;

public class Coin : MonoBehaviour
{
    private int _reward;
    [SerializeField] private int _minReward;
    [SerializeField] private int _maxReward;

    private void Start()
    {
        _reward = Random.Range(_minReward, _maxReward);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out VitalitySystem vitalitySystem))
        {
            EventManager.CallCoinPickedUp(_reward);
            Destroy(gameObject);
        }
    }
}
