using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] public int mapWidth = 100;
    [SerializeField] public int mapHeight = 100;
    [SerializeField] public GameObject wallPrefab;
    [SerializeField] public List<GameObject> worldPrefabs;
    [SerializeField] public GameObject defaultWorldTile;
    [SerializeField] public LayerMask mapLayer;
    
    [Header("Inventory")] 
    [SerializeField] public GameObject inventoryPrefab;
    
    [Header("Inventory")] 
    [SerializeField] public GameObject playerPrefab;
    
    [Header("Coins")]
    [SerializeField] public GameObject coinPrefab;
    [SerializeField] public int maxCoins = 10;
    
    [Header("Enemy")]
    [SerializeField] public List<GameObject> enemyPrefabs;
    [SerializeField] public int maxEnemies = 10;

    
    
    
    private GameObject[] enemiesOnMap;
    private GameObject[] coinsOnMap;
    
    
    void Start()
    {
        generateWorldMap();
        generateBorder();
        generatePlayer();
        generateInventory();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        generateCoins();
        enemiesOnMap = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (enemiesOnMap.Length < maxEnemies)
        {
            float spawnX = MathHelper.getRandomFloat(-1 * mapWidth / 2, mapWidth / 2);
            float spawnY = MathHelper.getRandomFloat(-1 * mapHeight / 2, mapHeight / 2);

            GameObject enemy = getRandomGameObject(enemyPrefabs);
            Instantiate(enemy, new Vector3(spawnX, spawnY,-8f), Quaternion.Euler(0, 0, 0));
        }
    }

    public void generateBorder()
    {
        GameObject mapParent = Instantiate( new GameObject("WorldBorder"), new Vector3(0,0,0), Quaternion.Euler(0,0,0));

        mapWidth += 2;
        mapHeight += 2;
        
        for (int i = 0; i <= mapWidth; i++)
        {
            GameObject wallPieceAbove = wallPrefab;
            wallPieceAbove.transform.position = new Vector3((-1 * mapWidth / 2) + i, mapHeight / 2, -0.1f);
            Instantiate(wallPieceAbove, mapParent.transform);
            
            GameObject wallPieceDown = wallPrefab;
            wallPieceDown.transform.position = new Vector3((-1 * mapWidth / 2) + i, -1* mapHeight / 2, -0.1f);
            Instantiate(wallPieceDown, mapParent.transform);
        }
        
        for (int i = 0; i <= mapHeight; i++)
        {
            GameObject wallPieceLeft = wallPrefab;
            wallPieceLeft.transform.position = new Vector3((-1 * mapWidth / 2), (-1 * mapHeight / 2) + i, -0.1f);
            Instantiate(wallPieceLeft, mapParent.transform);
            
            GameObject wallPieceRight = wallPrefab;
            wallPieceRight.transform.position = new Vector3((mapWidth / 2), (-1* mapHeight / 2)+i, -0.1f);
            Instantiate(wallPieceRight, mapParent.transform);
        }
    }


   public void generateWorldMap()
    {
        GameObject mapParent = Instantiate( new GameObject("WorldMap"), new Vector3(0,0,0), Quaternion.Euler(0,0,0)) as GameObject;

        for (int xPos = (-1* mapWidth/2); xPos <= mapWidth/2; xPos += (int) (1.5 * defaultWorldTile.transform.localScale.x))
        {
            for (int yPos = (-1* mapHeight/2); yPos <= mapHeight/2; yPos += (int) (1.5 * defaultWorldTile.transform.localScale.y))
            {
                float f = generateNoise(xPos, yPos, 16f);

                GameObject defaultWorldObj = defaultWorldTile;
                defaultWorldObj.transform.position = new Vector3(xPos, yPos, 0f);
                Instantiate(defaultWorldObj, mapParent.transform);
                
                if (f > 0.6f && Random.Range(0, 100) > 90)
                {
                    GameObject worldObj = getRandomGameObject(worldPrefabs);
                    worldObj.transform.position = new Vector3(xPos, yPos, -0.01f);
                    worldObj.transform.Rotate(Vector3.forward, 90 * Random.Range(0,3), Space.World);
                    
                    Collider2D[] hitColliders = Physics2D.OverlapBoxAll(worldObj.transform.position, transform.localScale, 0f, mapLayer);
                    if (hitColliders.Length > 0)
                    {
                        List<GameObject> collidersWithSameName = new List<GameObject>();
                        
                        foreach (Collider2D hitCollider in hitColliders)
                        {
                            if (hitCollider.GameObject().name.Contains(worldObj.name))
                            {
                                collidersWithSameName.Add(hitCollider.GameObject());
                            }
                        }
                        
                        foreach (GameObject o in collidersWithSameName)
                        {
                            Destroy(o);
                        }
                    }
                    
                    Instantiate(worldObj, mapParent.transform);
                }
            }
        }
    }

    private void generateCoins()
    {
        coinsOnMap = GameObject.FindGameObjectsWithTag("Coin");
        List<GameObject> coins = new List<GameObject>(coinsOnMap);
     
        if (coins.Count() < maxCoins)
        {
        for (int xPos = (-1 * mapWidth / 2); xPos <= mapWidth / 2; xPos++)
        {
            for (int yPos = (-1 * mapHeight / 2); yPos <= mapHeight / 2; yPos++)
            {
                float f = generateNoise(xPos, yPos, 5f);
                if (f >= 0.5 && Random.Range(0, 100) > 95 && coins.Count() < maxCoins)
                {
                    GameObject coin = coinPrefab;
                    coin.transform.position = new Vector3(xPos, yPos, -9f);
                    coins.Add(coin);
                    Instantiate(coin);
                }
            }
        }
    }
}
    private void generateInventory()
    {
        GameObject inventory = inventoryPrefab;
        inventory.transform.position = new Vector3(259, 247, 0);
        Instantiate(inventory);
    }
    
    private void generatePlayer()
    {
        GameObject player = playerPrefab;
        player.transform.position = new Vector3(0, 0, -8f);
        Instantiate(player);
    }

    private float generateNoise(int x, int y, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float yNoise = (y + this.transform.position.y) / detailScale;

        return Mathf.PerlinNoise(xNoise, yNoise);
    }

    public GameObject getRandomGameObject(List<GameObject> prefabs)
    {
        return prefabs[Random.Range(0,prefabs.Count)];
    }
    
}
