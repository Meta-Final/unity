using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Button btn_SlotItemPrefab;
    public Transform slot;
    
    

    void Start()
    {
        UpdateInventoryUI();
    }

    void Update()
    {
        
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < InventoryManager.instance.ScreenshotCount(); i++)
        {
            Button newButton = Instantiate(btn_SlotItemPrefab, slot);
            int index = i;
            newButton.onClick.AddListener(() => OnSlotClick(index));
        }
    }

    public void OnSlotClick(int index)
    {
        Debug.Log($"슬롯 클릭 : 인덱스{index}");

        InventoryManager.instance.ShowScreenshot(index);
        
    }

    

   
}
