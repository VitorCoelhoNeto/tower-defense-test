using UnityEngine;
using UnityEngine.UI;

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
    public Button[] levelButtons;

    // On the level selector menu, get the current game progress and disable the levels that still aren't unlocked
    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for(int i = (levelReached); i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
    }
    
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
