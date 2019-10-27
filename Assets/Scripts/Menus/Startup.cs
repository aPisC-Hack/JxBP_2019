using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Startup: MonoBehaviour
{
    public Sprite[] WelcomeSprites;
    void Start()
    {
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);

            }
        #endif

        SaveHandler.LoadSaves();
        DialogHandler.OpenDialogScene(WelcomeSprites, ()=> UnityEngine.SceneManagement.SceneManager.LoadScene("Menu"), 1500);
        //SliderHandler.OpenScene(WelcomeSprites, (x)=> UnityEngine.SceneManagement.SceneManager.LoadScene("Menu"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
