using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundData
{
	public string nomeDoTema;
	public int limiteDeTempo;
	public int pontosPorAcerto;
	public QuestionData[] perguntas;
}