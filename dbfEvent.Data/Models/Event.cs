using System;
using System.Collections.Generic;

namespace dbfEvent.Data.Models;

public partial class Event
{
    public int EventId { get; set; }

    public string? EventName { get; set; }

    public string? Description { get; set; }

    public string? EventType { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? FinishDate { get; set; }

    public string? Location { get; set; }

    public int? ParticipantsLimit { get; set; }

    public virtual ICollection<EventEmployeeAssignment> EventEmployeeAssignments { get; set; } = new List<EventEmployeeAssignment>();

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
