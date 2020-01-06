from django.test import TestCase
from interface.forms import AddRFIDKeysForm
from interface.models import RFIDKeysModel

class TestRFIDKeyForm(TestCase):

    @classmethod
    def setUpData(self):
        self.key = RFIDKeysModel.objects.create(key='1234567890123')

    def test_form_valid(self):
        form = AddRFIDKeysForm(data={'key':'1234567890123'})
        self.assertTrue(form.is_valid())

    def test_form_invalid(self):
        form = AddRFIDKeysForm(data={'key':'123456789012345'})
        self.assertFalse(form.is_valid())
