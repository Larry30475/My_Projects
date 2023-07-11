from PyQt5.QtWidgets import QMessageBox


class rmailController:

    def __init__(self, v_obj, m_obj):
        self.v_obj = v_obj
        self.m_obj = m_obj

    def displayError(self):

        QMessageBox.about(self.v_obj, "Error", "Load failed, try again")

    def display_recived_mails(self):

        self.m_obj.load_mails(self.v_obj.loginLine.text(), self.v_obj.passLine.text(), self.v_obj.numberLine.text())
        recived_msg = self.m_obj.get_mails()
        if recived_msg == "":
            self.displayError()
        else:
            QMessageBox.about(self.v_obj, "Msg", recived_msg)

    def closeApp(self):

        self.v_obj.close()
