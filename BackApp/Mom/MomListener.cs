using System;
using System.Text.Json;
using System.Threading;
using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS.Util;
using BackApp.Model;
using BackApp.Model.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ISession = Apache.NMS.ISession;

namespace Domain.Mom
{
    public class MomListener
    {
        private readonly ILogger _logger;
        public bool IsRunning { get; private set; }

        private Uri connecturi;

        private IConnectionFactory factory;

        private IConnection connection;

        private ISession session;

        private IMessageConsumer consumer;

        private IMessageProducer producer;

        private IDestination destination;

        private IDestination destinationOut;

        private MessageRepository _messageRepository;

        public MomListener(ILogger<MomListener> logger, MessageRepository messageRepository)
        {
            _logger = logger;
            _logger.LogInformation("Ctr du MomListener");
            _messageRepository = messageRepository;

        }

        public void Initialize()
        {
            connecturi = new Uri("activemq:tcp://localhost:61616");
            factory = new Apache.NMS.ActiveMQ.ConnectionFactory(connecturi);

        }

        public void Run()
        {
            connection = factory.CreateConnection("user", "user");
            connection.Start();

            session = connection.CreateSession(AcknowledgementMode.ClientAcknowledge);
            destination = SessionUtil.GetDestination(session, "queue://topic.Notification.Message");
            destinationOut = SessionUtil.GetDestination(session, "queue://topic.Notification.Message");
            consumer = session.CreateConsumer(destination);
            producer = session.CreateProducer(destinationOut);
            producer.DeliveryMode = MsgDeliveryMode.Persistent;

            consumer.Listener += new MessageListener(OnMessage);
            IsRunning = true;
 
        }
        public void OnMessage(IMessage receivedMsg)
        {
            ITextMessage message = receivedMsg as ITextMessage;
            MessageComm? messageComm = JsonSerializer.Deserialize<MessageComm>(message.Text);
            _logger.LogInformation($"MessageComm : {messageComm.Description}");
            MessagePersist? persistMessage = messageComm as MessagePersist;
            _messageRepository.WriteNewMessage(persistMessage);
            receivedMsg.Acknowledge();

        }
    }
}
