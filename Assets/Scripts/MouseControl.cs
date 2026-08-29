using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    public GameObject rightButton;
    public GameObject leftButton;
    public bool buttonActivated = true;
    private Vector3 mousePosition;
	public float moveSpeed = 0.001f;
    [SerializeField] private Button btnRight = null;
    [SerializeField] private Button btnLeft = null;
    [SerializeField] private CinemachineCamera vcam;
    private CinemachineFollow vcamFollow;
    private float currentWall = 1; // 1 is forward wall, 0 is left wall, 2 is right wall, 3 is back wall
    private float leftBound;
    private float rightBound;
    private float leftZBound;
    private float rightZBound;
    private float lowerBound;
    private float upperBound;

    private void Awake() {
        if (btnRight == null || btnLeft == null) {
            Debug.LogError("Need to set button references.");
        } else {
            vcamFollow = vcam.GetComponent<CinemachineFollow>();
            btnRight.onClick.AddListener(OnButtonRightClick);
            btnLeft.onClick.AddListener(OnButtonLeftClick);
        }
    }

    private void OnButtonRightClick() {
        Debug.Log("Turning right.");

        if(currentWall == 1) { // forward wall, bring to right wall
            vcamFollow.FollowOffset = new Vector3(-10f, 0.91f, 0);
            currentWall = 2;
        } else if (currentWall == 0) { // left wall, bring to forward wall
            vcamFollow.FollowOffset = new Vector3(0f, 0.91f, -10f);
            currentWall = 1;
        } else if(currentWall == 2) { // right wall, bring to back wall
            vcamFollow.FollowOffset = new Vector3(0f, 0.91f, 10f);
            currentWall = 3;
        } else if(currentWall == 3) { // back wall, bring to left wall
            vcamFollow.FollowOffset = new Vector3(10f, 0.91f, 0);
            currentWall = 0;
        }
        // new Vector3(-10f, 0f, -10f) moves it to the right wall with no corner
        // new Vector3(-10f, 0f, 10f) moves it to the right wall with corner (basically opposite of current view)
    }

    private void OnButtonLeftClick() {
        Debug.Log("Turning left.");

        if(currentWall == 1) { // forward wall, bring to left wall
            vcamFollow.FollowOffset = new Vector3(10f, 0.91f, 0);
            currentWall = 0;
        } else if (currentWall == 0) { // left wall, bring to back wall
            vcamFollow.FollowOffset = new Vector3(0f, 0.91f, 10f);
            currentWall = 3;
        } else if(currentWall == 2) { // right wall, bring to forward wall
            vcamFollow.FollowOffset = new Vector3(0f, 0.91f, -10f);
            currentWall = 1;
        } else if(currentWall == 3) { // back wall, bring to right wall
            vcamFollow.FollowOffset = new Vector3(-10f, 0.91f, 0);
            currentWall = 2;
        }
    }

    void Start()
    {
        calculateBounds(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Hides the right/left movement buttons when the inventory is pulled up
        if (Input.GetButtonDown("Inventory") && buttonActivated)
        {
            Debug.Log("Button Activated!");
            rightButton.SetActive(false);
            leftButton.SetActive(false);
            buttonActivated = false;
        }    
        else if (Input.GetButtonDown("Inventory") && !buttonActivated)
        {
            Debug.Log("Button Deactivated!");
            rightButton.SetActive(true);
            leftButton.SetActive(true);
            buttonActivated = true;
        }


        /* // Changes moveSpeed to 0 gradually when the mouse isn't moving
           // Brings player back to center of screen during this time
        if (Input.GetAxis("Mouse X") == 0) {
            moveSpeed = Mathf.Lerp(moveSpeed, 0f, Time.deltaTime * 5f);
        } else {
            moveSpeed = 0.001f;
        }*/

		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Bind mouse position, change between binding x and z depending on wall faced
        if(currentWall == 1 || currentWall == 3) {
            if(mousePosition.x < leftBound) {
                mousePosition.x = leftBound;
            }
            if(mousePosition.x > rightBound) {
                mousePosition.x = rightBound;
            }
            mousePosition.z = transform.position.z;
        }
        else {
            if(mousePosition.z < leftZBound) {
                mousePosition.z = leftZBound;
            }
            if(mousePosition.z > rightZBound) {
                mousePosition.z = rightZBound;
            }
            mousePosition.x = transform.position.x;
        }

        if(mousePosition.y < lowerBound) {
            mousePosition.y = lowerBound;
        }
        if(mousePosition.y > upperBound) {
            mousePosition.y = upperBound;
        }

		transform.position = Vector3.Lerp(transform.position, mousePosition, moveSpeed);
        //transform.position = Vector3.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);
        
    }

    // Calculate bounds based on mouseSphere's starting position
    public void calculateBounds(Vector3 targetPosition) {
        leftBound = targetPosition.x - 5f;
        rightBound = targetPosition.x + 5f;
        leftZBound = targetPosition.z - 5f;
        rightZBound = targetPosition.z + 5f;
        lowerBound = targetPosition.y - 5f;
        upperBound = targetPosition.y + 5f;
    }
}