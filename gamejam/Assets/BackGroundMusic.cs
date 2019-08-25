using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource BGM = GetComponent<AudioSource>();
        BGM.Play();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
