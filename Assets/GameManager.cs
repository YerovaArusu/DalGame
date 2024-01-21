using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] public int mapWidth = 100;
    [SerializeField] public int mapHeight = 100;
    [SerializeField] public GameObject wallPrefab;
    
    [Header("Enemy")]
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public int maxEnemies = 10;
    
    private GameObject[] enemiesOnMap;
    
    
    void Start()
    {
        //generateWorldMap();
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
            wallPieceAbove.transform.position = new Vector3((-1 * mapWidth / 2) + i, mapHeight / 2, 0f);
            Instantiate(wallPieceAbove, mapParent.transform);
            
            GameObject wallPieceDown = wallPrefab;
            wallPieceDown.transform.position = new Vector3((-1 * mapWidth / 2) + i, -1* mapHeight / 2, 0f);
            Instantiate(wallPieceDown, mapParent.transform);
        }
        
        for (int i = 0; i <= mapHeight; i++)
        {
            GameObject wallPieceLeft = wallPrefab;
            wallPieceLeft.transform.position = new Vector3((-1 * mapWidth / 2), (-1 * mapHeight / 2) + i, 0f);
            Instantiate(wallPieceLeft, mapParent.transform);
            
            GameObject wallPieceRight = wallPrefab;
            wallPieceRight.transform.position = new Vector3((mapWidth / 2), (-1* mapHeight / 2)+i, 0f);
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
                Vector3 pos = new Vector3(xPos, yPos, generateNoise(xPos, yPos, 8f));
                GameObject block = Instantiate(wallPrefab, pos, Quaternion.identity) as GameObject;
                
                Destroy(block.GetComponent<Collider2D>());
                block.transform.SetParent(mapParent.transform);
            }
        }
    }

    private float generateNoise(int x, int y, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float yNoise = (y + this.transform.position.y) / detailScale;

        return Mathf.PerlinNoise(xNoise, yNoise);
    }
    
}
