using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectible_System : MonoBehaviour
{
   private GameObject Player;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.GameObject() == Player.GameObject())
      {
         Destroy(this.GameObject());
      }
   }
}
