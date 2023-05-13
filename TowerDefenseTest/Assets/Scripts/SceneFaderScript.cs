using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/*
* This script is used to manage the transitions (fade in and out) between scenes
*
* Works in close relationship with the Main Menu, Pause Menu and Game Over scripts (MainMenuScript.cs, PauseMenuScript.cs and GameOverScript.cs)
*
* Used by GameObjects: SceneFader
*/

public class SceneFaderScript : MonoBehaviour
{
    // Public variables
    public Image img;
    public AnimationCurve fadeCurve;

    // Every scene with this script on a starting game object will fade in by default
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // This method fades to a different scene with the fade out animation playing on the current scene
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    // What this does is, it decreases the alpha of the black image that overlays our UI to create a fade animation
    // It is a coroutine because we want to wait the time between each frame every iteration
    IEnumerator FadeIn()
    {
        // Decrement the fadeTime, which will be the image's alpha channel, on each frame. Wait 1 frame or it all crashes (Why?), hence the coroutine
        float fadeTime = 1f;
        while(fadeTime > 0)
        {
            fadeTime -= Time.deltaTime; // Multiply by 1 for 1 second, multiply by 0.5 for 2 seconds, multiply by 2 for 0,5 seconds, etc.
            float a = fadeCurve.Evaluate(fadeTime); // Apply the curve to the fadeTime values, i. e., fadeTime is the x axis and the output value is the y
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    // Fade out animation with the transition to the new desired scene
    IEnumerator FadeOut(string scene)
    {
        float fadeTime = 0f;
        while(fadeTime < 1)
        {
            fadeTime += Time.deltaTime; // Multiply by 1 for 1 second, multiply by 0.5 for 2 seconds, multiply by 2 for 0,5 seconds, etc.
            float a = fadeCurve.Evaluate(fadeTime); // Apply the curve to the fadeTime values, i. e., fadeTime is the x axis and the output value is the y
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }

}
