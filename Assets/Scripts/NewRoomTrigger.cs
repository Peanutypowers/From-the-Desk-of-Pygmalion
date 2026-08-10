using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewRoomTrigger : MonoBehaviour
{
    public int rooms = 3;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Doorway"))
                {
                    SceneManager.UnloadSceneAsync(rooms);
                    SceneManager.UnloadSceneAsync(rooms + 1);
                    rooms = rooms + 2;
                    SceneManager.LoadScene(rooms, LoadSceneMode.Additive);
                }
            }
        }
    }
}
