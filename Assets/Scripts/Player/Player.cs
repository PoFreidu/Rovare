using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
	public static Player Instance { get; private set; }

	[SerializeField] private float moveSpeed = 10f;
	Vector2 inpVector;

	private Rigidbody2D rb;
	private float minMovingSpeed = 0.1f;
	private bool isRunning = false;

	private void Awake()
	{
		Instance = this;
		rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
	}

	private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
	{
		ActiveWeapon.Instance.GetActiveWeapon().Attack();
	}

	private void Update()
	{
		if (!PauseManager.isMenuPaused)
		{
		inpVector = GameInput.Instance.GetMovementVector();	
		}
	}
	private void FixedUpdate()
	{
		HandleMovement();
	}
	private void HandleMovement()
	{
		rb.MovePosition(rb.position + inpVector * (moveSpeed * Time.fixedDeltaTime));
		
		if (Mathf.Abs(inpVector.x) > minMovingSpeed || Mathf.Abs(inpVector.y) > minMovingSpeed) 
		{
			isRunning = true;
			
		}
		else
		{
			isRunning=false;
		}
	}
	
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