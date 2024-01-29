using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [Header("Objects for following")]
    [SerializeField] private Transform _mainCharacter;

    [Header("Camera Properties")]
    [SerializeField] private float _returnSpeed;
    [SerializeField] private float _height;
    [SerializeField] private float _rearDistance;

    private Vector3 currentVector;

    private void Start()
    {
        transform.position = new Vector3(_mainCharacter.position.x, _mainCharacter.position.y + _height, _mainCharacter.position.z - _rearDistance);
        transform.rotation = Quaternion.LookRotation(_mainCharacter.position - transform.position);
    }

    private void Update()
    {
        if (_mainCharacter != null)
        {
            CameraMove();
        }
    }

    private void CameraMove()
    {
        currentVector = new Vector3(_mainCharacter.position.x, _mainCharacter.position.y + _height, _mainCharacter.position.z - _rearDistance);
        transform.position = Vector3.Lerp(transform.position, currentVector, _returnSpeed * Time.deltaTime);
    }
}