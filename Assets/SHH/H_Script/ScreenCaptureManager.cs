using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;

public class ScreenCaptureManager : MonoBehaviour
{
    // �巡�� ������ �����ִ� �簢��
    private Rect dragArea;

    //�巡�� Ȯ�� ����
    private bool isDragging = false;

    //�巡�� ���� ���� ��ġ
    private Vector2 startPosition;

    //ĸ�ĵ� �̹����� ���� ����
    public Texture2D capturedTexture;

    // ĸó�� �̹��� ����Ƽ ȭ�鿡 �̸�����
    public RawImage displayImage;

    bool isCapturing = false;

    void Update()
    {
        // ���콺�� ���� ��ư�� ������ �� �巡�� ����
        if (Input.GetMouseButtonDown(0) && isCapturing)
        {
            StartDrag();
        }

        //�巡�� ��
        if (Input.GetMouseButton(0) && isDragging)
        {
            UpdateDrag();
        }
        
        //���콺�� ���� �巡�� ��
            if (Input.GetMouseButtonUp(0) && isDragging)
        {
            EndDrag();
        }

        // �����̽��� �Է� ó��
        if (Input.GetKeyDown(KeyCode.Space) && !isDragging)
        {
            StartCoroutine(CaptureScreen());
        }
    }

    private void StartDrag()
    {
        isDragging = true; //�巡�� Ȱ��ȭ
        startPosition = Input.mousePosition; //���� ���콺 ��ġ �α�
        startPosition.y = Screen.height - startPosition.y; // Y ��ǥ ����
    }

    private void UpdateDrag()
    {
        Vector2 currentMousePosition = Input.mousePosition; //���� ���콺 ��ġ �α�
        currentMousePosition.y = Screen.height - currentMousePosition.y; // Y ��ǥ ����

        // �巡�� ���� ������Ʈ
        dragArea = Rect.MinMaxRect(
            Mathf.Min(startPosition.x, currentMousePosition.x),
            Mathf.Min(startPosition.y, currentMousePosition.y),
            Mathf.Max(startPosition.x, currentMousePosition.x),
            Mathf.Max(startPosition.y, currentMousePosition.y)
        );
    }

    private void EndDrag()
    {
        isDragging = false; //�巡�� ��Ȱ��ȭ
        isCapturing = false;
    }

    private IEnumerator CaptureScreen()
    {
        // ��� ����Ͽ� �巡�װ� �Ϸ�� �� ĸó�� �����մϴ�.
        yield return new WaitForEndOfFrame();
        print("ĸ�ļ���!");

        // �巡�� ������ ũ��� ��ġ�� ������� �ؽ�ó�� �����մϴ�.
        capturedTexture = new Texture2D((int)dragArea.width, (int)dragArea.height, TextureFormat.RGB24, false);

        // ReadPixels �޼����� y ��ǥ ������ ���� ���� �巡�� ������ Y ��ǥ�� �����մϴ�.
        capturedTexture.ReadPixels(new Rect(dragArea.x, Screen.height - dragArea.yMax, dragArea.width, dragArea.height), 0, 0);
        capturedTexture.Apply();

        // RawImage�� ĸó�� �ؽ�ó�� �Ҵ��մϴ�.
        if (displayImage != null)
        {
            displayImage.texture = capturedTexture;
        }

        // ����� ���Ϸ� �����ϰų� ����� �� �ֽ��ϴ�.
        byte[] bytes = capturedTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/CapturedImage.png", bytes);
        print("���强��!");

        // �̹����� �κ��丮�� �߰�
        //string base64 = System.Convert.ToBase64String(bytes);
        //InventoryManager.instance.CaptureScreen(base64);
    }

<<<<<<< HEAD
    public void OnClickedButton()
    {
        isCapturing = true;
    }

    
=======
>>>>>>> parent of 8d2e907 (Feat : MyRoomMenubar)
    private void OnGUI()
    {
        // �巡�� ���� �ð�ȭ
        if (isDragging)
        {
            GUI.color = new Color(1, 1, 0, 0.5f); // ������ �����
            GUI.DrawTexture(dragArea, Texture2D.whiteTexture);
        }
    }
}
