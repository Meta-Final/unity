using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonConnection : MonoBehaviour
{
    public GameObject MagCanvas;
    public GameObject ChannelCanvas;
    //public GameObject exitButton;
    public Button scarbButton;

    Button btn_Exit;
    void Start()
    {
        MagCanvas = GameObject.Find("CanvasMag");
        ChannelCanvas = GameObject.Find("ChannelCanvas");

        //MagCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnButtonClick()
    {
        // ĵ���� Ȱ��ȭ/��Ȱ��ȭ
        //MagCanvas.SetActive(!MagCanvas.activeSelf);
        MagCanvas.SetActive(true);
        ChannelCanvas.SetActive(false);
    }
}


