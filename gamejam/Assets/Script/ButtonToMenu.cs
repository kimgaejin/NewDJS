﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToMenu : MonoBehaviour
{
    public GameObject StopPanel;
    public void GotoMain()
    {
        //SceneManager.LoadScene("UI");
        StopPanel.SetActive(true);
    }
    public void pExit()
    {
        //SceneManager.LoadScene("UI");
        StopPanel.SetActive(false);
    }
    public void Menubutton()
    {
        SceneManager.LoadScene("UI");
       
    }
}
