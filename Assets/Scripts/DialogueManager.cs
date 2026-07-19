using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // access anywhere

    //public Image characterIcon;
    public TextMeshProUGUI characterName;
    private TextMeshProUGUI dialogueArea;
    public GameObject notebook;
    public GameObject notice;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;
    public float typingSpeed = 0.2f;

    private int speaker;
    private DialogueLine currentLine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        lines = new Queue<DialogueLine>();
        isDialogueActive = true;

        lines.Clear();

        // add new dialoguelines to queue
        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    // either finish the current line or move on to the next
    // depending on if the text is full or not
    public void ContinueLine()
    {
        if(dialogueArea.text == currentLine.line)
        {
            DisplayNextDialogueLine();
        }
        else
        {
            FinishCurrentDialogueLine();
        }
    }

    void DisplayNextDialogueLine()
    {
     
        // end if queue is empty
        if(lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = lines.Dequeue(); // get line
        characterName.text = currentLine.character.name;
        SetSpeaker(currentLine.character.speaker);  

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine)); 
    }

    // finishes the current line
    public void FinishCurrentDialogueLine()
    {
        StopAllCoroutines();
        dialogueArea.text = currentLine.line;
    }

    // animates the text - add each character to end of string
    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach(char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
    }

    void SetSpeaker(int speaker)
    {
        // notebook
        if(speaker == 0)
        {
            notebook.SetActive(true);
            notice.SetActive(false);

            dialogueArea = notebook.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        } 
        // notice
        else
        {
            notebook.SetActive(false);
            notice.SetActive(true);

            dialogueArea = notice.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
    }
}
