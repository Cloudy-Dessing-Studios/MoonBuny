using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public string id = "A";
    public Color orignalColor;
    public Color selectedCOlor;
    public void onClickStar()
    {
        StarManager.instance.onClickStar(this);
    }
    public void changeColor(Color color)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = color; 
    }
}
