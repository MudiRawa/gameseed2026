using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questName;

    [TextArea]
    public string description;

    public bool isCompleted;
}