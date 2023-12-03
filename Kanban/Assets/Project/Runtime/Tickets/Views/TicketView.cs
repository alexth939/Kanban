using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tickets
{
    //! Don't remove "IDragHandler" inheritance. Necessary evil for the engine.
    public class TicketView : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _contentField;
        [SerializeField] private TextMeshProUGUI _titleField;

        public event Action<TicketView> StartingDrag;

        public TicketGroupView Holder { get; internal set; }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // grab card
            Debug.Log($"Drag started");

            StartingDrag?.Invoke(this);
        }

        [Obsolete("Necessary evil for the engine.", false)]
        public void OnDrag(PointerEventData eventData)
        { }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"card: clicked");
        }

        public void SetContent(string text) => _contentField.SetText(text);

        public void SetTitle(string text) => _titleField.SetText(text);
    }
}
