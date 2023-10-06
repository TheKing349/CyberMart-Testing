using System.Collections.Generic;
using UnityEngine;

public class KeybindsDictionarySwitcher : MonoBehaviour
{
    public Dictionary<string, KeyCode> CopyFirstToSecond(Dictionary<string, KeyCode> firstDictionary, Dictionary<string, KeyCode> secondDictionary)
    {
        if (secondDictionary.Count > 0)
        {
            foreach (var kvp in firstDictionary)
            {
                secondDictionary[kvp.Key] = kvp.Value;
            }
        }
        else
        {
            foreach (var kvp in firstDictionary)
            {
                secondDictionary.Add(kvp.Key, kvp.Value);
            }
        }
        return secondDictionary;
    }
}