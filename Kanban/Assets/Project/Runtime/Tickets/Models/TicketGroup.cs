using System;
using System.Collections.Generic;

namespace Tickets
{
    public class TicketGroup
    {
        private readonly List<Ticket> _ticketCollection = new();

        public string Title { get; internal set; }
        public Guid Id { get; internal set; }
        public uint SortingOrderIndex { get; internal set; }

        internal IEnumerable<Ticket> GetTickets() => _ticketCollection;

        internal void AddTicket(Ticket ticket) => _ticketCollection.Add(ticket);
    }
}
