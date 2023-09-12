using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    public float threshold = -20;
    private PlayerRespawn pr;
    private PointsController ps;

    [Header("Saco")]
    public GameObject sacoMap;
    public GameObject sacoPlayer;
    public GameObject sacoAlerta;

    public TMP_Text sacoAlertaText;

    void Start()
    {
        pr = GetComponent<PlayerRespawn>();
        ps = GetComponent<PointsController>();
        sacoMap = GameObject.FindGameObjectWithTag("Saco");
    }

    void Update()
    {
        if(transform.position.y < threshold)
        {
            switch(SceneManager.GetActiveScene().buildIndex)
            {
                case 4:
                    transform.position = new Vector3(0.0f,0.5f,0.0f);
                break;

                case 5:
                //update the position
                    transform.position = new Vector3(-10.0f,0.5f,0.0f);
                    pr.PlayerDamaged();
                    if(ps.sacoOn)
                    {
                        ps.sacoOn = false;
                        sacoMap.SetActive(true);
                        sacoPlayer.SetActive(false);
                        StartCoroutine(Alert());
                    }
                break;
                case 6:
                //update the position
                    transform.position = new Vector3(0.0f,3.2f,52.0f);
                    pr.PlayerDamaged();
                    if(ps.sacoOn)
                    {
                        ps.sacoOn = false;
                        sacoMap.SetActive(true);
                        sacoPlayer.SetActive(false);
                        StartCoroutine(Alert());
                    }
                break;
                case 7:
                    transform.position = new Vector3(1.0f,0.0f,-2.0f);
                    pr.PlayerDamaged();
                    if(ps.sacoOn)
                    {
                        ps.sacoOn = false;
                        sacoMap.SetActive(true);
                        sacoPlayer.SetActive(false);
                        StartCoroutine(Alert());
                    }
                break;
            }  
        }
    }

    public IEnumerator Alert()
    {
        sacoAlertaText.text = "Has perdido el saco";
        sacoAlerta.SetActive(true);
        yield return new WaitForSeconds(5);
        sacoAlerta.SetActive(false); 
    }
}
