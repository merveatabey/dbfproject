using System;
using System.Collections.Generic;

namespace dbfEvent.Data.Models;

public partial class Participant
{
    public int ParticipantId { get; set; }

    public string? ParticipantName { get; set; }

    public int? Age { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Status { get; set; }

    public int? EventId { get; set; }

    public virtual Event? Event { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
