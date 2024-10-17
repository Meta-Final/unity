using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro 사용 시 필요
using SFB;   // StandaloneFileBrowser 네임스페이스 사용

public class ImageMgr_KJS : MonoBehaviour
{
    public List<Button> buttons;  // 이미지가 할당될 여러 버튼들

    private void Start()
    {
        // 각 버튼에 클릭 이벤트 연결
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    // 버튼 클릭 시 호출되는 메서드
    private void OnButtonClick(Button targetButton)
    {
        OpenFileExplorerAndSetImage(targetButton);  // 파일 탐색기를 열고 이미지 설정
    }

    // 파일 탐색기를 열고 이미지 파일을 선택하는 메서드
    private void OpenFileExplorerAndSetImage(Button targetButton)
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel(
            "Select an Image", "",
            new[] { new ExtensionFilter("Image Files", "png", "jpg", "jpeg", "gif", "bmp") }, false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            SetImageToButton(targetButton, paths[0]);  // 이미지 할당
        }
    }

    // 이미지 파일을 버튼에 할당하는 메서드
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

        // 버튼의 Image 컴포넌트에 Sprite 할당
        button.GetComponent<Image>().sprite = newSprite;

        // 버튼의 텍스트 숨기기
        HideButtonText(button);
    }

    // 버튼의 Text 또는 TMP_Text 컴포넌트를 숨기는 메서드
    private void HideButtonText(Button button)
    {
        // 버튼 자식 오브젝트들에서 Text 또는 TMP_Text 컴포넌트를 찾기
        Text uiText = button.GetComponentInChildren<Text>();
        TMP_Text tmpText = button.GetComponentInChildren<TMP_Text>();

        // 각각의 텍스트를 비활성화
        if (uiText != null) uiText.gameObject.SetActive(false);
        if (tmpText != null) tmpText.gameObject.SetActive(false);
    }
}
