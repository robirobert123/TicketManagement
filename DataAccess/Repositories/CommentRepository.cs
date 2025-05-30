public IEnumerable<Comment> GetCommentsByTicketId(int ticketId)
{
    return context.Comments.Where(c => c.TicketID == ticketId).ToList();
}
