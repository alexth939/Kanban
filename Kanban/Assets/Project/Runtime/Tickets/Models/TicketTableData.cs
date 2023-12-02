using System;
using System.Collections.Generic;

namespace Tickets
{
    public class TicketTableData : ITicketTable
    {
        private List<TicketGroup> _groupsCollection;

        public TicketTableData()
        {
            _groupsCollection = new();
        }

        internal IEnumerable<TicketGroup> GetTicketGroups() => _groupsCollection;

        internal void AddGroup(TicketGroup group) => _groupsCollection.Add(group);
    }
}
