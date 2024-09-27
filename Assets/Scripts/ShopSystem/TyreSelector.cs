using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyreSelector : MonoBehaviour
{
    public GameObject[] tyres;
    public int currentTyreIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentTyreIndex = PlayerPrefs.GetInt("SelectedTyre", 0);
        foreach (GameObject tyre in tyres)
        {
            tyre.SetActive(false);
        }
        tyres[currentTyreIndex].SetActive(true);
    }

}
