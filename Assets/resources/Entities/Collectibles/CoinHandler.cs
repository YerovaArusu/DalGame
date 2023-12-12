using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class CoinHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Player;
    void Start()
    {
      Player = GameObject.FindGameObjectWithTag("Player");  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject() == Player.GameObject())
        {
            StatsSystem pStats = Player.GetComponent<StatsSystem>();
            if(pStats.canAddCoins(1)) {
                pStats.addCoin();
                Destroy(this.GameObject());
            }
        }
    }
    
}
