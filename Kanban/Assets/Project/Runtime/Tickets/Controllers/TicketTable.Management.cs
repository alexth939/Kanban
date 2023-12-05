using Object = UnityEngine.Object;

namespace Tickets
{
    public partial class TicketTable
    {
        private TicketTableData _data;
        private TicketTableView _tableView;
        private TicketTableConfiguration _configuration;

        public TicketTable(TicketTableView tableView, TicketTableConfiguration configuration)
        {
            _tableView = tableView;
            _configuration = configuration;
            _currentDraggingSession = new();
            GlobalInputEvents.PointerUp += OnGlobalPointerUp;
            _currentDraggingSession.EndingTicketDrag += OnEndingTicketDrag;
        }

        public void Load(TicketTableData data)
        {
            _tableView.DestroyContent();
            _data = data;
            ExtractDataToView();
        }

        public void Unload()
        {
            _data = null;
            _tableView.DestroyContent();
        }

        private void ExtractDataToView()
        {
            var ticketGroups = _data.GetTicketGroups();

            foreach(TicketGroup group in ticketGroups)
            {
                var listView = Object.Instantiate(_configuration.TicketListPrefab);

                _tableView.AddList(listView);

                listView.SetTitle(group.Title);
                listView.SetMinHeightTarget(_tableView.Viewport);
                listView.PointerEntered += OnPointerEnteredGroup;
                listView.PointerMoved += OnPointerMovedOnCurrentGroup;
                listView.PointerExited += OnPointerExitedGroup;

                foreach(Ticket ticket in group.GetTickets())
                {
                    var ticketView = Object.Instantiate(_configuration.TicketPrefab);

                    ticketView.SetTitle(ticket.Title);
                    ticketView.SetContent(ticket.TextContent);
                    ticketView.StartingDrag += OnStartingTicketDrag;
                    listView.InsertTicket(ticketView);
                }
            }
        }

        private void TransferTicket(TicketView ticket, TicketGroupView reciever, int siblingIndex)
        {
            reciever.InsertTicket(ticket, siblingIndex);
        }
    }
}
