using UnityEngine;

public class JoystickToMove : JoystickHandler
{
    private void FixedUpdate()
    {
        if (_inputVector.x != 0 || _inputVector.y != 0)
        {            
            _player.MoveCharacter(new Vector3(_inputVector.x, 0, _inputVector.y));
            _player.RotateCharacter(new Vector3(_inputVector.x, 0, _inputVector.y));
        }
        else if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {   
            _player.MoveCharacter(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            _player.RotateCharacter(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        }
        else
        {
            _player.IdleStateAnim();
        }
    }
}
