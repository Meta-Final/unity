using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateMgr_KJS : MonoBehaviour
{
    public GameObject panelPage;          // ������ �г� ������
    public GameObject textBox;            // ������ �ؽ�Ʈ �ڽ� ������
    public Transform content;             // �г��� �߰��� Content (��ũ�� ���� �θ�)
    public Scrollbar horizontalScrollbar; // ���� ��ũ�ѹ�
    public ScrollRect scrollRect;         // ScrollRect

    private int pageCount = 1;            // ������ ������ ��
    private float pageWidth;              // �� �������� ��

    public ToolMgr_KJS toolManager;       // ToolMgr_KJS ��ũ��Ʈ ����
    public EditorMgr_KJS editorMgr;       // EditorMgr_KJS ��ũ��Ʈ ����

    void Start()
    {
        if (panelPage == null || content == null) return;

        // �ʱ� ������ �� ����
        pageWidth = panelPage.GetComponent<RectTransform>().rect.width;
    }

    // ���ο� ������ ���� (���������� "�ؽ�Ʈ �ڽ� ���� ��ư" ����)
    public void CreatePage()
    {
        if (panelPage == null || content == null) return;

        // �� ������ ����
        GameObject newPage = Instantiate(panelPage, content);
        pageCount++;

        // "�ؽ�Ʈ �ڽ� ���� ��ư" ��������
        Button btn_TextBox = newPage.transform.Find("btn_TextBox")?.GetComponent<Button>();
        if (btn_TextBox != null)
        {
            // �ش� �������� �ؽ�Ʈ �ڽ� ���� �̺�Ʈ ����
            btn_TextBox.onClick.AddListener(() =>
            {
                CreateTextBox(newPage.transform);  // �ش� �������� �ؽ�Ʈ �ڽ� �߰�
            });
        }
        else
        {
            Debug.LogError("�������� '�ؽ�Ʈ �ڽ� ���� ��ư'�� �����ϴ�.");
        }

        // "������ ���� ��ư" ��������
        Button deleteButton = newPage.transform.Find("btn_Delete")?.GetComponent<Button>();
        if (deleteButton != null)
        {
            deleteButton.onClick.AddListener(() =>
            {
                RemovePage(newPage);  // �ش� ������ ����
            });
        }

        UpdateScrollbarSteps();
        UpdateContentWidth();
    }

    // Ư�� �������� �ؽ�Ʈ �ڽ��� �����ϴ� �޼���
    public void CreateTextBox(Transform parent)
    {
        // �ؽ�Ʈ �ڽ� �ν��Ͻ� ����
        GameObject newTextBox = Instantiate(textBox, parent);

        // ������ �ؽ�Ʈ �ڽ����� ��ư ������Ʈ ��������
        Button buttonComponent = newTextBox.GetComponentInChildren<Button>();
        if (buttonComponent != null)
        {
            // ��ư Ŭ�� �� toolManager�� editorMgr ����
            buttonComponent.onClick.AddListener(toolManager.OnClickTogglePanel);
            buttonComponent.onClick.AddListener(() =>
            {
                if (editorMgr != null)
                {
                    editorMgr.SetInputFieldTextFromButton(buttonComponent);
                }
                else
                {
                    Debug.LogError("EditorMgr_KJS�� �Ҵ���� �ʾҽ��ϴ�.");
                }
            });
        }
        else
        {
            Debug.LogError("�ؽ�Ʈ �ڽ� �����տ� Button ������Ʈ�� �����ϴ�.");
        }
    }

    // ������ ���� �޼���
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

    // ������ �� ������Ʈ
    private void UpdateContentWidth()
    {
        if (content == null) return;

        RectTransform contentRect = content.GetComponent<RectTransform>();
        float newWidth = pageWidth * pageCount;
        contentRect.sizeDelta = new Vector2(newWidth, contentRect.sizeDelta.y);
    }

    // ��ũ�� ��ġ ����
    public void OnEndDrag()
    {
        if (pageCount <= 1) return;

        float normalizedPosition = scrollRect.horizontalNormalizedPosition;
        float targetPage = Mathf.Round(normalizedPosition * (pageCount - 1));
        float targetPosition = targetPage / (pageCount - 1);
        StartCoroutine(SmoothScrollTo(targetPosition));
    }

    // ��ũ���� �ε巴�� �̵���Ű�� �ڷ�ƾ
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

    // ��ũ�ѹ� �ܰ� ������Ʈ
    private void UpdateScrollbarSteps()
    {
        if (horizontalScrollbar != null)
        {
            horizontalScrollbar.numberOfSteps = Mathf.Max(1, pageCount);
        }
    }
}