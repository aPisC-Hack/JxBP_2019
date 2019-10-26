using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiation : MonoBehaviour
{
    public float Intensity = 1, Depth = 1;
    public float minDistance = 0;
    public List<UnityEngine.GameObject> Cells;


    float GetDistance(Vector3 head)
    {
        return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(gameObject.transform.position.x - head.x), 2) + Mathf.Pow(Mathf.Abs(gameObject.transform.position.y - head.y), 2));

    }
    public void AddToCells(UnityEngine.GameObject Item)
    {
        //ha üres belerakja
        if (Cells.Count == 0)
        {
            Cells.Add(Item);
            return;
        }

        int i = 0;
        while (GetDistance(Item.transform.position) > GetDistance(Cells[i].transform.position)&&i<Cells.Count)
        {
            i++;
        }
        Cells.Insert(i, Item);
        minDistance = GetDistance(Cells[0].transform.position);


    }
    // Start is called before the first frame update
    void Start()
    {
        Cells = new List<UnityEngine.GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 s = transform.localScale;
        s.x = Intensity;
        transform.localScale = s;
    }
}
