using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonContent : MonoBehaviour
{
    public GameObject MagCanvas;
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
        // ĵ���� Ȱ��ȭ/��Ȱ��ȭ
        MagCanvas.SetActive(!MagCanvas.activeSelf);
    }
}
