package com.jetbrains;
import java.util.ArrayList;
public class Patient implements Subject2{
    public ArrayList<Observer2>observers = new ArrayList<>();
    private boolean state;
    private String patientName;
    public Patient(String patientName){
        this.patientName = patientName;
    }
    public String getPatient(){
        return patientName;
    }
    public boolean getState() {
        return state;
    }
    public void setState(boolean state) {
        this.state = state;
        notifyOb();
    }
    @Override
    public void addOb(Observer2 observer){
        observers.add(observer);
    }
    @Override
    public void notifyOb(){
        for(Observer2 observer: observers){
            observer.update1(this);
        }
    }
}