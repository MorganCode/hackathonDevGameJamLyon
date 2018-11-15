using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private static bool MAIN_MENU_DEBUG = false;

    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("MainMenuMusic") != null)
            DontDestroyOnLoad(GameObject.Find("MainMenuMusic"));

        if (MAIN_MENU_DEBUG)
            SceneLoader.StaticLoadScene("ControllerSelectionScene");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadIntroMenus()
    {
        SceneLoader.StaticLoadScene("IntroMenu");
    }
    public void LoadMenusExplications()
    {
        SceneLoader.StaticLoadScene("MenuRegles");
    }
    public void LoadControllerScene()
    {
        SceneLoader.StaticLoadScene("ControllerSelectionScene");
    }
    public void LoadMenus()
    {
        SceneLoader.StaticLoadScene("Menus");
    }
    public void LoadCredits()
    {
        SceneLoader.StaticLoadScene("Credits");
    }
    public void QuitGame()
    {
       #if UNITY_EDITOR 
              UnityEditor.EditorApplication.isPlaying = false;
       
        #else
              Application.Quit();

        #endif
    }
   
}
