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

        public int OriginalSiblingIndex { get; private set; }

        public int TargetSiblingIndex { get; set; }
        //private int _targetSiblingIndex;
        //public int TargetSiblingIndex
        //{
        //    get => _targetSiblingIndex;
        //    set
        //    {
        //        UnityEngine. Debug.Log($"setting sibling index to: {value}");
        //        _targetSiblingIndex = value;
        //    }
        //}

        public void Start(TicketView drivenTicket)
        {
            Stage = DraggingStage.Dragging;
            DrivenTicket = drivenTicket;
            TicketHolder = drivenTicket.Holder;
            PotentialReciever = drivenTicket.Holder;
            OriginalSiblingIndex = drivenTicket.transform.GetSiblingIndex();
            TargetSiblingIndex = OriginalSiblingIndex;
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
