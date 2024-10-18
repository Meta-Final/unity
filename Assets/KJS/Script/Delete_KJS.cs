using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delete_KJS : MonoBehaviour
{
    public Button deleteButton;  // ���� ��ư (������ ������ ��ư�� �Ҵ�)

    void Start()
    {
        if (deleteButton != null)
        {
            // ���� ��ư Ŭ�� �� ���� �ؽ�Ʈ �ڽ��� �����ϴ� �̺�Ʈ ����
            deleteButton.onClick.AddListener(DeleteTextBox);
        }
        else
        {
            Debug.LogError("���� ��ư�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    // �ؽ�Ʈ �ڽ�(GameObject)�� �����ϴ� �޼���
    public void DeleteTextBox()
    {
        Debug.Log($"{gameObject.name} ������.");
        Destroy(gameObject);  // �ڽ�(�ؽ�Ʈ �ڽ� ������Ʈ)�� �ı�
    }
}
