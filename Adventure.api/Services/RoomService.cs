using Cave.Data;
using Cave.Data.Repositories;
using Cave.Models;

namespace Adventure.api;

public class RoomService : IRoomService {
    private readonly IRoomRepo _roomRepo;

    public RoomService(IRoomRepo roomRepo)
    {
        _roomRepo = roomRepo;
    }

    // Get add Room
    public List<Room> ?GetAllRooms(){
        List<Room> result = _roomRepo.GetAllRooms();
        if(result.Count == 0){
            return null;
        }
        else{
            return result;
        }
    }

    //Get Rooms by Name
    public Room ?GetRoomByName(string name){
        Room ?room = _roomRepo.GetRoomByName(name);
        if (room != null)
        {
            return room;
        }
        else{
            return null;
        }
    }

    //Add new room
    public string AddRoom(Room room){
        if (room.Name != null && room.Description != null)
        {
            return $"${room.Name} was added to the level.";
        }
        else{
            throw new Exception("Invalid Room. Please validate name and description.");
        }
    }

    //Edit old room
    public Room EditRoom(Room room){
        Room ?searchedRoom = _roomRepo.GetRoomByName(room.Name);
        if (searchedRoom != null)
        {
            if (room.Name != null && room.Description != null)
            {
                searchedRoom.Name = room.Name;
                searchedRoom.Description = room.Description;
                searchedRoom.interactables = room.interactables;
                return searchedRoom;
            }
            else{
                throw new Exception("Invalid Room. Please validate name and description.");
            }
        }
        throw new Exception("Invalid Room. Does not exist.");
    }

    //Delete room
    public string DeleteRoom(String name){
        Room ?searchedRoom = _roomRepo.GetRoomByName(name);
        if (searchedRoom != null)
        {
            _roomRepo.deleteRoom(searchedRoom);
            return $"The ${name} was deleted.";
        }
        else{
            throw new Exception($"No room with name ${name} does not exist.");
        }
    }
}