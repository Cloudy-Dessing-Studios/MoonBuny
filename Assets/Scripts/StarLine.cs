using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StarLine
{
    [SerializeField]
    public string ruta;
    [SerializeField]
    public GameObject linea;

    public void activeLinea(bool sw)
    {
        linea.SetActive(sw);
    }
}
