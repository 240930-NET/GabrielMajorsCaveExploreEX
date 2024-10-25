using Moq;
namespace Cave.TESTS;
using Cave.Data;
using Adventure.api;
using Cave.Models;
using Cave.Data.Repositories;

public class UnitTest1
{
    [Fact]
    public void GetAllRoomsReturnsNullOnEmpty()
    {
        //Arrange
        Mock<IRoomRepo> mockRepo = new();
        RoomService roomService = new(mockRepo.Object);

        List<Room> rmList = [];

        mockRepo.Setup(repo => repo.GetAllRooms()).Returns(rmList);

        //Act
        var result = roomService.GetAllRooms();

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllRoomsReturnsFullList()
    {
        Mock<IRoomRepo> mockRepo = new();
        RoomService roomService = new(mockRepo.Object);

        List<Room> rmList = [
            new Room{Name = "Kitchen"},
            new Room{Name = "Jail"},
            new Room{}
        ];
        
        mockRepo.Setup(repo => repo.GetAllRooms()).Returns(rmList);

        //Act
        var result = roomService.GetAllRooms();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(3,result.Count);
        Assert.Contains(result, e => e.Name!.Equals("Kitchen"));
        Assert.Contains(result, e => e.Name!.Equals("Jail"));
    }

    [Fact]
    public void GetRoomByNameReturnsNull()
    {
        //Arrange
        Mock<IRoomRepo> mockRepo = new();
        RoomService roomService = new(mockRepo.Object);

        Room ?returnRm = null;

        mockRepo.Setup(repo => repo.GetRoomByName("Jail")).Returns(returnRm);

        //Act
        var result = roomService.GetRoomByName("Jail");

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetRoomByNameReturnsRoom()
    {
        //Arrange
        Mock<IRoomRepo> mockRepo = new();
        RoomService roomService = new(mockRepo.Object);

        Room returnRm = new Room{Name="Jail"};

        mockRepo.Setup(repo => repo.GetRoomByName("Jail")).Returns(returnRm);

        //Act
        var result = roomService.GetRoomByName("Jail");

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Jail", result.Name);
    }

    [Fact]
    public void AddRoomSuccessfully()
    {
        //Arrange
        Mock<IRoomRepo> mockRepo = new();
        RoomService roomService = new(mockRepo.Object);

        Room addedRm = new Room{Name="Foyer", Description="A large open room"};
        List<Room> rmList = [];

        mockRepo.Setup(repo => repo.addRoom(addedRm)).Callback<Room>(rmList.Add);

        //Action
        roomService.AddRoom(addedRm);

        //Assert
        Assert.Single(rmList);
        Assert.NotNull(rmList[0]);
        Assert.Equal("Foyer", rmList[0].Name);
    }

    [Fact]
    public void AddRoomPoorly()
    {
        //Arrange
        Mock<IRoomRepo> mockRepo = new();
        RoomService roomService = new(mockRepo.Object);

        Room addedRm = new Room{Name="Foyer"};
        List<Room> rmList = [];
        string exceptionM = "Invalid Room. Please validate name and description.";

        mockRepo.Setup(repo => repo.addRoom(addedRm)).Throws(new Exception(exceptionM));

        try{
            roomService.AddRoom(addedRm);
            Assert.Fail("Exception not thrown");
        }
        catch(Exception e){
            Assert.Equal(exceptionM, e.Message);
        }
    }

    [Fact]
    public void EditRoomSuccessfuly()
    {
        //Arrange
        Mock<IRoomRepo> mockRepo = new();
        RoomService roomService = new(mockRepo.Object);

        Room originalRm = new Room{Name="Foyer", Description="A large open room", interactables=["Cup"]};
        Room replacementRm = new Room{Name="Kitchen", Description="A black and white tiled room", interactables=["Apple"]};

        mockRepo.Setup(repo => repo.updateRoom(It.IsAny<Room>())).Callback<Room>(newRm => originalRm = newRm);
        mockRepo.Setup(repo => repo.GetRoomByName(It.IsAny<String>())).Returns(originalRm);

        roomService.EditRoom(replacementRm);

        Assert.Equal(replacementRm.Name, originalRm.Name);
        Assert.Equal(replacementRm.Description, originalRm.Description);
        Assert.Equal(replacementRm.interactables, originalRm.interactables);
    }

    [Fact]
    public void EditRoomUnSuccessfuly()
    {
        //Arrange
        Mock<IRoomRepo> mockRepo = new();
        RoomService roomService = new(mockRepo.Object);

        Room replacementRm = new Room{};
        string exceptionM = "Invalid Room. Does not exist.";

        mockRepo.Setup(repo => repo.updateRoom(It.IsAny<Room>())).Throws(new Exception(exceptionM));

        try
        {
            roomService.EditRoom(replacementRm);
            Assert.Fail("Exception not thrown");
        }
        catch(Exception e){
            Assert.Equal(exceptionM, e.Message);
        }
    }

    [Fact]
    public void DeleteRoomSuccessfuly()
    {
        //Arrange
        Mock<IRoomRepo> mockRepo = new();
        RoomService roomService = new(mockRepo.Object);

        Room roomToRemove = new Room{Name="Foyer", Description="A large open room"};
        List<Room> rmList = [roomToRemove];

        mockRepo.Setup(repo => repo.deleteRoom(It.IsAny<Room>())).Callback<Room>(x => rmList.Remove(x));
        mockRepo.Setup(repo => repo.GetRoomByName(It.IsAny<String>())).Returns(roomToRemove);

        //Action
        roomService.DeleteRoom(roomToRemove.Name);

        //Assert
        Assert.Empty(rmList);
    }
}