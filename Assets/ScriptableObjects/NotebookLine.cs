using UnityEngine;

[CreateAssetMenu(fileName = "NotebookLine", menuName = "Scriptable Objects/NotebookLine")]
public class NotebookLine : DialogueLine
{
    public void Reset()
    {
        speaker = 0;
    }
}
