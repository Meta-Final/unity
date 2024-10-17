using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro 사용 시 필요

public class SaveMgr_KJS : MonoBehaviour
{
    public List<Button> buttons;  // TMP 버튼들을 관리하는 리스트
    public List<string> userIds;  // 유저 ID를 관리하는 리스트
    public int selectedUserIndex = 0;  // 현재 선택된 유저 인덱스

    public Button saveButton;     // 저장 버튼
    public Button loadButton;     // 불러오기 버튼

    private string saveDirectory = @"C:\Users\Admin\Desktop\UserInfo";
    private string saveFileName = "ButtonTexts.json";
    private string savePath;

    // 유저별 데이터를 저장하는 Dictionary
    private Dictionary<string, UserPosts> userData = new Dictionary<string, UserPosts>();

    private void Start()
    {
        savePath = Path.Combine(saveDirectory, saveFileName);

        // Save / Load 버튼 이벤트 연결
        saveButton.onClick.AddListener(SaveButtonTextsToFile);
        loadButton.onClick.AddListener(LoadButtonTextsFromFile);

        // 경로가 존재하지 않으면 폴더 생성
        EnsureDirectoryExists();

        // JSON 파일이 이미 존재하면 데이터 로드
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            userData = JsonUtility.FromJson<SerializableDictionary>(json).ToDictionary();
        }
    }

    private void EnsureDirectoryExists()
    {
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
            Debug.Log($"Directory created at: {saveDirectory}");
        }
    }

    private string GetSelectedUserId()
    {
        if (userIds.Count == 0 || selectedUserIndex < 0 || selectedUserIndex >= userIds.Count)
        {
            Debug.LogWarning("Invalid user index or user list is empty.");
            return null;
        }
        return userIds[selectedUserIndex];
    }

    private void SaveButtonTextsToFile()
    {
        string userId = GetSelectedUserId();
        if (userId == null) return;

        if (!userData.ContainsKey(userId))
        {
            userData[userId] = new UserPosts();
        }

        UserPosts posts = userData[userId];
        posts.posts.Clear();  // 기존 데이터 초기화

        // 각 버튼의 텍스트와 이미지 저장
        foreach (Button button in buttons)
        {
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            Image buttonImage = button.GetComponent<Image>();

            string editorName = button.name;
            string content = buttonText != null ? buttonText.text : "";

            // 버튼에 할당된 이미지를 base64 문자열로 변환
            string imageBase64 = "";
            if (buttonImage != null && buttonImage.sprite != null)
            {
                Texture2D texture = buttonImage.sprite.texture;
                imageBase64 = TextureToBase64(texture);
            }

            // PostInfo에 텍스트와 이미지 정보 저장
            posts.posts.Add(new PostInfo(editorName, content, imageBase64));
        }

        string json = JsonUtility.ToJson(new SerializableDictionary(userData), true);
        File.WriteAllText(savePath, json);

        Debug.Log($"Button texts and images saved for User ID: {userId}");
    }

    private void LoadButtonTextsFromFile()
    {
        string userId = GetSelectedUserId();
        if (userId == null) return;

        if (userData.ContainsKey(userId))
        {
            UserPosts posts = userData[userId];

            for (int i = 0; i < posts.posts.Count; i++)
            {
                if (i < buttons.Count)
                {
                    TMP_Text buttonText = buttons[i].GetComponentInChildren<TMP_Text>();
                    Image buttonImage = buttons[i].GetComponent<Image>();

                    PostInfo post = posts.posts[i];

                    if (buttonText != null)
                    {
                        buttonText.text = post.content;  // 텍스트 설정
                    }

                    // 이미지가 없으면 기존 이미지 제거
                    if (string.IsNullOrEmpty(post.imageBase64))
                    {
                        if (buttonImage != null)
                        {
                            buttonImage.sprite = null;  // 기존 이미지 제거
                            Debug.Log($"Image removed from button: {buttons[i].name}");
                        }
                    }
                    else  // 이미지가 있으면 복원
                    {
                        Texture2D texture = Base64ToTexture(post.imageBase64);
                        buttonImage.sprite = Sprite.Create(
                            texture,
                            new Rect(0, 0, texture.width, texture.height),
                            new Vector2(0.5f, 0.5f)
                        );
                    }
                }
            }

            Debug.Log($"Button texts and images loaded for User ID: {userId}");
        }
        else
        {
            Debug.LogWarning($"No data found for User ID: {userId}");
        }
    }

    // Texture2D를 base64 문자열로 변환하는 메서드
    private string TextureToBase64(Texture2D texture)
    {
        byte[] textureBytes = texture.EncodeToPNG();
        return System.Convert.ToBase64String(textureBytes);
    }

    // base64 문자열을 Texture2D로 변환하는 메서드
    private Texture2D Base64ToTexture(string base64)
    {
        byte[] textureBytes = System.Convert.FromBase64String(base64);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(textureBytes);
        return texture;
    }

    [System.Serializable]
    private class PostInfo
    {
        public string editorName;
        public string content;
        public string imageBase64;  // 이미지 데이터 (base64 문자열)

        public PostInfo(string editorName, string content, string imageBase64)
        {
            this.editorName = editorName;
            this.content = content;
            this.imageBase64 = imageBase64;
        }
    }

    [System.Serializable]
    private class UserPosts
    {
        public List<PostInfo> posts = new List<PostInfo>();
    }

    [System.Serializable]
    private class SerializableDictionary
    {
        public List<string> keys = new List<string>();
        public List<UserPosts> values = new List<UserPosts>();

        public SerializableDictionary(Dictionary<string, UserPosts> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public Dictionary<string, UserPosts> ToDictionary()
        {
            Dictionary<string, UserPosts> dictionary = new Dictionary<string, UserPosts>();
            for (int i = 0; i < keys.Count; i++)
            {
                dictionary[keys[i]] = values[i];
            }
            return dictionary;
        }
    }
}
