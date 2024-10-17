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
    public Button boldButton;    // 볼드 버튼
    public Button italicButton;  // 기울임 버튼
    //public Button underlineButton; // 밑줄 버튼

    public Color pressedColor = new Color(0.8f, 0.8f, 0.8f); // 눌림 상태의 색상
    public Color defaultColor = Color.white; // 기본 상태의 색상

    private int fontSize = 24;
    private bool isBold = false;     // 볼드 상태 추적
    private bool isItalic = false;   // 기울임 상태 추적
    //private bool isUnderline = false; // 밑줄 상태 추적

    public void Start()
    {
        SetFunction_UI();

        // Dropdown의 옵션 설정 (폰트 크기 리스트)
        fontSizeDropdown.ClearOptions();
        List<string> fontSizeOptions = new List<string>();
        for (int i = 10; i <= 98; i += 2)
        {
            fontSizeOptions.Add(i.ToString());
        }
        fontSizeDropdown.AddOptions(fontSizeOptions);

        // 초기 폰트 크기를 Dropdown에서 선택되도록 설정
        fontSizeDropdown.value = fontSizeOptions.IndexOf(fontSize.ToString());

        // 이벤트 연결
        fontSizeDropdown.onValueChanged.AddListener(OnFontSizeDropdownChanged);
        fontSizeInputField.onEndEdit.AddListener(OnFontSizeInputFieldChanged);
        boldButton.onClick.AddListener(OnBoldButtonClicked); // 볼드 버튼 클릭 이벤트 연결
        italicButton.onClick.AddListener(OnItalicButtonClicked); // 기울임 버튼 클릭 이벤트 연결
        //underlineButton.onClick.AddListener(OnUnderlineButtonClicked); // 밑줄 버튼 클릭 이벤트 연결

        // InputField의 초기 폰트 크기 설정
        inputField.textComponent.fontSize = fontSize;

        // 폰트 크기 입력 필드 초기화
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

    // Dropdown에서 선택한 값으로 폰트 사이즈 변경
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

    // 텍스트 입력 필드에서 입력한 값으로 폰트 사이즈 변경
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

    // 볼드 버튼 클릭 시 텍스트를 볼드체로 토글하고 버튼 색상 변경
    public void OnBoldButtonClicked()
    {
        isBold = !isBold; // 볼드 상태 토글
        UpdateTextStyle(); // 텍스트 스타일 업데이트

        // 버튼 색상 업데이트
        boldButton.GetComponent<Image>().color = isBold ? pressedColor : defaultColor;
    }

    // 기울임 버튼 클릭 시 텍스트를 기울임체로 토글하고 버튼 색상 변경
    public void OnItalicButtonClicked()
    {
        isItalic = !isItalic; // 기울임 상태 토글
        UpdateTextStyle(); // 텍스트 스타일 업데이트

        // 버튼 색상 업데이트
        italicButton.GetComponent<Image>().color = isItalic ? pressedColor : defaultColor;
    }

    // 밑줄 버튼 클릭 시 텍스트에 밑줄을 토글하고 버튼 색상 변경
    //public void OnUnderlineButtonClicked()
    //{
    //    isUnderline = !isUnderline; // 밑줄 상태 토글
    //    UpdateTextStyle(); // 텍스트 스타일 업데이트

    //    // 버튼 색상 업데이트
    //    underlineButton.GetComponent<Image>().color = isUnderline ? pressedColor : defaultColor;
    //}

    // 텍스트의 스타일을 업데이트하는 메서드
    private void UpdateTextStyle()
    {
        inputField.textComponent.fontStyle = FontStyles.Normal; // 기본 스타일로 초기화

        if (isBold) inputField.textComponent.fontStyle |= FontStyles.Bold; // 볼드 적용
        if (isItalic) inputField.textComponent.fontStyle |= FontStyles.Italic; // 기울임 적용
        //if (isUnderline) inputField.textComponent.fontStyle |= FontStyles.Underline; // 밑줄 적용
    }
}