using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    public float Scale = 10;
    // Start is called before the first frame update
    void Start()
    {
        float scale = Mathf.Clamp01( Screen.width*1f / Screen.height);
        GetComponent<Transform>().localScale = new Vector3(Scale*scale, Scale*scale, 1);
        //GetComponent<Transform>().position = Vector3.up *2f * scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
