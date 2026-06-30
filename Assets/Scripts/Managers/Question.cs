using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Question")]
public class Question : ScriptableObject
{
    public string HeaderLine;
    public string[] AsnwerSlot;
    public bool[] IsCorrectAnswer;
    public string Reference;

}
