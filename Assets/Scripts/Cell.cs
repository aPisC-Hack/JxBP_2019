using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cell : MonoBehaviour
{
    private float hP = 1;
    public float HP { get => hP; set => hP = value; }

    
    


    //Gauss Bell Distribution
    float GaussBellDistribution(float x, float ex, float d2x)
    {
        return Mathf.Exp(-Mathf.Pow((x - ex), 2) / 2 * Mathf.Sqrt(d2x)) / (Mathf.Sqrt(Mathf.PI*Mathf.Sqrt(d2x)));
        
    }

    float GetDistance(Vector3 head)
    {
        return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(gameObject.transform.position.x - head.x), 2) + Mathf.Pow(Mathf.Abs(gameObject.transform.position.y - head.y), 2));

    }

    private void OnTriggerStay2D(Collider2D collision)
    {    
        float distance = GetDistance(collision.gameObject.transform.parent.transform.position);
        System.Console.WriteLine(distance);
        float intensity = collision.gameObject.transform.parent.GetComponent<Radiation>().Intensity;
        this.HP -= Mathf.Abs(GaussBellDistribution(distance, 1, intensity));
        Debug.Log(Mathf.Abs(GaussBellDistribution(distance, 1, intensity)));
        //Debug.Log(distance);
        //Debug.Log(intensity);
        //Debug.Log(gameObject.transform.position);
        //Debug.Log(this.HP);
        if (this.HP < 0)
        {
            Destroy(gameObject);
            
        }
        
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
