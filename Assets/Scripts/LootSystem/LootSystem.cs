using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemToSpawn
{
    public string name;
    public GameObject item;
    public float spawnRate;
    [HideInInspector] public float minSpawnProb, maxSpawnProb;
}

public class LootSystem : MonoBehaviour
{
    public Animator boxAnimator;
    public Button BuyButton;
    public Text OwnCoin;
    public Text uGot;
    public ItemToSpawn[] itemToSpawn;
    public GameObject send;
    
    void Start()
    {
        uGot.text = "";
        OwnCoin.text = GameData.Coins.ToString();
        for (int i = 0; i < itemToSpawn.Length; i++)
        {

            if (i == 0)
            {
                itemToSpawn[i].minSpawnProb = 0;
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].spawnRate - 1;
            }
            else
            {
                itemToSpawn[i].minSpawnProb = itemToSpawn[i - 1].maxSpawnProb + 1;
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].minSpawnProb + itemToSpawn[i].spawnRate - 1;
            }
        }
    }

   
    void Update()
    {
        OwnCoin.text = GameData.Coins.ToString();
        if (GameData.Coins >= 500)
        {
            BuyButton.interactable = true;
        }
        else
        {
            BuyButton.interactable = false;
        }
    }

    public void Shop()
    {
        SceneManager.LoadScene("SHOP");
    }

    public void Buttonpress()
    {
        boxAnimator.Play("IdleBox");
        Spawnner();
        GameData.Coins -= 500;
    }

   public void Spawnner()
    {
        Destroy(send);
        boxAnimator.Play("BoxOpen");
        float randomNum = Random.Range(0, 100);//56
        Vector3 spawnpoint = new Vector3(0, 1, -3);

        for (int i = 0; i < itemToSpawn.Length; i++)
        {
            if(randomNum>=itemToSpawn[i].minSpawnProb && randomNum<= itemToSpawn[i].maxSpawnProb)
            {
                string name = itemToSpawn[i].name;
                send = Instantiate(itemToSpawn[i].item, spawnpoint, Quaternion.Euler(new Vector3(-90, 0, 0)));
                StartCoroutine(ChangeData(name));
                boxAnimator.Play("IdleBox");
                break;
            }
        }
    }

    public IEnumerator ChangeData(string name)
    {
        if (name == "Key")
        {
            GameData.Keys += 1;
            uGot.text = "You Got - 1";
            yield return new WaitForSeconds(5f);
            uGot.text = "";
        }
        else if (name == "Magnet")
        {
            GameData.Magnets += 1;
            uGot.text = "You Got - 1";
            yield return new WaitForSeconds(5f);
            uGot.text = "";
        }
        else if (name == "Rocket")
        {
            GameData.Rockets += 1;
            uGot.text = "You Got - 1";
            yield return new WaitForSeconds(5f);
            uGot.text = "";
        }
        else if (name == "Star")
        {
            GameData.Stars += 1;
            uGot.text = "You Got - 1";
            yield return new WaitForSeconds(5f);
            uGot.text = "";
        }
    }
    
}
