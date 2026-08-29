using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewRoomTrigger : MonoBehaviour
{
    //public int rooms = 3;
    // Update is called once per frame
    /*public GameObject mouseSphere;
    public GameObject mainCam;
    public GameObject cinemachine;*/
    public GameObject cameraItem;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("DoorOne"))
                {
                    cameraItem.transform.position = new Vector3(-203.0162f, 0, 34.7f);
                }
                if (hit.collider.CompareTag("DoorTwo"))
                {
                    cameraItem.transform.position = new Vector3(0, 0, 215.6f);
                }
                if (hit.collider.CompareTag("DoorThree"))
                {
                    Debug.Log("DoorThree Clicked");
                    cameraItem.transform.position = new Vector3(189.1838f, 0, 34.7f);
                }
                if (hit.collider.CompareTag("DoorFour"))
                {
                    Debug.Log("DoorFour Clicked");
                    cameraItem.transform.position = new Vector3(0, 0, -150.6285f);
                }
                if (hit.collider.CompareTag("Return"))
                {
                    Debug.Log("Return Clicked");
                    cameraItem.transform.position = new Vector3(0, 0, 34.7f);
                }
            }
        }
    }
}
