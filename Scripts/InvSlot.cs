using Godot;

[Tool]
public partial class InvSlot : Resource
{
	[Export] public InvItem Item;
	[Export] public int Amount;
}
