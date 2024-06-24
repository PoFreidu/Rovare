using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
	[SerializeField] private GameObject goldCoin, heartHealth;

	public void DropItems() {
		int randomNum = Random.Range(1, 5);
		
		if (randomNum == 1) 
		{
			Instantiate(heartHealth, transform.position, Quaternion.identity);
		}
		
		if (randomNum == 2) 
		{
			int randomAmountOfGold = Random.Range(1, 3);
			
			for (int i = 0; i < randomAmountOfGold; i++)  
			{
				Instantiate(goldCoin, transform.position, Quaternion.identity);
			}
		}
	}
}
