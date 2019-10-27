using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveHandler
{
    private static readonly int saveCount = 4;
    private static Save[] saves = null;

    public static void LoadSaves(){
        if(saves != null) return;
        saves = new Save[saveCount];
        for(int i = 0; i < saveCount; i++){
            LoadGame(i);
        }
    }

    private static void LoadGame(int index)
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave-"+index+".save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave-"+index+".save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            saves[index] = save;
            file.Close();
        }
    }

    public static Save GetSave(int index){
        if(index < 0 || index >= saveCount) return null;
        return saves[index];
    }

    public static void SaveGame(int index, int width, int height, List<GameObject> cells)
    {
        Save save = CreateSaveGameObject(index, width, height, cells);
        SaveGame(index, save);
    }

    private static void SaveGame(int index, Save save){

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave-"+index+".save");
        bf.Serialize(file, save);
        file.Close();
        saves[index] = save;
        Debug.Log("saved game");
    }

    private static Save CreateSaveGameObject(int index, int width, int height, List<GameObject> cells)
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
        save.SessionId = index;

        return save;
    }

    public static void CreateNewSave(int index, int width, int height){
        Save save = new Save();

        float sin60 = Mathf.Sin(60 * Mathf.Deg2Rad);

        float gw = (width +1.5f) * 2 * sin60;
        float gh = (height+1 )* 0.5f;
        float radius = Mathf.Min(gw, gh) / 2;


        save.height = height;
        save.width = width;
        //##################################x
        save.DayCount = 0;
        save.SessionId = index;
        //Debug.Log(sin60);
        for(int i = 0; i < height; i++){
            for(int j = 0; j < width; j++){
                Vector3 pos = Vector3.zero;
                pos.x += (j-width/2f)*2*sin60;
                if(i%2== 1) pos.x += sin60;
                pos.y = (i-height/2f) * .5f+0.4f;

                if(pos.magnitude <= radius){

                    save.hps.Add(1000);
                    save.ids.Add(i * height + j);
                    save.cts.Add((CellTypes) (j % 3) + 1);
                }

                
            }
        }
        //saves[index] = save;
        SaveGame(index, save); 
    }

    public static void CreateNewSave(int index){
        CreateNewSave(index, 14, 32);
    }


}
