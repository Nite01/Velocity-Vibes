using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 3;
    private List<GameObject> activeTiles = new List<GameObject>();

    public Transform playerTransform;

    public float[] arraySpawn = {-2.49f, -2.5f, 0, 2.5f};

    public static TileManager instance;

    public GameObject keyPrefab;
    public GameObject magnetPrefab;
    public GameObject starPrefab;
    public GameObject rocketPrefab;

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

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if ( i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
            }
        }
    }

    public void StartRoutines()
    {
        StartCoroutine(SpawneKey());
        StartCoroutine(SpawneMagnet());
        StartCoroutine(SpawneStar());
        StartCoroutine(SpawneRocket());
    }

    void Update()
    {
        if (playerTransform.position.z - 35 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    IEnumerator SpawneKey()
    {
        int keyX = Random.Range(0, 3);
        yield return new WaitForSeconds(Random.Range(60, 180));
        Instantiate(keyPrefab, (transform.forward * zSpawn) + new Vector3(arraySpawn[keyX], 1.7f, 14), Quaternion.Euler(-90, 0, 0));
    }

    IEnumerator SpawneMagnet()
    {
        while (!PlayerManager.gameOver)
        {
            int magnetX = Random.Range(0, 3);
            yield return new WaitForSeconds(Random.Range(30, 90));
            Instantiate(magnetPrefab, (transform.forward * zSpawn) + new Vector3(arraySpawn[magnetX], 1.7f, 14), Quaternion.Euler(-90, 0, 0));
        }
    }

    IEnumerator SpawneStar()
    {
        while (!PlayerManager.gameOver)
        {
            int starX = Random.Range(0, 3);
            yield return new WaitForSeconds(Random.Range(30, 90));
            Instantiate(starPrefab, (transform.forward * zSpawn) + new Vector3(arraySpawn[starX], 1.7f, 14), Quaternion.Euler(-90, 0, 0));
        }
    }

    IEnumerator SpawneRocket()
    {
        while (!PlayerManager.gameOver)
        {
            int rocketX = Random.Range(0, 3);
            yield return new WaitForSeconds(Random.Range(120, 180));
            Instantiate(rocketPrefab, (transform.forward * zSpawn) + new Vector3(arraySpawn[rocketX], 1.7f, 14), Quaternion.Euler(-90, 0, 0));
        }
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
