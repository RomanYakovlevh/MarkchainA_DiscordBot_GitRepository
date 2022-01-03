using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using Discord.Rest;
using System.IO;
using System.Threading;

namespace MarkchainA_DiscordBot
{
    class Program
    {
        private DiscordSocketClient client;
        private CommandService commands;

        public static List<Commands.spaceHolder> allSpaces = new List<Commands.spaceHolder>();//to Add a secial space

        //The main points of evolveing the Markchain:
        // @ Eliminate a points as a separator between nexuses in a chain: 
        //      >>new value.Function1.value.X.in.MySpace.(.Add.X.2.) - it is not comfortable
        //      >>new value Function1 value X in MySpace ( Add X 2 ) - Much better, but there is a trouble - 
        //      discord do not allow a spaces between words in text after command(Maybe, try to use Group("..") => Group("..") => Command(".."))
        // @ Add a special "masks".Examples of expexted masks:
        //      + - * / ^ = != ! > < <= >= && || - the special symbols for simple mathematic operations. What i mean:
        //      NOT: >>new value Function1 value X in MySpace ( Add X 2 )
        //      BUT: >>new value Function1 value X in MySpace ( X + 2 ) - the special program 
        //      servant must replace masks with appropriate functions
        // @ ( ) < > too shall become like masks, but they must have a little bit different function:
        //      >>new value Function1 value X in MySpace ( X + 2 ) = >>new value Function1 value X in MySpace (X + 2)
        //      Let Program automatically detect it, and make user life more cozy
        // @ Function name checker: do not allow users made a functions which names include a special symbols or which have 
        //      used before
        // @ Solve a typization problem: While writing the CompilerA class i never have revise any function to type(Because all 
        //      types in Markchain is lines). Maybe, it will be good to remove any typization from Markchain? It will let us make 
        //      the quantities(arrays) more simple
        // @ Introduce the after-processed input functions. They will must be written in < > (without ( )), and will enter 
        //      to body function like just input line (In other words, this function would not be computedas input). 
        //      Why the Markchain need it? It let us create a structures like If-Else,
        //      and advanced quantity constructors. Examples:
        //      >>new value MyChosen value X in MySpace (If (X > 2) <Print X> <Print (Function2 X)>) -- >>
        //      If - IfElse function name
        //      (X > 2) - the condition
        //      <Print X> - THEN: this function would be computed if condition is true
        //      <Print (Function2 X)> - ELSE: this function would be computed if condition is false
        // @ Introduse a clever mathematic tool:
        //      >>new value Function3 value X value Y in MySpace (Y + ((X + 2) - 5)) => NO
        //      >>new value Function3 value X value Y in MySpace (Y + X + 2 - 5) => YES (i know that 2 - 5 is -3)

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            RootFunctionsProc.BeginRoot();

            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            commands = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Debug,
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
            });
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services: null);

            client.Log += Log;

            await client.LoginAsync(TokenType.Bot, "FAKE TOKEN LOL");//change token
            await client.StartAsync();

            client.MessageReceived += MessegeGet;

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task MessegeGet(SocketMessage msg)
        {
            SocketUserMessage message = msg as SocketUserMessage;
            SocketCommandContext context = new SocketCommandContext(client, message);

            if (context.User.IsBot
                && context.Message == null
                && context.Message.Content == " ") return;

            int prefixPos = 0;
            if (!message.HasMentionPrefix(client.CurrentUser, ref prefixPos)
                && !message.HasStringPrefix(">>", ref prefixPos, StringComparison.CurrentCulture)) return;
            
            var result = await commands.ExecuteAsync(context, prefixPos, null);
            if (!result.IsSuccess)
            {
                Console.WriteLine($"[{DateTime.Now}] something wrong. " +
                    $"\nText: {context.User.Username} : {context.Message.Content}" +
                    $"\nError: {result.ErrorReason}");
            }
        }
    }
}
