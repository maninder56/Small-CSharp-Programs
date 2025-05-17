using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsOnConsoleGraph.Model; 

public static class Message
{
    private static readonly ConsoleColor currentColor = Console.ForegroundColor; 

    public static ConsoleColor Color { get; private set; }

    public static StringBuilder Content { get; private set; } = new StringBuilder();

    public static bool MessagePending { get; private set; } = false; 

    public static void AddMessage(MessageType type, string message)
    {
        Content.Append(message);

        Color = type switch
        {
            MessageType.Warning => ConsoleColor.Yellow,
            MessageType.Error => ConsoleColor.Red,
            _ => currentColor
        }; 

        MessagePending = true;
    }

    public static void ClearMessage()
    {
        if (MessagePending)
        {
            Color = currentColor;
            Content.Clear();
            MessagePending = false;
        }
    }
}


