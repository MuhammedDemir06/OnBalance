using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Player Animation")]
    [SerializeField] private Animator playerAnim;
    [SerializeField] private PlayerController playerManager;
    [SerializeField] private PlayerAttack attackManager;
    [Header("Jump Effect")]
    [SerializeField] private Animator jumpAnim;

    private void OnEnable()
    {
        GameInputManager.PlayerInputX += MoveAnim;
        GameInputManager.PlayerJump += JumpAnim;
        GameInputManager.PlayerAttack += AttackAnim;
        PlayerHealth.PlayerDeath += DeadAnim;
    }
    private void OnDisable()
    {
        GameInputManager.PlayerInputX -= MoveAnim;
        GameInputManager.PlayerJump -= JumpAnim;
        GameInputManager.PlayerAttack -= AttackAnim;
        PlayerHealth.PlayerDeath -= DeadAnim;
    }
    private void DeadAnim()
    {
        playerAnim.SetTrigger("Death");
    }
    private void AttackAnim(InputAction.CallbackContext obj)
    {
        if (obj.ReadValueAsButton())
        {
            if (attackManager.CanAttack && !GameManager.Instance.GamePaused && !PlayerController.Instance.IsDead)
                playerAnim.SetTrigger("Attack");
        }
    }
    private void MoveAnim(float inputX)
    {
        playerAnim.SetFloat("MoveInput", inputX);
    }
    private void JumpAnim(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            playerAnim.SetTrigger("Jump");
            jumpAnim.SetTrigger("Jump");
        }
    }
    private void FallAnim()
    {
        if (!playerManager.IsEdge())
        {
            playerAnim.SetBool("Edge",false);
            playerAnim.SetBool("Ground", playerManager.IsGround());
        }
        else
        {
            playerAnim.SetBool("Ground", true);
            playerAnim.SetBool("Edge", playerManager.IsEdge());
        }
    }
    private void Update()
    {
        FallAnim();
    }
}
