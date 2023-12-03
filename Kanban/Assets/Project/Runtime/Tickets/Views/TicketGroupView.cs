using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Tickets
{
    public class TicketGroupView : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
    {
        [SerializeField] private Transform _placeSelector;
        [SerializeField] private Transform _ticketsContainer;
        [SerializeReference] private TextMeshProUGUI _title;

        public event Action TicketDragged;
        public event Action<TicketGroupView, Vector2> PointerEntered;
        public event Action<Vector2> PointerMoved;
        public event Action<TicketGroupView> PointerExited;

        public void SetTitle(string text) => _title.text = text;

        public void InsertTicket(TicketView ticket)
        {
            ticket.Holder = this;
            ticket.transform.SetParent(_ticketsContainer.transform);
            ticket.transform.SetAsLastSibling();
        }

        public void InsertTicket(TicketView ticket, int siblingIndex)
        {
            ticket.Holder = this;
            ticket.transform.SetParent(_ticketsContainer.transform);
            ticket.transform.SetSiblingIndex(siblingIndex);
        }

        [Obsolete("Used by the engine.", false)]
        public void OnPointerEnter(PointerEventData eventData) => PointerEntered?.Invoke(this, eventData.position);

        public void ShowPlaceSelector(int siblingIndex)
        {
            SetPlaceSelectorPosition(siblingIndex);
            _placeSelector.gameObject.SetActive(true);
        }

        public void HidePlaceSelector()
        {
            _placeSelector.gameObject.SetActive(false);
        }

        public void SetPlaceSelectorPosition(int siblingIndex)
        {
            _placeSelector.SetSiblingIndex(siblingIndex);
        }

        [Obsolete("Used by the engine.", false)]
        public void OnPointerMove(PointerEventData eventData) => PointerMoved?.Invoke(eventData.position);

        public int ComputeDesiredSiblingIndex(Vector2 pointerPosition)
        {
            for(int childIndex = 1; childIndex < _ticketsContainer.childCount; childIndex++)
            {
                var childA = _ticketsContainer.GetChild(childIndex - 1);
                var childB = _ticketsContainer.GetChild(childIndex);

                var rectA = childA.GetComponent<RectTransform>().rect;
                var rectB = childB.GetComponent<RectTransform>().rect;

                float topWorldLimit = childA.position.y + rectA.yMax;
                float bottomWorldLimit = childB.position.y + rectB.yMin;

                float averageY = (topWorldLimit + bottomWorldLimit) / 2;

                if(pointerPosition.y > averageY)
                    return childIndex - 1;
            }

            return _ticketsContainer.childCount;
        }

        [Obsolete("Used by the engine.", false)]
        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExited?.Invoke(this);
        }
    }
}
