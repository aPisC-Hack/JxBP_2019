using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScaler : MonoBehaviour
{

    public RectTransform TopPanel, BottomPanel, LeftPanel, RightPanel, MainPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rs = MainPanel.GetComponent<RectTransform>().sizeDelta;
        float cw = Screen.width , ch = Screen.height;
        float sizediff = (rs.y / rs.x) / (ch / cw);

        if(sizediff > 1){
            // required size is taller than the current
            // set the right and left padding
            float offset = (1-1/sizediff) / 2 * cw;
            float scale  = ch/rs.y;

            TopPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            BottomPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            LeftPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(offset, 0);
            RightPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(offset, 0);

            MainPanel.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1);
            //MainPanel.GetComponent<RectTransform>().position = new Vector3(offset, 0, 0);
        }
        else{
            // required size is wider then the current
            // set the top and bottom padding
            float offset = (1-sizediff) / 2 * ch;
            float scale = cw/rs.x;

            TopPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, offset);
            BottomPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, offset);
            LeftPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            RightPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

            MainPanel.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1);
            //MainPanel.GetComponent<RectTransform>().position = new Vector3(0, offset, 0);

        }



    }
}
