using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _joystickBG;
    [SerializeField] private Image _joystickKnob;
    [SerializeField] private Image _joystickArea;
    [SerializeField] protected PlayerBehaviour _player;

    private Vector2 _joystickBGStartPosition;

    public Vector2 _inputVector;

    private void Start()
    {
        _joystickBGStartPosition = _joystickBG.rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 joystickBackGroundPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea.rectTransform, eventData.position, null, out joystickBackGroundPosition))
        {
            _joystickBG.rectTransform.anchoredPosition = new Vector2(joystickBackGroundPosition.x, joystickBackGroundPosition.y);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joystickKnobPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBG.rectTransform, eventData.position, null, out joystickKnobPosition))
        {
            joystickKnobPosition.x = (joystickKnobPosition.x * 2 / _joystickBG.rectTransform.sizeDelta.x);
            joystickKnobPosition.y = (joystickKnobPosition.y * 2 / _joystickBG.rectTransform.sizeDelta.y);

            _inputVector = new Vector2(joystickKnobPosition.x, joystickKnobPosition.y);

            _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector;

            _joystickKnob.rectTransform.anchoredPosition =
                new Vector2(_inputVector.x * (_joystickBG.rectTransform.sizeDelta.x / 2), _inputVector.y * (_joystickBG.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickBG.rectTransform.anchoredPosition = _joystickBGStartPosition;
        _inputVector = Vector2.zero;
        _joystickKnob.rectTransform.anchoredPosition = Vector2.zero;
    }
}