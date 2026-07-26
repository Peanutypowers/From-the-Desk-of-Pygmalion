using UnityEngine;

[CreateAssetMenu(fileName = "NoticeLine", menuName = "Scriptable Objects/NoticeLine")]
public class NoticeLine : DialogueLine
{
    public void Reset()
    {
        speaker = 1;
    }
}
