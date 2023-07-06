using UnityEngine;

namespace Maratangsoft.SteakShooter
{
	public class Animal : MonoBehaviour
	{
		[SerializeField] private int maxHp = 1;
		[SerializeField] private float speed = 10.0f;

		private ObjectPool animalPool;
		private GameManager gameManager;
		private int currentHp;

		private void Start()
		{
			gameManager = GameManager.Instance;
			animalPool = gameManager.AnimalPool;
			currentHp = maxHp;
		}

		void Update()
		{
			transform.Translate(speed * Time.deltaTime * Vector3.forward);

			if (transform.position.z < gameManager.BottomBorder)
			{
				gameManager.Lose();
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			currentHp--;
			if (currentHp <= 0)
			{
				animalPool.ReturnObject(gameObject);
				currentHp = maxHp;
			}
		}
	}
}