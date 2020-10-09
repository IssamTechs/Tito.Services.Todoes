using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tito.Services.Todoes.Core.Entities;
using Tito.Services.Todoes.Core.ValueObjects;

namespace Tito.Services.Todoes.Infrastructure.Data
{
    public class TodoContext: DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().HasKey(x => x.Id);
            modelBuilder.Entity<Todo>().OwnsOne(x => x.Title, r => r.Property(p=>p.Value).HasColumnName("Title")); 
            modelBuilder.Entity<Todo>().OwnsOne(x => x.Description, r => r.Property(p => p.Value).HasColumnName("Description"));
            modelBuilder.Entity<Todo>().Property(x=>x.State).HasColumnName("State");
            modelBuilder.Entity<Todo>().Property(x=>x.Priority).HasColumnName("Priority");
            modelBuilder.Entity<Todo>().Property(x=>x.CreatedAt).HasColumnName("CreatedAt");
            //modelBuilder.Entity<Todo>().HasData(_todoes);
        }


        public DbSet<Todo> Todoes { get; set; }


        public static List<Todo> _todoes => new List<Todo>
        {
           new Todo(Guid.NewGuid(), new Title("Todo #1"), new Description("Todo #1 Description"), Priority.LOW, State.NEW, DateTimeOffset.UtcNow),
           new Todo(Guid.NewGuid(), new Title("Todo #2"), new Description("Todo #2 Description"), Priority.LOW, State.NEW, DateTimeOffset.UtcNow)
        };
    }
}
