using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Delete_KJS : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    //public Button deleteButton;  // 삭제 버튼 (필요에 따라 남겨둠)
    private ToolMgr_KJS toolManager;  // ToolMgr_KJS 스크립트 참조
    private bool isDragging = false;  // 드래그 중인지 여부

    void Start()
    {
        //if (deleteButton != null)
        //{
        //    // 삭제 버튼 클릭 시 이벤트 연결 (필요에 따라 유지)
        //    deleteButton.onClick.AddListener(DeleteBox);
        //}
        //else
        //{
        //    Debug.LogError("삭제 버튼이 할당되지 않았습니다.");
        //}

        // ToolMgr_KJS 오브젝트 찾기
        FindToolManager();
    }

    private void Update()
    {
        // 드래그 중일 때 백스페이스 키를 누르면 삭제
        if (isDragging && Input.GetKeyDown(KeyCode.Backspace))
        {
            DeleteBox();
        }
    }

    // ToolMgr_KJS 오브젝트를 찾아 할당하는 함수
    private void FindToolManager()
    {
        toolManager = FindObjectOfType<ToolMgr_KJS>();  // ToolMgr_KJS 스크립트가 있는 오브젝트 찾기
        if (toolManager == null)
        {
            Debug.LogError("ToolMgr_KJS 스크립트를 찾을 수 없습니다.");
        }
    }

    // 텍스트 박스(GameObject)를 삭제하는 메서드
    public void DeleteBox()
    {
        Debug.Log($"{gameObject.name} 삭제됨.");

        // ToolMgr_KJS가 있을 경우 패널 비활성화
        if (toolManager != null && toolManager.editorPanel != null)
        {
            toolManager.editorPanel.SetActive(false);  // 패널 비활성화
        }

        Destroy(gameObject);  // 자신(텍스트 박스 오브젝트)을 파괴
    }

    // 왼쪽 클릭으로 드래그 시작
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isDragging = true;
        }
    }

    // 드래그 종료
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isDragging = false;
        }
    }

    // 드래그 중일 때 (IDragHandler 구현)
    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중일 때 상태 유지
        isDragging = true;
    }
}
