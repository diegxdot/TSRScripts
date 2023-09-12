using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private GameObject menuManager;
    public AudioSource menuSound;
    // Start is called before the first frame update
    void Start()
    {
        Destruir();
    }

    private void Destruir()
    {
        menuManager = GameObject.Find("MenuManager");
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Menu");
        if(objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        switch(SceneManager.GetActiveScene().buildIndex){
            case 2:
                Destruir();
            break;

            case 3:
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Regresar();
                }
            break;

            case 4:
                Destroy(menuManager);
            break;

            case 5:
                Destroy(menuManager);
            break;
        }
    }

    public void Regresar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
