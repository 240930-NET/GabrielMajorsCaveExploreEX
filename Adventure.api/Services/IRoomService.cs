using Cave.Data;
using Cave.Models;

public interface IRoomService {
    // Get add Room
    public List<Room> ?GetAllRooms();

    //Get Rooms by Name
    public Room ?GetRoomByName(string name);

    //Add new room
    public string AddRoom(Room room);

    //Edit old room
    public Room ?EditRoom(Room room);

    //Delete room
    public string DeleteRoom(String name);
}