from django.forms import ModelForm
from .models import RFIDKeysModel
from django.contrib.admin import widgets

class AddRFIDKeysForm(ModelForm):
    class Meta:
        model = RFIDKeysModel
        fields = ['key']
        # key = forms.CharField(label="Cheie Acces", max_length=13, required=False, widget=forms.TextInput(attrs={'placeholder': 'eg.9181823604798'}))
