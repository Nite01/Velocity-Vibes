using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioManager AD;
    public void PlayGame()
    {
        Destroy(AD);
        SceneManager.LoadScene("Level");
        PlayerController.forwardSpeed = 10.5f;
    }

    public void LootBox()
    {
        SceneManager.LoadScene("LootBox");
    }

    public void Shop()
    {
        SceneManager.LoadScene("SHOP");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void AuthScene()
    {
        SceneManager.LoadScene("GoogleSignIn");
    }

}
