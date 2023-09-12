using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    public Animator buttons;
    public GameObject salirPanel;

    public GameObject seleccionPanel;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            salirPanel.SetActive(true);
        }
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void ComoJugarMetodo()
    {
        StartCoroutine(ComoJugar());
        
    }

    public void NormalMetodo()
    {
        StartCoroutine(Normal());
    }

    public void DificilMetodo()
    {
        StartCoroutine(Dificil());
    }

    public IEnumerator ComoJugar()
    {  
        buttons.SetTrigger("Picado");
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("Dificultad",0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public IEnumerator Normal()
    {
        buttons.SetTrigger("Picado");
        seleccionPanel.SetActive(false);
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("Dificultad",1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public IEnumerator Dificil()
    {
        buttons.SetTrigger("Picado");
        seleccionPanel.SetActive(false);
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("Dificultad",2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
