using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour
{
    public void level1()
    {
        SceneManager.LoadScene("Main");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
