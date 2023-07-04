using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBorder : MonoBehaviour
{
	[SerializeField] GameManager gameManager;

	private void OnTriggerEnter(Collider other)
	{
		gameManager.GameOver();
	}
}
