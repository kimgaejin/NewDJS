using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentDebugingKey : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.O)) Time.timeScale += 0.1f;
            if (Input.GetKeyDown(KeyCode.P)) Time.timeScale -= 0.1f;
            Debug.Log("TimeScale: " + Time.timeScale);
        }
    }
}
