using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditorMgr_KJS : MonoBehaviour
{
    public TextMeshProUGUI message;
    public TMP_InputField inputField;
    public TMP_Dropdown fontSizeDropdown;
    public TMP_InputField fontSizeInputField;
    public Button boldButton;    // ���� ��ư
    public Button italicButton;  // ����� ��ư
    //public Button underlineButton; // ���� ��ư

    public Color pressedColor = new Color(0.8f, 0.8f, 0.8f); // ���� ������ ����
    public Color defaultColor = Color.white; // �⺻ ������ ����

    private int fontSize = 24;
    private bool isBold = false;     // ���� ���� ����
    private bool isItalic = false;   // ����� ���� ����
    //private bool isUnderline = false; // ���� ���� ����

    public void Start()
    {
        SetFunction_UI();

        // Dropdown�� �ɼ� ���� (��Ʈ ũ�� ����Ʈ)
        fontSizeDropdown.ClearOptions();
        List<string> fontSizeOptions = new List<string>();
        for (int i = 10; i <= 98; i += 2)
        {
            fontSizeOptions.Add(i.ToString());
        }
        fontSizeDropdown.AddOptions(fontSizeOptions);

        // �ʱ� ��Ʈ ũ�⸦ Dropdown���� ���õǵ��� ����
        fontSizeDropdown.value = fontSizeOptions.IndexOf(fontSize.ToString());

        // �̺�Ʈ ����
        fontSizeDropdown.onValueChanged.AddListener(OnFontSizeDropdownChanged);
        fontSizeInputField.onEndEdit.AddListener(OnFontSizeInputFieldChanged);
        boldButton.onClick.AddListener(OnBoldButtonClicked); // ���� ��ư Ŭ�� �̺�Ʈ ����
        italicButton.onClick.AddListener(OnItalicButtonClicked); // ����� ��ư Ŭ�� �̺�Ʈ ����
        //underlineButton.onClick.AddListener(OnUnderlineButtonClicked); // ���� ��ư Ŭ�� �̺�Ʈ ����

        // InputField�� �ʱ� ��Ʈ ũ�� ����
        inputField.textComponent.fontSize = fontSize;

        // ��Ʈ ũ�� �Է� �ʵ� �ʱ�ȭ
        fontSizeInputField.text = fontSize.ToString();
    }

    public void SetFunction_UI()
    {
        ResetFunction_UI();
    }

    public void ResetFunction_UI()
    {
        inputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Input..";
        inputField.contentType = TMP_InputField.ContentType.Standard;
        inputField.lineType = TMP_InputField.LineType.MultiLineNewline;
    }

    // Dropdown���� ������ ������ ��Ʈ ������ ����
    public void OnFontSizeDropdownChanged(int index)
    {
        string selectedValue = fontSizeDropdown.options[index].text;
        if (int.TryParse(selectedValue, out int newSize))
        {
            fontSize = newSize;
            inputField.textComponent.fontSize = fontSize;
            fontSizeInputField.text = fontSize.ToString();
        }
    }

    // �ؽ�Ʈ �Է� �ʵ忡�� �Է��� ������ ��Ʈ ������ ����
    public void OnFontSizeInputFieldChanged(string input)
    {
        if (int.TryParse(input, out int newSize) && newSize >= 10 && newSize <= 100)
        {
            fontSize = newSize;
            inputField.textComponent.fontSize = fontSize;

            int dropdownIndex = fontSizeDropdown.options.FindIndex(option => option.text == fontSize.ToString());
            if (dropdownIndex != -1)
            {
                fontSizeDropdown.value = dropdownIndex;
            }
        }
        else
        {
            fontSizeInputField.text = fontSize.ToString();
        }
    }

    // ���� ��ư Ŭ�� �� �ؽ�Ʈ�� ����ü�� ����ϰ� ��ư ���� ����
    public void OnBoldButtonClicked()
    {
        isBold = !isBold; // ���� ���� ���
        UpdateTextStyle(); // �ؽ�Ʈ ��Ÿ�� ������Ʈ

        // ��ư ���� ������Ʈ
        boldButton.GetComponent<Image>().color = isBold ? pressedColor : defaultColor;
    }

    // ����� ��ư Ŭ�� �� �ؽ�Ʈ�� �����ü�� ����ϰ� ��ư ���� ����
    public void OnItalicButtonClicked()
    {
        isItalic = !isItalic; // ����� ���� ���
        UpdateTextStyle(); // �ؽ�Ʈ ��Ÿ�� ������Ʈ

        // ��ư ���� ������Ʈ
        italicButton.GetComponent<Image>().color = isItalic ? pressedColor : defaultColor;
    }

    // ���� ��ư Ŭ�� �� �ؽ�Ʈ�� ������ ����ϰ� ��ư ���� ����
    //public void OnUnderlineButtonClicked()
    //{
    //    isUnderline = !isUnderline; // ���� ���� ���
    //    UpdateTextStyle(); // �ؽ�Ʈ ��Ÿ�� ������Ʈ

    //    // ��ư ���� ������Ʈ
    //    underlineButton.GetComponent<Image>().color = isUnderline ? pressedColor : defaultColor;
    //}

    // �ؽ�Ʈ�� ��Ÿ���� ������Ʈ�ϴ� �޼���
    private void UpdateTextStyle()
    {
        inputField.textComponent.fontStyle = FontStyles.Normal; // �⺻ ��Ÿ�Ϸ� �ʱ�ȭ

        if (isBold) inputField.textComponent.fontStyle |= FontStyles.Bold; // ���� ����
        if (isItalic) inputField.textComponent.fontStyle |= FontStyles.Italic; // ����� ����
        //if (isUnderline) inputField.textComponent.fontStyle |= FontStyles.Underline; // ���� ����
    }
}