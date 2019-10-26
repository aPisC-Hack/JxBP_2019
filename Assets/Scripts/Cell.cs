using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellTypes
{
    Empty = 0,
    Important = 1,
    Good = 2,
    Cancer = 3,
    Dead = 4
}

public class Cell : MonoBehaviour
{
    public float RadiationImmunity = 1;
    private CellTypes cellType = CellTypes.Empty;
    public CellTypes CellType { 
        get {return cellType;} 
        set{
            if(value == CellTypes.Empty) Destroy(gameObject);
            if(cellType != value){
                switch (value)
                {
                    case CellTypes.Good:
                        GetComponent<SpriteRenderer>().sprite = goodSprites[Random.Range(0, goodSprites.Length)];
                        RadiationImmunity = 10;
                        break;
                    case CellTypes.Cancer:
                        GetComponent<SpriteRenderer>().sprite = badSprites[Random.Range(0, badSprites.Length)];
                        RadiationImmunity = 7;
                        break;
                    case CellTypes.Dead:
                        GetComponent<SpriteRenderer>().sprite = deadSprites[Random.Range(0, deadSprites.Length)];
                        break;
                    case CellTypes.Important:
                        GetComponent<SpriteRenderer>().sprite = SensibleSprites[Random.Range(0, SensibleSprites.Length)];
                        RadiationImmunity = 3;
                        break;
                }
            }
            cellType = value;
        } 
    }

    Collider2D coll = null;
    private bool needupdate = false;
    public float hP = 1000;
    public float maxHp = 1000;

    public float HP { get => hP; set => hP = value; }

    public Sprite[] deadSprites;
    public Sprite[] goodSprites;
    public Sprite[] badSprites;
    public Sprite[] SensibleSprites;

    


    //Gauss Bell Distribution
    float GaussBellDistribution(float x, float ex, float d2x)
    {
        d2x = 1 / d2x;
        return 500*Mathf.Exp(-Mathf.Pow((x - ex), 2) / 2 * Mathf.Sqrt(d2x)) / (Mathf.Sqrt(Mathf.PI*Mathf.Sqrt(d2x)));
        
    }

    float Electron_Damage(float dist, float power)
    {
        return GaussBellDistribution(dist*(float)0.6, 1/10, power/200)/5;
    }


    float GetDistance(Vector3 head)
    {
        return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(gameObject.transform.position.x - head.x), 2) + Mathf.Pow(Mathf.Abs(gameObject.transform.position.y - head.y), 2));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ray"){
            coll = collision;
            needupdate = true;        
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Ray"){
            coll = null;
            needupdate = false;
        }

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
            float intensity = coll.gameObject.transform.parent.GetComponent<Radiation>().Intensity;
            if(intensity == 0){
                needupdate = false;
                coll = null;
            }
            if(this.HP > 0){
                this.HP -= Mathf.Abs(GaussBellDistribution(distance, 1 / 3, intensity)) / 4 / RadiationImmunity;
            }
            if (this.HP <= 0)
            {
                CellType = CellTypes.Dead;

            }
        }
        Color c = GetComponent<SpriteRenderer>().color;
        c.a = hP > 0 ? (hP * 1f / maxHp / 2) +0.5f : 1;
        GetComponent<SpriteRenderer>().color = c;

    }
}
