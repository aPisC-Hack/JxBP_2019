using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiation : MonoBehaviour
{
    public Color[] colors;
    public Sprite[] sprites;
    public float Intensity {get {return transform.parent.GetComponent<LaserHeadMovement>().Intensity;}}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 s = transform.localScale;
        s.x = Intensity;
        transform.localScale = s;

        LaserHeadMovement.RadiationTypes rt = transform.parent.GetComponent<LaserHeadMovement>().RadiationType;
        
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[(int)rt];
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[(int)rt];
        
    }
}
