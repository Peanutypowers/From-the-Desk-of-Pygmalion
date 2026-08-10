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
                    SceneManager.UnloadSceneAsync(2);
                    SceneManager.UnloadSceneAsync(3);
                    SceneManager.LoadScene(5, LoadSceneMode.Additive);
                }
            }
        }
    }
}
