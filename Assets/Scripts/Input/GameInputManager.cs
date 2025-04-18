using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class GameInputManager : MonoBehaviour
{
    [Header("Player Input")]
    public float InputX;

    public static System.Action<float> PlayerInputX;

    //Jump
    public static System.Action<InputAction.CallbackContext> PlayerJump;
    // Attack
    public static System.Action<InputAction.CallbackContext> PlayerAttack;
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
    }
    //Attack
    private void Attack(InputAction.CallbackContext context)
    {
        if (PlayerController.Instance.CanMove)
            PlayerAttack?.Invoke(context);
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
        MoveInput();
    }
}