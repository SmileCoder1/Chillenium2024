using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public void Kill()
    {
        Debug.LogError("I DIED");
        Application.Quit();
        EditorApplication.ExitPlaymode();
    }
}
