using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tickets
{
    public class TicketTableView : MonoBehaviour, ITicketTable
    {
        private readonly List<TicketGroupView> _ticketListCollection = new();
        [SerializeReference] private Transform _groupsContainer;

        public event Action<TicketGroupView> PointerEnteredList;
        public event Action<TicketGroupView> PointerExitedList;

        [field: SerializeReference] public RectTransform Viewport { get; private set; }

        public void DestroyContent()
        {
            foreach(Transform child in _groupsContainer)
            {
                Destroy(child.gameObject);
            }
        }

        internal void DestroyContent_Obsolete()
        {
            _ticketListCollection.ForEach(list => Destroy(list.transform));
            _ticketListCollection.Clear();
        }

        public event Action<TicketView> StartingTicketDrag;

        internal void AddList(TicketGroupView listView)
        {
            _ticketListCollection.Add(listView);
            listView.transform.SetParent(_groupsContainer);
        }

        private void OnEndingTicketDrag(DraggingSession session)
        {
            if(session.PotentialReciever != session.TicketHolder)
                session.DrivenTicket.transform.SetParent(session.PotentialReciever.transform);

            session.DrivenTicket.gameObject.SetActive(true);
        }

        private void OnTicketDragged()
        {
        }
    }
}
