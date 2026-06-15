using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    private InventoryManager inventoryManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                if (hit.collider.gameObject == gameObject)
                {
                    DetectItem(hit.collider);
                    Debug.Log("Clicked on: " + hit.collider.name);   
                }
            }
        }
    }

    public void DetectItem(Collider other) // pick up item after clicking on it
    {
        if(other.gameObject.tag == "Items")
        {
            Debug.Log("Test Items");
            inventoryManager.AddItem(itemName, quantity, sprite);
            Destroy(other.gameObject);
        }
    }
}
