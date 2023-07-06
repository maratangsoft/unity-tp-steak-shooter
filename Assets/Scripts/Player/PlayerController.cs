using UnityEngine;

namespace Maratangsoft.SteakShooter
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private float speed = 25.0f;
		[SerializeField] private BulletShooter bulletShooter;

		[HideInInspector] private float horizontalInput;

		private float xRange = 17f;

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
				bulletShooter.Shoot();
			}

			horizontalInput = Input.GetAxis("Horizontal");
			transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
		}
	}
}