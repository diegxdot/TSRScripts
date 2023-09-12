using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PointsController : MonoBehaviour
{
    

    [Header("Coins")]
    public GameObject[] allCoins;
    public AudioSource coinsSfx;
    public int points;
    public int totalPoints;
    public TMP_Text textPoint = null;
    public bool yaGanaste;

    [Header("Alerta")]
    public GameObject alerta;
    public TMP_Text alertText;

    [Header("Saco")]
    public AudioSource sacoSfx;
    public GameObject sacoPlayer;
    public GameObject sacoHud;

    public Animator animator;
    public GameObject sacoMap;
    public bool sacoOn;

    public bool comprobante;

    [Header("Punto de victoria")]
    public GameObject exfillZone;

    // Start is called before the first frame update
    void Start()
    {   
        //Buscar objetos en el mapa
        allCoins = GameObject.FindGameObjectsWithTag("Coins");
        sacoMap = GameObject.FindGameObjectWithTag("Saco");
        exfillZone = GameObject.FindGameObjectWithTag("Victory");
        totalPoints = allCoins.Length;
        points = 0;
        textPoint.text = points + " / " + totalPoints;
        //Bool
        sacoOn=false;
        sacoPlayer.SetActive(false);
        alerta.SetActive(false);
        exfillZone.SetActive(false);
        yaGanaste=false;
    }

    //Si el player colisiona
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Coins") && !sacoOn){
            alertText.text = "Debes tener un saco para recoger la basura";
            StartCoroutine(Alert());
        }
        else if(other.gameObject.CompareTag("Coins") && sacoOn)
        {
            //Sumar puntos
            other.gameObject.SetActive(false);
            animator.SetTrigger("Obtener");
            points += 1;
            textPoint.text = points + " / " + totalPoints;
            coinsSfx.Play();
        }

        if(other.gameObject.CompareTag("Saco")){
            if(PlayerPrefs.GetInt("Dificultad")==0){
                alertText.text = "Con el saco puedes empezar a recoger la basura del mapa";
                StartCoroutine(Alert());
            }
            sacoOn=true;
            comprobante=true;
            sacoHud.SetActive(true);
            sacoPlayer.SetActive(true);
            sacoSfx.Play();
            sacoMap.SetActive(false);
        }

        if(other.gameObject.CompareTag("Victory"))
        {
            if(PlayerPrefs.GetInt("Dificultad")==0)
            {
                alertText.text="Completaste el nivel de prueba, ahora es momento de salvar el mundo submarino";
            }else
            {
                alertText.text="Limpiaron muy bien esta zona equipo de rescate submarino";
            }
            
            StartCoroutine(Ganaste());
        }

        if(points==totalPoints && sacoOn && comprobante)
        {
            //Gano
            if(PlayerPrefs.GetInt("Dificultad")==0)
            {
                alertText.text = "Al recoger toda la basura del mapa debes ir al punto de extracción, apuntado por el saco";
            }
            else{
                alertText.text = "Ve al punto de extracción";
            }
            exfillZone.SetActive(true);
            sacoPlayer.transform.LookAt(exfillZone.transform);
            StartCoroutine(Alert());
            comprobante=false;
        }
    }

    public IEnumerator Alert()
    {
        alerta.SetActive(true);
        yield return new WaitForSeconds(5);
        alerta.SetActive(false); 
    }

    public IEnumerator Ganaste()
    {
        StartCoroutine(Alert());
        yield return new WaitForSeconds(5);
        yaGanaste=true;
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddForce(transform.up * 100f, ForceMode.Impulse);
    }
}
