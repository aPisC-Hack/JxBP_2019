using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{

    private static IList<Sprite> crossSceneSprites = null;
    private static Action<int> crossSceneAfter = null;

    public static void OpenScene(IList<Sprite> sprites, Action<int> then){
        UnityEngine.SceneManagement.SceneManager.LoadScene("SliderScene");
        DialogHandler dh = GameObject.FindObjectOfType<DialogHandler>();
        crossSceneAfter = then;
        crossSceneSprites = sprites;
    }
    public GameObject DialogFrame;

    IList<Sprite> sprites;
    Action<int> then;

    float timeout;

    public float Timeout{get{return timeout;}}

    private int spriteId = -1;
    GameObject currentDialog = null;
    public void OpenNextDialog(){
        spriteId = (spriteId + 1) % (sprites?.Count ?? 0);
        if(currentDialog != null){
            Destroy(currentDialog);
            currentDialog = null;
        }
        if(sprites == null){
            if(then != null)then(-1);
            return;
        }

        currentDialog = GameObject.Instantiate(DialogFrame, transform);
        currentDialog.GetComponent<Image>().sprite = sprites[spriteId];

    }

    // Start is called before the first frame update
    void Start()
    {
        sprites = crossSceneSprites;
        then = crossSceneAfter;

        crossSceneAfter = null;
        crossSceneSprites = null;
        OpenNextDialog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCurrentFrame(){
        if(currentDialog != null){
            Destroy(currentDialog);
            currentDialog = null;
        }
        then(spriteId);
    }

    public void OpenPrevDialog(){
        spriteId += sprites.Count- 2;
        OpenNextDialog();
    }

    public void Cancel(){
        then(-1);
    }
}
