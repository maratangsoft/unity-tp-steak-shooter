using UnityEngine;

namespace Maratangsoft.SteakShooter
{
	public class PlayerController : MonoBehaviour
	{
		[HideInInspector] private float horizontalInput;
		[SerializeField] private float speed = 25.0f;

		private float xRange = 17f;
		private int numOfBulletTypes = 1;
		private int initPoolingCount = 6;

		private ObjectPool bulletPool;

		private void Start()
		{
			bulletPool = GameObject.Find("Bullet Object Pool").GetComponent<ObjectPool>();
			bulletPool.ExpandPool(numOfBulletTypes, initPoolingCount);
		}

		void Update()
		{
			if (transform.position.x < -xRange)
			{
				transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
			}
			if (transform.position.x > xRange)
			{
				transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				bulletPool.GetObject(transform.position, transform.rotation);
			}

			horizontalInput = Input.GetAxis("Horizontal");
			transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
		}
	}
}