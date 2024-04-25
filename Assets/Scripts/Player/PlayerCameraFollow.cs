using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private Transform playerTransform;
	[SerializeField] private string playerTag;
	[SerializeField] private float moveSpeed = 5f;
	
	private void Awake()
	{
		if (this.playerTransform == null)
		{
			if (this.playerTag == "")
			{
				this.playerTag = "Player";
			}
			
			this.playerTransform = GameObject.FindGameObjectWithTag(this.playerTag).transform;
		}

		this.transform.position = new Vector3()
		{
			x = this.playerTransform.position.x,
			y = this.playerTransform.position.y,
			z = this.playerTransform.position.z -1,
		};
	}
	
	private void FixedUpdate()
	{
		if (this.playerTransform)
		{
			Vector3 target = new Vector3()
			{
				x = this.playerTransform.position.x,
				y = this.playerTransform.position.y,
				z = this.playerTransform.position.z -1,
			};
			
		Vector3  pos = Vector3.Lerp(this.transform.position, target, this.moveSpeed * Time.fixedDeltaTime);
		
		this.transform.position = pos;
		}
	}
}