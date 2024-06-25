using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[SelectionBase]
public class Player : Singleton<Player>
{
    [SerializeField] private float moveSpeed = 10f;
    private Vector2 inputVector;

    private Rigidbody2D rb;
    public Animator Animator { get; private set; }
    private SpriteRenderer spriteRenderer;
    private Knockback knockback;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            ActiveWeapon.Instance.GetActiveWeapon().Attack();
        }
    }

    private void Update()
    {
        if (!PauseManager.isMenuPaused)
        {
            inputVector.x = Input.GetAxisRaw("Horizontal");
            inputVector.y = Input.GetAxisRaw("Vertical");
            
            Animator.SetFloat("Horizontal", inputVector.x);
            Animator.SetFloat("Vertical", inputVector.y);
            Animator.SetFloat("Speed", inputVector.sqrMagnitude);
            
            if(inputVector.x != 0 || inputVector.y != 0)
            {
                Animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
                Animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
            }
            
            inputVector = GameInput.Instance.GetMovementVector();    
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(knockback.GettingKnockedBack || PlayerHealth.Instance.IsDead)
        {
            return;
        }
        rb.MovePosition(rb.position + inputVector * (moveSpeed * Time.fixedDeltaTime));
    }

    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }
}
