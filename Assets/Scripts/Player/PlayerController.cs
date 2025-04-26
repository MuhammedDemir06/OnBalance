using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Player Controller")]
    public bool IsDead;
    [HideInInspector] public bool CanMove;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [Header("Jump")]
    public float JumpForce = 5;
    [SerializeField] private int maxJumpCount = 2;
    private int jumpCount;
    [Header("Charge Effect")]
    [SerializeField] private float powerOfCharge = 5f;
    [SerializeField] private GameObject chargedEffect;
    private Rigidbody2D rb;
    [Header("Sounds")]
    [SerializeField] private AudioSource powerSound;
    private void OnEnable()
    {
        GameInputManager.PlayerInputX += Move;
        GameInputManager.PlayerJump += Jump;
        PlayerHealth.PlayerDeath += Dead;
        GameInputManager.PlayerPower += Charged;
    }
    private void OnDisable()
    {
        GameInputManager.PlayerInputX -= Move;
        GameInputManager.PlayerJump -= Jump;
        PlayerHealth.PlayerDeath -= Dead;
        GameInputManager.PlayerPower -= Charged;
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        CanMove = true;

        JumpForce = 3.5f;

        rb = GetComponent<Rigidbody2D>();
    }
    private void Move(float input)
    {
        rb.velocity = new Vector2(moveSpeed * input, rb.velocity.y);

        var newScale = transform.localScale;
        if (input > 0)
            newScale.x = 1;
        else if (input < 0)
            newScale.x = -1;

        transform.localScale = newScale;

        if (IsGround())
            jumpCount = maxJumpCount;
    }
    private void Charged(InputAction.CallbackContext context,float input)
    {
        if (context.ReadValueAsButton() && !IsDead && UIManager.Instance.PowerAmount > 98)
        {
            if (powerSound.enabled)
                powerSound.Play();
            var newEffect = Instantiate(chargedEffect, transform.position, Quaternion.identity);
            var effectScale = newEffect.transform.localScale;

            if (input > 0)
                effectScale.x = 1;
            else if (input < 0)
                effectScale.x = -1;

            newEffect.transform.localScale = effectScale;

            transform.position += Vector3.right * input * powerOfCharge;
        }
    }
    private void Dead()
    {
        IsDead = true;
        CanMove = false;
        Debug.LogWarning("Player Death");
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            jumpCount --;
        }
    }
    private void WallEdge()
    {
        if (IsEdge())
            rb.gravityScale = 0;
        else
            rb.gravityScale = 1;
    }
    private void Update()
    {
        WallEdge();
    }
    public bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundLayer);
    }
    public bool IsEdge()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundDistance, wallLayer);
    }
}
