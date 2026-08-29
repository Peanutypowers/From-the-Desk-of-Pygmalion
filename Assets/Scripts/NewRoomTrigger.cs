using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewRoomTrigger : MonoBehaviour
{
    //public int rooms = 3;
    // Update is called once per frame
    public GameObject mouseSphere;
    public GameObject mainCam;
    public GameObject cinemachine;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("DoorOne"))
                {
                    //SceneManager.UnloadSceneAsync(0);
                    //SceneManager.UnloadSceneAsync(rooms + 1);
                    //rooms = rooms + 2;
                    //SceneManager.LoadScene(2, LoadSceneMode.Additive);
                }
                if (hit.collider.CompareTag("DoorThree"))
                {
                    Debug.Log("DoorThree Clicked");
                    mouseSphere.transform.position = new Vector3(199f, 20.87f, -28.36f);
                    mainCam.transform.position = new Vector3(199f, 21.78f, -38.36f);
                    cinemachine.transform.position = new Vector3(199f, 21.78f, -38.36f);
                    //SceneManager.UnloadSceneAsync(0);
                    //SceneManager.UnloadSceneAsync(rooms + 1);
                    //rooms = rooms + 2;
                    //SceneManager.LoadScene(2, LoadSceneMode.Additive);

                }
            }
        }
    }
}
