using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaOpciones : MonoBehaviour
{
    public ControladorOpciones panelOpciones;

    void Start()
    {
        panelOpciones = GameObject.FindGameObjectWithTag("Opciones").GetComponent<ControladorOpciones>();
    }

    public void MostrarOpciones()
    {
        panelOpciones.pantallaOpciones.SetActive(true);
    }

    public void OcultarOpciones()
    {
        panelOpciones.pantallaOpciones.SetActive(false);
    }
}
