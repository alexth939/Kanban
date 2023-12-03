using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tickets
{
    public class TicketView : MonoBehaviour, IBeginDragHandler, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _contentField;
        [SerializeField] private TextMeshProUGUI _titleField;

        public event Action<TicketView> StartingDrag;

        public TicketGroupView Holder { get; internal set; }

        public void OnBeginDrag(PointerEventData eventData) => StartingDrag?.Invoke(this);
        //{
        //    // grab card
        //    // Debug.Log($"Drag started");

        //    StartingDrag?.Invoke(this);
        //}

        public void SetContent(string text) => _contentField.SetText(text);

        public void SetTitle(string text) => _titleField.SetText(text);

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"card: clicked");
        }
    }
}
