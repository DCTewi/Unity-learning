using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Canvas canvas;
    private RectTransform rtrans;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rtrans = GetComponent<RectTransform>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    private void Start()
    {
        canvas = transform.GetComponentInParent<Canvas>();
        Debug.Log("Get canvas", canvas);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag", gameObject);
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rtrans.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Debug.Log("OnEndDrag", gameObject);
    }
}
