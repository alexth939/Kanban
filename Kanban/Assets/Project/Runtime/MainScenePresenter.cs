using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tickets;

public class MainScenePresenter : ScenePresenter
{
    [SerializeReference] private TicketTableConfiguration _tableConfiguration;
    [SerializeReference] private TicketTableView _tableView;

    private TicketTable _tableController;

    protected override void OnEnteringScene()
    {
        new GameObject("Input Events", typeof(GlobalInputEvents));

        _tableController = new TicketTable(_tableView, _tableConfiguration);

        var tableData = GenerateTicketTable();

        _tableController.Load(tableData);
    }

    private TicketTableData GenerateTicketTable()
    {
        var table = new TicketTableData();

        var group1 = new TicketGroup()
        {
            Id = Guid.NewGuid(),
            Title = "group #1",
            SortingOrderIndex = 0,
        };

        var group2 = new TicketGroup()
        {
            Id = Guid.NewGuid(),
            Title = "group #2",
            SortingOrderIndex = 1,
        };

        group1.AddTicket(new Ticket()
        {
            Title = "Example_1",
            TextContent = "Nice content."
        });

        group1.AddTicket(new Ticket()
        {
            Title = "Example_2",
            TextContent = "Nice content."
        });

        group2.AddTicket(new Ticket()
        {
            Title = "Example_3",
            TextContent = "Nice content."
        });

        group2.AddTicket(new Ticket()
        {
            Title = "Example_4",
            TextContent = "Nice content."
        });

        table.AddGroup(group1);
        table.AddGroup(group2);

        return table;
    }
}
