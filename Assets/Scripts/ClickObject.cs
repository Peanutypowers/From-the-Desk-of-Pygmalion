using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;

public class ClickObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            //Debug.Log("Mouse clicked!");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                //Debug.Log("Clicked on: " + hit.collider.name);
            }
        }
    }
}
