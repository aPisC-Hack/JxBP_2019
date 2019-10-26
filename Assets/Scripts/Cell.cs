using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellTypes
{
    important,
    good,
    cancer
}

public class Cell : MonoBehaviour
{

    Collider2D coll = null;
    private bool needupdate = false;
    public float hP = 1000;

    public float HP { get => hP; set => hP = value; }

    
    


    //Gauss Bell Distribution
    float GaussBellDistribution(float x, float ex, float d2x)
    {
        d2x = 1 / d2x;
        return 500*Mathf.Exp(-Mathf.Pow((x - ex), 2) / 2 * Mathf.Sqrt(d2x)) / (Mathf.Sqrt(Mathf.PI*Mathf.Sqrt(d2x)));
        
    }

    float GetDistance(Vector3 head)
    {
        return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(gameObject.transform.position.x - head.x), 2) + Mathf.Pow(Mathf.Abs(gameObject.transform.position.y - head.y), 2));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        coll = collision;
        needupdate = true;        
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        coll = null;
        needupdate = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (needupdate)
        {
            float distance = GetDistance(coll.gameObject.transform.parent.transform.position) * (float)0.5;
            System.Console.WriteLine(distance);
            float intensity = coll.gameObject.transform.parent.GetComponent<Radiation>().Intensity;
            this.HP -= Mathf.Abs(GaussBellDistribution(distance, 1 / 3, intensity)) / 4;
            Debug.Log(Mathf.Abs(GaussBellDistribution(distance, 1, intensity)));
            if (this.HP < 0)
            {
                Destroy(gameObject);

            }


        }
    }
}
