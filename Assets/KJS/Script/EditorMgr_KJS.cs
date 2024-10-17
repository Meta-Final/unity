using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class EditorMgr_KJS : MonoBehaviour
{
    public TMP_InputField inputField;         // 텍스트를 수정할 InputField
    public TMP_Dropdown fontSizeDropdown;
    public TMP_InputField fontSizeInputField;
    public Button boldButton;
    public Button italicButton;

    public Color pressedColor = new Color(0.8f, 0.8f, 0.8f);
    public Color defaultColor = Color.white;

    private int fontSize = 40;
    private bool isBold = false;
    private bool isItalic = false;
    private Button selectedButton;  // 현재 선택된 버튼

    // 폰트 크기 배율 조정 (InputField 대비 버튼 폰트 크기)
    private const float FontSizeMultiplier = 1.5f;

    public void Start()
    {
        SetFunction_UI();

        // Dropdown 옵션 초기화
        fontSizeDropdown.ClearOptions();
        List<string> fontSizeOptions = new List<string>();
        for (int i = 10; i <= 98; i += 2)
        {
            fontSizeOptions.Add(i.ToString());
        }
        fontSizeDropdown.AddOptions(fontSizeOptions);
        fontSizeDropdown.value = fontSizeOptions.IndexOf(fontSize.ToString());

        // 이벤트 연결
        fontSizeDropdown.onValueChanged.AddListener(OnFontSizeDropdownChanged);
        fontSizeInputField.onEndEdit.AddListener(OnFontSizeInputFieldChanged);
        boldButton.onClick.AddListener(OnBoldButtonClicked);
        italicButton.onClick.AddListener(OnItalicButtonClicked);

        // InputField 텍스트 변경 시 호출
        inputField.onValueChanged.AddListener(OnInputFieldTextChanged);

        inputField.textComponent.fontSize = fontSize;
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

    public void OnFontSizeDropdownChanged(int index)
    {
        string selectedValue = fontSizeDropdown.options[index].text;
        if (int.TryParse(selectedValue, out int newSize))
        {
            fontSize = newSize;
            inputField.textComponent.fontSize = fontSize;
            fontSizeInputField.text = fontSize.ToString();

            // 선택된 버튼의 폰트 크기 적용
            UpdateButtonTextStyle();
        }
    }

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

            // 선택된 버튼의 폰트 크기 적용
            UpdateButtonTextStyle();
        }
        else
        {
            fontSizeInputField.text = fontSize.ToString();
        }
    }

    public void OnBoldButtonClicked()
    {
        isBold = !isBold;
        UpdateTextStyle();
        boldButton.GetComponent<Image>().color = isBold ? pressedColor : defaultColor;

        // 선택된 버튼의 스타일 적용
        UpdateButtonTextStyle();
    }

    public void OnItalicButtonClicked()
    {
        isItalic = !isItalic;
        UpdateTextStyle();
        italicButton.GetComponent<Image>().color = isItalic ? pressedColor : defaultColor;

        // 선택된 버튼의 스타일 적용
        UpdateButtonTextStyle();
    }

    private void UpdateTextStyle()
    {
        inputField.textComponent.fontStyle = FontStyles.Normal;

        if (isBold) inputField.textComponent.fontStyle |= FontStyles.Bold;
        if (isItalic) inputField.textComponent.fontStyle |= FontStyles.Italic;
    }

    // 버튼 클릭 시 해당 버튼의 텍스트를 InputField에 복사
    public void SetInputFieldTextFromButton(Button button)
    {
        selectedButton = button;  // 현재 선택된 버튼 저장
        string buttonText = button.GetComponentInChildren<TextMeshProUGUI>().text;
        inputField.text = buttonText;  // InputField에 버튼의 텍스트 표시

        // 버튼의 스타일을 InputField에 동기화
        TMP_Text buttonTextComponent = button.GetComponentInChildren<TMP_Text>();
        inputField.textComponent.fontSize = (int)(buttonTextComponent.fontSize / FontSizeMultiplier);
        inputField.textComponent.fontStyle = buttonTextComponent.fontStyle;

        fontSizeInputField.text = ((int)(buttonTextComponent.fontSize / FontSizeMultiplier)).ToString();
    }

    // InputField 텍스트가 변경될 때마다 선택된 버튼의 텍스트를 업데이트
    public void OnInputFieldTextChanged(string newText)
    {
        if (selectedButton != null)  // 선택된 버튼이 있을 경우에만 실행
        {
            selectedButton.GetComponentInChildren<TextMeshProUGUI>().text = newText;
        }
    }

    // 선택된 버튼의 텍스트 스타일 업데이트
    private void UpdateButtonTextStyle()
    {
        if (selectedButton != null)  // 선택된 버튼이 있을 때만 실행
        {
            TMP_Text buttonTextComponent = selectedButton.GetComponentInChildren<TMP_Text>();

            // InputField의 폰트 스타일과 크기를 버튼에 적용 (1.5배 크기 조정)
            buttonTextComponent.fontSize = inputField.textComponent.fontSize * FontSizeMultiplier;
            buttonTextComponent.fontStyle = inputField.textComponent.fontStyle;
        }
    }
}