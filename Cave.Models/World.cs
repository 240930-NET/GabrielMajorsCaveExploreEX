using System.Xml.Serialization;
﻿using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Cave.Models;

namespace Cave.Models;

public sealed class World
{
    public List<Item> Inventory {get;} = [];
    public Dictionary<string, Room> Map {get;} = [];
    public static readonly World instance = new();
    public Room ?CurrentRoom {
        get ;
        set ;
    }
    private World() {}

    private static void Move(string path)
    {
        /*
        if (instance.CurrentRoom != null)
        {
            
            if (instance.CurrentRoom.interactables.ContainsKey(path))
            {
                Interactable potentialExit = instance.CurrentRoom.interactables[path];
                if (potentialExit is Exit)
                    instance.CurrentRoom.interactables[path].Action();
                else
                    Console.WriteLine("This is not an exit.");
            } 
            else
                Console.WriteLine("Please enter a valid path");
        }
        else
            Console.WriteLine("You are not in a room to move");

        return;
        */
    }

    private static void Pickup(string foundObj)
    {/*
        if (instance.CurrentRoom != null)
        {
            if (instance.CurrentRoom.interactables.ContainsKey(foundObj))
            {
                Interactable potentialItem = instance.CurrentRoom.interactables[foundObj];
                if (potentialItem is Item)
                {
                    potentialItem.Action();
                    instance.CurrentRoom.interactables.Remove(foundObj);
                }
                else
                    Console.WriteLine("This is not an item.");
            }
            else
                Console.WriteLine("Please enter a valid Item");
        }
        else
            Console.WriteLine("You are not in a room to find anything");
*/
        return;
    }
    private static void DisplayInventory()
    {
        Console.WriteLine("Current inventory: \n");
        foreach(Item invIt in instance.Inventory)
        {
            Console.WriteLine(invIt.Name); 
            Console.WriteLine(invIt.Description + "\n");
        }
    }

    public static string CombineStrList(List<string> brokenString)
    {
        return brokenString.Aggregate((com1, com2) => com1 + ' ' + com2);
    }
    private static List<string> validCommands = ["Help", "Move", "Inventory", "Pickup", "Talk", "Use", "Exit", "Reset"];
    public static List<string> getValidCommands() => validCommands;
    public static void PrintCommandList()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine(
            "   Help\n   Move {Door Name}\n   Inventory\n   Pickup {Item Name}\n   Talk {Person}\n   Use {First Item} {Second Item}\n   Exit\n   Reset\n"
            );
    }
    public static void SaveData()
    {
        using(StreamWriter writer = new StreamWriter("AdventureSave.json"))
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                IncludeFields = true,
                WriteIndented = true
            };
            writer.WriteLine(JsonSerializer.Serialize(instance.Inventory, options));
            writer.WriteLine(JsonSerializer.Serialize(instance.Map, options));
            writer.WriteLine(JsonSerializer.Serialize(instance.CurrentRoom, options));
        }
    }
    public static void LoadData()
    {
        //Inventory = JsonSerializer.Deserialize<List<Item>>(jsonSave);
        using(StreamReader sr = new StreamReader("AdventureSave.json"))
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                IncludeFields = true
            };

            string ?tempData = "";
            if ((tempData = sr.ReadLine()) != null)
            {
                List<Item> invTemp = JsonSerializer.Deserialize<List<Item>>(tempData, options) ?? [];
                instance.Inventory.Clear();
                foreach(Item ?tempItem in invTemp)
                {
                    if (tempItem != null)
                        instance.Inventory.Add(tempItem);
                }
                    
            }

            if ((tempData = sr.ReadLine()) != null)
            {
                Dictionary<string, Room> tempMap = JsonSerializer.Deserialize<Dictionary<string, Room>>(tempData, options) ?? [];
                instance.Map.Clear();
                foreach(KeyValuePair<string, Room> ?tempLocation in tempMap)
                {
                    if (tempLocation != null)
                        instance.Map.Add(tempLocation.Value.Key, tempLocation.Value.Value);
                }
                    
            }

            if ((tempData = sr.ReadLine()) != null)
            {
                instance.CurrentRoom = JsonSerializer.Deserialize<Room>(tempData, options);
            }
        }
    }
    public void NextCommand(List<string> comms)
    {
        if (comms.Count > 0)
        {
            if (comms[0].Equals("Move", StringComparison.OrdinalIgnoreCase))
            {
                comms.RemoveAt(0);
                if (comms.Count > 0)
                {
                    Move(CombineStrList(comms));
                }
                else
                    Console.WriteLine("Please enter a direction with your move command\n"); 
            }
            else if (comms[0].Equals("Help", StringComparison.OrdinalIgnoreCase))
            {
                PrintCommandList();
            }
            else if (comms[0].Equals("Inventory", StringComparison.OrdinalIgnoreCase))
            {
                DisplayInventory();
            }
            else if (comms[0].Equals("Pickup", StringComparison.OrdinalIgnoreCase))
            {
                comms.RemoveAt(0);
                if (comms.Count > 0)
                {
                    World.Pickup(CombineStrList(comms));
                }
                else
                    Console.WriteLine("Please enter an item to pickup\n"); 
            }
            else if (comms[0].Equals("Reset", StringComparison.OrdinalIgnoreCase))
            {
                ClearData();
                InitializeWorld(instance);
                PrintCommandList();
                CurrentRoom?.WriteAppearance();
            }
            else if(comms[0].Equals("Exit", StringComparison.OrdinalIgnoreCase)) 
            {}
            else
            {
                Console.WriteLine("Please enter a valid command");
            }
        }
    }

    public void ClearData()
    {
        Inventory.Clear();
        Map.Clear();
    }
     public static void InitializeWorld(World world)
    {
        /*
        world.Map.Add("Start", new Room(
            "Start", 
            "You wake up in a dark cave with nothing on your person but a set of worn shirt and matching pants. The walls echo an eerie dripping, despite the room being dry and warm. "
            + "Between the crevices of the cave is a blue clay like substance that rubs off to the touch. You notice because of where you were laying, it has already marked your clothes."
            , []
        ));
        world.CurrentRoom = world.Map["Start"];
        world.Map["Start"].interactables.Add(
            "Candle",
            new Item("Candle", "A red candle in a in a bronze cup holder sits in the corner unlit.", "You picked up the candle.", "It's a red candle in a bronze cup holder.")
        );

        Room tempRoom = new Room(
            "WatchRoom",
            "You continue down the cave, passing wooden chairs and tapastries depicting beings from what you assume to be some cultures epics or legends."
            + " Some seem horrorfics, some triumphant. As you continue further, the cave gives way to porceline tile floors. Man made wooden "
            + "struts extend from the floors and up along the walls till they touch the ceiling. This part of the cave was clearly... not natural..."
            + "The hallway eventually clears to a room.",
            []
        );

        tempRoom.interactables.Add("Matches",
            new Item(
                "Matches", "A box of matches sits on a wooden table by the wall.", "You take the matches", "Surprisingly modern"
            )
        );

        tempRoom.interactables.Add("Book",
            new Item(
                "Book", "On the table sits an ominous looking text, it's flipped to a page with various symbols you can't identify.", 
                "You shut the book and carry it under your arm.", 
                "The pages are filled with an indescribable language, you can't decipher the maing behind any images either."
            )
        );

        tempRoom.interactables.Add("Bucket",
            new Item(
                "Bucket of Water", 
                "There's a bucket in the corner collecting water from the stalagtite ceiling, seems that's where the dripping sound was from", 
                "You pickup the bucket by the handle, being careful not to spill the water.", 
                "It's a bucket of cave ceilign water."
            )
        );

        tempRoom.interactables.Add("Dark Hallway",
            new Exit(
                "Dark Hallway", 
                "A dark coridoor extends from the opposite side of the room, you can't make out any details, but a light humming sound can be heard from its depths.", 
                "You don't want to travel right now.", new Room("","", [])
            )
        );
        
        world.CurrentRoom.interactables.Add(
            "Jail Door",
            new Exit(
                "Jail Door",
                "Further into the cave, you find a row of jail bars blocking the path, with and iron door embeded into the middle.",
                "The lock on the door is old and rusted, as well as much of the door itself, your easilly able to kick it aside",
                tempRoom
            )
        );*/
    }
}