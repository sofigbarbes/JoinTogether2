using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    private enum GameState {MainMenu, Playing, Ended, Paused};
    private GameState gameState;
    [SerializeField]
    private GameObject MenuScreen;
    [SerializeField]
    private GameObject Level1Screen;
    [SerializeField]
    private GameObject DiedScreen;
    [SerializeField]
    private GameObject WinScreen;    
    [SerializeField]
    private GameObject RulesScreen;
    [SerializeField]
    private GameObject PauseScreen;
    
    
    private GameObject level1;
    
    
    private void Awake()
    {
        PlayerController.Died += Died;
        PlayerController.Win += Win;
    }

    public void Play()
    {
        Time.timeScale = 1f;
        MenuScreen.SetActive( false );
        DiedScreen.SetActive( false );
        WinScreen.SetActive( false );
        PauseScreen.SetActive( false );
        RulesScreen.SetActive( false );
        OpenLevel();

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
       

    }
    private void OpenLevel(){
        if(level1 == null)
        {
            level1 = Instantiate(Level1Screen);
        }
        else{
            Destroy(level1);
            level1 = Instantiate(Level1Screen);
        }
        level1.SetActive(true);
    }
    private void Died()
    {
        Destroy(level1);
        level1.SetActive(false);
        DiedScreen.SetActive(true);
    }
    public void Quit()
    {
       Application.Quit();
    }

    public void Rules(){
        MenuScreen.SetActive( false );
        RulesScreen.SetActive( true );
    }

    public void BackToMenu()
    {
        MenuScreen.SetActive( true );
        Level1Screen.SetActive( false );
        DiedScreen.SetActive( false );
        WinScreen.SetActive( false );
        PauseScreen.SetActive( false );
        RulesScreen.SetActive( false );
    }

    private void Win(){
        Destroy(level1);
        Level1Screen.SetActive( false );
        WinScreen.SetActive( true );
    }

}
