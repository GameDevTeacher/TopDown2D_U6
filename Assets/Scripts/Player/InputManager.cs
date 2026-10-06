using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class InputManager : MonoBehaviour
{
    public Vector2 MoveDirection { get; private set; }
    public Vector2 LookDirection { get; private set;}
    public bool InteractPressed { get; private set;}
    public bool AttackPressed { get; private set;}
    
    private void Update()
    {
        MoveDirection = SetMoveDirection();
        if (MoveDirection != Vector2.zero) LookDirection = MoveDirection;
        
        InteractPressed = _inputSystemActions.Player.Interact.WasPressedThisFrame();
        AttackPressed = _inputSystemActions.Player.Attack.WasPressedThisFrame();
    }
    
    private InputSystem_Actions _inputSystemActions;
    
    private void Awake() => _inputSystemActions = new InputSystem_Actions();
    private void OnEnable() => _inputSystemActions.Enable();
    private void OnDisable() => _inputSystemActions.Disable();

    #region 4 DIR MOVEMENT
    
    private bool[] LRUD = {false, false, false, false}; // Left, Right, Up, Down bools
    private readonly Vector2[] _moveDirection = new [] {new Vector2(-1,0), new Vector2(1, 0), new Vector2(0,1), new Vector2(0, -1) }; // L R U D Directions
    [SerializeField] private List<Vector2> directions; // List to save the current direction
    
    // The value included here is to make the transition between Gamepad Stick directions smoother
    // This is required to seperate the diagonal axes
    private readonly float _stickDeadzone = 0.625f; //625f

    private Vector2 SetMoveDirection()
    {
        // Here we get each movement direction and check whether there is a value greater than the Deadzone
        LRUD[0] = _inputSystemActions.Player.Move.ReadValue<Vector2>().x < -_stickDeadzone || _inputSystemActions.Player.Left.IsPressed();
        LRUD[1] = _inputSystemActions.Player.Move.ReadValue<Vector2>().x > _stickDeadzone || _inputSystemActions.Player.Right.IsPressed();
        LRUD[2] = _inputSystemActions.Player.Move.ReadValue<Vector2>().y > _stickDeadzone || _inputSystemActions.Player.Up.IsPressed();
        LRUD[3] = _inputSystemActions.Player.Move.ReadValue<Vector2>().y < -_stickDeadzone || _inputSystemActions.Player.Down.IsPressed();
        
        for (int i = 0; i < LRUD.Length; i++)
        {
            if (LRUD[i] && !directions.Contains(_moveDirection[i]))
            {
                directions.Add(_moveDirection[i]); 
            }
            else if (LRUD[i]  == false)
            {
                directions.Remove(_moveDirection[i]);
            }
        }
        return directions.Count > 0 ? directions.Last() : Vector2.zero;
    }
    #endregion
    
}