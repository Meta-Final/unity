using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CaptureScreenshot();
        }
        
    }

    public void CaptureScreenshot()
    {
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        Camera.main.targetTexture = renderTexture;
        RenderTexture.active = renderTexture;

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // RenderTexture 초기화
        RenderTexture.active = null;
        Camera.main.targetTexture = null;

        // 이미지 저장 및 인벤토리에 추가
        string base64 = System.Convert.ToBase64String(screenshot.EncodeToPNG());
        InventoryManager.instance.AddScreenshot(base64);
    }

    
}
