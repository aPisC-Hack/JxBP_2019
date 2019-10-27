using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public GameObject cell;
    int counter = 0;
    public int width = 5, height = 5;

    public List<GameObject> cells = new List<GameObject>();
    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        foreach (var item in cells)
        {
            
            save.hps.Add(item.gameObject.GetComponent<Cell>().HP);
            save.ids.Add(item.gameObject.GetComponent<Cell>().id);
            save.cts.Add(item.gameObject.GetComponent<Cell>().CellType);

            
        }
        save.height = height;
        save.width = width;
        //##################################x
        save.DayCount = 0;
        save.SessionId = 0;


        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("saved game");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();


            float sin60 = Mathf.Sin(60 * Mathf.Deg2Rad);

            float gw = (save.width + 1.5f) * 2 * sin60;
            float gh = (save.height + 1) * 0.5f;
            float radius = Mathf.Min(gw, gh) / 2;


            for (int i = 0; i < cells.Count; i++)
            {
                Destroy(cells[i]);
            }
            cells.Clear();
            //Debug.Log(sin60);
            for (int i = 0; i < save.height; i++)
            {
                for (int j = 0; j < save.width; j++)
                {
                    Vector3 pos = Vector3.zero;
                    pos.x += (j - save.width / 2f) * 2 * sin60;
                    if (i % 2 == 1) pos.x += sin60;
                    pos.y = (i - save.height / 2f) * .5f + 0.4f;

                    if (pos.magnitude <= radius)
                    {

                        //  Debug.Log(pos);
                        GameObject c = GameObject.Instantiate(cell, transform);
                        c.transform.localPosition = pos;
                        int k = 0;
                        for (k = 0; k < save.ids.Count && save.ids[k] != i*j+j; k++)
                        {

                        }
                        
                        c.GetComponent<Cell>().id = save.ids[save.ids.IndexOf(i * height + j)];
                        Debug.Log("ID" + save.ids[save.ids.IndexOf(i * height + j)]);
                        c.GetComponent<Cell>().CellType = save.cts[save.ids.IndexOf(i * height + j)];
                        cells.Add(c);
                    }


                }
            }
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        float sin60 = Mathf.Sin(60 * Mathf.Deg2Rad);

        float gw = (width +1.5f) * 2 * sin60;
        float gh = (height+1 )* 0.5f;
        float radius = Mathf.Min(gw, gh) / 2;
        //Debug.Log(sin60);
        for(int i = 0; i < height; i++){
            for(int j = 0; j < width; j++){
                Vector3 pos = Vector3.zero;
                pos.x += (j-width/2f)*2*sin60;
                if(i%2== 1) pos.x += sin60;
                pos.y = (i-height/2f) * .5f+0.4f;

                if(pos.magnitude <= radius){

                    //  Debug.Log(pos);
                    GameObject c = GameObject.Instantiate(cell, transform);
                    c.transform.localPosition = pos;
                    c.GetComponent<Cell>().id = i * height + j;
                    c.GetComponent<Cell>().CellType = (CellTypes) (j % 3) + 1;
                    cells.Add(c);
                }

                
            }
        }

        float size = radius * 2.8f;
        float diff = 2 / size;
        transform.localScale = new Vector3(diff, diff, 1);
        SaveGame();
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        Debug.Log(counter);

        if (counter == 300)
        {
            counter = 0;
            LoadGame();
            
        }
        if (counter == 100)
        {
            SaveGame();
        }
        
    }
}
