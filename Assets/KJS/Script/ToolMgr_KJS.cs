using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToolMgr_KJS : MonoBehaviour
{
    public GameObject editorPanel;  // Ȱ��ȭ/��Ȱ��ȭ�� �г�

    // �г��� Ȱ��ȭ ���¸� ����ϴ� �Լ�
    public void OnClickTogglePanel()
    {
        bool isActive = editorPanel.activeSelf;
        editorPanel.SetActive(!isActive);
    }
}
