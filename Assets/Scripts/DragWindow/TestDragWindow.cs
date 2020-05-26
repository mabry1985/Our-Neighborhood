using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestDragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{

    [SerializeField] private RectTransform dragRectTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color backgroundColor;



    private void Awake()
    {
        if (dragRectTransform == null)
        {
            dragRectTransform = transform.parent.GetComponent<RectTransform>();
        }

        if(canvas == null)
        {
            Transform testCanvasTransform = transform.parent;
            while (testCanvasTransform != null)
            {
                canvas = testCanvasTransform.GetComponent<Canvas>();
                if (canvas != null)
                {
                    break;
                }
                testCanvasTransform = testCanvasTransform.parent;
            }
        }

        if(backgroundImage == null)
        {
            Transform testBackgroundImage = transform.parent;
            while (testBackgroundImage != null)
            {
                backgroundImage = testBackgroundImage.GetComponentInChildren<Image>();
                if (backgroundImage != null)
                {
                    break;
                }
            }
            backgroundColor = backgroundImage.color;
        }

        if(backgroundColor == null)
        {
            _ = backgroundImage.color;
        }

        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        backgroundColor.a = .1f;
        backgroundImage.color = backgroundColor;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        backgroundColor.a = 1f;
        backgroundImage.color = backgroundColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragRectTransform.SetAsLastSibling();
    }
}
