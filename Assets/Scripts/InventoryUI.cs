using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public DroppedItem testStart;


    [Header("Prefabs")]
    [SerializeField] GameObject uiItemPrefab;

    [Header("References")]
    [SerializeField] Inventory inventory;
    [SerializeField] Transform uiInventoryParent;

    [Header("State")]
    [SerializeField] SerializedDictionary<string, GameObject> inventoryUI = new();

    void Start() {
        inventory.AddItem(testStart.item); // testing
        //AddUIItem("test", testStart.item); // testing for just UI
    }

    public void AddUIItem(string inventoryId, Item item) // add new item to ui and initialize it
    {
        var itemUI = Instantiate(uiItemPrefab).GetComponent<ItemUI>();
        itemUI.transform.SetParent(uiInventoryParent);
        inventoryUI.Add(inventoryId, itemUI.gameObject);
        itemUI.Initialize(inventoryId, item, inventory.DropItem);
        Debug.Log($"Added {item.name} to inventory UI with id {inventoryId}");
    }

    public void RemoveUIItem(string inventoryId)
    {
        var itemUI = inventoryUI.GetValueOrDefault(inventoryId);
        Debug.Log($"Removed {itemUI.name} from inventory UI with id {inventoryId}");

        inventoryUI.Remove(inventoryId);
        Destroy(itemUI);
    }
}
