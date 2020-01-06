Name

   rabbitmq-service.bat -- manage RabbitMQ AMQP service

Synopsis

   rabbitmq-service.bat [command]

Description

   RabbitMQ is an implementation of AMQP, the emerging standard for high
   performance enterprise messaging. The RabbitMQ server is a robust and
   scalable implementation of an AMQP broker.

   Running rabbitmq-service allows the RabbitMQ broker to be run as a service
   on NT/2000/2003/XP/Vista(R) environments. The RabbitMQ broker service can
   be started and stopped using the Windows(R) services applet.

   By default the service will run in the authentication context of the local
   system account. It is therefore necessary to synchronise Erlang cookies
   between the local system account (typically C:\WINDOWS\.erlang.cookie and
   the account that will be used to run rabbitmqctl.

Commands

   help

           Display usage information.

   install

           Install the service. The service will not be started. Subsequent
           invocations will update the service parameters if relevant
           environment variables were modified.

   remove

           Remove the service. If the service is running then it will
           automatically be stopped before being removed. No files will be
           deleted as a consequence and rabbitmq-server will remain operable.

   start

           Start the service. The service must have been correctly installed
           beforehand.

   stop

           Stop the service. The service must be running for this command to
           have any effect.

   disable

           Disable the service. This is the equivalent of setting the startup
           type to Disabled using the service control panel.

   enable

           Enable the service. This is the equivalent of setting the startup
           type to Automatic using the service control panel.

Environment

   RABBITMQ_SERVICENAME

           Defaults to RabbitMQ.

   RABBITMQ_BASE

           Defaults to the application data directory of the current user.
           This is the location of log and database directories.

   RABBITMQ_NODENAME

           Defaults to rabbit. This can be useful if you want to run more
           than one node per machine - RABBITMQ_NODENAME should be unique per
           erlang-node-and-machine combination. See the clustering on a
           single machine guide for details.

   RABBITMQ_NODE_IP_ADDRESS

           By default RabbitMQ will bind to all interfaces, on IPv4 and IPv6
           if available. Set this if you only want to bind to one network
           interface or address family.

   RABBITMQ_NODE_PORT

           Defaults to 5672.

   ERLANG_SERVICE_MANAGER_PATH

           Defaults to C:\Program Files\erl5.5.5\erts-5.5.5\bin (or
           C:\Program Files (x86)\erl5.5.5\erts-5.5.5\bin for 64-bit
           environments). This is the installation location of the Erlang
           service manager.

   RABBITMQ_CONSOLE_LOG

           Set this varable to new or reuse to have the console output from
           the server redirected to a file named SERVICENAME.debug in the
           application data directory of the user that installed the service.
           Under Vista this will be C:\Users\AppData\username\SERVICENAME.
           Under previous versions of Windows this will be C:\Documents and
           Settings\username\Application Data\SERVICENAME. If
           RABBITMQ_CONSOLE_LOG is set to new then a new file will be created
           each time the service starts. If RABBITMQ_CONSOLE_LOG is set to
           reuse then the file will be overwritten each time the service
           starts. The default behaviour when RABBITMQ_CONSOLE_LOG is not set
           or set to a value other than new or reuse is to discard the server
           output.
