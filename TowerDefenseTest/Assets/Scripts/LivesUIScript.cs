using UnityEngine;
using UnityEngine.UI;

public class LivesUIScript : MonoBehaviour
{
    public Text livesText;

    // Update is called once per frame
    void Update()
    {
            livesText.text = PlayerStatsScript.Lives.ToString() + " LIVES";
    }
}
