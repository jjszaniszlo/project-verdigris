using Godot;
using Godot.Collections;
using System;
using static Godot.Shader;

[Tool]
[GlobalClass]
public partial class PixelizeUVNode : VisualShaderNodeCustom
{
    public override string _GetName()
    {
        return "PixelizeUV";
    }
    public override string _GetCategory()
    {
        return "CustomShaderNodes";
    }
    public override string _GetDescription()
    {
        return "Quantize coordinates based on pixel size";
    }

    public PixelizeUVNode()
    {
        SetInputPortDefaultValue(1, new Vector2(0.1f, 0.1f));
    }

    public override PortType _GetReturnIconType()
    {
        return PortType.Vector2D;
    }

    public override int _GetInputPortCount()
    {
        return 2;
    }

    public override string _GetInputPortName(int port)
    {
        switch (port)
        {
            case 0:
                return "UV";
            case 1:
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
                return PortType.Vector2D;
            case 1:
                return PortType.Vector2D;
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
        return PortType.Vector2D;
    }

    public override string _GetGlobalCode(Mode mode)
    {
        return @"
        float floatPixelate1(float f, float amount) {
            return floor(f * amount) / amount;
        }

        vec2 pixelate_uv(vec2 P, vec2 pixel_size) {
            float x_amount = 1.0 / pixel_size.x;
            float y_amount = 1.0 / pixel_size.y;
            return vec2(floatPixelate1(P.x, x_amount), floatPixelate1(P.y, y_amount));
        }";
    }

    public override string _GetCode(Array<string> inputVars, Array<string> outputVars, Mode mode, VisualShader.Type type)
    {
        return outputVars[0] + " = pixelate_uv(" + inputVars[0] + ".xy, " + inputVars[1] + ".xy);";
    }
}
