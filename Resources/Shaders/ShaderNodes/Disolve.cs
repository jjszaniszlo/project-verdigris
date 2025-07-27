using Godot;
using Godot.Collections;
using System;
using static Godot.Shader;

[GlobalClass]
[Tool]
public partial class Disolve : VisualShaderNodeCustom
{
	public override string _GetName()
	{
		return "Disolve";
	}
	public override string _GetCategory()
	{
		return "CustomShaderNodes";
	}
	public override string _GetDescription()
	{
		return "Disolve effect based on a threshold value";
	}

	public override PortType _GetReturnIconType()
	{
		return PortType.Vector4D;
	}

	public override int _GetInputPortCount()
	{
		return 3;
	}

	public override string _GetInputPortName(int port)
	{
		switch (port)
		{
			case 0:
				return "NoiseColor";
			case 1:
				return "PixelColor";
			case 2:
				return "Threshold";
			default:
				throw new ArgumentOutOfRangeException(nameof(port), "Invalid input port index");
		}
	}

	public override PortType _GetInputPortType(int port)
	{
		switch (port)
		{
			case 0:
				return PortType.Vector4D;
			case 1:
				return PortType.Vector4D;
			case 2:
				return PortType.Scalar;
			default:
				throw new ArgumentOutOfRangeException(nameof(port), "Invalid input port index");
		}
	}

	public override int _GetOutputPortCount()
	{
		return 1;
	}

	public override string _GetOutputPortName(int port)
	{
		return "Result";
	}

	public override PortType _GetOutputPortType(int port)
	{
		switch (port)
		{
			case 0:
				return PortType.Vector4D;
			default:
				return PortType.Scalar;
		}
	}

	public override string _GetGlobalCode(Mode mode)
	{
		return @"
		vec4 disolve(vec4 noise_color, vec4 pixel_color, float threshold) {
			if (noise_color.r > threshold) {
				return vec4(0.0, 0.0, 0.0, 0.0);
			} else {
				return pixel_color;
			}
        }";
	}

	public override string _GetCode(Array<string> inputVars, Array<string> outputVars, Mode mode, VisualShader.Type type)
	{
		return outputVars[0] + " = disolve(" + inputVars[0] + ", " + inputVars[1] + ", " + inputVars[2] + ");";
	}
}
