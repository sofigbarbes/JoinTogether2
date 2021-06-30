using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

[SerializeField]
private GameObject PauseMenuUI;
public static bool GameIsPaused = false;
    void Update()
    {
        if(Input.GetKeyDown( KeyCode.Escape ))
        {
            Debug.Log( "Pausing" );
            if(GameIsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }
    void Resume()
    {
        PauseMenuUI.SetActive( false );
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    
    void Pause()
    {
        PauseMenuUI.SetActive( true );
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    
}
