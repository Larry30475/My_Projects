package com.jetbrains;
import javax.swing.JOptionPane;
public class Main {
    public static void main(String[] args) {
        double rez = 0;
        String [] matact = new String [4];
        matact[0] = "+";
        matact[1] = "-";
        matact[2] = "*";
        matact[3] = "/";
        Object[] endopt = {"YES", "NO"};
        JOptionPane.showMessageDialog(null, "This is calculator. Please enter ONLY numbers. In other way it will give an error", "Calculator", JOptionPane.PLAIN_MESSAGE);
        while(true) {
            String fn = JOptionPane.showInputDialog(null, "Enter first number: ", "Calculator", JOptionPane.PLAIN_MESSAGE);
            double num1 = Integer.parseInt(fn);
            Object selact = JOptionPane.showInputDialog(null, "Choose action: ", "Calculator", JOptionPane.PLAIN_MESSAGE, null, matact, matact[0]);
            String sn = JOptionPane.showInputDialog(null, "Enter second number: ", "Calculator", JOptionPane.PLAIN_MESSAGE);
            double num2 = Integer.parseInt(sn);
            if (selact == "+") {
                rez = num1 + num2;
            }
            if (selact == "-") {
                rez = num1 - num2;
            }
            if (selact == "*") {
                rez = num1 * num2;
            }
            if (selact == "/") {
                rez = num1 / num2;

            }
            JOptionPane.showMessageDialog(null, "The result is " + rez, "Calculator", JOptionPane.PLAIN_MESSAGE);
            int finmes = JOptionPane.showOptionDialog(null, "Do you want to continue?", "Calculator", JOptionPane.YES_NO_OPTION, JOptionPane.QUESTION_MESSAGE, null, endopt, endopt[0]);
            if (finmes == JOptionPane.NO_OPTION){
                JOptionPane.showMessageDialog(null, "Thank you for using my calculator", "Calculator", JOptionPane.PLAIN_MESSAGE);
                break;
            }
        }
    }
}
