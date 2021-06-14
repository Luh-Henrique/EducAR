using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour
{

	private RoundData[] todasAsRodadas;

	private int rodadaIndex;
	private int playerHighScore;

	private string gameDataFileName = "data.Json";

	// Use this for initialization
	void Start()
	{
		DontDestroyOnLoad(gameObject);

		SceneManager.LoadScene("Menu");

		LoadGameData();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetRoundData(int round)
	{
		rodadaIndex = round;
	}

	public RoundData GetCurrentRoundData()
	{
		return todasAsRodadas[rodadaIndex];
	}

	private void LoadGameData()
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

		if(File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
			todasAsRodadas = loadedData.todasAsRodadas;
		}

		else
		{
			Debug.LogError("Não foi possí­vel carregar dados!");
		}
	}

	public void EnviarNovoHighScore(int newScore)
	{
		if(newScore > playerHighScore)
		{
			playerHighScore = newScore;
			SavePlayerProgress();
		}
	}

	public int GetHighScore()
	{
		return playerHighScore;
	}

	private void LoadPlayerProgress()
	{
		if(PlayerPrefs.HasKey("highScore"))
		{
			playerHighScore = PlayerPrefs.GetInt("highScore");
		}
	}

	private void SavePlayerProgress()
	{
		PlayerPrefs.SetInt("highScore", playerHighScore);
	}
}