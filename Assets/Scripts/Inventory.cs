using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;

//[RequireComponent(typeof(Collider2D))]
public class Inventory : MonoBehaviour
{
    [Header("References")]
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] AudioSource audioSource;

    [Header("Prefabs")]
    [SerializeField] GameObject droppedItemPrefab;

    [Header("Audio Clips")]
    [SerializeField] AudioClip pickUpItemClip;
    [SerializeField] AudioClip dropItemClip;

    [Header("State")]
    [SerializeField] SerializedDictionary<string, Item> inventory = new();

    public bool interactingWithPuzzle;
    /*i created this variable, that way we can use the interactingWithPuzzle variable as a way to check if any puzzles are being
     * interacted with at all, though the second more specific variable, interactingWithSlidePuzzle, will allow us to differentiate
     * the slide puzzle from other uzzles, and as other puzzle types get added, we can differentiate them. There may be a more 
     * efficient way to do this, though this is what I can currently think of. -jorge/puggy
     */
    public bool interactingWithSlidePuzzle;

    public void DetectItem(Collider other) // pick up item after clicking on it
    {
        if (other.CompareTag("SlidePuzzleActivator"))
        {
            //swaps the variables, that way it can identify that a puzzle is being interacted with, specifically the slide puzzle
            interactingWithPuzzle = !interactingWithPuzzle;
            interactingWithSlidePuzzle = !interactingWithSlidePuzzle;
        }
        if (other.CompareTag("DroppedItem") && !interactingWithPuzzle)
        {
            var droppedItem = other.GetComponent<DroppedItem>();
            if(droppedItem.pickedUp) return; // make sure the item wasn't already picked up somehow
            Debug.Log($"Detected item: {droppedItem.item.name}");
            AddItem(droppedItem.item);
            Destroy(other.gameObject);
            audioSource.PlayOneShot(pickUpItemClip);
        }
    }

    public void AddItem(Item item)
    {
        var inventoryId = Guid.NewGuid().ToString(); // allows for multiple instances of same item
        // add to dictionary and update UI
        inventory.Add(inventoryId, item);
        inventoryUI.AddUIItem(inventoryId, item);
        Debug.Log($"Added {item.name} to normal inventory with id {inventoryId}");
    }

    public void DropItem(string inventoryId)
    {
        // create a new object with item data
        var droppedItem = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity).GetComponent<DroppedItem>();
        var item = inventory.GetValueOrDefault(inventoryId);
        droppedItem.Initialize(item);
        // remove from dictionary and update UI
        inventory.Remove(inventoryId);
        inventoryUI.RemoveUIItem(inventoryId);
        audioSource.PlayOneShot(dropItemClip);

    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                DetectItem(hit.collider);
                Debug.Log("Clicked on: " + hit.collider.name);
            }
        }
    }
}
