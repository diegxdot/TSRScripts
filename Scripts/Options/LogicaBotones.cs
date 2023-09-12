using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaBotones : MonoBehaviour
{
    public ControladorBotones panelBotones;

    void Start()
    {
        panelBotones = null;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            panelBotones = GameObject.FindGameObjectWithTag("Botones").GetComponent<ControladorBotones>();
        }
    }

    public void MostrarOpciones()
    {
        panelBotones.pantallaBotones.SetActive(true);
    }

    public void OcultarOpciones()
    {
        panelBotones.pantallaBotones.SetActive(false);
    }
}
