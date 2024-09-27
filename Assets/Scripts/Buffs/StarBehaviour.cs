using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.gotScored = true;
            FindAnyObjectByType<AudioManager>().PlaySound("PickUpCoin");
            Destroy(gameObject);
        }

        if (other.CompareTag("Rocket"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Magnet"))
        {
            Destroy(gameObject);
        }
    }
}
