using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public Sprite[] WelcomeSprites;
    void Start()
    {
        DialogHandler.OpenDialogScene(WelcomeSprites, ()=> UnityEngine.SceneManagement.SceneManager.LoadScene("Menu"), 1500);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
