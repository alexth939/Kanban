using UnityEngine;

namespace Tickets
{
    [CreateAssetMenu()]
    public class TicketTableConfiguration : ScriptableObject
    {
        [field: SerializeReference] public TicketView TicketPrefab { get; private set; }
        [field: SerializeReference] public TicketGroupView TicketListPrefab { get; private set; }
    }
}
