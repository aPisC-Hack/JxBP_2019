using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BatteryTypes
{
    Empty = 0,
    Low = 1,
    Medium = 2,
    Extreme = 3,
    Epic = 4
}

public class Battery : MonoBehaviour
{
    private BatteryTypes batteryType = BatteryTypes.Empty;
    public Sprite[] sprites;
    public GameObject lazer;

    public BatteryTypes BatteryType {
        get { return batteryType; }
        set {
            if(batteryType != value)
            {
                batteryType = value;
                switch (value)
                {
                    case BatteryTypes.Empty:
                        GetComponent<Image>().sprite = sprites[0];
                        break;
                    case BatteryTypes.Low:
                        GetComponent<Image>().sprite = sprites[1];
                        break;
                    case BatteryTypes.Medium:
                        GetComponent<Image>().sprite = sprites[2];
                        break;
                    case BatteryTypes.Extreme:
                        GetComponent<Image>().sprite = sprites[3];
                        break;
                    case BatteryTypes.Epic:
                        GetComponent<Image>().sprite = sprites[4];
                        break;
                    default:
                        break;
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        BatteryType = (BatteryTypes)lazer.GetComponent<LaserHeadMovement>().IntensityId;
    }
}
