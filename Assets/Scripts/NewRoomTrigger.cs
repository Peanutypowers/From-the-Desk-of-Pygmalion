using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewRoomTrigger : MonoBehaviour
{
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
                    SceneManager.UnloadSceneAsync(0);
                    SceneManager.UnloadSceneAsync(1);
                    SceneManager.LoadScene(3, LoadSceneMode.Additive);
                }
            }
        }
    }
}
