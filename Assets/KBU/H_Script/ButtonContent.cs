using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonContent : MonoBehaviour
{
    public GameObject MagCanvas;
    public GameObject exitButton;
    public Button scarbButton;

    Button btn_Exit;
    void Start()
    {
        MagCanvas = GameObject.Find("CanvasMag");

        MagCanvas.SetActive(false);

        //exitButton = MagCanvas.transform.GetChild(8).gameObject;

       // btn_Exit = exitButton.GetComponent<Button>();

        //btn_Exit.onClick.AddListner(OnbuttonExitClick);
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
    }

    public void OnbuttonExitClick()
    {
        MagCanvas.SetActive(false);
    }
}
