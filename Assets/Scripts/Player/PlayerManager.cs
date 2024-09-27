using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public GameObject pausePanel;
    public GameObject pauseButton;

    public static bool isGameStarted;
    public GameObject startingText;

    public static int numberOfCoins;

    public Text CoinsText;

    public Text scoreText;
    public static int scoreNumber = 0;

    public static int ownedCoins;
    public Text highScoreText;

    public GameObject movePanel;

    public static PlayerManager instance;

    public float useSpeed;

    public Text MagnetText, RocketText, StarText;

    public static int ownMagnet = 0;
    public static int ownRocket = 0;
    public static int ownStar = 0;

    public Sprite[] boostArray;
    public Image boostImage;

    public GameObject MagnetBtn;
    public GameObject RocketBtn;
    public GameObject StarBtn;

    public static bool gotActivated = false;
    public static bool gotScored = false;
    public static bool gotRocket = false;

    public static bool isMagnet = false;
    public static bool isScoreStar = false;
    public static bool isRocketed = false;

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
        MagnetBtn.SetActive(false);
        RocketBtn.SetActive(false);
        StarBtn.SetActive(false);

        boostImage.sprite = boostArray[0];
    }

    void Start()
    {
        ownMagnet = PlayerPrefs.GetInt("ownMagnet", 0);
        ownRocket = PlayerPrefs.GetInt("ownRocket", 0);
        ownStar = PlayerPrefs.GetInt("ownStar", 0);
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoins = 0;
        highScoreText.text = "HighScore :" + PlayerPrefs.GetInt("HighScore", 0);
        ownedCoins = PlayerPrefs.GetInt("OwnedCoins", 0);
        AdManager.instance.RequestInterstitial();
        if(ownMagnet > 0)
        {
            MagnetBtn.SetActive(true);
            MagnetText.text = ownMagnet.ToString();
        }
        if(ownRocket > 0)
        {
            RocketBtn.SetActive(true);
            RocketText.text = ownRocket.ToString();
        }
        if(ownStar > 0)
        {
            StarBtn.SetActive(true);
            StarText.text = ownStar.ToString();
        }
    }


    void Update()
    {
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            PlayerPrefs.SetInt("OwnedCoins", ownedCoins);
            if(scoreNumber > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", scoreNumber);
                highScoreText.text = "HighScore :" + scoreNumber;
            }
            if(Random.Range(0, 3) == 0)
            {
                AdManager.instance.ShowInterstitialAd();
            }
        }

        if (gotActivated)
        {
            StartCoroutine(Magnetised());
            gotActivated = false;
        }

        if (gotScored)
        {
            StartCoroutine(Scoredoubled());
            gotScored = false;
        }

        if (gotRocket)
        {
            StartCoroutine(Invincible());
            gotRocket = false;
        }

        CoinsText.text = "Coins : " + numberOfCoins;
        scoreText.text = "Score : " + scoreNumber;

        if(scoreNumber == 5)
        {
            MagnetBtn.SetActive(false);
            RocketBtn.SetActive(false);
            StarBtn.SetActive(false);
        }

        if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            Destroy(startingText);
            movePanel.SetActive(false);
            StartCoroutine(ScoreManager());
            TileManager.instance.StartRoutines();
            if (Events.isRevived)
            {
                Events.isRevived = false;
                numberOfCoins = Events.useCoins;
                scoreNumber = Events.useScore;
                PlayerController.forwardSpeed = 12;
            }
        }
    }

    public void MagnetBtnPressed()
    {
        gotActivated = true;
        GameData.Magnets -= 1;
        MagnetBtn.SetActive(false);
        RocketBtn.SetActive(false);
        StarBtn.SetActive(false);
    }

    public void RocketBtnPressed()
    {
        gotRocket = true;
        GameData.Rockets -= 1;
        MagnetBtn.SetActive(false);
        RocketBtn.SetActive(false);
        StarBtn.SetActive(false);
    }

    public void StarBtnPressed()
    {
        gotScored = true;
        GameData.Stars -= 1;
        MagnetBtn.SetActive(false);
        RocketBtn.SetActive(false);
        StarBtn.SetActive(false);
    }

    IEnumerator Magnetised()
    {
        isMagnet = true;
        boostImage.sprite = boostArray[1];
        yield return new WaitForSeconds(10f);
        boostImage.sprite = boostArray[0];
        isMagnet = false;
    }

    IEnumerator Scoredoubled()
    {
        isScoreStar = true;
        boostImage.sprite = boostArray[3];
        yield return new WaitForSeconds(10f);
        boostImage.sprite = boostArray[0];
        isScoreStar = false;
    }

    IEnumerator Invincible()
    {
        isRocketed = true;
        boostImage.sprite = boostArray[2];
        useSpeed = PlayerController.forwardSpeed;
        PlayerController.forwardSpeed = 18f;
        yield return new WaitForSeconds(10f);
        PlayerController.forwardSpeed = useSpeed;
        boostImage.sprite = boostArray[0];
        isRocketed = false;
    }

    IEnumerator ScoreManager()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(1f);
            scoreNumber += 1;
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

}
