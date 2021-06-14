using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GameDataEditor : EditorWindow
{

	public GameData gameData;

	private string gameDataFilePath = "/StreamingAssets/data.json";
	public Vector2 scrollPosition;

	[MenuItem("Window/Game Data Editor")]
	static void Init()
	{
		GameDataEditor windows = (GameDataEditor)EditorWindow.GetWindow(typeof(GameDataEditor));
		windows.Show();
	}

	private void OnGUI()
	{

		if (gameData != null)
		{

			scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(600), GUILayout.Height(600));

			SerializedObject serializedObject = new SerializedObject(this);
			SerializedProperty serializedProperty = serializedObject.FindProperty("gameData");

			EditorGUILayout.PropertyField(serializedProperty, true);

			serializedObject.ApplyModifiedProperties();
			GUILayout.EndScrollView();

			if (GUILayout.Button("Save Data"))
			{
				SaveGameData();
			}

		}

		if (GUILayout.Button("Load Data"))
		{
			LoadGameData();
		}

	}

	private void LoadGameData()
	{
		string filePath = Application.dataPath + gameDataFilePath;

		if (File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			gameData = JsonUtility.FromJson<GameData>(dataAsJson);
		}
		else
		{
			gameData = new GameData();
		}

	}

	private void SaveGameData()
	{
		string dataAsJson = JsonUtility.ToJson(gameData);
		string filePath = Application.dataPath + gameDataFilePath;
		File.WriteAllText(filePath, dataAsJson);
	}

}