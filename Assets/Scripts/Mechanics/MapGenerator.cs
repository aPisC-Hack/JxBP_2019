using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> cells = new List<GameObject>();
    public int[] sums= new int[5];
    public GameObject cell;
    public int width = 5, height = 5;
    public static int saveid=0;

    public void Flash(){
        var sel = cells.OrderByDescending((x)=> 
            x.GetComponent<Cell>().neighbours.Sum(
                y => {
                    var cell = cells.FirstOrDefault(z => z.GetComponent<Cell>().id == y)?.GetComponent<Cell>();
                    if(cell == null || cell.CellType != CellTypes.Cancer) return 0;
                    return cell.HP;
                }
            )
        ).First();

        var nei = sel.GetComponent<Cell>().neighbours
            .Select(x=>cells.FirstOrDefault(z => z.GetComponent<Cell>().id == x)?.GetComponent<Cell>()
            ).Where(x => x!= null);

        foreach(var n in nei){
            n.HP = -1;
        }
        sel.GetComponent<Cell>().HP=-1;
            
    }

    public void LoadGame()
    {
        Save save = SaveHandler.GetSave(saveid);
        if (save != null)
        {
            width = save.width;
            height = save.height;
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
                        if(save.ids.IndexOf(i * height + j) != -1){
                            //  Debug.Log(pos);
                            GameObject c = GameObject.Instantiate(cell, transform);
                            c.transform.localPosition = pos;
                            
                            c.GetComponent<Cell>().id = save.ids[save.ids.IndexOf(i * height + j)];
                            // Debug.Log("ID" + save.ids[save.ids.IndexOf(i * height + j)]);
                            c.GetComponent<Cell>().CellType = save.cts[save.ids.IndexOf(i * height + j)];
                            c.GetComponent<Cell>().HP = save.hps[save.ids.IndexOf(i * height + j)];
                            cells.Add(c);
                            sums[(int)c.GetComponent<Cell>().CellType]++;

                        }
                    }


                }
            }

            cells.ForEach(x=> x.GetComponent<Cell>().findNeighbours());
            cells.ForEach(x=> x.GetComponent<Cell>().Spread());

            float size = radius * 2.8f;
            float diff = 2 / size;
            transform.localScale = new Vector3(diff, diff, 1);
        }
    }
    
    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
