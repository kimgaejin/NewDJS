using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirror : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        if (this.gameObject.tag == "mirror12") {
            this.gameObject.tag = "mirror41";
        }
       else if (this.gameObject.tag == "mirror41")
        {
            this.gameObject.tag = "mirror34";
        }
       else if (this.gameObject.tag == "mirror34") {
            this.gameObject.tag = "mirror23";
        }
        else if(this.gameObject.tag == "mirror23") {
            this.gameObject.tag = "mirror12";
        }
    }
}
