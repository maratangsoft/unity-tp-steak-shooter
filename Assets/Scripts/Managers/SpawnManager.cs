using System.Collections;
using UnityEngine;

namespace Maratangsoft.SteakShooter
{
	public class SpawnManager : MonoBehaviour
	{
		[SerializeField] private ObjectPool animalPool;
		[SerializeField] private GameObject powerUpItem;

		// common spawn range
		private readonly float spawnRangeX = 16f;
		private readonly float spawnRangeZ = 20f;

		// animal spawn
		private Coroutine spawnAnimalCoroutine;
		private Quaternion animalSpawnRotation = new Quaternion(0, 180, 0, 1);

		// power up item spawn
		private Coroutine spawnPowerUpCoroutine;

		public ObjectPool AnimalPool { get => animalPool; }

		public void StartAnimalSpawning(int numOfAnimalTypes,
										float spawnInterval)
		{
			animalPool.SetPool(numOfAnimalTypes, numOfAnimalTypes * 3);
			spawnAnimalCoroutine = StartCoroutine(SpawnRandomAnimal(spawnInterval));
		}

		public void StartPowerUpSpawning(float spawnInterval)
		{
			spawnPowerUpCoroutine = StartCoroutine(SpawnPowerUp(spawnInterval));
		}

		public void StopSpawning()
		{
			StopCoroutine(spawnAnimalCoroutine);
			animalPool.DestroyAll();

			StopCoroutine(spawnPowerUpCoroutine);
			powerUpItem.SetActive(false);
		}

		private IEnumerator SpawnRandomAnimal(float spawnInterval)
		{
			while (true)
			{
				Vector3 animalSpawnPosition =
					new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnRangeZ);
				animalPool.GetRandomObject(animalSpawnPosition, animalSpawnRotation);

				yield return new WaitForSeconds(spawnInterval);
			}
		}

		private IEnumerator SpawnPowerUp(float spawnInterval)
		{
			while (true)
			{
				yield return new WaitForSeconds(spawnInterval);

				Vector3 powerUpSpawnPosition =
					new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 1, spawnRangeZ);
				powerUpItem.transform.position = powerUpSpawnPosition;
				powerUpItem.SetActive(true);
			}
		}
	}
}