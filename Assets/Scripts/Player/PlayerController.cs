using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Player Controller")]
    public bool IsDead;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [Header("Jump")]
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private int maxJumpCount = 2;
    private int jumpCount;
    private Rigidbody2D rb;
    private void OnEnable()
    {
        GameInputManager.PlayerInputX += Move;
        GameInputManager.PlayerJump += Jump;
    }
    private void OnDisable()
    {
        GameInputManager.PlayerInputX -= Move;
        GameInputManager.PlayerJump -= Jump;
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
    private void Jump(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
