using Maratangsoft.SteakShooter;
using System;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
	[SerializeField] private PowerUp type = PowerUp.TRIPLE_SHOT;
    [SerializeField] private float speed = 4.0f;
    [SerializeField] private float degreePerSecond = 90.0f;

    private GameManager gameManager;


	private void Start()
	{
		gameManager = GameManager.Instance;
	}
	void Update()
    {
        if (gameObject.activeInHierarchy)
        {
			transform.Translate(speed * Time.deltaTime * Vector3.back, Space.World);
			transform.Rotate(degreePerSecond * Time.deltaTime * new Vector3(1, 1, 0));
		}

		if (transform.position.z < gameManager.BottomBorder)
		{
			gameObject.SetActive(false);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
        {
            gameManager.BulletShooter.PowerUp(type);
			gameObject.SetActive(false);
		}
	}
}
