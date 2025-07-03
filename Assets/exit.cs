using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();
    }
}
