using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maratangsoft.SteakShooter
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager _instance;

		[Header("Game Objects")]
		[SerializeField] private GameObject player;
		[SerializeField] private SpawnManager spawnManager;
		[SerializeField] private UIManager uiManager;

		[Header("Game Configuration")]
		[SerializeField] private int startStageNum = 1;
		[SerializeField] private int stageTime = 60;
		[SerializeField] private float powerUpSpawnInterval = 20.0f;
		[SerializeField] private float powerUpDuration = 10.0f;

		[HideInInspector] private readonly float topBorder = 20.0f;
		[HideInInspector] private readonly float bottomBorder = -8.0f;

		// player
		BulletShooter bulletShooter;
		private Vector3 playerSpawnPosition = Vector3.zero;

		// stage timer
		private Coroutine timer;
		private int currentTime;

		// stage details
		private List<StageDetails> stageDetailsList;
		private int currentStageNum;
		

		public static GameManager Instance => _instance;
		public ObjectPool AnimalPool => spawnManager.AnimalPool;
		public ObjectPool BulletPool => bulletShooter.BulletPool;
		public BulletShooter BulletShooter => bulletShooter;
		public float PowerUpDuration => powerUpDuration;
		public float TopBorder => topBorder;
		public float BottomBorder => bottomBorder;

		private void Awake()
		{
			// for singleton instance
			if (_instance == null) _instance = this;
			else if (_instance != this) Destroy(gameObject);
			// DontDestroyOnLoad(gameObject);

			// for reference to other objects
			bulletShooter = player.GetComponent<BulletShooter>();

			// for stage setting
			currentStageNum = startStageNum;
			stageDetailsList = GetStageDetailsList();
		}

		private List<StageDetails> GetStageDetailsList()
		{
			return new List<StageDetails>()
			{
				new StageDetails(1.5f, 3),
				new StageDetails(1.2f, 3),
				new StageDetails(1.2f, 4),
				new StageDetails(0.8f, 4),
				new StageDetails(0.8f, 5),
			};
		}

		public void StartStage()
		{
			uiManager.ShowStageMenu(currentStageNum, stageTime);

			// Start timer
			currentTime = stageTime;
			timer = StartCoroutine(Timer());

			// Activate player object
			player.SetActive(true);

			// Spawn animals
			StageDetails currentStage = stageDetailsList[currentStageNum - 1];
			float animalSpawnInterval = currentStage.SpawnInterval;
			int numOfAnimalTypes = currentStage.NumOfAnimalTypes;
			spawnManager.StartAnimalSpawning(numOfAnimalTypes, animalSpawnInterval);

			// Spawn power up item
			spawnManager.StartPowerUpSpawning(powerUpSpawnInterval);
		}

		private IEnumerator Timer()
		{
			while (true)
			{
				yield return new WaitForSeconds(1.0f);
				if (currentTime <= 0)
				{
					Win();
					break;
				}
				currentTime--;
				uiManager.ShowTimer(currentTime);
			}
		}

		public void Win()
		{
			if (currentStageNum >= stageDetailsList.Count)
			{
				AllCleared();
				return;
			}
			currentStageNum++;
			uiManager.ShowClearMenu();
			ResetStage();
		}

		public void Lose()
		{
			uiManager.ShowGameOverMenu();
			ResetStage();
		}

		private void ResetStage()
		{
			bulletShooter.TurnOffPowerUp();
			StopCoroutine(timer);
			spawnManager.StopSpawning();

			player.SetActive(false);
			player.transform.position = playerSpawnPosition;
		}

		private void AllCleared()
		{
			uiManager.ShowAllClearMenu();
			ResetStage();
		}
	}
}