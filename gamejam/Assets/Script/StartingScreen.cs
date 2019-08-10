using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScreen : MonoBehaviour
{
    public GameObject Level_Panel;
    public GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Game_Start() {
        Panel.SetActive(false);
        Level_Panel.SetActive(true);

    }
    public void Game_End() {


    }
    public void Back() {
        Panel.SetActive(true);
        Level_Panel.SetActive(false);
    }
}
