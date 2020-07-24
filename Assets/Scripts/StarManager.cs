using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StarManager : MonoBehaviour
{
    public static StarManager instance;
    public List<string> userPath;
    public List<StarLine> finalPath;
    public int linesTurnOn = 0;
    public Star selected;
    public string nameScene;
    public bool win = false;
    public bool lose = false;
    public float timerWin = 60;
    public GameObject winGameObject;
    public GameObject loseGameObject;
    public float maxTime = 60;
    public Text textTime;
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
        textTime.text = ((int)(maxTime)).ToString();
    }
    private void Update()
    {
        textTime.text = ((int)(maxTime)).ToString();
        if (!win && !lose)
        {
            maxTime -= Time.deltaTime;
            if (maxTime <= 0)
            {
                lose = true;
                StartCoroutine("EndGame");
            }
        }
    }
    public void onClickStar(Star star)
    {
        if (win || lose)
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
    IEnumerator EndGame()
    {
        loseGameObject.SetActive(true);
        winGameObject.SetActive(false);
        yield return new WaitForSeconds(timerWin);
        SceneManager.LoadScene(nameScene);
    }
}
