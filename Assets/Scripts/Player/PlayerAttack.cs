using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Player Attack")]
    [SerializeField] private float attackTime = 1;
    [SerializeField] private float attackDistance = 1;
    [SerializeField] private Transform startPos;
    [HideInInspector] public int DamageAmount = 5;
    private int reverseAxis;
    private float delay;
    public bool CanAttack;

    [SerializeField] private GameObject hitEffectPrefab;

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
    private void Start()
    {
        DamageAmount = 1;
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
            if(hit.collider.GetComponent<Damageable>()!=null)
            {
                hit.collider.GetComponent<Damageable>().Damage(DamageAmount);            
            }
        }
    }
    public void Update()
    {
        if (GameManager.Instance.GamePaused)
            return;
        AttackDelay();
       // Attack();
    }
}