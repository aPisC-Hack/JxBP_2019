using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DosisCalculator : MonoBehaviour
{
    public Sprite[] TreatmentFinishedSprites;
    MapGenerator map;
    public float maxDosis;
    public float dosis = 0;

    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<MapGenerator>();
    }

    public void addDosis(float dosis){
        maxDosis = map.width * map.height * 50;
        this.dosis+= dosis;
        if(this.dosis >= maxDosis){
            MapGenerator m = GetComponent<MapGenerator>();
            SaveHandler.SaveGame(MapGenerator.saveid, m.width, m.height, m.cells);
            DialogHandler.OpenDialogScene(TreatmentFinishedSprites, ()=> UnityEngine.SceneManagement.SceneManager.LoadScene("Menu"), 0);
        }
    }

}
