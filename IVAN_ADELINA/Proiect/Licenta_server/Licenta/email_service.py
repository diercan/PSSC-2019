import smtplib, ssl
from email.mime.text import MIMEText
from email.mime.multipart import MIMEMultipart

#port=465
port=587
password="2Run-re#6564"
smtp_server = "smtp.gmail.com"
sender_email = "ivan.adelina02@gmail.com"
reciever_email = "brancsebastian@gmail.com"

message = MIMEMultipart("alternative")
message["Subject"] = "Smart House Daily report"
message["From"] = sender_email
message["To"] = reciever_email

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
       This is today's Dailt Mail containing SmartHouse report
       <table">
          <tr>
	    <th> Temperature </th>
            <th> Last LED state </th>
          </tr>
          <tr>
            <td> 23*C </td>
            <td> ON </td>
          </tr>
       </table> <br>
 <a href="http://www.realpython.com">Real Python</a> 
       has many great tutorials.
    </p>
  </body>
</html>
"""

# Turn these into plain/html MIMEText objects
part1 = MIMEText(text, "plain")
part2 = MIMEText(html, "html")

# Add HTML/plain-text parts to MIMEMultipart message
# The email client will try to render the last part first
message.attach(part1)
message.attach(part2)


context = ssl.create_default_context()

server = smtplib.SMTP(smtp_server, port)
server.starttls()
server.login(sender_email, password)
server.sendmail(sender_email, reciever_email, message.as_string())
server.quit()
