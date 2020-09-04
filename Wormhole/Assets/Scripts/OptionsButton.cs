using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButton : MonoBehaviour
{
    public GameObject OptionsScreen;
    private bool isPaused;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ToggleOptions);
    }

    public void ToggleOptions()
    {
        
        if (!isPaused)
        {
            OptionsScreen.SetActive(true);
            isPaused = true;
            // actually pause the game
            Time.timeScale = 0f;
        }
        else
        {
            OptionsScreen.SetActive(false);
            isPaused = false;

            // reset the screen freeze
            Time.timeScale = 1f;

        }

    }
}
