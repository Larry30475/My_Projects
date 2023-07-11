#include "CTable.h"

const std::string s_name_const = "Hello";
const int i_size_const = 10;

CTable::CTable()
{
    s_name = s_name_const;
    iSize_of_table = i_size_const;
    std::cout << "bezp: " << s_name << " " << iSize_of_table << std::endl;
    piTable = new int[iSize_of_table];
    for (int i = 0; i < iSize_of_table; i++)
    {
        piTable[i] = 10 + i;
    }
}//CTable::CTable()

CTable::CTable(std::string sName, int iTableLen)
{
    if (iTableLen <= 0)
    {
        std::cout << "Rozmiar musi byc wiekszy od 0" << std::endl;
    }
    else
    {
        s_name = sName;
        iSize_of_table = iTableLen;
        std::cout << "parametr: " << s_name << " " << iSize_of_table << std::endl;
        piTable = new int[iSize_of_table];
        for (int i = 0; i < iSize_of_table; i++) {
            piTable[i] = i + 10;
            std::cout << piTable[i] << " ";
        }
        std::cout << std::endl;
    }
}//CTable::CTable(std::string sName, int iTableLen)

CTable::CTable(CTable& pcOther)
{
    s_name = pcOther.s_name + "_copy";
    iSize_of_table = pcOther.iSize_of_table;

    piTable = new int[(pcOther.iSize_of_table)];
    for (int i = 0; i < pcOther.iSize_of_table; i++)
    {
        piTable[i] = pcOther.piTable[i];
        std::cout << piTable[i] << " ";
    }//for (int i = 0; i < pcOther.iSize_of_table; i++)
    std::cout << std::endl;
    std::cout << "kopiuj: " << s_name << " " << iSize_of_table << std::endl;
}//CTable::CTable(CTable& pcOther)

/*
CTable::~CTable()
{
    if (piTable != NULL)
    {
        delete[] piTable;
        std::cout << "usuwam: " << s_name << std::endl;
    }
    else {
        std::cout << "usuwam: " << s_name << std::endl;
    }//if (piTable != NULL)
}//CTable::~CTable()
*/

void CTable::vSetName(std::string sName)
{
    std::cout << "Do modyfikacji: " << s_name << std::endl;
    s_name = sName;
    std::cout << "Po modyfikacji: " << s_name << std::endl;
}//void CTable::vSetName(std::string sName)

bool CTable::bSetNewSize(int iTableLen)
{
    if (iTableLen <= 0)
    {
        std::cout << "Rozmiar musi byc wiekszy od 0" << std::endl;
        return false;
    }
    else
    {
        int* piTable_new = new int[iTableLen];

        for (int i = 0; i < iTableLen; i++)
        {
            piTable_new[i] = piTable[i];
        }

        delete[] piTable;
        piTable = piTable_new;
        std::cout << "Do modyfikacji: " << iSize_of_table << std::endl;
        iSize_of_table = iTableLen;
        std::cout << "Po modyfikacji: " << iSize_of_table << std::endl;
        return true;
    }//if (iTableLen <= 0)
}//bool CTable::bSetNewSize(int iTableLen)

CTable* CTable::pcClone()
{
    std::string sNameNew = s_name;
    int iSizeOfTableNew = iSize_of_table;
    CTable* cTableNew = new CTable(sNameNew, iSizeOfTableNew);
    std::cout << "To jest klon: " << s_name << " " << iSize_of_table << std::endl;
    return cTableNew;
}//CTable* CTable::pcClone()

int CTable::getSizeTable()
{
    return iSize_of_table;
}//int CTable::getSizeTable()

void CTable::operator=(CTable& pcOther)
{
    piTable = pcOther.piTable;
    iSize_of_table = pcOther.iSize_of_table;
}//void CTable::operator=(CTable &pcOther)

void CTable::vSetValueAt(int iOffset, int iNewVal)
{
    if (iOffset > iSize_of_table) 
    {
        std::cout << "Offset is out of range" << std::endl;
    }
    else
    {
        std::cout << "Do modyfikacji: " << piTable[iOffset] << std::endl;
        piTable[iOffset] = iNewVal;
        std::cout << "Po modyfikacji: " << piTable[iOffset] << std::endl;
    }//if (iOffset >= iSize_of_table)
}//void CTable::vSetValueAt(int iOffset, int iNewVal)

void CTable::vPrint() 
{
    for (int i = 0; i < iSize_of_table; i++)
    {
        std::cout << piTable[i] << " ";
    }
    std::cout << std::endl;
}//void CTable::vPrint()

void CTable::operator+(CTable& table2)
{
    bSetNewSize(iSize_of_table + table2.iSize_of_table);
    for (int i = 0; i < table2.iSize_of_table; i++)
    {
        piTable[iSize_of_table - table2.iSize_of_table + i] = table2.piTable[i];
    }
}//void CTable::operator+(CTable& table2)