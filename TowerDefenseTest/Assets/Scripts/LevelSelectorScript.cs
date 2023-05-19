using UnityEngine;

/*
* This script is used to select the wanted level to be loaded
*
* Works in close relationship with the scene fader script (SceneFaderScript.cs)
*
* Used by GameObjects: LevelSelectorManager
*/

public class LevelSelectorScript : MonoBehaviour
{
    // Public variables
    public SceneFaderScript sceneFader;
    public string menuScene = "MainMenu";
    
    // Selects the wanted level
    public void SelectLevel(string levelName)
    {
        sceneFader.FadeTo(levelName);
    }

    // Go back to main menu
    public void BackToMenu()
    {
        sceneFader.FadeTo(menuScene);
    }
}
