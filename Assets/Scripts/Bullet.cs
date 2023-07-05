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
			bulletPool = GameObject.Find("Bullet Object Pool").GetComponent<ObjectPool>();
		}

		void Update()
		{
			transform.Translate(Vector3.forward * Time.deltaTime * speed);

			if (transform.position.z > gameManager.topBorder)
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