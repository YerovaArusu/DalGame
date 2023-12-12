using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicEnemyHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 3f;
    [SerializeField] private float maxDistanceToPlayerWithoutReaction = 4f;
    private GameObject player;
    private int startTime;
    private Vector2 targetPos;
    private float distanceToPlayer;
    private Rigidbody2D rb;
    private StatsSystem enemySystem;
    private StatsSystem playerSystem;
    
    void Start()
    {
        startTime = Time.frameCount;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        targetPos = Random.insideUnitCircle * 2.5f;
        enemySystem = GetComponent<StatsSystem>();
        playerSystem = player.GetComponent<StatsSystem>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            playerSystem.receiveDamage(20,this.gameObject);
            enemySystem.receiveDamage(5,this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = rb.position - (Vector2)targetPos;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        if (distanceToPlayer < maxDistanceToPlayerWithoutReaction)
        {
            rb.angularVelocity = -2 * rotateAmount;
            rb.velocity = -direction * speed;
        }

        if ((Time.frameCount - startTime) % 20 == 0)
        {
            distanceToPlayer = Vector2.Distance(player.transform.position,
                transform.position);
        }

        
        if (distanceToPlayer < maxDistanceToPlayerWithoutReaction)
        {
                targetPos = player.transform.position;
        }
        else
        {
            targetPos = Random.insideUnitCircle.normalized * 2.5f;    
        }
        
    }


}
