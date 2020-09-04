using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWon : MonoBehaviour
{

    public GameObject UIPLayerScore;
    public void ResumeButton()
    {
        
        Time.timeScale = 1f;
        UIPLayerScore.SetActive(false);

    }
    
    public void QuitButton()
    {
        
        SceneManager.LoadScene(0);
    }
}


