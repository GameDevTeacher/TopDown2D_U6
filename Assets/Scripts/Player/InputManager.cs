using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 moveDirection;
    public Vector2 lookDirection;
    public bool interactPressed;
    public bool attackPressed;
    
    private void Update()
    {
        moveDirection = SetMoveDirection();
        if (moveDirection != Vector2.zero) lookDirection = moveDirection;
        
        interactPressed = _inputSystemActions.Player.Interact.WasPressedThisFrame();
        attackPressed = _inputSystemActions.Player.Attack.WasPressedThisFrame();
    }
    
    private InputSystem_Actions _inputSystemActions;
    
    private void Awake() => _inputSystemActions = new InputSystem_Actions();
    private void OnEnable() => _inputSystemActions.Enable();
    private void OnDisable() => _inputSystemActions.Disable();

    #region 4 DIR MOVEMENT
    
    private bool[] LRUD = new []{false, false, false, false}; // Left, Right, Up, Down bools
    private readonly Vector2[] _moveDirection = new [] {new Vector2(-1,0), new Vector2(1, 0), new Vector2(0,1), new Vector2(0, -1) }; // L R U D Directions
    [SerializeField] private List<Vector2> directions; // List to save the current direction
    
    // The value included here is to make the transition between Gamepad Stick directions smoother
    // This is required to seperate the diagonal axes
    private readonly float _stickDeadzone = 0.625f;

    private Vector2 SetMoveDirection()
    {
        // Here we get each movement direction and check whether there is a value greater than the Deadzone
        LRUD[0] = _inputSystemActions.Player.Move.ReadValue<Vector2>().x < -_stickDeadzone;
        LRUD[1] = _inputSystemActions.Player.Move.ReadValue<Vector2>().x > _stickDeadzone;
        LRUD[2] = _inputSystemActions.Player.Move.ReadValue<Vector2>().y > _stickDeadzone;
        LRUD[3] = _inputSystemActions.Player.Move.ReadValue<Vector2>().y < -_stickDeadzone;
     
        for (int i = 0; i < LRUD.Length; i++)
        {
            if (LRUD[i] && !directions.Contains(_moveDirection[i]))
            {
                directions.Add(_moveDirection[i]); 
            }
            else if (LRUD[i] == false)
            {
                directions.Remove(_moveDirection[i]);
            }
        }
        return directions.Count > 0 ? directions.Last() : Vector2.zero;
    }
    #endregion
}