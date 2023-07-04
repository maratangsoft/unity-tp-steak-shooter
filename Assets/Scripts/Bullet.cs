using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float speed = 40.0f;
	private ObjectPool bulletPool;

	private void Start()
	{
		bulletPool = GameObject.Find("Bullet Object Pool").GetComponent<ObjectPool>();
	}

	void Update()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}

	private void OnTriggerEnter(Collider other)
	{
		bulletPool.RetrieveObject(gameObject);
	}
}
