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
        inpVector = GameInput.Instance.GetMovementVector();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        //inpVector = inpVector.normalized;
        rb.MovePosition(rb.position + inpVector * (moveSpeed * Time.fixedDeltaTime));
        
        if (Mathf.Abs(inpVector.x) > minMovingSpeed || Mathf.Abs(inpVector.y) > minMovingSpeed) 
        {
            isRunning = true;
            
        }
        else
        {
            isRunning=false;
        }
        //Debug.Log(inpVector);
    }

    //Vector2 inpVector = new Vector2(0, 0);

    //if (Input.GetKey(KeyCode.W))
    //{
    //    inpVector.y = 1f;
    //}

    //if (Input.GetKey(KeyCode.A))
    //{
    //    inpVector.x = -1f;
    //}

    //if (Input.GetKey(KeyCode.S))
    //{
    //    inpVector.y = -1f;
    //}

    //if (Input.GetKey(KeyCode.D))
    //{
    //    inpVector.x = 1f;
    //}

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
