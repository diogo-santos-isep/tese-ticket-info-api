# tese-ticket-info-api
Api to manage Tickets 

Command to run RabbitMQ: 
docker run -d --hostname rabbit-local --name TicketAppRabbitMQ -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=ticketapp -e RABBITMQ_DEFAULT_PASS=ticketapp rabbitmq:3-management
