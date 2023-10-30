using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickToFire : JoystickHandler, IPointerClickHandler, IDropHandler
{
    
    [SerializeField] private GameObject _shootDirection;

    private void FixedUpdate()
    {
        if (_inputVector.x != 0 || _inputVector.y != 0)
        {
            _player.VisualShootDirection(new Vector3(_inputVector.x, 0, _inputVector.y));
            _shootDirection.SetActive(true);
        }
        else
        {
            _shootDirection.SetActive(false);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        _player.Shoot();
        _shootDirection.SetActive(false);
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
    }
}
