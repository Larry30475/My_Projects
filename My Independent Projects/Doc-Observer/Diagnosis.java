package com.jetbrains;
import java.util.Scanner;
public class Diagnosis{
    Scanner sc;
    public Diagnosis(){
        sc = new Scanner(System.in);
    }
    public void diag(){
        System.out.println("How do you feel?");
        String condition = sc.nextLine();
        System.out.println("What temperature do you have?");
        int temperature = sc.nextInt();
        sc.nextLine();
        System.out.println("Do you have a cough?");
        String cough = sc.nextLine();
        double temper = temperature;
        sc.reset();
        if (condition.equals("sick")) {
            if (temper >= 37.0) {
                if (cough.equals("yes")) {
                    System.out.println("You have a strong cough and your temperature is " + temper + ". Take that pill and lie in a bed at least for a day. And drink lot of liquid");
                } else {
                    System.out.println("Your temperature is " + temper + ". Take this mixture and lie in a bed at least for a day. And drink lot of liquid");
                }
            } else {
                System.out.println("You should go to the hospital, because you`re really " + condition);
            }
        } else {
            System.out.println("You`re " + condition + " as a horse. Don`t worry about a thing, go home and just rest a bit");
        }
    }
}