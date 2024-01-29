using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 4f;
    [SerializeField] private float runningSpeed = 12f;
    private GameManager gameManager;
    private float speedX;
    private float speedY;
    private Vector3 iniLocalScale;
    public StatsSystem statsSystem;
    private WeaponHandler weaponHandler;
    private bool facingRight = true;
    private Transform playerSprite;
    void Start()
    {
        statsSystem = this.GetComponent<StatsSystem>();
        iniLocalScale = transform.localScale;
        weaponHandler = transform.Find("Weapon").GetComponent<WeaponHandler>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //Rotate Player Sprite depending on mouse Pos
        //Adapted from: https://discussions.unity.com/t/character-facing-the-position-of-mouse-cursor-2d-platformer-sprite-changing-based-on-mouse-position/220613
        float dx = transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        if (dx >= 0 && !facingRight) { 
            transform.localScale = new Vector3(iniLocalScale.x*-1,iniLocalScale.y,iniLocalScale.z); 
            facingRight = true;
        } else if (dx < 0 && facingRight)
        {
            transform.localScale = iniLocalScale; 
            facingRight = false;
        }
            
            
        //  Stamina/Walking/Running
        speedX = Input.GetAxis("Horizontal");
        speedY = Input.GetAxis("Vertical");
        float actualSpeed = baseSpeed;
        if (statsSystem.hasStaminaRegenerated && Input.GetKey(KeyCode.LeftShift))
        {
            statsSystem.consumeStamina();
            actualSpeed = runningSpeed;
        }
        else statsSystem.isSprinting = false;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(speedX * actualSpeed, speedY * actualSpeed);

        //Attacking
        if (Input.GetMouseButtonDown(0))
        {
            //TODO: DO MORE
            weaponHandler.attack();
        }

    }

    private void FixedUpdate()
    {
        handleCamera();
    }

    //Handles Camera Movement in a way so the player can Absolutely not see whats happening outsie of the map
    public void handleCamera()
    {
        Bounds camerabounds = playerCameraBounds();
        Bounds mapBounds = new Bounds(new Vector3(0, 0,0f), new Vector3(gameManager.mapWidth, gameManager.mapHeight, 0f));
        
        float x, y;
        if (camerabounds.min.x > mapBounds.min.x && camerabounds.max.x < mapBounds.max.x)
            x = gameObject.transform.position.x;
        else x = Camera.main.transform.position.x;
        
        if (camerabounds.min.y > mapBounds.min.y && camerabounds.max.y < mapBounds.max.y)
            y = gameObject.transform.position.y;
        else y = Camera.main.transform.position.y;
        
        Camera.main.transform.position = new Vector3(x, y, -18);
        
    }
    
    public Bounds playerCameraBounds()
    {
        var x = gameObject.transform.position.x;
        var y = gameObject.transform.position.y;
        var size = Camera.main.orthographicSize * 2;
        var height = size;
        var width = height*Camera.main.aspect;
        return new Bounds(new Vector3(x, y, 0), new Vector3(width, height, 0));
    }
    
    
}
