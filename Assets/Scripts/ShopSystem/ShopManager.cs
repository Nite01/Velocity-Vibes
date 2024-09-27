using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Animator tyreHolder;
    public Text ownedAmountText;
    public Text ownedAmountKey;

    public int currentTyreIndex;
    public GameObject[] playerModels;

    public TyreBluePrint[] tyres;
    public Button buyButtonCoins;
    public Button buyButtonKeys;

    public GameObject buyFromCoinPanel;
    public GameObject buyFromKeyPanel;

    // Start is called before the first frame update
    void Start()
    {
        ownedAmountText.text = "" + GameData.Coins;
        ownedAmountKey.text = "" + GameData.Keys;
        foreach (TyreBluePrint tyre in tyres)
        {
            if(tyre.price == 0)
            {
                tyre.isUnlocked = true;
            }
            else
            {
                tyre.isUnlocked = PlayerPrefs.GetInt(tyre.name, 0) == 0 ? false : true;
            }
        }

        currentTyreIndex = PlayerPrefs.GetInt("SelectedTyre", 0);
        foreach (GameObject tyre in playerModels)
        {
            tyre.SetActive(false);
        }
        playerModels[currentTyreIndex].SetActive(true);
    }

    void Update()
    {
        tyreHolder.Play("ShopAnimation");
        ownedAmountText.text = "" + GameData.Coins;
        ownedAmountKey.text = "" + GameData.Keys;
        UpdateUICoin();
        UpdateUIKey();
    }

    public void changeRight()
    {
        playerModels[currentTyreIndex].SetActive(false);

        currentTyreIndex++;
        if(currentTyreIndex == playerModels.Length)
        {
            currentTyreIndex = 0;
        }

        playerModels[currentTyreIndex].SetActive(true);
        
        TyreBluePrint t = tyres[currentTyreIndex];
        if (!t.isUnlocked)
        {
            return;
        }

        PlayerPrefs.SetInt("SelectedTyre", currentTyreIndex);
    }

    public void changeLeft()
    {
        playerModels[currentTyreIndex].SetActive(false);

        currentTyreIndex--;
        if (currentTyreIndex == -1)
        {
            currentTyreIndex = playerModels.Length - 1;
        }

        playerModels[currentTyreIndex].SetActive(true);

        TyreBluePrint t = tyres[currentTyreIndex];
        if (!t.isUnlocked)
        {
            return;
        }

        PlayerPrefs.SetInt("SelectedTyre", currentTyreIndex);
    }

    public void UnlockTyreUseCoin()
    {
        TyreBluePrint t = tyres[currentTyreIndex];
        PlayerPrefs.SetInt(t.name, 1);
        PlayerPrefs.SetInt("SelectedTyre", currentTyreIndex);
        t.isUnlocked = true;
        GameData.Coins -= t.price;
        buyFromCoinPanel.SetActive(false);
    }

    public void UnlockTyreUseKey()
    {
        TyreBluePrint t = tyres[currentTyreIndex];
        PlayerPrefs.SetInt(t.name, 1);
        PlayerPrefs.SetInt("SelectedTyre", currentTyreIndex);
        t.isUnlocked = true;
        GameData.Keys -= t.keys;
        buyFromKeyPanel.SetActive(false);
    }

    private void UpdateUICoin()
    {
        TyreBluePrint t = tyres[currentTyreIndex];
        if (t.isUnlocked)
        {
            buyButtonCoins.gameObject.SetActive(false);
        }
        else
        {
            buyButtonCoins.gameObject.SetActive(true);
            buyButtonCoins.GetComponentInChildren<Text>().text = "Buy - " + t.price;
            if ( t.price <= GameData.Coins )
            {
                buyButtonCoins.interactable = true;
            }
            else
            {
                buyButtonCoins.interactable = false;
            }
        }
    }

    private void UpdateUIKey()
    {
        TyreBluePrint t = tyres[currentTyreIndex];
        if (t.isUnlocked)
        {
            buyButtonKeys.gameObject.SetActive(false);
        }
        else
        {
            buyButtonKeys.gameObject.SetActive(true);
            buyButtonKeys.GetComponentInChildren<Text>().text = "Buy - " + t.keys;
            if (t.keys <= GameData.Keys)
            {
                buyButtonKeys.interactable = true;
            }
            else
            {
                buyButtonKeys.interactable = false;
            }
        }
    }

    public void BuyCoinPanelActive()
    {
        buyFromCoinPanel.SetActive(true);
    }

    public void BuyCoinPanelDeactive()
    {
        buyFromCoinPanel.SetActive(false);
    }

    public void BuyKeyPanelActive()
    {
        buyFromKeyPanel.SetActive(true);
    }

    public void BuyKeyPanelDeactive()
    {
        buyFromKeyPanel.SetActive(false);
    }
}
