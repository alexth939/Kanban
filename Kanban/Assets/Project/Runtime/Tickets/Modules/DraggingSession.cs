using System;

namespace Tickets
{
    public class DraggingSession
    {
        public event Action EndingTicketDrag;

        public DraggingStage Stage { get; private set; } = DraggingStage.Idle;

        public TicketView DrivenTicket { get; set; }

        public TicketGroupView TicketHolder { get; private set; }

        public TicketGroupView PotentialReciever { get; set; }

        //public int TargetSiblingIndex { get; set; }
        private int _targetSiblingIndex;
        public int TargetSiblingIndex
        {
            get => _targetSiblingIndex;
            set
            {
                UnityEngine. Debug.Log($"setting sibling index to: {value}");
                _targetSiblingIndex = value;
            }
        }

        public void Start(TicketView drivenTicket, TicketGroupView ticketHolder)
        {
            DrivenTicket = drivenTicket;
            Stage = DraggingStage.Dragging;
            TicketHolder = ticketHolder;
            PotentialReciever = ticketHolder;
        }

        public void End()
        {
            EndingTicketDrag?.Invoke();
            Stage = DraggingStage.Idle;
            DrivenTicket = null;
            TicketHolder = null;
            PotentialReciever = null;
        }
    }
}
