using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonContent : MonoBehaviour
{
    public GameObject MagCanvas;

    public Button scarbButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonClick()
    {
        // 캔버스 활성화/비활성화
        MagCanvas.SetActive(!MagCanvas.activeSelf);
    }
}
