using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipePage : MonoBehaviour, IEndDragHandler
{
    [Header("Elements")]
    [SerializeField] private RectTransform levelPageRect;

    [Header("Settings")]
    [SerializeField] private int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] private Vector3 pageStep;
    [SerializeField] private float tweenTime;
    [SerializeField] private LeanTweenType tweenType;
    float dragThreshold;
    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPageRect.localPosition;
        dragThreshold = Screen.height / 15;
    }
    private void NextPage()
    {
        if(currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }
    private void PreviosPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }
    private void MovePage()
    {
        levelPageRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Mathf.Abs(eventData.position.y - eventData.pressPosition.y) > dragThreshold)
        {
            if (eventData.position.y > eventData.pressPosition.y ) NextPage();
            else PreviosPage();
        }
        else
        {
            MovePage();
        }
    }
}
