using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Sprite[] exitSprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame(){
        DialogHandler.OpenDialogScene(exitSprites, ()=> Application.Quit(), 1000);
    }
}
