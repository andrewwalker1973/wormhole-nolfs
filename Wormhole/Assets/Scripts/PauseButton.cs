using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{

    public GameObject  pauseScreen;
    private bool isPaused;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(TogglePause);
    }

    public void TogglePause()
    {
       
        if (!isPaused)
        {
            pauseScreen.SetActive(true);
            isPaused = true;
            // actually pause the game
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            isPaused = false;

            // reset the screen freeze
            Time.timeScale = 1f;

        }

    }

    
}
