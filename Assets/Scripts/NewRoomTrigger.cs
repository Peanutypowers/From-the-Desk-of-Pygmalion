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
    //public GameObject mouseSphere;
    //public CameraController scriptCam;

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
                    cameraItem.transform.GetChild(0).GetComponent<CameraController>().calculateBounds(cameraItem.transform.GetChild(0).transform.position);
                }
                if (hit.collider.CompareTag("DoorTwo"))
                {
                    cameraItem.transform.position = new Vector3(0, 0, 215.6f);
                    cameraItem.transform.GetChild(0).GetComponent<CameraController>().calculateBounds(cameraItem.transform.GetChild(0).transform.position);
                }
                if (hit.collider.CompareTag("DoorThree"))
                {
                    Debug.Log("DoorThree Clicked");
                    cameraItem.transform.position = new Vector3(189.1838f, 0, 34.7f);
                    cameraItem.transform.GetChild(0).GetComponent<CameraController>().calculateBounds(cameraItem.transform.GetChild(0).transform.position);
                }
                if (hit.collider.CompareTag("DoorFour"))
                {
                    Debug.Log("DoorFour Clicked");
                    cameraItem.transform.position = new Vector3(0, 0, -150.6285f);
                    cameraItem.transform.GetChild(0).GetComponent<CameraController>().calculateBounds(cameraItem.transform.GetChild(0).transform.position);
                }
                if (hit.collider.CompareTag("Return"))
                {
                    Debug.Log("Return Clicked");
                    cameraItem.transform.position = new Vector3(0, 0, 34.7f);
                    cameraItem.transform.GetChild(0).GetComponent<CameraController>().calculateBounds(cameraItem.transform.GetChild(0).transform.position);
                }
            }
        }
    }
}
