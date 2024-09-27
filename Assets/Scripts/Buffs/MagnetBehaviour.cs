using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.gotActivated = true;
            FindAnyObjectByType<AudioManager>().PlaySound("PickUpCoin");
            Destroy(gameObject);
        }

        if (other.CompareTag("Rocket"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Key"))
        {
            Destroy(gameObject);
        }
    }

}
