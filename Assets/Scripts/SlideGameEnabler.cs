using UnityEngine;

public class SlideGameEnabler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public GameObject slidePuzzle;
    //i know the default is false, but im setting it just incase so it is clear, since the puzzle starts not visible
    private bool slidePuzzleActive = false;

    // Update is called once per frame
    void Update()
    {
        //i took the hit detection from the inventory script, changing the tag comparison to use the one I specified for 
        //the slide puzzle activation, im specifying incase something goes wrong
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("SlidePuzzleActivator"))
                {
                    Debug.Log("bear 5 interacted");
                    slidePuzzleActive = !slidePuzzleActive;
                    slidePuzzle.gameObject.SetActive(slidePuzzleActive);
                }
            }
        }
    }
}
