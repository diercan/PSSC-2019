from django.urls import path, include 
from . import views 
app_name ='interface'

urlpatterns = [
    path('render_info_page', views.render_info_page, name='render_info_page'),
    path('render_control_page', views.render_control_page, name='render_control_page'),
    path('power_on_led', views.power_on_led, name='power_on_led'),
    path('power_off_led', views.power_off_led, name='power_off_led'),
    path('send_info_mail', views.send_info_mail, name='send_info_mail'),
    path('add_new_key', views.add_new_key, name='add_new_key'),
 #path('adv_search', views.adv_search, name='adv_search'), path('NA', 
    #views.not_available_link, name='NA')
#    path('interface/',include('django.contrib.auth.urls'))
]

