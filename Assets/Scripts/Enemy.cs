using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float speed = 40.0f;
	private ObjectPool enemyPool;

	private void Start()
	{
		enemyPool = GameObject.Find("Enemy Object Pool").GetComponent<ObjectPool>();
	}

	void Update()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}

	private void OnTriggerEnter(Collider other)
	{
		enemyPool.RetrieveObject(gameObject);
	}
}
