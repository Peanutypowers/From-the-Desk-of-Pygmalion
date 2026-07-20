using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Cinemachine;

public class iSpyEnabler : MonoBehaviour
{

    private GameObject cam;
    private GameObject vcam;
    public CinemachineFollow vcamFollow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        vcam = GameObject.Find("CinemachineCamera");
        vcamFollow = vcam.GetComponent<CinemachineFollow>();
    }
        
    public Button returnCamera;
    public GameObject checklist;
    public bool iSpyPuzzleActive;

    //getting the interacting with puzzle variable so you can't start the iSpy while in the slide puzzle or any other puzzle

    //this function is allowing the return button to make the puzzle interactable again.
    //it also re-enables the enabler's collider, i disable it so you can click it without
    //clicking a thing hidden in the puzzle while not in the puzzle
    public void iSpyPuzzleDeactivator()
    {
        iSpyPuzzleActive = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        vcam.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //checking for if its active because you only want to be able to interact with the enabler while not in the puzzle for this one
                //if (hit.collider.CompareTag("iSpyPuzzleEnabler") && !iSpyPuzzleActive && !inventory.interactingWithPuzzle)
                if (hit.collider.CompareTag("iSpyPuzzleEnabler") && !iSpyPuzzleActive)
                {
                    this.gameObject.GetComponent<BoxCollider>().enabled = false;
                    Debug.Log("ispy interacted");


                    //this is turning on the exit puzzle button and checklist
                    returnCamera.gameObject.SetActive(true);
                    checklist.gameObject.SetActive(true);

                    //this is my attempt at making the camera go to the puzzle
                    //disabled this because the cnimeachine was overriding where I was wanting to move the camera
                    //you dont want the camera wobbling when youre doing an i spy puzzle anyway
                    vcam.gameObject.SetActive(false);
                    cam.transform.position = new Vector3(17.17f, 17.54f, 72.93f);
                    cam.GetComponent<Camera>().orthographicSize = 5.0f;
                    Debug.Log("Camera Moved");

                    /*the reason im not just setting it to the opposite value is because there is
                   going to be a seperate button to exit the puzzle, since the image for the puzzle itself is
                   the enabler. You don't want to be taken out of the puzzle every time you misclick.
                   */
                    iSpyPuzzleActive = true;
                }
            }
        }
    }
}
