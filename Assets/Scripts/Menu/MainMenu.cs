using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame(int nbPlayers)
    {
        MenuStaticClass.NumberOfPlayers = nbPlayers;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // This line because Application.Quit() is not going to shutdown the game inside the Unity editor
        // at least we can see in the logs that it's working
        Debug.Log("Quit");
        Application.Quit();
    }
}
