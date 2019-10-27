using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Sprite[] exitSprites;
    public Sprite[] newGameSprites;
    public Sprite[] characterSprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SliderHandler.OpenScene(characterSprites, (i)=>{
            if(i != -1){
                SaveHandler.CreateNewSave(i);
                MapGenerator.saveid = i;
                DialogHandler.OpenDialogScene(newGameSprites, ()=> UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"), 0);
            }
            else UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        });
    }

    public void Continue(){
        List<Sprite> sp = new List<Sprite>();
        for(int i = 0; i < characterSprites.Length; i++){
            if(SaveHandler.GetSave(i) !=  null){
                sp.Add(characterSprites[i]);
            }
        }

        SliderHandler.OpenScene(sp, (i)=>{
            if(i != -1){
                MapGenerator.saveid = i;
                //DialogHandler.OpenDialogScene(newGameSprites, ()=> UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"), 0);
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
            }
            else UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        });
    }

    public void ExitGame(){
        DialogHandler.OpenDialogScene(exitSprites, ()=> Application.Quit(), 1000);
    }

    public void Settings(){
    }
}
