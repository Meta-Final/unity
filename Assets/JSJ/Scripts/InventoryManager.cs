using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ScreenshotData
{
    public List<string> screenshotList = new List<string>();
}

public class InventoryManager : MonoBehaviour
{
    public GameObject img_Background;
    public RawImage img_Preview;

    string saveImagePath;

    public static InventoryManager instance;

    ScreenshotData screenshotData;

    
  

    private void Awake()
    {
        instance = this;

        // Json 파일 경로
        saveImagePath = Application.persistentDataPath + "/screenshots.json";

        LoadScreenshot();
    }

    void Start()
    {
       
    }

    void Update()
    {
        // 인벤토리 UI Toggle
        if (Input.GetKeyDown(KeyCode.I))
        {
            img_Background.SetActive(!img_Background.activeSelf);
        }
    }

    // 스크린샷 추가
    public void AddScreenshot(string base64Image)
    {
        if (screenshotData == null)
        {
            screenshotData = new ScreenshotData(); 
        }

        screenshotData.screenshotList.Add(base64Image);

        SaveScreenshot();
    }

    // 스크린샷 저장
    public void SaveScreenshot()
    {
        string jsonData = JsonUtility.ToJson(screenshotData, true);
        File.WriteAllText(saveImagePath, jsonData);
        Debug.Log("스크린샷 저장 성공" + jsonData);
    }

    // 스크린샷 로드
    public void LoadScreenshot()
    {
        if (File.Exists(saveImagePath))
        {
            string jsonData = File.ReadAllText(saveImagePath);
            screenshotData = JsonUtility.FromJson<ScreenshotData>(jsonData);
        }
        // 파일이 없으면 새로운 인스턴스 생성
        else
        {
            screenshotData = new ScreenshotData();
        }
    }
    public int ScreenshotCount()
    {
        return screenshotData.screenshotList.Count;
    }

    public void ShowScreenshot(int index)
    {
        if (index >= 0 && index < ScreenshotCount())
        {
            byte[] imageByte = System.Convert.FromBase64String(screenshotData.screenshotList[index]);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageByte);

            img_Preview.texture = texture;
            img_Preview.gameObject.SetActive(true);
        }

    }

    
   
}
