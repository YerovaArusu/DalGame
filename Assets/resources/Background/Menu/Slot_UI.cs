using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot_UI : MonoBehaviour
{
   public Image itemIcon;
   public TextMeshProUGUI quantityText;

   public void SetItem(Inventory.Slot slot)
   {
      Debug.Log(slot.icon+ "," + slot.count  +","+ slot.type);
      if (slot != null)
      {
         Debug.Log("hallo?");
         itemIcon.sprite = slot.icon;
         itemIcon.color = new Color(1, 1, 1, 1);
         quantityText.text = slot.count.ToString();
      }
   }

   public void SetEmpty()
   {
      itemIcon.sprite = null;
      itemIcon.color = new Color(1, 1, 1, 0);
      quantityText.text = "";
   }
}
