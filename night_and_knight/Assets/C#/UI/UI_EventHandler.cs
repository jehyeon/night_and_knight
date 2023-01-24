using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * @brief GameObject가 여러 이벤트(클릭, 드래그)를 받을 수 있게 해주는 Class
 */
public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

    /*
     * @brief OnClickHandler에 등록된 함수 모두 실행
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }

    /*
     * @brief OnDragHandler에 등록된 함수 모두 실행
     */
    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
    }
}