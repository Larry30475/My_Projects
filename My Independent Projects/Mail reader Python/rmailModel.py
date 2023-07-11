from typing import List
import imaplib
import email
from email.header import decode_header


class rmailModel():

    def __init__(self):
        self.__mail_string = ''

    def get_mails(self):
        return self.__mail_string

    def load_mails(self, loginLine: str, passLine: str, numberLine: int) -> str:
        global body
        try:
            imap_server = "outlook.office365.com"
            imap = imaplib.IMAP4_SSL(imap_server)
            imap.login(loginLine, passLine)

            status, messages = imap.select("INBOX")

            self.__mail_string = " "

            if not numberLine.lstrip('-').isdigit():
                self.__mail_string = "Bruuuh u can't do that it is not a digit"
                return

            numberMess = int(numberLine)

            if numberMess < 0:
                self.__mail_string = "Bruuuh u can't do that the number is below 0"
                return

            messages = int(messages[0])
            numberMess = int(numberLine)

            if numberMess > messages:
                self.__mail_string += "Sorry, but we couldn't find " + str(
                    numberMess) + " messages. Here all your messages XD\n\n\n"
            for i in range(messages, messages - numberMess, -1):
                res, msg = imap.fetch(str(i), "(RFC822)")
                for response in msg:
                    if isinstance(response, tuple):
                        msg = email.message_from_bytes(response[1])
                        subject, encoding = decode_header(msg["Subject"])[0]
                        if isinstance(subject, bytes):
                            subject = subject.decode(encoding)
                        From, encoding = decode_header(msg.get("From"))[0]
                        if isinstance(From, bytes):
                            From = From.decode(encoding)
                        self.__mail_string += "Subject:" + subject + "\n"
                        self.__mail_string += "From:" + From + "\n"
                        if msg.is_multipart():
                            for part in msg.walk():
                                content_type = part.get_content_type()
                                content_disposition = str(part.get("Content-Disposition"))
                                try:
                                    body = part.get_payload(decode=True).decode()
                                except:
                                    pass
                                if content_type == "text/plain" not in content_disposition:
                                    self.__mail_string += body + "\n"
                        else:
                            content_type = msg.get_content_type()
                            body = msg.get_payload(decode=True).decode()
                            if content_type == "text/plain":
                                self.__mail_string += body + "\n"
                                self.__mail_string += "=" * 100 + "\n"
            imap.close()
            imap.logout()
            return self.__mail_string
        except:
            return ''
