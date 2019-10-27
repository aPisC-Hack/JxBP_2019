using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Cella
{
    public CellTypes type;
    public float HP;
    public int Id;//sorszám

}

[System.Serializable]
public class Save
{
    public int height = 0;
    public int width = 0;
    public int SessionId = 0;//hányas pálya
    public int DayCount = 0;//eltelt napok száma

    public List<int> ids = new List<int>();
    public List<float> hps = new List<float>();
    public List<CellTypes> cts = new List<CellTypes>();
}
