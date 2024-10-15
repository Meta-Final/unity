using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditorMgr_KJS : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text text;

    private void Start()
    {
        inputField.onEndEdit.AddListener(HandleInput);
    }

    private void HandleInput(string input)
    {
        text.text = input;
        inputField.text = "";
    }
}
