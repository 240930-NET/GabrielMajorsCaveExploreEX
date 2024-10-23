using Microsoft.EntityFrameworkCore;
using Cave.Models;
using Cave.Data.Repositories;

namespace Cave.Data;

public class RoomRepo : IRoomRepo{

    private readonly RoomContext _context;

    public RoomRepo(RoomContext context){
        _context = context;
    }
        // Get add Room
    public List<Room> GetAllRooms(){
        return _context.Rooms.ToList();
    }

    //Get Rooms by Name
    public Room ?GetRoomByName(string name){
        return _context.Rooms.Find(name);
    }

    //Add new Room
    public void addRoom(Room room){
        _context.Rooms.Add(room);
        _context.SaveChanges();
    }

    //Delete Room
    public void deleteRoom(Room room){
        _context.Rooms.Remove(room);
        _context.SaveChanges();
    }

    //Update Room
    public void updateRoom(Room room){
        _context.Rooms.Update(room);
        _context.SaveChanges();
    }
}