using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int HP = 1000;


    //Gauss Bell Distribution
    float GaussBellDistribution(float x, float ex, float d2x)
    {
        return Mathf.Exp(-Mathf.Pow((x - ex), 2) / 2 * d2x) / (Mathf.Sqrt(Mathf.PI*Mathf.Sqrt(d2x)));
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Destroy(gameObject);        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
