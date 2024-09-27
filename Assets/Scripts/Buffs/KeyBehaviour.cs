using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    public static int keyOwned = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindAnyObjectByType<AudioManager>().PlaySound("PickUpCoin");
            keyOwned += 1;
            Destroy(gameObject);
            GameData.Keys += keyOwned;
        }

        if (other.CompareTag("Star"))
        {
            Destroy(other);
        }

        if (other.CompareTag("Magnet"))
        {
            Destroy(other);
        }

        if (other.CompareTag("Rocket"))
        {
            Destroy(other);
        }
    }
}
