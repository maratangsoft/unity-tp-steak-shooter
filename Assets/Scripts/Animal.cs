using Maratangsoft.SteakShooter;
using UnityEngine;

namespace Maratangsoft.SteakShooter
{
	public class Animal : MonoBehaviour
	{
		[SerializeField] private int maxHp = 1;
		[SerializeField] private float speed = 10.0f;

		private ObjectPool animalPool;
		private GameManager gameManager = GameManager.Instance;
		private int currentHp;

		private void Start()
		{
			animalPool = GameObject.Find("Animal Object Pool").GetComponent<ObjectPool>();
			currentHp = maxHp;
		}

		void Update()
		{
			transform.Translate(Vector3.forward * Time.deltaTime * speed);

			if (transform.position.z < gameManager.bottomBorder)
			{
				animalPool.ReturnObject(gameObject);
				gameManager.StageOver(StageResult.LOSE);
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