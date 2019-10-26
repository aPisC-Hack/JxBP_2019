using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogHandler : MonoBehaviour
{
    public GameObject DialogFrame;

    private static IList<Sprite> crossSceneSprites = null;
    private static Action crossSceneAfter = null;
    private static float crossSceneTimeout = 0;

    public static void OpenDialogScene(IList<Sprite> sprites, Action then, float timeout = 0){
        UnityEngine.SceneManagement.SceneManager.LoadScene("DialogScene");
        DialogHandler dh = GameObject.FindObjectOfType<DialogHandler>();
        crossSceneAfter = then;
        crossSceneSprites = sprites;
        crossSceneTimeout = timeout;
    }

    IList<Sprite> sprites;
    Action then;

    float timeout;

    public float Timeout{get{return timeout;}}

    private int spriteId = -1;
    GameObject currentDialog = null;
    public void OpenNextDialog(){
        spriteId += 1;
        if(currentDialog != null){
            Destroy(currentDialog);
            currentDialog = null;
        }
        if(sprites == null || spriteId >= sprites.Count){
            if(then != null)then();
            return;
        }

        currentDialog = GameObject.Instantiate(DialogFrame, transform);
        currentDialog.GetComponent<Image>().sprite = sprites[spriteId];

    }

    // Start is called before the first frame update
    void Start()
    {
        then = crossSceneAfter;
        sprites = crossSceneSprites;
        timeout = crossSceneTimeout;

        crossSceneTimeout = 0;
        crossSceneAfter = null;
        crossSceneSprites = null;
        OpenNextDialog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
