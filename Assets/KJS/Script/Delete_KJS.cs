using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Delete_KJS : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    //public Button deleteButton;  // ���� ��ư (�ʿ信 ���� ���ܵ�)
    private ToolMgr_KJS toolManager;  // ToolMgr_KJS ��ũ��Ʈ ����
    private bool isDragging = false;  // �巡�� ������ ����

    void Start()
    {
        //if (deleteButton != null)
        //{
        //    // ���� ��ư Ŭ�� �� �̺�Ʈ ���� (�ʿ信 ���� ����)
        //    deleteButton.onClick.AddListener(DeleteBox);
        //}
        //else
        //{
        //    Debug.LogError("���� ��ư�� �Ҵ���� �ʾҽ��ϴ�.");
        //}

        // ToolMgr_KJS ������Ʈ ã��
        FindToolManager();
    }

    private void Update()
    {
        // �巡�� ���� �� �齺���̽� Ű�� ������ ����
        if (isDragging && Input.GetKeyDown(KeyCode.Backspace))
        {
            DeleteBox();
        }
    }

    // ToolMgr_KJS ������Ʈ�� ã�� �Ҵ��ϴ� �Լ�
    private void FindToolManager()
    {
        toolManager = FindObjectOfType<ToolMgr_KJS>();  // ToolMgr_KJS ��ũ��Ʈ�� �ִ� ������Ʈ ã��
        if (toolManager == null)
        {
            Debug.LogError("ToolMgr_KJS ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    // �ؽ�Ʈ �ڽ�(GameObject)�� �����ϴ� �޼���
    public void DeleteBox()
    {
        Debug.Log($"{gameObject.name} ������.");

        // ToolMgr_KJS�� ���� ��� �г� ��Ȱ��ȭ
        if (toolManager != null && toolManager.editorPanel != null)
        {
            toolManager.editorPanel.SetActive(false);  // �г� ��Ȱ��ȭ
        }

        Destroy(gameObject);  // �ڽ�(�ؽ�Ʈ �ڽ� ������Ʈ)�� �ı�
    }

    // ���� Ŭ������ �巡�� ����
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isDragging = true;
        }
    }

    // �巡�� ����
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isDragging = false;
        }
    }

    // �巡�� ���� �� (IDragHandler ����)
    public void OnDrag(PointerEventData eventData)
    {
        // �巡�� ���� �� ���� ����
        isDragging = true;
    }
}
