using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    public float Scale = 10;
    public float TopPad, BottomPad, LeftPad, RightPad; 
    // Start is called before the first frame update
    void Start()
    {
        
        //GetComponent<Transform>().position = Vector3.up *2f * scale;
    }

    // Update is called once per frame
    void Update()
    {
        float aH = Screen.height - TopPad - BottomPad, aW = Screen.width - LeftPad - RightPad;
        float x = Scale / Screen.height * 2;

        //Debug.Log(aH + " " +  aW + " " + x);
        transform.localScale = new Vector3(aW*x, aH*x, 1);
        transform.position = new Vector3((LeftPad - RightPad) * x , (BottomPad-TopPad) * x , 0);
        
    }
}
