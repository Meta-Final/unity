using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Button btn_SlotItemPrefab;
    public Button btn_Delete;
    public Transform slot;
    public RawImage img_Preview;

    int selectIndex = -1;

    private void Start()
    {
        btn_Delete.interactable = false;
       
    }

    // 인벤토리 UI 업데이트
    public void UpdateInventoryUI()
    {
        foreach (Transform child in slot)
        {
            // Slot 의 모든 자식 오브젝트를 제거
            Destroy(child.gameObject); 
        }

        for (int i = 0; i < InventoryManager.instance.ScreenshotCount(); i++)
        {
            Button newButton = Instantiate(btn_SlotItemPrefab, slot);
            int index = i;
            
            newButton.onClick.AddListener(() => OnSlotClick(index));
            
        }
    }

    // 클릭하면 발동하는 함수
    public void OnSlotClick(int index)
    {
        selectIndex = index;

        InventoryManager.instance.ShowScreenshot(selectIndex);

        btn_Delete.interactable = true;
        btn_Delete.onClick.AddListener(() => OnSlotDeleteClick(selectIndex));
    }

    // 삭제 기능
    public void OnSlotDeleteClick(int index)
    {
        InventoryManager.instance.DeleteScreenshot(index);
        btn_Delete.interactable = false;
        selectIndex = -1;
    }
    

    // 이미지 로드
    public void DisplayScreenshot(string path)
    {
        StartCoroutine(LoadImage(path));
    }

    public IEnumerator LoadImage(string path)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture("file:///" + path);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            img_Preview.texture = DownloadHandlerTexture.GetContent(request);
        }
        else
        {
            Debug.LogError("이미지 로드 실패: " + request.error);
        }
    }
}
