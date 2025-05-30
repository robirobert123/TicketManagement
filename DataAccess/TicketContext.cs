protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    
    // Configure cascade delete for Ticket-Comment relationship
    modelBuilder.Entity<Ticket>()
        .HasMany(t => t.Comments)
        .WithOne(c => c.Ticket)
        .HasForeignKey(c => c.TicketID)
        .OnDelete(DeleteBehavior.Cascade);
        
    // Other configurations...
}