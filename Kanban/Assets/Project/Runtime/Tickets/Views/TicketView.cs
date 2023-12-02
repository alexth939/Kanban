using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Tickets
{
    public class TicketView : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI _titleField;
        [SerializeField] private TextMeshProUGUI _contentField;

        public event Action<TicketView> StartingDrag;

        public void OnClicked()
        {
            Debug.Log($"card: clicked");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // grab card
            Debug.Log($"Drag started");

            StartingDrag?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // draw card at cursor position
            //Debug.Log($"Draging");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Highlight
        }

        public void SetTitle(string text) => _titleField.SetText(text);

        public void SetContent(string text) => _contentField.SetText(text);

        public void OnPointerExit(PointerEventData eventData)
        {
            // Dim
        }

        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }
    }
}
