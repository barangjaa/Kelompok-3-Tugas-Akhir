using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagement : MonoBehaviour
{
   public void PLayGame()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
    public void Close()
    {
        SceneManager.LoadSceneAsync("Start");
    }
    public void Survival()
    {
        SceneManager.LoadSceneAsync("Survival");
    }
}
