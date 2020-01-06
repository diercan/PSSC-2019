import RPi.GPIO as GPIO
from abc import abstractmethod, ABCMeta

#Set DATA pin
#BCM 4 => BOARD 7
#DHT = 4

class Sensor:
    _metaclass_ = ABCMeta
    def __init__(self, pin, mode):
        self.pin = pin
        self.mode = mode

    def import_libraries(self):
        pass

    @abstractmethod
    def read_sensor_value(self):
        pass


class TemperatureSensor(Sensor):

    def __init__(self, pin, mode):
        Sensor.__init__(self, pin, mode)
        self.temperature = 0.0
        self.humidity = 0.0

    def read_sensor_value(self):
       import Adafruit_DHT
       GPIO.setmode(GPIO.BCM)
       self.humidity, self.temperature = Adafruit_DHT.read_retry(Adafruit_DHT.DHT22, self.pin)

    def display_sensor_value(self):
       self.read_sensor_value()
       return '{0:0.1f}*C'.format(self.temperature)
