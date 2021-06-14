using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	public Text textoPergunta;
	public Text textoPontos;
	public Text textoTimer;
	public Text highScoreText;

	public SimpleObjectPool answerButtonObjectPool;
	public Transform answerButtonParent;
	public GameObject painelDePerguntas;
	public GameObject painelFimRodada;

	private DataController dataController;
	private RoundData rodadaAtual;
	private QuestionData[] questionPool;

	private bool rodadaAtiva;
	private float tempoRestante;
	private int questionIndex;
	private int playerScore;

	List<int> usedValues = new List<int>();

	private List<GameObject> answerButtonGameObjects = new List<GameObject>();


	// Use this for initialization
	void Start()
	{

		dataController = FindObjectOfType<DataController>();
		rodadaAtual = dataController.GetCurrentRoundData();

		questionPool = rodadaAtual.perguntas;
		tempoRestante = rodadaAtual.limiteDeTempo;

		UpdateTimer();

		playerScore = 0;
		questionIndex = 0;
		ShowQuestion();
		rodadaAtiva = true;

	}

	// Update is called once per frame
	void Update()
	{

		if (rodadaAtiva)
		{
			tempoRestante -= Time.deltaTime;
			UpdateTimer();
			if (tempoRestante <= 0)
			{
				EndRound();
			}
		}

	}

	private void UpdateTimer()
	{
		textoTimer.text = "Timer: " + Mathf.Round(tempoRestante).ToString();
	}

	private void ShowQuestion()
	{
		RemoveAnswerButtons();

		int random = Random.Range(0, questionPool.Length);
		while (usedValues.Contains(random))
		{
			random = Random.Range(0, questionPool.Length);
		}

		QuestionData questionData = questionPool[random];
		usedValues.Add(random);
		textoPergunta.text = questionData.textoDaPergunta;

		for (int i = 0; i < questionData.respostas.Length; i++)
		{
			GameObject answerButtongameObject = answerButtonObjectPool.GetObject();

			answerButtongameObject.transform.SetParent(answerButtonParent);

			answerButtonGameObjects.Add(answerButtongameObject);

			AnswerButton answerButton = answerButtongameObject.GetComponent<AnswerButton>();
			answerButton.Setup(questionData.respostas[i]);
		}
	}

	private void RemoveAnswerButtons()
	{
		while (answerButtonGameObjects.Count > 0)
		{
			answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
			answerButtonGameObjects.RemoveAt(0);
		}
	}

	public void AnswerButtonClicked(bool estaCorreto)
	{
		if (estaCorreto)
		{
			playerScore += rodadaAtual.pontosPorAcerto;
			textoPontos.text = "Score: " + playerScore.ToString();
		}

		if (questionPool.Length > questionIndex + 1)
		{
			questionIndex++;
			ShowQuestion();
		}
		else
		{
			EndRound();
		}
	}

	public void EndRound()
	{
		rodadaAtiva = false;

		dataController.EnviarNovoHighScore(playerScore);
		highScoreText.text = "High Score: " + dataController.GetHighScore().ToString();

		painelDePerguntas.SetActive(false);
		painelFimRodada.SetActive(true);
	}

	public void ReturnToMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void AbrirAR()
	{
		SceneManager.LoadScene("AR");
	}
}