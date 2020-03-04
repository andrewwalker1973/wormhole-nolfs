using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWon : MonoBehaviour
{

    public GameObject UIPLayerScore;
    public void ResumeButton()
    {
        Debug.Log("ResumeButton pressed");
        Time.timeScale = 1f;
        UIPLayerScore.SetActive(false);

    }
    
    public void QuitButton()
    {
        Debug.Log("Quit Button Pressed");
        SceneManager.LoadScene(0);
    }
}


