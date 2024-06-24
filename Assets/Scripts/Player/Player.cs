using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore;
using UnityEngine.SceneManagement;

[SelectionBase]
public class Player : Singleton<Player>
{
/* 	public static Player Instance { get; private set; } */
	
	/* private PlayerInputActions playerInputActions; */
	[SerializeField] private float moveSpeed = 10f;
	Vector2 inpVector;

	private Rigidbody2D rb;
	public Animator animator;
/* 	private float minMovingSpeed = 0.1f; */
	private bool isRunning = false;
	private SpriteRenderer spriteRenderer;
	private Knockback knockback;

		protected override void Awake()
	{
		base.Awake();
/* 		Instance = this; */
		/* playerInputActions = new PlayerInputActions(); */
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		knockback = GetComponent<Knockback>();
	}


	private void Start()
	{
		GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
/* 		animator = GetComponent<Animator>(); */
	}
	
/* 	private void OnEnable() {
		playerInputActions.Enable();
	}

	private void OnDisable() {
		playerInputActions.Disable();
	} */

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
		/* inpVector = playerInputActions.Player.Move.ReadValue<Vector2>(); */
		
		inpVector.x = Input.GetAxisRaw("Horizontal");
		inpVector.y = Input.GetAxisRaw("Vertical");
		
		animator.SetFloat("Horizontal", inpVector.x);
		animator.SetFloat("Vertical", inpVector.y);
		animator.SetFloat("Speed", inpVector.sqrMagnitude);
		
		if( inpVector.x != 0 || inpVector.y != 0 )
		{
			animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
			animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
		}
		
/* 		if (!PauseManager.isMenuPaused)
		{ */
		inpVector = GameInput.Instance.GetMovementVector();	
/* 		} */
		}

	}
	private void FixedUpdate()
	{
		HandleMovement();
/* 		Move(); */
	}
	private void HandleMovement()
	{
		if(knockback.GettingKnockedBack || PlayerHealth.Instance.isDead)
		{
			return;
		}
		rb.MovePosition(rb.position + inpVector * (moveSpeed * Time.fixedDeltaTime));
		
/* 		if (Mathf.Abs(inpVector.x) > minMovingSpeed || Mathf.Abs(inpVector.y) > minMovingSpeed) 
		{
			isRunning = true;
			
		}
		else
		{
			isRunning=false;
		} */
	}
	
/* 	public void OnMove(InputValue value)
	{
		inpVector = value.Get<Vector2>();
		
		if (inpVector != Vector2.zero)
		{
			animator.SetFloat("LastHorizontal", inpVector.x);
			animator.SetFloat("LastVertical", inpVector.y);
		}
	} */
	
/* 	private void Move()
	{
		if(knockback.GettingKnockedBack)
		{
			return;
		}
		rb.MovePosition(rb.position + inpVector * (moveSpeed * Time.fixedDeltaTime));
	} */
	
	public bool IsRunning() 
	{ 
		return isRunning; 
	}

	public Vector3 GetPlayerScreenPosition()
	{
		Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
		return playerScreenPosition;
	}
}