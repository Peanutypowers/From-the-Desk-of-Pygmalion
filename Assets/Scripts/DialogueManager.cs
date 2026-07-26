using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // access anywhere

    //public Image characterIcon;
    //public TextMeshProUGUI characterName;
    private TextMeshProUGUI dialogueArea;
    private AudioSource audioSource;
    public GameObject notebook;
    public GameObject notice;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;
    public float typingSpeed = 0.2f;
    public float duration = 0.3f;

    public Animator animator;

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
        animator.Play("show");

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
        //characterName.text = currentLine.character.name;
        SetSpeaker(currentLine.speaker);  

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
        //PlayAudio(); // play it once
        dialogueArea.text = "";
        foreach(char letter in dialogueLine.line.ToCharArray())
        {
            PlayAudio(); // play it for each letter
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("hide");
    }

    void SetSpeaker(int speaker)
    {
        // notebook
        if(speaker == 0)
        {
            notebook.SetActive(true);
            notice.SetActive(false);

            dialogueArea = notebook.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            audioSource = notebook.transform.GetChild(1).GetComponent<AudioSource>();
        } 
        // notice
        else
        {
            notebook.SetActive(false);
            notice.SetActive(true);

            dialogueArea = notice.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            audioSource = notice.transform.GetChild(1).GetComponent<AudioSource>();
        }
    }

    private void PlayAudio()
    {
        audioSource.pitch = Mathf.Lerp(1.0f, 1.5f, Random.Range(0f, 1f)); // slight pitch variations

        double currentTime = AudioSettings.dspTime;
        audioSource.PlayScheduled(currentTime);
        audioSource.SetScheduledEndTime(currentTime + duration);
    }
}
