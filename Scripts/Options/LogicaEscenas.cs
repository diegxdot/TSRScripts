using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaEscenas : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Opciones");
        if(objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }
}
