namespace Cave.Models;

using System.ComponentModel.DataAnnotations;
using System.Linq;

public class Room
{
    [Key]
    public string Name { get; set; }
    public string ?Description { get; set; }
    public List<string> ?interactables {get; set;}

    public Room(){}
    public Room(string name, string desc, List<string> ?its)
    {
        this.Name = name;
        this.Description = desc;
        this.interactables = its ?? new List<string>();
    }
    public void WriteAppearance()
    {
        /*
        Console.WriteLine(Description);
        List<Interactable> listInts = interactables;
        foreach (Interactable interactable in listInts)
            Console.WriteLine("\n" + interactable.AppearanceText);

        Console.WriteLine("\nYou can interact with the...");
        foreach (Interactable interactable in listInts)
            Console.Write("\t\t" + interactable.Name);

        Console.WriteLine();*/
        return;
    }
}
