using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerArrowHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject pointerArrow;
    private Vector3 mouseVec;
    void Start()
    {
        pointerArrow = transform.Find("Pointer").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
                
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        pointerArrow.transform.rotation = Quaternion.FromToRotation(Vector3.up,direction);

    }
    
    
}
