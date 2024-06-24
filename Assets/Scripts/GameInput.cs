using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{
	public static GameInput Instance {  get; private set; }

	private PlayerInputActions playerInputActions;

	public event EventHandler OnPlayerAttack;

	private void Awake()
	{
		Instance = this;

		playerInputActions = new PlayerInputActions();
		playerInputActions.Enable();

		playerInputActions.Combat.Attack.started += PlayerAttack_started;
	}

	private void PlayerAttack_started(InputAction.CallbackContext obj)
	{
		if(SceneManager.GetActiveScene().name != "Menu")
		{
			OnPlayerAttack?.Invoke(this, EventArgs.Empty);
		}
		
	}

	public Vector2 GetMovementVector()
	{
		Vector2 inpVector = playerInputActions.Player.Move.ReadValue<Vector2>();
		return inpVector;
	}

	public Vector3 GetMousePosition()
	{
		Vector3 mousePos = Mouse.current.position.ReadValue();
		return mousePos;
	}
}
