using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateMgr_KJS : MonoBehaviour
{
    public GameObject panelPage;          // 생성할 패널 프리팹
    public GameObject textBox;            // 생성할 텍스트 박스 프리팹
    public Transform content;             // 패널이 추가될 Content (스크롤 안의 부모)
    public Scrollbar horizontalScrollbar; // 수평 스크롤바
    public ScrollRect scrollRect;         // ScrollRect

    private int pageCount = 1;            // 생성된 페이지 수
    private float pageWidth;              // 각 페이지의 폭

    public ToolMgr_KJS toolManager;       // ToolMgr_KJS 스크립트 참조
    public EditorMgr_KJS editorMgr;       // EditorMgr_KJS 스크립트 참조

    void Start()
    {
        if (panelPage == null || content == null) return;

        // 초기 페이지 폭 설정
        pageWidth = panelPage.GetComponent<RectTransform>().rect.width;
    }

    // 새로운 페이지 생성 (페이지마다 "텍스트 박스 생성 버튼" 포함)
    public void CreatePage()
    {
        if (panelPage == null || content == null) return;

        // 새 페이지 생성
        GameObject newPage = Instantiate(panelPage, content);
        pageCount++;

        // "텍스트 박스 생성 버튼" 가져오기
        Button btn_TextBox = newPage.transform.Find("btn_TextBox")?.GetComponent<Button>();
        if (btn_TextBox != null)
        {
            // 해당 페이지에 텍스트 박스 생성 이벤트 연결
            btn_TextBox.onClick.AddListener(() =>
            {
                CreateTextBox(newPage.transform);  // 해당 페이지에 텍스트 박스 추가
            });
        }
        else
        {
            Debug.LogError("페이지에 '텍스트 박스 생성 버튼'이 없습니다.");
        }

        // "페이지 삭제 버튼" 가져오기
        Button deleteButton = newPage.transform.Find("btn_Delete")?.GetComponent<Button>();
        if (deleteButton != null)
        {
            deleteButton.onClick.AddListener(() =>
            {
                RemovePage(newPage);  // 해당 페이지 삭제
            });
        }

        UpdateScrollbarSteps();
        UpdateContentWidth();
    }

    // 특정 페이지에 텍스트 박스를 생성하는 메서드
    public void CreateTextBox(Transform parent)
    {
        // 텍스트 박스 인스턴스 생성
        GameObject newTextBox = Instantiate(textBox, parent);

        // 생성된 텍스트 박스에서 버튼 컴포넌트 가져오기
        Button buttonComponent = newTextBox.GetComponentInChildren<Button>();
        if (buttonComponent != null)
        {
            // 버튼 클릭 시 toolManager와 editorMgr 연동
            buttonComponent.onClick.AddListener(toolManager.OnClickTogglePanel);
            buttonComponent.onClick.AddListener(() =>
            {
                if (editorMgr != null)
                {
                    editorMgr.SetInputFieldTextFromButton(buttonComponent);
                }
                else
                {
                    Debug.LogError("EditorMgr_KJS가 할당되지 않았습니다.");
                }
            });
        }
        else
        {
            Debug.LogError("텍스트 박스 프리팹에 Button 컴포넌트가 없습니다.");
        }
    }

    // 페이지 삭제 메서드
    public void RemovePage(GameObject page)
    {
        if (page != null)
        {
            Destroy(page);
            pageCount = Mathf.Max(1, pageCount - 1);

            UpdateScrollbarSteps();
            UpdateContentWidth();

            if (pageCount > 1)
            {
                float targetPosition = (pageCount - 1) / (float)(pageCount - 1);
                scrollRect.horizontalNormalizedPosition = Mathf.Clamp(targetPosition, 0f, 1f);
            }
            else
            {
                scrollRect.horizontalNormalizedPosition = 0f;
            }
        }
    }

    // 페이지 폭 업데이트
    private void UpdateContentWidth()
    {
        if (content == null) return;

        RectTransform contentRect = content.GetComponent<RectTransform>();
        float newWidth = pageWidth * pageCount;
        contentRect.sizeDelta = new Vector2(newWidth, contentRect.sizeDelta.y);
    }

    // 스크롤 위치 조정
    public void OnEndDrag()
    {
        if (pageCount <= 1) return;

        float normalizedPosition = scrollRect.horizontalNormalizedPosition;
        float targetPage = Mathf.Round(normalizedPosition * (pageCount - 1));
        float targetPosition = targetPage / (pageCount - 1);
        StartCoroutine(SmoothScrollTo(targetPosition));
    }

    // 스크롤을 부드럽게 이동시키는 코루틴
    private IEnumerator SmoothScrollTo(float targetPosition)
    {
        float duration = 0.2f;
        float elapsedTime = 0f;
        float startPosition = scrollRect.horizontalNormalizedPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newPosition = Mathf.Lerp(startPosition, targetPosition, elapsedTime / duration);
            scrollRect.horizontalNormalizedPosition = newPosition;
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = targetPosition;
    }

    // 스크롤바 단계 업데이트
    private void UpdateScrollbarSteps()
    {
        if (horizontalScrollbar != null)
        {
            horizontalScrollbar.numberOfSteps = Mathf.Max(1, pageCount);
        }
    }
}