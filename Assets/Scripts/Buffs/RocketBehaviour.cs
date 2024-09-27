using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.gotRocket = true;
            FindAnyObjectByType<AudioManager>().PlaySound("PickUpCoin");
            Destroy(gameObject);
        }

        if (other.CompareTag("Key"))
        {
            Destroy(gameObject);
        }
    }
}
