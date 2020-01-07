"""Licenta URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/2.2/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.contrib import admin 
from django.urls import path
#import debug_toolbar
from django.conf.urls import url, include
from django.views.generic.base import TemplateView
#from rest_framework import routers
from interface import views
#router = routers.SimpleRouter() router.register(r'api/rfs/tests', 
#views.TestsViewSet)
urlpatterns = [
    path('admin/', admin.site.urls),
    #url(r'^', include(router.urls)),
 #   url(r'^__debug__/', include(debug_toolbar.urls)),
    path('interface/', include('django.contrib.auth.urls')),
    path('interface/',include('interface.urls')),
    path('render_info_page/', include('interface.urls', namespace='render_info_page')),
    path('render_control_page/', include('interface.urls', namespace='render_control_page')),
    path('',TemplateView.as_view(template_name='home.html'), name='home'),
    #path('services/', include('services.urls')) 
    #path('adv_search/', include('rfs_tests.urls', 
    #namespace='adv_search')), path('NA/', include('rfs_tests.urls', 
    #namespace='NA'))
]
