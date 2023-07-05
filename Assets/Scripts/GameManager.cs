using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Maratangsoft.SteakShooter
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager _instance;
		public static GameManager Instance
		{
			get
			{
				return _instance;
			}
		}

		[HideInInspector] public float topBorder = 20.0f;
		[HideInInspector] public float bottomBorder = -8.0f;

		[Header("UI Objects")]
		[SerializeField] private GameObject mainMenu;
		[SerializeField] private GameObject stageMenu;
		[SerializeField] private Text stageText;
		[SerializeField] private Text timerText;
		[SerializeField] private GameObject clearMenu;
		[SerializeField] private GameObject gameOverMenu;

		[Header("Game Objects")]
		[SerializeField] private GameObject playerPrefab;
		[SerializeField] private ObjectPool animalPool;

		[Header("Game Configuration")]
		[SerializeField] private int startStageNum = 1;
		[SerializeField] private int stageTime = 10;

		// player spawning
		private Vector3 playerSpawnPosition = Vector3.zero;
		private GameObject playerInstance;

		// animal spawning
		private Coroutine spawnAnimalCoroutine;
		private readonly float spawnRangeX = 16f;
		private readonly float spawnRangeZ = 20f;
		private Quaternion animalSpawnRotation = new Quaternion(0, 180, 0, 0);

		// stage timer
		private Coroutine timer;
		private int currentTime;

		// stage details
		private List<StageDetails> stageDetailsList;
		private int currentStageNum;

		private void Awake()
		{
			// for singleton instance
			if (_instance == null) _instance = this;
			else if (_instance != this) Destroy(gameObject);
			DontDestroyOnLoad(gameObject);

			// for stage setting
			currentStageNum = startStageNum;
			stageDetailsList = GetStageDetailsList();
			animalPool = GameObject.Find("Animal Object Pool").GetComponent<ObjectPool>();
		}

		private List<StageDetails> GetStageDetailsList()
		{
			return new List<StageDetails>()
			{
				new StageDetails(1.5f, 3),
				new StageDetails(1.5f, 1),
				new StageDetails(1.2f, 0),
				new StageDetails(1.2f, 1),
				new StageDetails(0.8f, 0),
			};
		}

		public void StartStage()
		{
			mainMenu.SetActive(false);
			stageMenu.SetActive(true);
			clearMenu.SetActive(false);
			gameOverMenu.SetActive(false);

			stageText.text = "stage " + currentStageNum;
			timerText.text = GetTimeString(stageTime);

			// instantiate player
			playerInstance = Instantiate(playerPrefab,
										 playerSpawnPosition,
										 playerPrefab.transform.rotation);

			// get animals
			StageDetails currentStage = stageDetailsList[currentStageNum - 1];

			float spawnInterval = currentStage.SpawnInterval;

			int numOfAnimalTypesToAdd = currentStage.NumOfAnimalTypesToAdd;
			animalPool.ExpandPool(numOfAnimalTypesToAdd, numOfAnimalTypesToAdd * 3);
			spawnAnimalCoroutine = StartCoroutine(SpawnRandomAnimal(spawnInterval));

			// start timer
			currentTime = stageTime;
			timer = StartCoroutine(Timer());
		}

		IEnumerator SpawnRandomAnimal(float spawnInterval)
		{
			while (true)
			{
				Vector3 animalSpawnPosition =
					new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnRangeZ);
				animalPool.GetObject(animalSpawnPosition, animalSpawnRotation);

				yield return new WaitForSeconds(spawnInterval);
			}
		}

		IEnumerator Timer()
		{
			while (true)
			{
				yield return new WaitForSeconds(1.0f);
				if (currentTime <= 0)
				{
					StageOver(StageResult.WIN);
					break;
				}
				currentTime--;
				timerText.text = GetTimeString(currentTime);
			}
		}

		string GetTimeString(int time)
		{
			int minute = time / 60;
			int second = (time - minute * 60) % 60;

			string minuteString, secondString;

			if (minute < 10) minuteString = "0" + minute;
			else minuteString = minute.ToString();

			if (second < 10) secondString = "0" + second;
			else secondString = second.ToString();

			return minuteString + " : " + secondString;
		}

		public void StageOver(StageResult stageResult)
		{
			switch (stageResult)
			{
				case StageResult.WIN:
					clearMenu.SetActive(true);
					currentStageNum++;
					break;

				case StageResult.LOSE:
					gameOverMenu.SetActive(true);
					break;
			}
			Destroy(playerInstance);
			StopCoroutine(spawnAnimalCoroutine);
			StopCoroutine(timer);
			animalPool.ReturnAll();
		}
	}
}