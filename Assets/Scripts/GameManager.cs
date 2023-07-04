using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject stageMenu;
	[SerializeField] private GameObject clearMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject playerPrefab;
	[SerializeField] private ObjectPool enemyPool;

	private Vector3 playerSpawnPosition = Vector3.zero;

	private GameObject playerInstance;
	private Coroutine spawnAnimalCoroutine;

	private float spawnRangeX = 16;
	private float spawnRangeZ = 20;
	private float spawnInterval = 1.5f;

	public void StartStage()
    {
        mainMenu.SetActive(false);
        stageMenu.SetActive(true);
		gameOverMenu.SetActive(false);

		// instantiate player
        playerInstance = Instantiate(playerPrefab, playerSpawnPosition, playerPrefab.transform.rotation);
		// instantiate enemy
		spawnAnimalCoroutine = StartCoroutine(SpawnRandomAnimal());
	}

	IEnumerator SpawnRandomAnimal()
	{
		while (true)
		{
			Vector3 spawnPosition =
				new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnRangeZ);
			enemyPool.GetObject(spawnPosition);
			
			yield return new WaitForSeconds(spawnInterval);
		}
	}

	IEnumerator Timer()
	{
		yield return new WaitForSeconds(spawnInterval);
	}

	public void GameOver()
	{
		gameOverMenu.SetActive(true);
		Destroy(playerInstance);
		StopCoroutine(spawnAnimalCoroutine);
		enemyPool.RetrieveAll();
	}
}
