using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInputs : MonoBehaviour
{
    private System.Array values = System.Enum.GetValues(typeof(KeyCode));

    public KeyCode jump = KeyCode.Space;

    public string inputName = KeyCode.Space.ToString();

    public Text inputNameText;

    private void Start()
    {
        inputName = (string)System.Enum.GetName(typeof(KeyCode), jump);
        inputNameText.text = inputName;
    }

    void Update()
    {
        foreach (KeyCode code in values)
        {
            if(Input.GetKeyDown(code))
            {
                jump = code;
                Refresh();
                print(System.Enum.GetName(typeof(KeyCode), code));
            }
        }
    }

    void Refresh()
    {
        inputName = (string)System.Enum.GetName(typeof(KeyCode), jump);
        inputNameText.text = inputName;
    }
}
