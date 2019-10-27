using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    public Sprite[] HelpersSprites;
    public Sprite[] GameplaySprites;
    public Sprite[] TreatmentSprites;
    public Sprite[] ControlsSprites;


    public void Helpers(){
        DialogHandler.OpenDialogScene(HelpersSprites, ()=> UnityEngine.SceneManagement.SceneManager.LoadScene("TutorialMenu"), 0);
    }

    public void Gameplay(){
        
        DialogHandler.OpenDialogScene(GameplaySprites, ()=> UnityEngine.SceneManagement.SceneManager.LoadScene("TutorialMenu"), 0);
    }

    public void Treatment(){
        
        DialogHandler.OpenDialogScene(TreatmentSprites, ()=> UnityEngine.SceneManagement.SceneManager.LoadScene("TutorialMenu"), 0);
    }

    public void Controls(){
        
        DialogHandler.OpenDialogScene(ControlsSprites, ()=> UnityEngine.SceneManagement.SceneManager.LoadScene("TutorialMenu"), 0);
    }

    public void Back(){

         UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
