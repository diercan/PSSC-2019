from django.db import models

# Create your models here.

class DateTimeModel(models.Model):
    current_datetime = models.DateTimeField(blank=True, null=True)


class RFIDKeysModel(models.Model):
    key = models.CharField(max_length=13, blank=True)
