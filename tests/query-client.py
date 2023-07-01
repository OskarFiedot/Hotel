import pika
import uuid
import json


class HotelRpcClient(object):
    def __init__(self):
        self.connection = pika.BlockingConnection(
            pika.ConnectionParameters(host="localhost")
        )

        self.channel = self.connection.channel()

        result = self.channel.queue_declare(queue="", exclusive=True)
        self.callback_queue = result.method.queue

        self.channel.basic_consume(
            queue=self.callback_queue,
            on_message_callback=self.on_response,
            auto_ack=True,
        )

        self.response = None
        self.corr_id = None

    def on_response(self, ch, method, props, body):
        if self.corr_id == props.correlation_id:
            self.response = body

    def call(self, query):
        self.response = None
        self.corr_id = str(uuid.uuid4())
        self.channel.basic_publish(
            exchange="",
            routing_key="hotelQueryQueue",
            properties=pika.BasicProperties(
                reply_to=self.callback_queue,
                correlation_id=self.corr_id,
            ),
            body=json.dumps(query),
        )
        self.connection.process_data_events(time_limit=None)
        return self.response


hotel_rpc = HotelRpcClient()

query = {
    "Type": "FindRoomsByReservationQuery",
    "ReservationId": "4ff0b30b-eb6b-447d-a23b-f4b0268c7713",
}
print(" [x] Quering data")
response = hotel_rpc.call(query)
print(f" [.] Got {response}")
