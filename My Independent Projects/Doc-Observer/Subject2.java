package com.jetbrains;
public interface Subject2 {
    boolean getState();
    void setState(boolean state);
    void addOb(Observer2 o);
    void notifyOb();
}