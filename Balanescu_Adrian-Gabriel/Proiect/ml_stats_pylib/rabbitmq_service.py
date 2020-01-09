import pika


class QService:
    def __init__(self, host, exchange):
        self.exchange = exchange
        self.connection = pika.BlockingConnection(
            pika.ConnectionParameters(host=host)
        )
        self.channel = self.connection.channel()
        self.channel.exchange_declare(exchange=self.exchange, exchange_type='direct')

    def send_live(self, routingKey, body):
        self.channel.basic_publish(exchange=self.exchange,
                                   routing_key=routingKey,
                                   body=body)
        print(routingKey + " : " + body)
