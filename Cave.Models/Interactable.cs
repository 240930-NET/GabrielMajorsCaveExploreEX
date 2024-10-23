namespace Cave.Models;

public abstract class Interactable
{
    public string Name {get; set;}
    public string AppearanceText {get; set;} 
    public string InteractionText {get; set;} 
    public Interactable(string Name, string appearanceText, string interactionText)
    {
        this.Name = Name;
        this.AppearanceText = appearanceText;
        this.InteractionText = interactionText;
    }

    public void Interact()
    {
        Console.WriteLine(InteractionText);
        Action();
    }
    public abstract void Action();
}