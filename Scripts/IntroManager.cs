using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    //GameObject
    private GameObject introManager;

    public VideoClip videoVideo;

    [Header("Animaciones")]
    public Animator transicionIntroATitulo;
    public Animator tituloSalida;

    private float duracionVideo;

    void Start()
    {
        //Buscar el gamemanager
        introManager = GameObject.Find("IntroM");

        //Duracion
        duracionVideo = (float)videoVideo.length;
    }

    void Update()
    {
        switch(SceneManager.GetActiveScene().buildIndex){
            case 0:
                StartCoroutine(toTitle());
                //Cambiar Escenas
                if(Input.anyKeyDown){
                    StartCoroutine(toTransicion());
                }
            break;

            case 1:
                //Cambiar Escenas
                if(Input.anyKeyDown){
                    StartCoroutine(toButtons());
                }
            break;
        }
    }

    private IEnumerator toTitle()
    {   
        
        yield return new WaitForSeconds(duracionVideo);
        StartCoroutine(toTransicion());
    }

    private IEnumerator toTransicion()
    {
        transicionIntroATitulo.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator toButtons()
    {  
        tituloSalida.SetTrigger("Picado");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
