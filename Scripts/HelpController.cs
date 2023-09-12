using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpController : MonoBehaviour
{
    [Header("UI")]
    public GameObject pantallaAyuda;
    public GameObject ganaste;

    [Header("Referencias")]
    public AudioSource levelSound;
    public AudioSource winSound;

    public bool yaGane;


    void Start()
    {
        pantallaAyuda.SetActive(false);
        ganaste.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if(yaGane)
            {
                levelSound.Stop();
                winSound.Play();
                ganaste.SetActive(true);
            }else{
                StartCoroutine(QuitarAyuda());
            }
        }
    }

    public IEnumerator QuitarAyuda()
    {
        pantallaAyuda.SetActive(true);
        yield return new WaitForSeconds(5);
        pantallaAyuda.SetActive(false);
    }

}
