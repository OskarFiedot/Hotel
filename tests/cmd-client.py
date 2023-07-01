import pika
import json
import uuid

connection = pika.BlockingConnection(pika.ConnectionParameters(host="localhost"))
channel = connection.channel()

channel.queue_declare(queue="hotelCommandQueue", durable=True)

message = {
    "Id": str(uuid.uuid4()),
    "Type": "CreateReservationCommand",
    "User": "ofiedot",
    "StartDate": "2012-04-23T18:25:43.511Z",
    "EndDate": "2012-04-23T18:25:43.511Z",
    "TotalPrice": 1000,
    "Hotel": "23ebf707-ede7-4145-ba63-85508a29a538",
    "RoomReserved": ["05970b6c-889d-4f97-8f9a-94774143c9aa"],
}

# message = {
#     "Id": "4ff0b30b-eb6b-447d-a23b-f4b0268c7713",
#     "Type": "EditReservationCommand",
#     "StartDate": "2012-04-23T18:25:43.511Z",
#     "EndDate": "2014-04-23T18:25:43.511Z",
#     "TotalPrice": 111,
#     "RoomReserved": ["16ec61df-90aa-4862-ab19-6dfc51516be0", "66b682fd-deab-4063-8352-21d8ac5dabf9"],
# }

# message = {
#     "Id": "4ff0b30b-eb6b-447d-a23b-f4b0268c7713",
#     "Type": "CancelReservationCommand",
#     "User": "ofiedot",
# }

channel.basic_publish(
    exchange="",
    routing_key="hotelCommandQueue",
    body=json.dumps(message),
    properties=pika.BasicProperties(delivery_mode=pika.spec.PERSISTENT_DELIVERY_MODE),
)

print(" [x] Sent %r" % message)
connection.close()
