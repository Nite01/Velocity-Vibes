using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(50 * Time.deltaTime, 0, 0);
        if (PlayerManager.isMagnet)
        {
            Transform target = GameObject.FindGameObjectWithTag("Player").transform;
            transform.position = Vector3.Lerp(transform.position, target.position, 12 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PlayerManager.isScoreStar)
        {
            FindAnyObjectByType<AudioManager>().PlaySound("PickUpCoin");
            PlayerManager.numberOfCoins += 1;
            Destroy(gameObject);
        }

        if (other.CompareTag("Player") && PlayerManager.isScoreStar)
        {
            FindAnyObjectByType<AudioManager>().PlaySound("PickUpCoin");
            PlayerManager.numberOfCoins += 2;
            Destroy(gameObject);
        }
    }

}
