using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Inventory
{
   [System.Serializable]
   public class Slot
   {
   public Collectible_Type type;
   public int count;
   public int maxAllowed;
   public Sprite icon; 

   public Slot()
   {
      type = Collectible_Type.NONE;
      count = 0;
      maxAllowed = 99;
   }

   public bool CanAddItem()
   {
      if (count < maxAllowed)
      {
         return true;
      }

      return false;
   }

   public void AddItem(Collectible_System item)
   {
      this.type = item.type;
      this.icon = item.icon; 
      count++;
   }
}

   public List<Slot> slots = new List<Slot>();

   public Inventory(int numSlots)
   {
      for (int i = 0; i < numSlots; i++)
      {
         Slot slot = new Slot();
         slots.Add(slot);
      }
     
      
   }

   public void Add(Collectible_System item)
   {
      foreach (Slot slot in slots)
      {
         if (slot.type == item.type && slot.CanAddItem())
         {
            slot.AddItem(item);
            return;
         }
      }
      foreach (Slot slot in slots)
      {
         if (slot.type == Collectible_Type.NONE)
         {
            slot.AddItem(item);
            return;
         }
      }
   }

}
 