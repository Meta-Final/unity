using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro ��� �� �ʿ�
using SFB;   // StandaloneFileBrowser ���ӽ����̽� ���

public class ImageMgr_KJS : MonoBehaviour
{
    public List<Button> buttons;  // �̹����� �Ҵ�� ���� ��ư��

    private void Start()
    {
        // �� ��ư�� Ŭ�� �̺�Ʈ ����
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    private void OnButtonClick(Button targetButton)
    {
        OpenFileExplorerAndSetImage(targetButton);  // ���� Ž���⸦ ���� �̹��� ����
    }

    // ���� Ž���⸦ ���� �̹��� ������ �����ϴ� �޼���
    private void OpenFileExplorerAndSetImage(Button targetButton)
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel(
            "Select an Image", "",
            new[] { new ExtensionFilter("Image Files", "png", "jpg", "jpeg", "gif", "bmp") }, false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            SetImageToButton(targetButton, paths[0]);  // �̹��� �Ҵ�
        }
    }

    // �̹��� ������ ��ư�� �Ҵ��ϴ� �޼���
    private void SetImageToButton(Button button, string imagePath)
    {
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);

        Sprite newSprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );

        // ��ư�� Image ������Ʈ�� Sprite �Ҵ�
        button.GetComponent<Image>().sprite = newSprite;

        // ��ư�� �ؽ�Ʈ �����
        HideButtonText(button);
    }

    // ��ư�� Text �Ǵ� TMP_Text ������Ʈ�� ����� �޼���
    private void HideButtonText(Button button)
    {
        // ��ư �ڽ� ������Ʈ�鿡�� Text �Ǵ� TMP_Text ������Ʈ�� ã��
        Text uiText = button.GetComponentInChildren<Text>();
        TMP_Text tmpText = button.GetComponentInChildren<TMP_Text>();

        // ������ �ؽ�Ʈ�� ��Ȱ��ȭ
        if (uiText != null) uiText.gameObject.SetActive(false);
        if (tmpText != null) tmpText.gameObject.SetActive(false);
    }
}