using Cave.Models;

namespace Cave.Data.Repositories;
public interface IRoomRepo {
    // Get add Room
    public List<Room> GetAllRooms();

    //Get Rooms by Name
    public Room ?GetRoomByName(string name);

    //Add Room
    public void addRoom(Room room);

    //Delete Room
    public void deleteRoom(Room room);

    //Update Room
    public void updateRoom(Room room);
}