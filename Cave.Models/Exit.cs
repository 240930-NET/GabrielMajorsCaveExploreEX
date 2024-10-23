namespace Cave.Models;

public class Exit(string Name, string appearanceText, string interactionTxt, Room next) : Interactable(Name, appearanceText, interactionTxt)
{
    private Room next { get; set; } = next;

    public override void Action()
    {
        World.instance.CurrentRoom = next;
        next.WriteAppearance();
    }
}