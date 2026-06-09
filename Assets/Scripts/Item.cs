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
            Debug.Log("Mouse clicked Item.cs!");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                Debug.Log("Clicked on: " + hit.collider.name);
                inventoryManager.AddItem(itemName, quantity, sprite);
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
