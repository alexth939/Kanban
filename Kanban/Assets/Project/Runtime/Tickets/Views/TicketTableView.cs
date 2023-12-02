using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tickets
{
    public class TicketTableView : MonoBehaviour, ITicketTable
    {
        private readonly List<TicketGroupView> _ticketListCollection = new();

        public event Action<TicketGroupView> PointerEnteredList;
        public event Action<TicketGroupView> PointerExitedList;

        public void DestroyContent()
        {
            foreach(Transform child in this.transform)
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
            listView.transform.SetParent(this.transform);
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
