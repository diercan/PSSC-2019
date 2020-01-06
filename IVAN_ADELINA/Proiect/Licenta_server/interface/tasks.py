from __future__ import absolute_import
from celery import shared_task
from .mail import Mail

@shared_task
def send_mail():
    mail_object = Mail()
    return mail_object.send_mail()
