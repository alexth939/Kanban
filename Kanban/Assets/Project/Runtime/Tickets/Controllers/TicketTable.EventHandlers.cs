using System;
using UnityEngine;

namespace Tickets
{
    public partial class TicketTable
    {
        private readonly DraggingSession _currentDraggingSession;

        private void OnStartingTicketDrag(TicketView ticket, TicketGroupView ticketHolder)
        {
            _currentDraggingSession.Start(ticket, ticketHolder);
            ticket.gameObject.SetActive(false);
            ticketHolder.ShowPlaceSelector(ticket.transform.GetSiblingIndex());
        }

        private void OnGlobalPointerUp()
        {
            if(_currentDraggingSession.Stage is DraggingStage.Dragging)
                _currentDraggingSession.End();
        }

        private void OnEndingTicketDrag()
        {
            var drivenTicket = _currentDraggingSession.DrivenTicket;
            var holder = _currentDraggingSession.TicketHolder;
            var reciever = _currentDraggingSession.PotentialReciever;
            int targetSiblingIndex = _currentDraggingSession.TargetSiblingIndex;

            TransferTicket(drivenTicket, holder, reciever, targetSiblingIndex);
            drivenTicket.gameObject.SetActive(true);
            reciever.HidePlaceSelector();
        }

        private void OnPointerEnteredGroup(TicketGroupView group, Vector2 pointerPosition)
        {
            if(_currentDraggingSession.Stage is DraggingStage.Dragging)
            {
                _currentDraggingSession.PotentialReciever = group;
                _currentDraggingSession.TargetSiblingIndex = group.ComputeDesiredSiblingIndex(pointerPosition);
                group.ShowPlaceSelector(_currentDraggingSession.TargetSiblingIndex);
            }
        }

        private void OnPointerMovedOnCurrentGroup(Vector2 pointerPosition)
        {
            if(_currentDraggingSession.Stage is DraggingStage.Dragging)
            {
                var desiredGroup = _currentDraggingSession.PotentialReciever;
                int desiredSiblingIndex = desiredGroup.ComputeDesiredSiblingIndex(pointerPosition);
                desiredGroup.SetPlaceSelectorPosition(desiredSiblingIndex);
                _currentDraggingSession.TargetSiblingIndex = desiredSiblingIndex;
            }
        }

        private void OnPointerExitedGroup(TicketGroupView group)
        {
            if(_currentDraggingSession.Stage is DraggingStage.Dragging)
            {
                _currentDraggingSession.PotentialReciever = _currentDraggingSession.TicketHolder;
                _currentDraggingSession.TargetSiblingIndex = -1;
                group.HidePlaceSelector();
            }
        }
    }
}
