package com.jetbrains;
public class Main {
    public static void main(String[]args){
        Diagnosis diagnosis = new Diagnosis();
        Doc doc1 = new Doc("Phillip");
        Patient pat1 = new Patient("Anna");
        Patient pat2 = new Patient("Larry");
        Patient pat3 = new Patient("Gabby");
        pat1.addOb(doc1);
        pat2.addOb(doc1);
        pat3.addOb(doc1);
        pat1.setState(true);
        diagnosis.diag();
        pat2.setState(true);
        diagnosis.diag();
        pat3.setState(true);
        diagnosis.diag();
    }
}