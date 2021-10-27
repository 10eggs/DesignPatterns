using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Builder.Exercise
{
    public class FacadeBuilderExercise
    {
        public static void Demo()
        {
            Ticket ticketBuilder = new TicketBuilder()
                .CreatedBy
                    .Author("Tomek")
                .WithContent
                    .TicketText("Maly to obzartus")
                .AssignedTo
                    .QaName("Maly gym");

            Console.WriteLine(ticketBuilder);
        }
    }

    public class Ticket
    {
        public string Author { get; set; }
        public TicketBody TicketBody { get; set; }
        public string AssignedTo { get; set; }

        public override string ToString()
        {
            return $"This is the ticket created by {Author}, at {TicketBody.CreatedAt}.\nTicket text: \t{TicketBody.Text}\nAssigned to:{AssignedTo}";
        }
    }

    public struct TicketBody
    {
        public TicketBody(string txt)
        {
            CreatedAt = DateTime.UtcNow;
            Text = txt;
        }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
    }

    public class TicketBuilder
    {
        protected Ticket ticket = new Ticket();

        public TicketContentBuilder WithContent => new TicketContentBuilder(ticket);
        public TicketAuthorBuilder CreatedBy => new TicketAuthorBuilder(ticket);
        public TicketAssignTo AssignedTo => new TicketAssignTo(ticket);

        public static implicit operator Ticket(TicketBuilder tb)
        {
            return tb.ticket;
        }

    }

    public class TicketAuthorBuilder:TicketBuilder
    {
        public TicketAuthorBuilder(Ticket t)
        {
            ticket = t;
        }

        public TicketAuthorBuilder Author(string author)
        {
            this.ticket.Author = author;
            return this;
        }
    }

    public class TicketContentBuilder : TicketBuilder
    {
        public TicketContentBuilder(Ticket t)
        {
            ticket = t;
        }

        public TicketContentBuilder TicketText(string text)
        {
            ticket.TicketBody = new TicketBody(text);
            return this;
        }
    }

    public class TicketAssignTo : TicketBuilder
    {
        public TicketAssignTo(Ticket t)
        {
            ticket = t;
        }

        public TicketAssignTo QaName(string name)
        {
            ticket.AssignedTo = name;
            return this;
        }
    }


}
