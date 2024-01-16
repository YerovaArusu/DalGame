using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Collectible_System : MonoBehaviour
{
   private StatsSystem statsSystem;
   public GameObject Player;
   public Collectible_Type type;

   private void Awake()
   {
   Player = GameObject.FindGameObjectWithTag("Player");
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject == Player.gameObject)
      {
         Destroy(gameObject); // Destroy the current GameObject
         statsSystem = Player.GetComponent<StatsSystem>();
         statsSystem.inventory.Add(type);
      }
   }
}

public enum Collectible_Type
{
   NONE, COIN
}
