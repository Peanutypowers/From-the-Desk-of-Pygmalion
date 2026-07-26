using System.Collections.Generic;
using UnityEngine;

/*
[System.Serializable]
public class DialogueCharacter
{
    //public string name;
    public int speaker;
}

[System.Serializable]
public class DialogueLine
{
    public int speaker;
    [TextArea(3, 10)]
    public string line;
}*/

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    // start the dialogue
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    // trigger dialogue start upon clicking
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            TriggerDialogue();
        }
    }*/

    private void OnMouseDown() {
        TriggerDialogue();
    }
}
