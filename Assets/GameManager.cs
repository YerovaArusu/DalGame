using System;
using System.Collections;
using System.Collections.Generic;
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
    
    [Header("Enemy")]
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public int maxEnemies = 10;
    
    private GameObject[] enemiesOnMap;
    
    
    void Start()
    {
        generateWorldMap();
        generateBorder();
        
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemiesOnMap = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (enemiesOnMap.Length < maxEnemies)
        {
            float spawnX = MathHelper.getRandomFloat(-1 * mapWidth / 2, mapWidth / 2);
            float spawnY = MathHelper.getRandomFloat(-1 * mapHeight / 2, mapHeight / 2);

            GameObject enemy = enemyPrefab;
            Instantiate(enemy, new Vector2(spawnX, spawnY), Quaternion.Euler(0, 0, 0));
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

        for (int xPos = (-1* mapWidth/2); xPos <= mapWidth/2; xPos++)
        {
            for (int yPos = (-1* mapHeight/2); yPos <= mapHeight/2; yPos++)
            {
                float f = generateNoise(xPos, yPos, 8f);

                GameObject defaultWorldObj = defaultWorldTile;
                defaultWorldObj.transform.position = new Vector3(xPos, yPos, 0f);
                Instantiate(defaultWorldObj, mapParent.transform);
                
                
                if (f > 0.7f && Random.Range(0, 100) > 95)
                {
                    GameObject worldObj = getRandomGameObject(worldPrefabs);
                    worldObj.transform.position = new Vector3(xPos, yPos, -0.01f);
                    worldObj.transform.Rotate(Vector3.forward, 90 * Random.Range(0,3), Space.World);

                    
                    

                    Instantiate(worldObj, mapParent.transform);
                    
                    //Does not work... YET
                    Collider2D[] hitColliders = Physics2D.OverlapBoxAll(worldObj.transform.position, transform.localScale / 2, 0f, mapLayer);
                    if (hitColliders.Length > 0)
                    {
                        for (int i = 0; i < hitColliders.Length; i++)
                        {
                            Destroy(hitColliders[i].GameObject());
                        }
                    }
                }
                

                
                
            }
        }
    }

    private float generateNoise(int x, int y, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float yNoise = (y + this.transform.position.y) / detailScale;

        return Mathf.PerlinNoise(xNoise, yNoise);
    }

    public GameObject getRandomGameObject(List<GameObject> prefabs)
    {
        return prefabs[Random.Range(0,prefabs.Count-1)];
    }
    
}
