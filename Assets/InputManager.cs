using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 moveDirection;
    public Vector2 lookDirection;
    public bool interactPressed;
    public bool dashPressed;
    
    private void Update()
    {
        moveDirection = _inputSystem_Actions.Player.Move.ReadValue<Vector2>();
        lookDirection = _inputSystem_Actions.Player.Look.ReadValue<Vector2>();
        interactPressed = _inputSystem_Actions.Player.Interact.WasPressedThisFrame();
        dashPressed = _inputSystem_Actions.Player.Dash.WasPressedThisFrame();
    }
    
    private InputSystem_Actions _inputSystem_Actions;
    private void Awake() => _inputSystem_Actions = new InputSystem_Actions();
    private void OnEnable() => _inputSystem_Actions.Enable();
    private void OnDisable() => _inputSystem_Actions.Disable();
}