package com.jetbrains;
public class Doc implements Observer2{
    private String doctor;
    public Doc(String doctor){
        this.doctor = doctor;
    }
    public void update1(Object patient){
        if(((Patient) patient).getState()) {
            System.out.println("My name is " + doctor + " and I am your doctor. So " + ((Patient) patient).getPatient() + " what`s your problem?");
        }
    }
}