using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogClickHandler : MonoBehaviour
{
    // Start is called before the first frame update
    DialogHandler dh;
    DateTime datetime;

    void Start()
    {
        datetime = DateTime.Now;
        dh = transform.parent.GetComponent<DialogHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dh.Timeout > 0 && (DateTime.Now - datetime).TotalMilliseconds > dh.Timeout){
            LoadNext();
        }
    }

    public void LoadNext(){
        transform.parent.GetComponent<DialogHandler>().OpenNextDialog();
    }
}
