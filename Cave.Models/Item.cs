using System.Text.Json.Serialization;
namespace Cave.Models;

public class Item : Interactable{
    [JsonInclude]
    public string Description {get; set;}
    [JsonConstructor]
    public Item(string Name, string AppearanceText, string InteractionTxt, string Description) : base(Name, AppearanceText, InteractionTxt)
    {
        this.Description = Description;
    }

    public override void Action()
    {
        Console.WriteLine(InteractionText);
        World.instance.Inventory.Add(this);
    }
}