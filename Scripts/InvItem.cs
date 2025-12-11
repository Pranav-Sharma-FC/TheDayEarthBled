using Godot;

[Tool]
public partial class InvItem : Resource
{
	[Export] public Texture2D Icon { get; set; }
	[Export] public string Name { get; set; } = "";
} 
