#pragma once

#include <string>
#include <iostream>

class CTable
{
public:
    CTable();
    CTable(std::string sName, int iTableLen);
    CTable(CTable&& pcOther);
    ~CTable();

    void vSetName(std::string&& sName);
    bool bSetNewSize(int iTableLen);
    int getSizeTable();
    void vSetValueAt(int iOffset, int iNewVal);
    void vPrint();

    CTable &operator=(CTable&& cOther);
    CTable &operator+(CTable&& table2);

private:
    std::string s_name;
    int* piTable;
    int iSize_of_table;

    void move(CTable&& other);
};