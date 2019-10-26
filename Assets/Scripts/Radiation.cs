using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiation : MonoBehaviour
{
    public float Intensity = 1, Depth = 1;
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
    }
}
