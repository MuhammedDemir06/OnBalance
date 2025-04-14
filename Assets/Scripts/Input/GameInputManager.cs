using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    [Header("Player Input")]
    public float InputX;

    public static System.Action<float> PlayerInputX;

    //Jump
    public static System.Action<InputAction.CallbackContext> PlayerJump;
    //input
    private GameInputSystem gameInput;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        gameInput = new GameInputSystem();
        gameInput.Player.Enable();

        //Input X
        InputX = gameInput.Player.Move.ReadValue<Vector2>().x;
        //Jump
        gameInput.Player.Jump.performed += Jump;
    }
    private void MoveInput()
    {
        //Move
        InputX = Input.GetAxis("Horizontal");
        PlayerInputX?.Invoke(InputX);
    }
    //Jump
    private void Jump(InputAction.CallbackContext context)
    {
        PlayerJump?.Invoke(context);
    }
    private void Update()
    {
        MoveInput();
    }
}