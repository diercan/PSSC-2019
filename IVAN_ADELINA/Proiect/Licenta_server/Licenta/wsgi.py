"""
WSGI config for Licenta project.

It exposes the WSGI callable as a module-level variable named ``application``.

For more information on this file, see
https://docs.djangoproject.com/en/2.2/howto/deployment/wsgi/
"""

import os, sys

from django.core.wsgi import get_wsgi_application
#INTERP = "/home/pi/Desktop/environment/bin/python"

#if sys.executable != INTERP: os.execl(INTERP, INTERP, *sys.argv)
os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'Licenta.settings')

application = get_wsgi_application()
