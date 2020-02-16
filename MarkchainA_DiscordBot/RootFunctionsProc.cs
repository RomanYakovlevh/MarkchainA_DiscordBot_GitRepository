using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkchainA_DiscordBot
{
    class RootFunctionsProc
    {
        static Commands.spaceHolder rootFunctions = new Commands.spaceHolder("RootSpace", @"ASIA22#0234");

        public static void BeginRoot()
        {
            rootFunctions.functions.Add(new Commands.functionHolder(
                "Add",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                    $"Y"
                },
                "it is function adout addition X to Y",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Divide",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                    $"Y"
                },
                "it is function adout division Y from X",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Multiply",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                    $"Y"
                },
                "it is function adout multiping X to Y",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Separate",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                    $"Y"
                },
                "it is function adout separating X on Y degree",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Pow",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                    $"Y"
                },
                "it is function adout multiplying X to X in Y times",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Root",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                    $"Y"
                },
                "Root in RootSpace, hehe",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Equal",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                    $"Y"
                },
                "it is function adoul cheking X with Y to equality",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Notequal",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                    $"Y"
                },
                "it is function adoul cheking X with Y to non-equality",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "More",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                    $"Y"
                },
                "X > Y",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Less",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                    $"Y"
                },
                "X < Y",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "And",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                    $"Y"
                },
                "X & Y is true (not 0)",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Or",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                    $"Y"
                },
                "||",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Xor",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                    $"Y"
                },
                "^",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "If",
                Commands.privacityType.overall,
                Commands.functionType.line,
                new string[]
                {
                    $"Condition",
                    $"ThanFunction",
                    $"ElseFunction"
                },
                "If..Than...Else",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Than",
                Commands.privacityType.overall,
                Commands.functionType.line,
                new string[]
                {
                    $"ThanFunction",
                },
                "If..Than...Else",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Else",
                Commands.privacityType.overall,
                Commands.functionType.line,
                new string[]
                {
                    $"ElseFunction",
                },
                "If..Than...Else",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Print",
                Commands.privacityType.overall,
                Commands.functionType.empty,
                new string[]
                {
                    $"X",
                },
                "Printing anything",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
                "Negation",
                Commands.privacityType.overall,
                Commands.functionType.value,
                new string[]
                {
                    $"X",
                },
                "!",
                true
                ));

            rootFunctions.functions.Add(new Commands.functionHolder(
               "Random",
               Commands.privacityType.overall,
               Commands.functionType.value,
               new string[]
               {
                    $"X",
                    $"Y"
               },
               "Generate a random number behind 0 and 1",
               true
               ));

            Program.allSpaces.Add(rootFunctions);
        }

        public static async Task<string> ProcessRoot(string funcName, string[] inputValues, SocketCommandContext context)
        {
            switch (funcName)
            {
                case "Print":
                    try
                    {
                        Console.WriteLine(inputValues[0]);
                        await context.Channel.SendMessageAsync(inputValues[0]);
                    }
                    catch (Exception)
                    {

                    }
                    return "0";
                case "Add":
                    {
                        double x = 0;
                        double y = 0;
                        Double.TryParse(inputValues[0], out x);
                        Double.TryParse(inputValues[1], out y);
                        return (x + y).ToString();
                    }
                case "Divide":
                    {
                        double x = 0;
                        double y = 0;
                        Double.TryParse(inputValues[0], out x);
                        Double.TryParse(inputValues[1], out y);
                        return (x - y).ToString();
                    }
                case "Multiply":
                    {
                        double x = 0;
                        double y = 0;
                        Double.TryParse(inputValues[0], out x);
                        Double.TryParse(inputValues[1], out y);
                        return (x * y).ToString();
                    }
                case "Separate":
                    {
                        double x = 0;
                        double y = 0;
                        Double.TryParse(inputValues[0], out x);
                        Double.TryParse(inputValues[1], out y);
                        return (x / y).ToString();
                    }
                case "Pow":
                    {
                        double x = 0;
                        double y = 0;
                        Double.TryParse(inputValues[0], out x);
                        Double.TryParse(inputValues[1], out y);
                        return (Math.Pow(x, y)).ToString();
                    }
                case "Root":
                    {
                        double x = 0;
                        double y = 0;
                        Double.TryParse(inputValues[0], out x);
                        Double.TryParse(inputValues[1], out y);
                        if (x < 0)
                            return (-Math.Pow(-x, 1 / y)).ToString();
                        else
                            return Math.Pow(x, 1 / y).ToString();
                    }
                case "Equal":
                    if (inputValues[0] == inputValues[1])
                        return "1";
                    else
                        return "0";
                case "Notequal":
                    if (inputValues[0] != inputValues[1])
                        return "1";
                    else
                        return "0";
                case "More":
                    {
                        double x = 0;
                        double y = 0;
                        Double.TryParse(inputValues[0], out x);
                        Double.TryParse(inputValues[1], out y);
                        if (x > y)
                            return "1";
                        else
                            return "0";
                    }
                case "Less":
                    {
                        double x = 0;
                        double y = 0;
                        Double.TryParse(inputValues[0], out x);
                        Double.TryParse(inputValues[1], out y);
                        if (x < y)
                            return "1";
                        else
                            return "0";
                    }
                case "And":
                    {
                        double x = 0;
                        double y = 0;
                        Double.TryParse(inputValues[0], out x);
                        Double.TryParse(inputValues[1], out y);
                        if (x != 0 && y != 0)
                            return "1";
                        else
                            return "0";
                    }
                case "Or":
                    {
                        double x = 0;
                        double y = 0;
                        Double.TryParse(inputValues[0], out x);
                        Double.TryParse(inputValues[1], out y);
                        if (x != 0 || y != 0)
                            return "1";
                        else
                            return "0";
                    }
                case "Xor":
                    {
                        double x = 0;
                        double y = 0;
                        Double.TryParse(inputValues[0], out x);
                        Double.TryParse(inputValues[1], out y);
                        if (x != 0 ^ y != 0)
                            return "1";
                        else
                            return "0";
                    }
                case "Negation":
                    {
                        double x = 0;
                        Double.TryParse(inputValues[0], out x);
                        if (x == 0)
                            return "1";
                        else
                            return "0";
                    }
                case "Random":
                    Virion rnd = new Virion();
                    return ((int)rnd.GetRandomV1((int)Convert.ToSingle(inputValues[0]), (int)Convert.ToSingle(inputValues[1]))).ToString();
                default:
                    return "0";
                    
            }
        }
    }
}
