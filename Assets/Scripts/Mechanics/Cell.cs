using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public int id;
    public float RadiationImmunity = 1;
    private CellTypes cellType = CellTypes.Empty;
    public int[] neighbours = new int[6];
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
    public float maxHp = 1000;
    public float hP = 1000;
    
    private float sqrt2 = Mathf.Sqrt(2);
    public float HP { get => hP; set => hP = value; }
    

    public Sprite[] deadSprites;
    public Sprite[] goodSprites;
    public Sprite[] badSprites;
    public Sprite[] SensibleSprites;

    


    //Gauss Bell Distribution
    float GaussBellDistribution(float x, float ex, float d2x)
    {
        d2x = 1/d2x;
        return 40*Mathf.Exp(-Mathf.Pow((x - ex), 2) / 2 * d2x) / (Mathf.Sqrt(Mathf.PI*Mathf.Sqrt(d2x)));
        
    }
    float Fixed_GaussBellDistribution(float x, float ex, float d2x)
    {
        d2x = 1 / d2x;
        return 2500 * Mathf.Exp(-Mathf.Pow((x - ex), 2) / 2 * d2x) / (Mathf.Sqrt(Mathf.PI * Mathf.Sqrt(d2x)));

    }

    float Electron_Damage(float dist, float power)
    {
        return Fixed_GaussBellDistribution(dist*(float)0.3, 1/3, power/200);
    }
    float Proton_Damage(float dist, float power, float maxdist)
    {
        if (dist>maxdist)
        {
            return 0;
        }
        return (dist)*power;
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
        findNeighbours();
    }

    public void findNeighbours()
    {
        int height = this.gameObject.transform.parent.GetComponent<MapGenerator>().height;
        int x = Mathf.FloorToInt(id / height);
        int y = id % height;
        if(x % 2 == 0)
        {
            if (x!=0)
            {
                if (y!=0)
                {
                    neighbours[0] = (x - 1) * height + y - 1;
                }
                if (y!=height)
                {
                    neighbours[2] = (x - 1) * height + y + 1;
                }
               
            }

            if (y>0)
            {
                neighbours[3] = x * height + y - 1;
                if (y>-1)
                {
                    neighbours[1] = x * height + y - 2;

                }
            }

            if (y < height)
            {
                neighbours[5] = x * height + y + 1;

                if (y < height+1)
                {
                    neighbours[4] = x * height + y + 2;

                }
            }
            
        }
        else
        {
            neighbours[0] = x * height + y - 1;
            neighbours[1] = x * height + y - 2;
            neighbours[2] = x * height + y + 1;
            neighbours[3] = (x + 1) * height + y - 1;
            neighbours[4] = x * height + y + 2;
            neighbours[5] = (x + 1) * height + y + 1;
        }

        for (int i = 0; i < 6; i++)
        {
            if(neighbours[i] < 0)
            {
                neighbours[i] = -1;
            }
        }
    }


    /// <summary>
    /// Visszadja a terjedés valószínűségét
    /// </summary>
    /// <param name="GoodHP"></param>
    /// <param name="CancerHp"></param>
    /// <param name="MaxHp"></param>
    /// <returns>Valószínűsség</returns>
    static double Oraculum_Func(float GoodHP, float CancerHp, float MaxHp)
    {
        double balancer = 0.6;
        return (balancer * CancerHp - GoodHP) / MaxHp;

    }
    /// <summary>
    /// Megmondja hogy 2 sejt esetén a jó sejt rákos lesz-e
    /// </summary>
    /// <param name="GoodHP"></param>
    /// <param name="CancerHp"></param>
    /// <param name="MaxHp"></param>
    /// <returns>Terjed-e</returns>
    static bool Will_Spread(float GoodHP, float CancerHp, float MaxHp)
    {
        //Debug.Log(Oraculum_Func(GoodHP, CancerHp, MaxHp));
        double val = Oraculum_Func(GoodHP, CancerHp, MaxHp);
        double rand = Random.Range(0, 1);
        return rand < val;
    }

    public void Spread()
    {

        for (int i = 0; i < 6; i++)
        {
            if(neighbours[i] != -1)
            {
                List<GameObject> gameObjects = this.gameObject.transform.parent.GetComponent<MapGenerator>().cells;
                Cell cell = null;
                int count = 0;
                while (count < gameObjects.Count && gameObjects[count].gameObject.GetComponent<Cell>().id != neighbours[i])
                {
                    count++;
                }
                if (count<gameObjects.Count)
                {
                    cell = gameObjects[count].gameObject.GetComponent<Cell>();
                }
                
                if (cell != null &&( cell.CellType == CellTypes.Cancer && cellType != CellTypes.Dead && Will_Spread(HP, cell.HP, maxHp)))
                {
                    
                    this.gameObject.transform.parent.GetComponent<MapGenerator>().sums[(int)cell.CellType] -= 1;
                    CellType = CellTypes.Cancer;
                    this.gameObject.transform.parent.GetComponent<MapGenerator>().sums[(int)cell.CellType] += 1;



                }
            }
        }

    }
    float calculateDosis(float dist, float power, int type)
    {
        if (type == 0)
        {
            return Mathf.Abs(GaussBellDistribution(dist, 1 / 7, power)) / RadiationImmunity;
        }
        else if (type == 1)
        {
            return Electron_Damage(dist, power)/ RadiationImmunity;
        }
        else if (type == 2)
        {
            return Proton_Damage(dist, power, 4)/ RadiationImmunity;
        }

        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {


            //Debug.Log("New: "+newdistance);
            //Debug.Log("Old: " + distance);
            try{
            if(this.HP > 0 && needupdate && coll != null && coll.gameObject.transform.parent.parent.GetComponent<LaserHeadMovement>() != null){
                float intensity = coll.gameObject.transform.parent.GetComponent<Radiation>().Intensity;
                if(intensity == 0){
                    needupdate = false;
                    coll = null;
                }
                float rotation = coll.gameObject.transform.parent.parent.GetComponent<LaserHeadMovement>().Rotation;

                //proton távolság
                //float newdistance = GetDistance(new Vector3(Mathf.Sin(rotation), Mathf.Cos(rotation))) * (float)0.5;

                float newdistance = GetDistance(new Vector3(Mathf.Sin(rotation) * sqrt2 * 10, Mathf.Cos(rotation) * sqrt2 * 10)) * (float)0.5;
                

                float distance = GetDistance(coll.gameObject.transform.parent.transform.position) * (float)0.5;
                float dosis = Proton_Damage(distance, intensity, 4);
                this.HP -= dosis;
                transform.parent.GetComponent<DosisCalculator>().addDosis(dosis);
               // this.HP -= Mathf.Abs(GaussBellDistribution(distance, 1 / 3, intensity)) / 4 / RadiationImmunity;
            }
            }catch{}
            if (this.HP <= 0 && CellType != CellTypes.Dead)
            {
                this.gameObject.transform.parent.GetComponent<MapGenerator>().sums[(int)CellType] -= 1;
                CellType = CellTypes.Dead;                
                this.gameObject.transform.parent.GetComponent<MapGenerator>().sums[(int)CellType] += 1;
                if (this.gameObject.transform.parent.GetComponent<MapGenerator>().sums[(int)CellTypes.Cancer] <=0)
                {
                    SaveHandler.DestroySave(MapGenerator.saveid);
                    transform.parent.GetComponent<DosisCalculator>().GameWonDialog();
                }
                else if (this.gameObject.transform.parent.GetComponent<MapGenerator>().sums[(int)CellTypes.Good] <= 0)
                {
                    transform.parent.GetComponent<DosisCalculator>().GameLooseDialog();
                }
                else if (this.gameObject.transform.parent.GetComponent<MapGenerator>().sums[(int)CellTypes.Important] <= 0)
                {
                    transform.parent.GetComponent<DosisCalculator>().GameLooseDialog();
                }


            }
        }
        Color c = GetComponent<SpriteRenderer>().color;
        c.a = hP > 0 ? (hP * 1f / maxHp / 2) +0.5f : 1;
        GetComponent<SpriteRenderer>().color = c;

    }
}
