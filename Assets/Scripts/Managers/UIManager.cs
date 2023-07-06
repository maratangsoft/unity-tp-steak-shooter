using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[Header("UI Objects")]
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject stageMenu;
	[SerializeField] private Text stageText;
	[SerializeField] private Text timerText;
	[SerializeField] private GameObject clearMenu;
	[SerializeField] private GameObject gameOverMenu;
	[SerializeField] private GameObject allClearMenu;

	public void ShowStageMenu(int currentStageNum, int stageTime)
	{
		mainMenu.SetActive(false);
		stageMenu.SetActive(true);
		clearMenu.SetActive(false);
		gameOverMenu.SetActive(false);

		stageText.text = "STAGE " + currentStageNum;
		ShowTimer(stageTime);
	}

	public void ShowTimer(int time)
	{
		int minute = time / 60;
		int second = (time - minute * 60) % 60;

		string minuteString, secondString;

		if (minute < 10) minuteString = "0" + minute;
		else minuteString = minute.ToString();

		if (second < 10) secondString = "0" + second;
		else secondString = second.ToString();

		timerText.text = minuteString + " : " + secondString;
	}

	public void ShowClearMenu()
	{
		clearMenu.SetActive(true);
	}

	public void ShowGameOverMenu()
	{
		gameOverMenu.SetActive(true);
	}

	public void ShowAllClearMenu()
	{
		allClearMenu.SetActive(true);
	}
}
