using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    //Variables
    [Header("Corazon")]
    public GameObject corazon;
    public int vida;

    public Animator animator;

    public TMP_Text textPoint = null;

    public AudioSource tomarDaño;

    [Header("Personaje")]
    public Animator animatorP;

    public GameObject colisionPreventiva;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Dificultad")==2)
        {
            vida = 1;
        }else{
            vida = 3;
        }
        
        textPoint.text = ""+vida;

        colisionPreventiva.SetActive(false);
    }
    void Update()
    {
        if (vida < 1)
        {
            textPoint.text = ""+vida;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void PlayerDamaged()
    {
        vida--;
        tomarDaño.Play();
        animator.SetTrigger("Damaged");
        textPoint.text = ""+vida;
        StartCoroutine(PrevenirDaño());
    }
    
    public IEnumerator PrevenirDaño()
    {
        animatorP.SetTrigger("Lastimado");
        colisionPreventiva.SetActive(true);
        yield return new WaitForSeconds(3);
        colisionPreventiva.SetActive(false);
    }

}
