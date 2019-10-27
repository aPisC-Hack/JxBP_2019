using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorizer : MonoBehaviour
{
    public Color disabledColor, color;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = this.GetComponent<Button>().enabled ? color: disabledColor;
        
    }
}
