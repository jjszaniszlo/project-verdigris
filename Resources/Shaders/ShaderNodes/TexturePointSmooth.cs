using Godot;
using Godot.Collections;
using System;
using static Godot.Shader;

[Tool]
[GlobalClass]
public partial class TexturePointSmooth : VisualShaderNodeCustom
{
	public override string _GetName()
	{
		return "TexturePointSmooth";
	}
	public override string _GetCategory()
	{
		return "CustomShaderNodes";
	}
	public override string _GetDescription()
	{
		return "Smooths texture sampling at point size, useful for pixel art.";
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
				return "Sampler2D";
			case 1:
				return "UV";
			case 2:
				return "PixelSize";
			default:
				throw new ArgumentOutOfRangeException(nameof(port), "Invalid input port index");
		}
	}

	public override PortType _GetInputPortType(int port)
	{
		switch (port)
		{
			case 0:
				return PortType.Sampler;
			case 1:
				return PortType.Vector2D;
			case 2:
				return PortType.Vector2D;
			default:
				throw new ArgumentOutOfRangeException(nameof(port), "Invalid input port index");
		}
	}

	public override int _GetOutputPortCount()
	{
		return 4;
	}

	public override string _GetOutputPortName(int port)
	{
		return "Result";
	}

	public override PortType _GetOutputPortType(int port)
	{
		return PortType.Vector4D;
	}

	public override string _GetGlobalCode(Mode mode)
	{
		return @"
		vec4 texturePointSmooth(sampler2D smp, vec2 uv, vec2 pixel_size)
		{
			vec2 ddx = dFdx(uv);
			vec2 ddy = dFdy(uv);
			vec2 lxy = sqrt(ddx * ddx + ddy * ddy);

			vec2 uv_pixels = uv / pixel_size;

			vec2 uv_pixels_floor = round(uv_pixels) - vec2(0.5f);
			vec2 uv_dxy_pixels = uv_pixels - uv_pixels_floor;

			uv_dxy_pixels = clamp((uv_dxy_pixels - vec2(0.5f)) * pixel_size / lxy + vec2(0.5f), 0.0f, 1.0f);

			uv = uv_pixels_floor * pixel_size;

			return textureGrad(smp, uv + uv_dxy_pixels * pixel_size, ddx, ddy);
        }";
	}

	public override string _GetCode(Array<string> inputVars, Array<string> outputVars, Mode mode, VisualShader.Type type)
	{
		return outputVars[0] + " = texturePointSmooth(" + inputVars[0] + ", " + inputVars[1] + ", " + inputVars[2] + ");";
	}
}
