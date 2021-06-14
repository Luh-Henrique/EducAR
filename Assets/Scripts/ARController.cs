using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ARController : MonoBehaviour
{
    private float tempoRestante = 30;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tempoRestante -= Time.deltaTime;
        Debug.Log("Tempo: " + tempoRestante);
        
        if(tempoRestante<=0)
        {
            VoltarAoJogo();
        }
    }

    void VoltarAoJogo()
    {
        SceneManager.LoadScene("Game");
    }
}
