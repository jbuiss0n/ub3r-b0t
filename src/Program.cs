﻿namespace UB3RB0T
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var botType = BotType.Discord;
            var shard = 0;

            if (args != null)
            {
                foreach (string arg in args)
                {
                    var argParts = arg.Split(new[] { ':' }, 2);

                    switch (argParts[0])
                    {
                        case "/?":
                            Console.WriteLine("dotnet UB3RB0T.dll [/t:type] [/s:shard]");
                            Console.WriteLine("/t:type \t The type of bot to create. [Irc, Discord]");
                            Console.WriteLine("/s:shard \t The shard for this instance (Discord only)");
                            Console.WriteLine("/c:path \t The path to botconfig.json");
                            return;

                        case "/t":
                            if (!Enum.TryParse(argParts[1], /* ignoreCase */true, out botType))
                            {
                                throw new ArgumentException("Invalid bot type specified.");
                            }

                            break;

                        case "/c":
                            Environment.SetEnvironmentVariable(JsonConfig.PathEnvironmentVariableName, argParts[1]);
                            break;

                        case "/s":
                            shard = int.Parse(argParts[1]);
                            break;
                    }
                }
            }

            // Convert to async main method
            new Bot(botType, shard).RunAsync().GetAwaiter().GetResult();
        }
    }
}