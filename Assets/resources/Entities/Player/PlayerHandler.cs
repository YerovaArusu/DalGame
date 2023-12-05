using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 4f;
    [SerializeField] private float runningSpeed = 12f;
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
    
}
