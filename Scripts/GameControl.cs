using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    [Header("Pausa")]
    public bool gameRunning;
    public GameObject pausaPanel=null;

    public GameManager gameManager;

    //Adio GO
    public AudioSource gameOverAudio=null;
    public AudioSource mainTheme=null;

    void Start()
    {
        gameRunning = true;
        //Referenciar
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeGameRunningState();
        }
    }

    public void ChangeGameRunningState()
    {
        gameRunning = !gameRunning;

        if(gameRunning)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Reanudado");
            mainTheme.Play();
            Time.timeScale = 1f;
            pausaPanel.SetActive(false);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("Pausado");
            mainTheme.Pause();
            Time.timeScale = 0f;
            pausaPanel.SetActive(true);
        }
    }

    //Metodos
    public void SalirAlMenuMetodo()
    {
        StartCoroutine(SalirAlMenu());
    }

    public void SalirAlMenuMetodoGO()
    {
        StartCoroutine(SalirAlMenuGO());
    }

    public void SalirMetodo()
    {   
        StartCoroutine(Salir());
    }

    public void ReiniciarMetodo()
    {
        StartCoroutine(Reiniciar());
    }

    public bool IsGameRunning()
    {
        return gameRunning;
    }

    //IEnumerators
    public IEnumerator Reiniciar()
    {
        gameManager.animator.SetTrigger("Trancision");
        gameOverAudio.Stop();
        gameManager.Fiumba();
        yield return new WaitForSeconds(1);
        gameManager.Reiniciado();
    }

    public IEnumerator SalirAlMenu()
    {
        Time.timeScale = 1f;
        gameManager.animator.SetTrigger("Trancision");
        gameManager.Fiumba();
        mainTheme.Stop();
        yield return new WaitForSeconds(1);
        gameRunning = !gameRunning;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pausaPanel.SetActive(false);
        SceneManager.LoadScene(2);
    }

    public IEnumerator SalirAlMenuGO()
    {
        Time.timeScale = 1f;
        gameManager.animator.SetTrigger("Trancision");
        gameManager.Fiumba();
        gameOverAudio.Stop();
        yield return new WaitForSeconds(1);
        gameRunning = !gameRunning;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
    }

    public IEnumerator Salir()
    {
        gameManager.animator.SetTrigger("Trancision");
         gameManager.Fiumba();
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
