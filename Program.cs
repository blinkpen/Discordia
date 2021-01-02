using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace Discordia
{
    class Program
    {

        @base person = new @base();
        wizard wizard = new wizard();
        witch witch = new witch();
        mage mage = new mage();
        nightwalker nightwalker = new nightwalker();
        warrior warrior = new warrior();

        public static void Main(string[] args)
      => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;


        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler;
            _client.Log += Log;
            _client.ReactionAdded += AddedReactEvent;
            _client.ReactionRemoved += RemoveReactEvent;

        


            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = File.ReadAllText("token.txt");

            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            //var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            //var token = File.ReadAllText("token.txt");
            //var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            var config = new DiscordSocketConfig { MessageCacheSize = 100 };
            var client = new DiscordSocketClient(config);

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task CommandHandler(SocketMessage message)
        {
            //Global Role Variables
            SocketGuild guild0 = _client.GetGuild(793087550314774572);//guild          
            SocketRole adminRole = guild0.GetRole(793093176000643112);//admin role
            SocketRole wizardRole = guild0.GetRole(793088360814084147);//wizard role
            SocketRole witchRole = guild0.GetRole(793089716278657056);//witch role
            SocketRole mageRole = guild0.GetRole(793088755044319233);//mage role
            SocketRole nightWalkerRole = guild0.GetRole(793089225196175370);//nightwalker role
            SocketRole warriorRole = guild0.GetRole(793088862033149992);//warrior role
            SocketGuildUser user0 = guild0.GetUser(message.Author.Id);//user that sent message

        
            #region command variables and filtering
            //variables
            string command = "";
            int lengthOfCommand = -1;

            //filtering messages begin here
            if (!message.Content.StartsWith("//")) //This is your prefix
                return;

            if (message.Author.IsBot) //This ignores all commands from bots
                return;

            if (message.Content.Contains(' '))
                lengthOfCommand = message.Content.IndexOf(' ');
            else
                lengthOfCommand = message.Content.Length;

            command = message.Content.Substring(2, lengthOfCommand - 2).ToLower();
            #endregion
            

            /////Commands begin here/////
            await message.Channel.DeleteMessageAsync(message); //DELETE ALL COMMMAND MESSAGES
            string proNoun = "";
            if (user0.Roles.Contains(wizardRole))
            {
                proNoun = "wizard";
            }
            else if(user0.Roles.Contains(witchRole))
            {
                proNoun = "witch";
            }
            else if (user0.Roles.Contains(mageRole))
            {
                proNoun = "mage";
            }
            else if (user0.Roles.Contains(nightWalkerRole))
            {
                proNoun = "night walker";
            }
            else if (user0.Roles.Contains(warriorRole))
            {
                proNoun = "warrior";
            }
  

            //normal commands
            if (command == "help")
            {
                await message.Channel.SendMessageAsync($"{wizardRole.Mention}");
                
            }

            if (command == "melee")
            {               
                warrior.strength = 10;
                warrior.AttackMelee();
                await message.Channel.SendMessageAsync($"{proNoun} beat ass with {warrior.strength} damage.");
            }

            if(command == "hurt")
            {
                wizard.hp -= 22;
                EmbedBuilder eb = new EmbedBuilder();
                eb.WithTitle("");
                eb.WithDescription("");
                eb.WithFooter($"Health: {wizard.hp}hp | Experience: {wizard.experience}xp | Strength: {wizard.strength} | Stealth: {wizard.stealth} | Magic: {wizard.magic} | Intelligence: {wizard.intelligence} | Wisdom: {wizard.wisdom} | Charisma: {wizard.charisma} | Level: {wizard.level}");
                eb.WithTimestamp(DateTime.Now);
                await message.Channel.SendMessageAsync("", false, eb.Build());
            }

            if (!(user0.Roles.Contains(adminRole))) { return; }//only admin can use commands after this
            //admin commands


        }


        //ADD ROLE BY REACTION
        public static async Task AddedReactEvent(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            //if (message.Id == 788423261041983508)
            //{
            //    if (reaction.Emote.Name == "😵")
            //    {
            //        var user = ((IGuildUser)reaction.User.Value);
            //        var role = ((ITextChannel)channel).Guild.GetRole(785872551896940544); //psychonaut role
            //        await user.AddRoleAsync(role);
            //    }

            //    if (reaction.Emote.Name == "👩")
            //    {
            //        var user = ((IGuildUser)reaction.User.Value);
            //        //var role0 = ((ITextChannel)channel).Guild.GetRole(785871999322947604); //male role
            //        //await user.RemoveRoleAsync(role0);
            //        var role = ((ITextChannel)channel).Guild.GetRole(785872194650898502); //female role
            //        await user.AddRoleAsync(role);

            //        var emote0 = new Emoji("👨");
            //        var poop = await message.GetOrDownloadAsync();
            //        await poop.RemoveReactionAsync(emote0, user);
            //    }

            //    if (reaction.Emote.Name == "👨")
            //    {
            //        var user = ((IGuildUser)reaction.User.Value);
            //        //var role0 = ((ITextChannel)channel).Guild.GetRole(785872194650898502); //female role
            //        //await user.RemoveRoleAsync(role0);
            //        var role = ((ITextChannel)channel).Guild.GetRole(785871999322947604); //male role
            //        await user.AddRoleAsync(role);

            //        var emote0 = new Emoji("👩");
            //        var poop = await message.GetOrDownloadAsync();
            //        await poop.RemoveReactionAsync(emote0, user);
            //    }

            //    Emote strainCommand = Emote.Parse("<:drechronic:787941399534108732>");
            //    if (reaction.Emote.Name == strainCommand.Name)
            //    {
            //        var user = ((IGuildUser)reaction.User.Value);
            //        var role = ((ITextChannel)channel).Guild.GetRole(788390195418103849); //straincommand role
            //        await user.AddRoleAsync(role);
            //    }

            //}
        }

        //REMOVE ROLE BY REACTION
        public static async Task RemoveReactEvent(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            //if (message.Id == 788423261041983508)
            //{
            //    if (reaction.Emote.Name == "😵")
            //    {
            //        var user = ((IGuildUser)reaction.User.Value);
            //        var role = ((ITextChannel)channel).Guild.GetRole(785872551896940544); //psychonaut role
            //        await user.RemoveRoleAsync(role);
            //    }

            //    if (reaction.Emote.Name == "👩")
            //    {
            //        var user = ((IGuildUser)reaction.User.Value);
            //        var role = ((ITextChannel)channel).Guild.GetRole(785872194650898502); //female role
            //        await user.RemoveRoleAsync(role);
            //    }

            //    if (reaction.Emote.Name == "👨")
            //    {
            //        var user = ((IGuildUser)reaction.User.Value);
            //        var role = ((ITextChannel)channel).Guild.GetRole(785871999322947604); //male role
            //        await user.RemoveRoleAsync(role);
            //    }

            //    Emote strainCommand = Emote.Parse("<:drechronic:787941399534108732>");
            //    if (reaction.Emote.Name == strainCommand.Name)
            //    {
            //        var user = ((IGuildUser)reaction.User.Value);
            //        var role = ((ITextChannel)channel).Guild.GetRole(788390195418103849); //straincommand role
            //        await user.RemoveRoleAsync(role);
            //    }  
                
            }
        }



}

