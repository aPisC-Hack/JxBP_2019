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
    private static bool allowClick = true;

    public static void OpenDialogScene(IList<Sprite> sprites, Action then, float timeout = 0, bool clickAllowed = true){
        UnityEngine.SceneManagement.SceneManager.LoadScene("DialogScene");
        DialogHandler dh = GameObject.FindObjectOfType<DialogHandler>();
        crossSceneAfter = then;
        crossSceneSprites = sprites;
        crossSceneTimeout = timeout;
        allowClick = clickAllowed;
    }

    IList<Sprite> sprites;
    Action then;
    bool click;
    float timeout;

    public float Timeout{get{return timeout;}}
    public bool ClickAllowed { get { return click; } }

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

    public void SkipDialogs(){
        if(currentDialog != null){
            Destroy(currentDialog);
            currentDialog = null;
        }
        if(then != null)then();
    }

    // Start is called before the first frame update
    void Start()
    {
        then = crossSceneAfter;
        sprites = crossSceneSprites;
        timeout = crossSceneTimeout;
        click = allowClick;

        crossSceneTimeout = 0;
        crossSceneAfter = null;
        crossSceneSprites = null;
        allowClick = true;
        OpenNextDialog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
