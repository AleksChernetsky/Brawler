using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private MovementHandler _movementHandler;
    private Animator _animHandler;
    private float _moveSpeed;
    private float currentSpeed;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _movementHandler = GetComponent<MovementHandler>();
        _animHandler = GetComponent<Animator>();
        _moveSpeed = _movementHandler.MoveSpeed;
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _rigidBody.velocity = new Vector3(direction.x * _moveSpeed, 0, direction.z * _moveSpeed);
            transform.LookAt(transform.position + direction, Vector3.up);
            currentSpeed = _rigidBody.velocity.magnitude;
            _animHandler.SetFloat("Speed", currentSpeed);
        }
    }
}