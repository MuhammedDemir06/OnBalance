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
    // Attack
    public static System.Action<InputAction.CallbackContext> PlayerAttack;
    //Power
    public static System.Action<InputAction.CallbackContext,float> PlayerPower;
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
        //Attack
        gameInput.Player.Attack.performed += Attack;
        //Power
        gameInput.Player.Power.performed += Power;
    }
    //Attack
    private void Attack(InputAction.CallbackContext context)
    {
        if (PlayerController.Instance.CanMove)
            PlayerAttack?.Invoke(context);
    }
    private void Power(InputAction.CallbackContext context)
    {
        PlayerPower?.Invoke(context,InputX);
    }
    private void MoveInput()
    {
        //Move
        if (PlayerController.Instance.CanMove)
        {
            InputX = Input.GetAxis("Horizontal");
            PlayerInputX?.Invoke(InputX);
        }    
    }
    //Jump
    private void Jump(InputAction.CallbackContext context)
    {
        if (PlayerController.Instance.CanMove)
            PlayerJump?.Invoke(context);
    }
    private void Update()
    {
        if (GameManager.Instance.GamePaused)
            return;

        MoveInput();
    }
}