import smtplib, ssl
from email.mime.text import MIMEText
from email.mime.multipart import MIMEMultipart

#port=465

# Create the plain-text and HTML version of your message
text = """\
Hi,
This is today's Daily Mail containing SmartHouse  report
"""
html = """\
<html>
<head>
<style>
table, th, td {
  border: 1px solid black;
  border-collapse: collapse;
}
</style>
</head>
  <body>
    <p>Hi,<br>
       This is today's Daily Mail containing SmartHouse report
       <table">
          <tr>
	    <th> Temperature </th>
            <th> LED state </th>
          </tr>
          <tr>
            <td> 23*C </td>
            <td> OFF </td>
          </tr>
       </table> <br>
    </p>
  </body>
</html>
"""

class Mail:

    def __init__(self):
        self.port=587
        self.password="2Run-re#6564"
        self.smtp_server = "smtp.gmail.com"
        self.sender_email = "ivan.adelina02@gmail.com"
        self.reciever_email = "adelina.ivan97@gmail.com"


    def create_message(self):
        message = MIMEMultipart("alternative")
        message["Subject"] = "Smart House Daily report"
        message["From"] = self.sender_email
        message["To"] = self.reciever_email

        # Turn these into plain/html MIMEText objects
        part1 = MIMEText(text, "plain")
        part2 = MIMEText(html, "html")

        # Add HTML/plain-text parts to MIMEMultipart message
        # The email client will try to render the last part first
        message.attach(part1)
        message.attach(part2)

        context = ssl.create_default_context()
        return message

    def send_mail(self):
        message = self.create_message()
        server = smtplib.SMTP(self.smtp_server, self.port)
        server.starttls()
        server.login(self.sender_email, self.password)
        server.sendmail(self.sender_email, self.reciever_email, message.as_string())
        server.quit()
