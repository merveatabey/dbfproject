using System;
using System.Collections.Generic;

namespace dbfEvent.Data.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string? TicketType { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }

    public int? EventId { get; set; }

    public int? ParticipantId { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Participant? Participant { get; set; }
}
