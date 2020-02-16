using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.IO;

namespace MarkchainA_DiscordBot.Commands
{
    public class CompilerA : ModuleBase<SocketCommandContext>
    {
        static char sep = ';';

        [Command("repeat")]
        public async Task Respond(string text)
        {
            await Context.Channel.SendMessageAsync(text);
        }

        [Command("new")]
        public async Task New(string command)
        {
            //examples:
            //>>new space.MySpace
            //>>new value.MyConst.in.MySpace.5
            //>>new value.AddFunc.value.X.value.Y.in.MySpace.(.Add.X.Y.)
            //>>new value_SuperFunc_value_X_value_Y_value_Q_in_MySpace_(_Add_(_AddFunc_X_Y_)_Q_)
            //>>new value_QuantityFiller_value_X_in_MySpace_(_Multiple_X_2_)
            //>>new quantity_Storage1_value_X_i_ MySpace_< When < Equals < IEEE X 2 1 return < QuantityFiller X // by deafult every element in quantity is 0
            //>>new quantity Storage2 value X value Y in MySpace// maybe, will be able to use multidimensional quantities 
            //>>usein MySpace print ( SuperFunc 3 4 ( MyConst ) )
            //Do not forget about case when >>new space   mySpace   (many empty elements in chain between spaces)
            //Create a new space of functions
            string[] chain = command.Split(sep);

            if(chain[0] == "space")
            {

                //if(chain[1] != ":")
                //{
                //    System.Console.WriteLine($"not found \":\" after type reference");
                //    await Context.Channel.SendMessageAsync($"not found \":\" after type reference");
                //
                //    return;
                //}

                try
                {
                    Program.allSpaces.Add(new spaceHolder(chain[1], Context.User.ToString())); //Do not forgot to check names avalibe

                    System.Console.WriteLine($"space {chain[1]} is ceated for user {Context.User.ToString()}");
                    await Context.Channel.SendMessageAsync($"space {chain[1]} is ceated for user {Context.User.ToString()}");
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine($"not avalibe space name or user ID");
                    await Context.Channel.SendMessageAsync($"not avalibe space name or user ID");
                }
            }
            else if(/*chain[0] == "value" || chain[0] == "line" || chain[0] == "empty"*/chain[0] == "let")
            {
                functionType typeOfFunction = functionType.pure;

                //switch(chain[0])
                //{
                //    case "value":
                //        typeOfFunction = functionType.value;
                //        break;
                //    case "line":
                //        typeOfFunction = functionType.line;
                //        break;
                //    case "empty":
                //        typeOfFunction = functionType.empty;
                //        break;
                //    default:
                //        typeOfFunction = functionType.line;
                //        break;
                //}

                string funcName;

                try
                {
                    funcName = chain[1];//Do not forgot to check names avalibe
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine($"not avalibe function name (position: {1})");
                    await Context.Channel.SendMessageAsync($"not avalibe function name (position: {1})");

                    return;
                }

                List<string> inputFunc = new List<string>();

                int i = 2;

                try
                {
                    while (chain[i] != "in") // do not forgot to write while into trycatch!!//Do not forgot to check names avalibe
                    {
                        //if (/*chain[i] == "value" || chain[i] == "line" || chain[i] == "empty"*/chain[0] == "let")
                        //{
                        //    try
                        //    {
                        //        inputFunc.Add($"{chain[i]}{sep}{chain[i + 1]}");
                        //    }
                        //    catch (System.Exception)
                        //    {
                        //        System.Console.WriteLine($"not avalibe function name (position: {i + 1})");
                        //        await Context.Channel.SendMessageAsync($"not avalibe function name (position: {i + 1})");

                        //        return;
                        //    }

                        //    i += 2;
                        //}
                        //else
                        //{
                        //    System.Console.WriteLine($"nexus is not Markchain syntax (position: {i})");
                        //    await Context.Channel.SendMessageAsync($"nexus is not Markchain syntax (position: {i})");

                        //    return;
                        //}

                        try
                        {
                            inputFunc.Add(chain[i]);//Do not forgot to check names avalibe
                        }
                        catch (System.Exception)
                        {
                            System.Console.WriteLine($"not avalibe function name (position: {i})");
                            await Context.Channel.SendMessageAsync($"not avalibe function name (position: {i})");

                            return;
                        }

                        i++;
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine($"nexus is not Markchain syntax (position: {i})");
                    await Context.Channel.SendMessageAsync($"nexus is not Markchain syntax (position: {i})");

                    return;
                }

                // try
                // {
                //     if(chain[i + 2] != "=")
                //     {
                //         System.Console.WriteLine($"not found \"=\" after function definition (position: {i + 2})");
                //         await Context.Channel.SendMessageAsync($"not found \"=\" after function definition (position: {i + 2})");
                
                //         return;
                //     }
                // }
                // catch (System.Exception)
                // {
                //     System.Console.WriteLine($"not found \"=\" after function definition (position: {i + 2})");
                //     await Context.Channel.SendMessageAsync($"not found \"=\" after function definition (position: {i + 2})");
                
                //     return;
                // }


                if(chain.Length < i + 2)
                {
                    System.Console.WriteLine($"function body not found");
                    await Context.Channel.SendMessageAsync($"function body not found");
                
                    return;
                }

                string functionBody;

                functionBody = chain[i + 2];

                for (int j = i + 3; j < chain.Length; j++)
                {
                    functionBody = functionBody + sep + chain[j];
                }

                if (Program.allSpaces.Find(x => x.spaceName == chain[i + 1]).owner != Context.User.ToString())
                {
                    System.Console.WriteLine($"you haven`t access to create functions in this space");
                    await Context.Channel.SendMessageAsync($"you haven`t access to create functions in this space");

                    return;
                }

                Program.allSpaces.Find(x => x.spaceName == chain[i + 1]).functions.Add(new functionHolder(funcName, privacityType.peculiar, typeOfFunction, inputFunc.ToArray(), functionBody, false));

                System.Console.WriteLine($"function {funcName} is created in space {chain[i + 1]}, with type number {typeOfFunction}, with body {functionBody}, with inputs: ");

                foreach (var item in inputFunc)
                {
                    Console.WriteLine(item);
                }

                await Context.Channel.SendMessageAsync($"function {funcName} is created in space {chain[i + 1]}, with type number {typeOfFunction}, with body {functionBody}, with inputs: ");

                foreach (var item in inputFunc)
                {
                    await Context.Channel.SendMessageAsync(item);
                }
            }
            else
            {
                System.Console.WriteLine($"nexus is not Markchain syntax (position: {0})");
                await Context.Channel.SendMessageAsync($"nexus is not Markchain syntax (position: {0})");
            }
        }

        [Command("usein")]
        public async Task Usein(string command)
        {
            //>>new space MySpace
            //>>new space NExtspace
            //>>new line ConstLine in MySpace hello,_world!
            //>>new value Funct value X in MySpace ( Add ( Degree X 2 ) 5 )
            //>>changein MySpace privacityof Funct crossspace
            //>>new value superFun value X value Y in NExtspace ( Multiple Y ( in MySpace Funct X ) )
            //>>new value RecuFunct value X value Y in NExtspace ( If ( Isbigger 10 Y ) then ( RecuFunct ( Multiple X 2 ) ( Add Y 1 ) ) else X )
            //>>usein MySpace Print hello,_world!
            //>>usein MySpace Print ( ConstLine )
            //>>usein MySpace Print ( Funct 3 )
            string[] chain = command.Split(sep);

            string spaceName;

            try
            {
                spaceName = chain[0];
            }
            catch (System.Exception)
            {
                System.Console.WriteLine($"invalid space name (position: {0})");
                await Context.Channel.SendMessageAsync($"invalid space name (position: {0})");

                return;
            }

            try
            {
                List<string> functChain = new List<string>();

                for (int i = 1; i < chain.Length; i++)
                {
                    functChain.Add(chain[i]);
                }

                ProcessFunction(spaceName, functChain.ToArray(), Context);
            }
            catch (System.Exception)
            {
                System.Console.WriteLine($"wrong syntax");
                await Context.Channel.SendMessageAsync($"wrong syntax");
            }
        }

        [Command("dick"), Alias("penis", "suck", "cock", "fuck")]
        public async Task Dick(string command)
        {
            System.Console.WriteLine($"ERROR 46 55 43 4B");
            await Context.Channel.SendMessageAsync($"Why are you send it to me? Can you just go away and die somewhere? Are you hearing me fuckin moron?");
        }

        [Command("changein")]
        public async Task Changein(string command)
        {
            //>>new space|MySpace
            //>>new value|DivideQuadro|value|X|in|MySpace|(|Degree|(|Divide|X|5|)|2|)
            //>>changein MySpace privacityof DivideQuadro crossspace
            //>>changein MySpace nameof DivideQuadro SquareDivide
            //>>changein MySpace inputvaluesof SquareDivide value X value Y
            //>>changein MySpace bodyof SquareDivide ( Degree ( Divide X Y ) 2 )
            //>>changein MySpace remove SquareDivide


            string[] chain = command.Split(sep);

            string spaceName;

            string funcName;

            try
            {
                spaceName = chain[0];
            }
            catch (System.Exception)
            {
                System.Console.WriteLine($"invalid space name (position: {0})");
                await Context.Channel.SendMessageAsync($"invalid space name (position: {0})");

                return;
            }

            if (Program.allSpaces.Find(x => x.spaceName == spaceName).owner != Context.User.ToString())
            {
                System.Console.WriteLine($"you haven`t access to create functions in this space");
                await Context.Channel.SendMessageAsync($"you haven`t access to create functions in this space");

                return;
            }

            try
            {
                funcName = chain[2];
            }
            catch (System.Exception)
            {
                System.Console.WriteLine($"invalid function name (position: {2})");
                await Context.Channel.SendMessageAsync($"invalid function name (position: {2})");

                return;
            }

            if (chain[1] == "privacityof")
            {
                privacityType ptype;

                try
                {
                    switch (chain[3])
                    {
                        case "peculiar":
                            ptype = privacityType.peculiar;
                            break;
                        case "crossspace":
                            ptype = privacityType.crossspace;
                            break;
                        case "overall":
                            ptype = privacityType.overall;
                            break;
                        default:
                            System.Console.WriteLine($"invalid privasity type (position: {3})");
                            await Context.Channel.SendMessageAsync($"invalid privasity type (position: {3})");
                            return;
                    }
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine($"invalid privasity type (position: {3})");
                    await Context.Channel.SendMessageAsync($"invalid privasity type (position: {3})");
                    return;
                }

                Program.allSpaces.Find(ч => ч.spaceName == spaceName).functions.Find(x => x.funcName == funcName).typeOfPrivacity = ptype;

                System.Console.WriteLine($"privacity type is sucessfully changed in {funcName} from {spaceName} to {ptype}");
                await Context.Channel.SendMessageAsync($"privacity type is sucessfully changed in {funcName} from {spaceName} to {ptype}");
            }
            //else if (chain[1] == "functiontypeof")
            //{
            //    functionType ftype;

            //    try
            //    {
            //        switch (chain[3])
            //        {
            //            case "value":
            //                ftype = functionType.value;
            //                break;
            //            case "line":
            //                ftype = functionType.line;
            //                break;
            //            case "empty":
            //                ftype = functionType.empty;
            //                break;
            //            case "quantity":
            //                ftype = functionType.quantity;
            //                break;
            //            default:
            //                System.Console.WriteLine($"invalid function type (position: {3})");
            //                await Context.Channel.SendMessageAsync($"invalid function type (position: {3})");
            //                return;
            //        }
            //    }
            //    catch (System.Exception)
            //    {
            //        System.Console.WriteLine($"invalid function type (position: {3})");
            //        await Context.Channel.SendMessageAsync($"invalid function type (position: {3})");
            //        return;
            //    }

            //    Program.allSpaces.Find(x => x.spaceName == spaceName).functions.Find(x => x.funcName == funcName).typeOfFunction = ftype;

            //    System.Console.WriteLine($"function type is sucessfully changed in {funcName} from {spaceName} to {ftype}");
            //    await Context.Channel.SendMessageAsync($"function type is sucessfully changed in {funcName} from {spaceName} to {ftype}");
            //}
            else if (chain[1] == "remove")
            {
                Program.allSpaces.Find(x => x.spaceName == spaceName).functions.Remove(Program.allSpaces.Find(x => x.spaceName == spaceName).functions.Find(x => x.funcName == funcName));

                System.Console.WriteLine($"{funcName} from {spaceName} is removed");
                await Context.Channel.SendMessageAsync($"{funcName} from {spaceName} is removed");
            }
            else if (chain[1] == "nameof")
            {
                try
                {
                    Program.allSpaces.Find(x => x.spaceName == spaceName).functions.Find(x => x.funcName == funcName).funcName = chain[3];
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine($"invalid name (position: {3})");
                    await Context.Channel.SendMessageAsync($"invalid name (position: {3})");
                    return;
                }
            }
            else if (chain[1] == "bodyof")
            {
                try
                {
                    Program.allSpaces.Find(x => x.spaceName == spaceName).functions.Find(x => x.funcName == funcName).functionBody = chain[3];

                    System.Console.WriteLine($"New body {chain[3]}");
                    await Context.Channel.SendMessageAsync($"New body {chain[3]}");
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine($"invalid body (position: {3})");
                    await Context.Channel.SendMessageAsync($"invalid body (position: {3})");
                    return;
                }
            }
            else if (chain[1] == "inputvaluesof")
            {
                int i = 3;

                List<string> inputFunc = new List<string>();

                try
                {
                    while (i < chain.Length)
                    {
                        try
                        {
                            //if (/*chain[i] == "value" || chain[i] == "line" || chain[i] == "empty"*/ )
                            //{
                            //    //try
                            //    //{
                            //    //    inputFunc.Add($"{chain[i]}{sep}{chain[i + 1]}");
                            //    //}
                            //    //catch (System.Exception)
                            //    //{
                            //    //    System.Console.WriteLine($"not avalibe function name (position: {i + 1})");
                            //    //    await Context.Channel.SendMessageAsync($"not avalibe function name (position: {i + 1})");

                            //    //    return;
                            //    //}

                            //}
                            //else
                            //{
                            //    System.Console.WriteLine($"nexus is not Markchain syntax (position: {i})");
                            //    await Context.Channel.SendMessageAsync($"nexus is not Markchain syntax (position: {i})");

                            //    return;
                            //}

                            inputFunc.Add($"{chain[i]}");

                            i += 1;
                        }
                        catch (System.Exception)
                        {
                            System.Console.WriteLine($"nexus is not Markchain syntax (position: {i})");
                            await Context.Channel.SendMessageAsync($"nexus is not Markchain syntax (position: {i})");

                            return;
                        }
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine($"wrong syntax");
                    await Context.Channel.SendMessageAsync($"wrong syntax");

                    return;
                }

                Program.allSpaces.Find(x => x.spaceName == spaceName).functions.Find(x => x.funcName == funcName).inputFunctions = inputFunc.ToArray();

                System.Console.WriteLine($"{funcName} from {spaceName} changed with inputs: ");

                foreach (var item in inputFunc)
                {
                    Console.WriteLine(item);
                }

                await Context.Channel.SendMessageAsync($"{funcName} from {spaceName} changed with inputs: ");

                foreach (var item in inputFunc)
                {
                    await Context.Channel.SendMessageAsync(item);
                }
            }
            else
            {
                System.Console.WriteLine($"nexus is not Markchain syntax (position: {1})");
                await Context.Channel.SendMessageAsync($"nexus is not Markchain syntax (position: {1})");
            }
        }

        public static object ProcessFunction(string space, string[] chain, SocketCommandContext Context)
        {
            try
            {
                string funcName;

                int curpos = 0;

                string[] inputValues;

                halfProc:

                (funcName, inputValues, curpos, space) = GetFunctionHeaderData(chain, space, Context).Result;

                functionHolder funct; //Maybe, there is needing trycatch
                funct = Program.allSpaces.Find(x => x.spaceName == "RootSpace").functions.Find(x => x.funcName == funcName);//not forget about special processing for root functions

                if (funct == null)
                {
                    funct = Program.allSpaces.Find(x => x.spaceName == space).functions.Find(x => x.funcName == funcName);
                }


                if (funct.rootFunction == true)
                {
                    return RootFunctionsProc.ProcessRoot(funct.funcName, inputValues, Context).Result;
                }
                else if (funct.functionBody[0] == '(')
                {
                    string[] bodyChain = funct.functionBody.Split(sep);

                    int bracket = 1;

                    List<string> bodyFunc = new List<string>();

                    int curpos2 = 0;

                    curpos2++;

                    if (bodyChain[curpos2] == ")")
                    {
                        bracket--;
                    }

                    while (bracket > 0)
                    {
                        bodyFunc.Add(bodyChain[curpos2]);
                        curpos2++;

                        if (bodyChain[curpos2] == ")")
                        {
                            bracket--;
                        }
                        else if (bodyChain[curpos2] == "(")
                        {
                            bracket++;
                        }
                    }

                    for (int i = 0; i < funct.inputFunctions.Length; i++)
                    {
                        string argumentName = funct.inputFunctions[i];

                        for (int j = 0; j < bodyFunc.Count; j++)
                        {
                            if (bodyFunc[j] == argumentName)
                            {
                                bodyFunc[j] = inputValues[i];
                            }
                        }
                    }

                    //Here is needing the part which written before halfProc (iam not writing it now because in Vscode is ugly automatic tabulation system)

                    chain = bodyFunc.ToArray();

                    goto halfProc;
                }
                else
                {
                    return funct.functionBody;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        static async Task<(string funcName, string[] inputValues, int curpos, string space)> GetFunctionHeaderData(string[] chain, string space, SocketCommandContext Context)
        {
            string funcName;

            string downSpace = space;

            int curpos = 0;

            try
            {
                try
                {
                    if (chain[0] == "in")
                    {
                        try
                        {
                            funcName = chain[2];

                            functionHolder infunc;

                            infunc = Program.allSpaces.Find(x => x.spaceName == chain[1]).functions.Find(x => x.funcName == funcName);

                            if (space != chain[1] && infunc.typeOfPrivacity == privacityType.peculiar)
                            {
                                System.Console.WriteLine($"the called function have the privacity type peculiar, and can`t be called from non-father space");
                                await Context.Channel.SendMessageAsync($"the called function have the privacity type peculiar, and can`t be called from non-father space");

                                return (null, null, 0, null);
                            }


                            space = chain[1];
                            curpos = 3;

                            if (Program.allSpaces.Find(x => x.spaceName == space).owner != Context.User.ToString() && Program.allSpaces.Find(x => x.spaceName == space).functions.Find(x => x.funcName == funcName).typeOfPrivacity != privacityType.overall)
                            {
                                System.Console.WriteLine($"you not have access to use this function");
                                await Context.Channel.SendMessageAsync($"you not have access to use this function");

                                return (null, null, 0, null);
                            }
                        }
                        catch (System.Exception)
                        {
                            System.Console.WriteLine($"wrong body syntax: uncorrect body function header");
                            await Context.Channel.SendMessageAsync($"wrong body syntax: uncorrect body function header");

                            return (null, null, 0, null);
                        }
                    }
                    else
                    {
                        try
                        {
                            funcName = chain[0];
                            curpos = 1;

                            if (Program.allSpaces.Find(x => x.spaceName == "RootSpace").functions.Exists(x => x.funcName == funcName))
                            {
                                downSpace = "RootSpace";
                            }

                            if (Program.allSpaces.Find(x => x.spaceName == downSpace).owner != Context.User.ToString() && Program.allSpaces.Find(x => x.spaceName == downSpace).functions.Find(x => x.funcName == funcName).typeOfPrivacity != privacityType.overall)
                            {
                                System.Console.WriteLine($"you not have access to use this function");
                                await Context.Channel.SendMessageAsync($"you not have access to use this function");

                                return (null, null, 0, null);
                            }
                        }
                        catch (System.Exception)
                        {
                            System.Console.WriteLine($"wrong body syntax: uncorrect body function header");
                            await Context.Channel.SendMessageAsync($"wrong body syntax: uncorrect body function header");

                            return (null, null, 0, null);
                        }
                    }
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine($"wrong body syntax: uncorrect body function header");
                    await Context.Channel.SendMessageAsync($"wrong body syntax: uncorrect body function header");

                    return (null, null, 0, null);
                }

                List<string> inputValues = new List<string>();

                try
                {
                    while (curpos < chain.Length)
                    {
                        if (chain[curpos] == "(")//Write a case without function
                        {
                            int bracket = 1;

                            List<string> nextChain = new List<string>();

                            curpos++;

                            if (chain[curpos] == ")")
                            {
                                bracket--;

                                inputValues.Add("0");
                            }
                            else
                            {
                                while (bracket > 0)
                                {
                                    nextChain.Add(chain[curpos]);
                                    curpos++;

                                    if (chain[curpos] == ")")
                                    {
                                        bracket--;
                                    }
                                    else if (chain[curpos] == "(")
                                    {
                                        bracket++;
                                    }
                                }

                                inputValues.Add(ProcessFunction(space, nextChain.ToArray(), Context) as string);
                            }

                            curpos++;
                        }
                        else
                        {
                            inputValues.Add(chain[curpos]);

                            curpos++;

                            //System.Console.WriteLine($"wrong body syntax");
                            //await Context.Channel.SendMessageAsync($"wrong body syntax");

                            //return (null, null, 0); ; //is beatiful to show what function have an error
                        }
                    }
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine($"wrong body syntax");
                    await Context.Channel.SendMessageAsync($"wrong body syntax");

                    return (null, null, 0, null);
                }

                return (funcName, inputValues.ToArray(), curpos, space);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (null, null, 0, null);
            }
        }
    }

    enum functionType
    {
        pure,
        value,
        line,
        empty,
        quantity
    }

    enum privacityType
    {
        overall,
        crossspace,
        peculiar
    }

    internal class spaceHolder
    {
        public string spaceName { get; set; }

        public string owner { get; set; }

        public List<functionHolder> functions = new List<functionHolder>();

        public spaceHolder(string _spaceName, string _owner)
        {
            spaceName = _spaceName;
            owner = _owner;
        }
    }

    internal class functionHolder
    {
        public string funcName {get; set;}

        public privacityType typeOfPrivacity {get; set;}

        public functionType typeOfFunction {get; set;}

        public string[] inputFunctions {get; set;}

        public string functionBody {get; set;}

        public bool rootFunction { get; }

        public functionHolder(string _funcName, privacityType _typeOfPrivacity, functionType _typeOfFunction, string[] _inputFunctions, string _functionBody, bool _rootFunction)
        {
            funcName = _funcName;
            typeOfPrivacity = _typeOfPrivacity;
            typeOfFunction = _typeOfFunction;
            inputFunctions = _inputFunctions;
            functionBody = _functionBody;
            rootFunction = _rootFunction;
        }
    }
}
