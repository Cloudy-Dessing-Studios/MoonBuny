using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StarManager : MonoBehaviour
{
    public static StarManager instance;
    public List<string> userPath;
    public List<StarLine> finalPath;
    public int linesTurnOn = 0;
    public Star selected;
    public string nameScene;
    public bool win = false;
    public float timerWin = 60;
    public GameObject winGameObject;
    private void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void onClickStar(Star star)
    {
        if (win)
        {
            return;
        }
        if (selected != null)
        {
            selected.changeColor(selected.orignalColor);
        }
        star.changeColor(star.selectedCOlor);
        selected = star;

        userPath.Add(star.id);
        if (userPath.Count > 1)
        {
            string actual = userPath[userPath.Count - 1];
            string anterior = userPath[userPath.Count - 1 - 1];

            for (int i = 0; i< finalPath.Count; i ++)
            {
                StarLine par = finalPath[i];
                if (par.ruta == actual+anterior || par.ruta == anterior + actual)
                {
                    if (par.linea.activeSelf == false)
                    {
                        linesTurnOn +=1;
                    }
                    par.linea.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("Minimo no alcanzado");
        }
        verifyWin();
    }

    public void verifyWin()
    {
        if (linesTurnOn >= finalPath.Count)
        {
            Debug.Log("WIN");
            win = true;
            winGameObject.SetActive(true);
            StartCoroutine("timers");
        }
    }
    IEnumerator timers()
    {
        yield return new WaitForSeconds(timerWin);
        SceneManager.LoadScene(nameScene);
    }
}
