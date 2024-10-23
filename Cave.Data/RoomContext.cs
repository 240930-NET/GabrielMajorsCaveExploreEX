using Cave.Models;
using Microsoft.EntityFrameworkCore;

namespace Cave.Data;

public class RoomContext : DbContext
{
    public RoomContext() : base(){}
    public RoomContext(DbContextOptions<RoomContext> options) : base(options) {}

    public DbSet<Room> Rooms {get; set;}
}
