from PyQt5 import QtWidgets
from PyQt5.QtWidgets import QLineEdit, QLabel, QPushButton, QMainWindow
from PyQt5.QtCore import QSize
import sys
from rmailController import rmailController
from rmailModel import rmailModel


class MailProg(QMainWindow):
    def __init__(self, m_obj):
        self.m_obj = m_obj
        self.c_obj = rmailController(self, self.m_obj)

        QMainWindow.__init__(self)
        self.setMinimumSize(QSize(350, 180))
        self.setWindowTitle("Sign-in")

        self.loginLabel = QLabel(self)
        self.loginLabel.setText('Login: ')
        self.loginLine = QLineEdit(self)
        self.loginLine.setText("testforfinalproject@outlook.com")

        self.loginLine.move(85, 20)
        self.loginLine.resize(250, 25)
        self.loginLabel.move(20, 20)

        self.passLabel = QLabel(self)
        self.passLabel.setText('Password: ')
        self.passLine = QLineEdit(self)
        self.passLine.setText("Qwerty123456")
        self.passLine.move(85, 50)
        self.passLine.resize(250, 25)
        self.passLabel.move(20, 50)

        self.numberLabel = QLabel(self)
        self.numberLabel.setText('How many mails to fetch: ')
        self.numberLine = QLineEdit(self)

        self.numberLine.move(175, 80)
        self.numberLine.resize(50, 25)
        self.numberLabel.move(20, 80)
        self.numberLabel.resize(160, 30)

        pybutton1 = QPushButton('OK', self)
        pybutton1.clicked.connect(self.c_obj.display_recived_mails)
        pybutton1.resize(100, 32)
        pybutton1.move(20, 120)

        pybutton2 = QPushButton('Exit', self)
        pybutton2.clicked.connect(self.c_obj.closeApp)
        pybutton2.resize(100, 32)
        pybutton2.move(230, 120)


if __name__ == '__main__':
    newModel = rmailModel()
    app = QtWidgets.QApplication(sys.argv)
    mail = MailProg(newModel)
    mail.show()
    sys.exit(app.exec_())
