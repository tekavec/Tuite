using System.Collections.Generic;
using System.Linq;
using Tuite.Commands;
using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;
using Tuite.Utils;

namespace Tuite
{
    public class CommandParser : ICommandParser
    {
        private readonly char[] _Separators = { ' ' };

        private readonly IEnumerable<ICommandFactory> _AllCommands;
        private readonly IUserRepository _UserRepository;
        private readonly IMessageRepository _MessageRepository;
        private readonly ISubscriptionRepository _SubscriptionRepository;
        private readonly IMessagePrinter _MessagePrinter;

        public CommandParser(
            IEnumerable<ICommandFactory> allCommands, 
            IUserRepository userRepository, 
            IMessageRepository messageRepository, 
            ISubscriptionRepository subscriptionRepository, 
            IMessagePrinter messagePrinter)
        {
            _AllCommands = allCommands;
            _UserRepository = userRepository;
            _MessageRepository = messageRepository;
            _SubscriptionRepository = subscriptionRepository;
            _MessagePrinter = messagePrinter;
        }

        public ICommand Parse(string input)
        {
            string[] inputParts = input.Split(_Separators, 3);

            var commandNameResolver = new CommandNameResolver(inputParts);
            var command = GetCommandBy(commandNameResolver.GetCommandName());
            return command.CreateCommand(inputParts, _UserRepository, _MessageRepository, _SubscriptionRepository, _MessagePrinter);
        }

        private ICommandFactory GetCommandBy(string commandName)
        {
            return _AllCommands.FirstOrDefault(a => a.Name == commandName);
        }

    }
}