using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class EditorMgr_KJS : MonoBehaviour
{
    public TMP_InputField inputField;         // �ؽ�Ʈ�� ������ InputField
    public TMP_Dropdown fontSizeDropdown;
    public TMP_InputField fontSizeInputField;
    public Button boldButton;
    public Button italicButton;

    public Color pressedColor = new Color(0.8f, 0.8f, 0.8f);
    public Color defaultColor = Color.white;

    private int fontSize = 40;
    private bool isBold = false;
    private bool isItalic = false;
    private Button selectedButton;  // ���� ���õ� ��ư

    // ��Ʈ ũ�� ���� ���� (InputField ��� ��ư ��Ʈ ũ��)
    private const float FontSizeMultiplier = 1.5f;

    public void Start()
    {
        SetFunction_UI();

        // Dropdown �ɼ� �ʱ�ȭ
        fontSizeDropdown.ClearOptions();
        List<string> fontSizeOptions = new List<string>();
        for (int i = 10; i <= 98; i += 2)
        {
            fontSizeOptions.Add(i.ToString());
        }
        fontSizeDropdown.AddOptions(fontSizeOptions);
        fontSizeDropdown.value = fontSizeOptions.IndexOf(fontSize.ToString());

        // �̺�Ʈ ����
        fontSizeDropdown.onValueChanged.AddListener(OnFontSizeDropdownChanged);
        fontSizeInputField.onEndEdit.AddListener(OnFontSizeInputFieldChanged);
        boldButton.onClick.AddListener(OnBoldButtonClicked);
        italicButton.onClick.AddListener(OnItalicButtonClicked);

        // InputField �ؽ�Ʈ ���� �� ȣ��
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

            // ���õ� ��ư�� ��Ʈ ũ�� ����
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

            // ���õ� ��ư�� ��Ʈ ũ�� ����
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

        // ���õ� ��ư�� ��Ÿ�� ����
        UpdateButtonTextStyle();
    }

    public void OnItalicButtonClicked()
    {
        isItalic = !isItalic;
        UpdateTextStyle();
        italicButton.GetComponent<Image>().color = isItalic ? pressedColor : defaultColor;

        // ���õ� ��ư�� ��Ÿ�� ����
        UpdateButtonTextStyle();
    }

    private void UpdateTextStyle()
    {
        inputField.textComponent.fontStyle = FontStyles.Normal;

        if (isBold) inputField.textComponent.fontStyle |= FontStyles.Bold;
        if (isItalic) inputField.textComponent.fontStyle |= FontStyles.Italic;
    }

    // ��ư Ŭ�� �� �ش� ��ư�� �ؽ�Ʈ�� InputField�� ����
    public void SetInputFieldTextFromButton(Button button)
    {
        selectedButton = button;  // ���� ���õ� ��ư ����
        string buttonText = button.GetComponentInChildren<TextMeshProUGUI>().text;
        inputField.text = buttonText;  // InputField�� ��ư�� �ؽ�Ʈ ǥ��

        // ��ư�� ��Ÿ���� InputField�� ����ȭ
        TMP_Text buttonTextComponent = button.GetComponentInChildren<TMP_Text>();
        inputField.textComponent.fontSize = (int)(buttonTextComponent.fontSize / FontSizeMultiplier);
        inputField.textComponent.fontStyle = buttonTextComponent.fontStyle;

        fontSizeInputField.text = ((int)(buttonTextComponent.fontSize / FontSizeMultiplier)).ToString();
    }

    // InputField �ؽ�Ʈ�� ����� ������ ���õ� ��ư�� �ؽ�Ʈ�� ������Ʈ
    public void OnInputFieldTextChanged(string newText)
    {
        if (selectedButton != null)  // ���õ� ��ư�� ���� ��쿡�� ����
        {
            selectedButton.GetComponentInChildren<TextMeshProUGUI>().text = newText;
        }
    }

    // ���õ� ��ư�� �ؽ�Ʈ ��Ÿ�� ������Ʈ
    private void UpdateButtonTextStyle()
    {
        if (selectedButton != null)  // ���õ� ��ư�� ���� ���� ����
        {
            TMP_Text buttonTextComponent = selectedButton.GetComponentInChildren<TMP_Text>();

            // InputField�� ��Ʈ ��Ÿ�ϰ� ũ�⸦ ��ư�� ���� (1.5�� ũ�� ����)
            buttonTextComponent.fontSize = inputField.textComponent.fontSize * FontSizeMultiplier;
            buttonTextComponent.fontStyle = inputField.textComponent.fontStyle;
        }
    }
}