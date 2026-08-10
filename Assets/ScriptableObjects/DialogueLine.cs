using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLine", menuName = "Scriptable Objects/DialogueLine")]
public class DialogueLine : ScriptableObject
{
    public int speaker; // 0 for notebook, 1 for notice
    [TextArea(3, 10)]
    public string line;
}
