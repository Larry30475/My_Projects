#pragma once

#include <string>
#include <iostream>

class CTable
{
public:
    CTable();
    CTable(std::string sName, int iTableLen);
    CTable(CTable& pcOther);
    ~CTable();
    void vSetName(std::string sName);
    bool bSetNewSize(int iTableLen);
    CTable* pcClone();
    int getSizeTable();
    void operator=(CTable& pcOther);
private:
    std::string s_name;
    int* piTable;
    int iSize_of_table;
};