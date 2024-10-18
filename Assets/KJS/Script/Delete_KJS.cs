using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delete_KJS : MonoBehaviour
{
    public Button deleteButton;  // 삭제 버튼 (프리팹 내부의 버튼에 할당)

    void Start()
    {
        if (deleteButton != null)
        {
            // 삭제 버튼 클릭 시 현재 텍스트 박스를 삭제하는 이벤트 연결
            deleteButton.onClick.AddListener(DeleteTextBox);
        }
        else
        {
            Debug.LogError("삭제 버튼이 할당되지 않았습니다.");
        }
    }

    // 텍스트 박스(GameObject)를 삭제하는 메서드
    public void DeleteTextBox()
    {
        Debug.Log($"{gameObject.name} 삭제됨.");
        Destroy(gameObject);  // 자신(텍스트 박스 오브젝트)을 파괴
    }
}
