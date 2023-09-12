using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LogicaFullScreen : MonoBehaviour
{
    public Toggle toggle;

    public Dropdown dropdownRes;
    Resolution[] resolutions;

    void Start()
    {
        if(Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
        //
        RevisarResolucion();
        //
    }

    public void ActivarFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    //
    public void RevisarResolucion()
    {
        resolutions = Screen.resolutions;
        dropdownRes.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for(int i = 0; i <resolutions.Length; i++)
        {
            string opcion = resolutions[i].width + "x" + resolutions[i].height;
            opciones.Add(opcion);

            if(Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
                {
                    resolucionActual = i;
                }
        }
        dropdownRes.AddOptions(opciones);
        dropdownRes.value = resolucionActual;
        dropdownRes.RefreshShownValue();

        //
        dropdownRes.value = PlayerPrefs.GetInt("numeroResolucion",0);
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        //
        PlayerPrefs.SetInt("numeroResolucion",dropdownRes.value);
        //
        Resolution resolution = resolutions[indiceResolucion];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
