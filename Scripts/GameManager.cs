using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Declarar el game manager
    [Header("GameManager")]
    private GameObject gameManager;

    [Header("Dificultad y nivel")]
    public int dificultad;
    public int nivel;

    public int nivelPrevio;

    [Header("Jugador")]
    public GameObject[] personajes;
    public int personaje =0;

    public int vida = 0;
    public GameObject spawnPoint;

    [Header("Trancisiones")]
    public Animator animator;
    public AudioSource trancisionFiumba;

    [Header("Referencias")]
    public PlayerRespawn playerRespawn=null;

    public PointsController pointsController=null;

    public bool win;

    [Header("Alerta")]
    public GameObject alertaGM;
    public TMP_Text alertaGMText;

    [Header("GameOver")]
    public bool gameOverBool;
    public bool trabajaALoPendejo;

    void Start()
    {
        //Buscar el gamemanager
        gameManager = GameObject.Find("GameManager");

        //Para que no se destruya
        DontDestroyOnLoad(gameManager);

        //Asignar dificultad
        dificultad = PlayerPrefs.GetInt("Dificultad");  

        //conocer gameover
        nivel = 8;
    }

    //Metodos
    void Update()
    {
        switch(SceneManager.GetActiveScene().buildIndex){
            case 4:
                trabajaALoPendejo=true;
                spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
                //Referenciar
                playerRespawn = FindObjectOfType<PlayerRespawn>();
                pointsController = FindObjectOfType<PointsController>();
            break;
            case 5:
                trabajaALoPendejo=true;
                spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
                //Referenciar
                playerRespawn = FindObjectOfType<PlayerRespawn>();
                pointsController = FindObjectOfType<PointsController>();
            break;
            case 6:
                trabajaALoPendejo=true;
                spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
                //Referenciar
                playerRespawn = FindObjectOfType<PlayerRespawn>();
                pointsController = FindObjectOfType<PointsController>();
            break;
            case 7:
                trabajaALoPendejo=true;
                spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
                //Referenciar
                playerRespawn = FindObjectOfType<PlayerRespawn>();
                pointsController = FindObjectOfType<PointsController>();
            break;
        }

        if(SceneManager.GetActiveScene().buildIndex>3 && trabajaALoPendejo)
        {
            vida = playerRespawn.vida;
            win = pointsController.yaGanaste;
            PlayerPrefs.SetInt("Previo",SceneManager.GetActiveScene().buildIndex);
            if(vida<1 && gameOverBool)
            {
                Destroy(playerRespawn.gameObject);
                StartCoroutine(GameOverr());
            }
            else if(win)
            {   
                win=false;
                if(PlayerPrefs.GetInt("Dificultad")==0)
                {
                    StartCoroutine(GanePractica());
                }else if(PlayerPrefs.GetInt("Dificultad")>0)
                {
                    StartCoroutine(Gane());
                }
            }
        }
        else if(SceneManager.GetActiveScene().buildIndex<3)
        {
            Destroy(gameManager);
        }
        else if(SceneManager.GetActiveScene().buildIndex==9){
            if(Input.anyKeyDown)
            {
                StartCoroutine(SiguienteNivel());
            }
        }
    }

    //Metodos

    public void Fiumba()
    {
        trancisionFiumba.Play();
    }

    public void SeleccionarWalter()
    {
        if(PlayerPrefs.GetInt("Dificultad")==0)
        {
            StartCoroutine(HowToWalter());
        }
        else if(PlayerPrefs.GetInt("Dificultad")>0)
        {
            StartCoroutine(NormalWalter());
        }
    }

    public void SeleccionarShirley()
    {
        if(PlayerPrefs.GetInt("Dificultad")==0)
        {
            StartCoroutine(HowToShirley());
        }
        else if(PlayerPrefs.GetInt("Dificultad")>0)
        {
            StartCoroutine(NormalShirley());
        }
    }

    public void Reiniciado()
    {
        StartCoroutine(ReiniciaarNew());
    }

    public void CrearPersonaje()
    {
        int selectedChar = PlayerPrefs.GetInt("selectedCharacter");
        GameObject prefab = personajes[selectedChar];
        GameObject clone = Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
        Debug.Log("PersonajeCreado en: "+SceneManager.GetActiveScene().buildIndex);
    }

    //IEnumerators
    public IEnumerator HowToWalter()
    {
        personaje=0;
        PlayerPrefs.SetInt("selectedCharacter",personaje);
        Fiumba();
        animator.SetTrigger("Trancision");
        SceneManager.LoadScene(4);
        yield return new WaitForSeconds(1);
        CrearPersonaje();
    }

    public IEnumerator HowToShirley()
    {
        personaje=1;
        PlayerPrefs.SetInt("selectedCharacter",personaje);
        animator.SetTrigger("Trancision");
        Fiumba();
        SceneManager.LoadScene(4);
        yield return new WaitForSeconds(1);
        CrearPersonaje();
    }

    public IEnumerator NormalWalter()
    {
        personaje=0;
        PlayerPrefs.SetInt("selectedCharacter",personaje);
        animator.SetTrigger("Trancision");
        SceneManager.LoadScene(5);
        gameOverBool=true;
        Fiumba();
        yield return new WaitForSeconds(1);
        CrearPersonaje();
    }

    public IEnumerator NormalShirley()
    {
        personaje=1;
        PlayerPrefs.SetInt("selectedCharacter",personaje);
        animator.SetTrigger("Trancision");
        SceneManager.LoadScene(5);
        gameOverBool=true;
        Fiumba();
        yield return new WaitForSeconds(1);
        CrearPersonaje();
    }

    public IEnumerator Alert()
    {
        alertaGM.SetActive(true);
        yield return new WaitForSeconds(5);
        alertaGM.SetActive(false); 
    }

    public IEnumerator GameOverr()
    {
        gameOverBool=false;
        animator.SetTrigger("Trancision");
        Fiumba();
        yield return new WaitForSeconds(1);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("GameOver");
        trabajaALoPendejo=false;
    }

    public IEnumerator Reiniciar()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Previo"));
        gameOverBool=true;
        yield return new WaitForSeconds(0.5f);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CrearPersonaje();
    }

    public IEnumerator ReiniciaarNew()
    {
        SceneManager.LoadScene(nivelPrevio);
        gameOverBool=true;
        yield return new WaitForSeconds(0.5f);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CrearPersonaje();
        Debug.Log(PlayerPrefs.GetInt("Previo"));
    }

    public IEnumerator GanePractica()
    {
        Fiumba();
        animator.SetTrigger("Trancision");
        yield return new WaitForSeconds(1);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
    }

    public IEnumerator Gane()
    {
        Fiumba();
        nivelPrevio = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Previo",nivelPrevio);
        animator.SetTrigger("Trancision");
        SceneManager.LoadScene("LevelClear");
        trabajaALoPendejo=false;
        yield return new WaitForSeconds(1f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public IEnumerator SiguienteNivel()
    {
        Fiumba();
        animator.SetTrigger("Trancision");
        SceneManager.LoadScene(PlayerPrefs.GetInt("Previo")+1);
        yield return new WaitForSeconds(1f);
        CrearPersonaje();
    }
}
