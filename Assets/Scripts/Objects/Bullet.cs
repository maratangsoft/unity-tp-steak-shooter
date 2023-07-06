using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maratangsoft.SteakShooter
{
	public class Bullet : MonoBehaviour
	{
		[SerializeField] private float speed = 40.0f;

		private ObjectPool bulletPool;
		private GameManager gameManager = GameManager.Instance;

		private void Start()
		{
			bulletPool = gameManager.BulletPool;
		}

		void Update()
		{
			transform.Translate(speed * Time.deltaTime * Vector3.forward);

			if (transform.position.z > gameManager.TopBorder)
			{
				bulletPool.ReturnObject(gameObject);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			bulletPool.ReturnObject(gameObject);
		}
	}
}