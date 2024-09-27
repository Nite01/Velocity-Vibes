using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static int useCoins = 0;
    public static int useScore = 0;
    public static bool isRevived = false;

    public float useSpeed;

    public static Events instance;
    public GameObject revivePanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ReplayGame()
    {
        PlayerManager.isMagnet = false;
        PlayerManager.isScoreStar = false;
        PlayerManager.isRocketed = false;
        PlayerController.forwardSpeed = 10.5f;
        GameData.Coins += PlayerManager.numberOfCoins;
        SceneManager.LoadScene("Level");
    }

    public void BackGame ()
    {
        PlayerManager.isMagnet = false;
        PlayerManager.isScoreStar = false;
        PlayerManager.isRocketed = false;
        SceneManager.LoadScene("MainMenu");
        GameData.Coins += PlayerManager.numberOfCoins;
    }

    public void OnUserRevived()
    {
        isRevived = true;
        useCoins = PlayerManager.numberOfCoins;
        useSpeed = PlayerController.forwardSpeed;
        useScore = PlayerManager.scoreNumber;
        SceneManager.LoadScene("Level");
        PlayerController.forwardSpeed = useSpeed;
    }

    public void OnClickReward()
    {
        PlayerManager.isMagnet = false;
        PlayerManager.isScoreStar = false;
        PlayerManager.isRocketed = false;
        revivePanel.SetActive(true);
        useSpeed = PlayerController.forwardSpeed;
    }

    public void OnConfirmWatchVideo()
    {
        AdManager.instance.ShowRewardedAd();
    }

    public void OnClickCancel()
    {
        revivePanel.SetActive(false);
    }
}
