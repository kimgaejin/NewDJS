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

        transform.Rotate(new Vector3(0, 0, -45));
        if (this.gameObject.tag == "812R456")
        {
            this.gameObject.tag = "123R567";
        }
        else if (this.gameObject.tag == "123R567")
        {
            this.gameObject.tag = "234R678";
        }
        else if (this.gameObject.tag == "234R678")
        {
            this.gameObject.tag = "345R781";
        }
        else if (this.gameObject.tag == "345R781")
        {
            this.gameObject.tag = "812R456";
        }
    }
}
