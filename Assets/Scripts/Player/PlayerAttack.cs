using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Player Attack")]
    [SerializeField] private float attackTime = 1;
    [SerializeField] private float attackDistance = 1;
    [SerializeField] private Transform startPos;
    private int reverseAxis;
    private float delay;
    public bool CanAttack;

    private void OnEnable()
    {
        GameInputManager.PlayerAttack += AttackButton;
        GameInputManager.PlayerInputX += AttackDirection;
    }
    private void OnDisable()
    {
        GameInputManager.PlayerAttack -= AttackButton;
        GameInputManager.PlayerInputX -= AttackDirection;
    }

    private void AttackButton(InputAction.CallbackContext obj)
    {
        if(obj.ReadValueAsButton())
        {
            Attack();
            delay = 0;
        }
    }
    private void AttackDelay()
    {
        if (delay < attackTime)
        {
            delay += Time.deltaTime;
            CanAttack = false;
        }
        else
        {
            CanAttack = true;
        }
            
    }
    private void AttackDirection(float input)
    {
        if (input > 0)
            reverseAxis = 1;
        else if (input < 0)
            reverseAxis = -1;
    }
    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(startPos.position, transform.right, attackDistance * reverseAxis);

        Debug.DrawRay(startPos.position, transform.right * attackDistance*reverseAxis, Color.red);

        if (hit.collider != null && CanAttack)
        {
            Debug.Log("Trigger");
        }
    }
    public void Update()
    {
        AttackDelay();
       // Attack();
    }
}