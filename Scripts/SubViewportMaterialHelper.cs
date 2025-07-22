using Godot;
using System;

public partial class SubViewportMaterialHelper : SubViewportContainer
{
	public override void _Ready()
	{
		Globals.Instance.SubViewportShaderMaterial = Material as ShaderMaterial;
	}
}
