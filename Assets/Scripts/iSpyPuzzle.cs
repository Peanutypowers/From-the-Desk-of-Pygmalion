using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class iSpyPuzzle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public iSpyEnabler iSpyEnabler;

    public GameObject iSpyObjects;

    public int iSpyObjectsFound = 0;
    public bool puzzleCompleted;

    // Update is called once per frame
    void Update()
    {
        if (iSpyEnabler.iSpyPuzzleActive && !puzzleCompleted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.CompareTag("iSpyObjOne"))
                    {
                        Debug.Log("Duck Clicked");
                        iSpyObjectsFound++;
                        iSpyEnabler.checklist.transform.GetChild(1).GetComponent<TMP_Text>().text = "<s>1. A Rubber Duck</s>";
                        iSpyEnabler.checklist.transform.GetChild(1).GetComponent<TMP_Text>().color = Color.gray;
                        iSpyObjects.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    if (hit.collider.CompareTag("iSpyObjTwo"))
                    {
                        Debug.Log("Birdhouse Clicked");
                        iSpyObjectsFound++;
                        iSpyEnabler.checklist.transform.GetChild(2).GetComponent<TMP_Text>().text = "<s>2. A Birdhouse</s>";
                        iSpyEnabler.checklist.transform.GetChild(2).GetComponent<TMP_Text>().color = Color.gray;
                        iSpyObjects.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    if (hit.collider.CompareTag("iSpyObjThree"))
                    {
                        Debug.Log("Plane Clicked");
                        iSpyObjectsFound++;
                        iSpyEnabler.checklist.transform.GetChild(3).GetComponent<TMP_Text>().text = "<s>3. A Plane</s>";
                        iSpyEnabler.checklist.transform.GetChild(3).GetComponent<TMP_Text>().color = Color.gray;
                        iSpyObjects.transform.GetChild(2).gameObject.SetActive(false);
                    }
                    if (hit.collider.CompareTag("iSpyObjFour"))
                    {
                        Debug.Log("TwoFishTruck Clicked");
                        iSpyObjectsFound++;
                        iSpyEnabler.checklist.transform.GetChild(4).GetComponent<TMP_Text>().text = "<s>4. A Two Fish Truck</s>";
                        iSpyEnabler.checklist.transform.GetChild(4).GetComponent<TMP_Text>().color = Color.gray;
                        iSpyObjects.transform.GetChild(3).gameObject.SetActive(false);
                    }
                    if (hit.collider.CompareTag("iSpyObjFive"))
                    {
                        Debug.Log("Apple Clicked");
                        iSpyObjectsFound++;
                        iSpyEnabler.checklist.transform.GetChild(5).GetComponent<TMP_Text>().text = "<s>5. A Red Apple</s>";
                        iSpyEnabler.checklist.transform.GetChild(5).GetComponent<TMP_Text>().color = Color.gray;
                        iSpyObjects.transform.GetChild(4).gameObject.SetActive(false);
                    }
                    if (hit.collider.CompareTag("iSpyObjSix"))
                    {
                        Debug.Log("Zebrajeep Clicked");
                        iSpyObjectsFound++;
                        iSpyEnabler.checklist.transform.GetChild(6).GetComponent<TMP_Text>().text = "<s>6. A Zebra Jeep</s>";
                        iSpyEnabler.checklist.transform.GetChild(6).GetComponent<TMP_Text>().color = Color.gray;
                        iSpyObjects.transform.GetChild(5).gameObject.SetActive(false);
                    }
                }
            }
            if (iSpyObjectsFound == 6)
            {
                puzzleCompleted = true;
                iSpyObjects.gameObject.SetActive(false);
            }
        }
    }
}
