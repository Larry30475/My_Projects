#include "Zad1.h"

void v_alloc_table_add_5(int iSize)
{
    if (iSize <= 0)
    {
        std::cout << "Rozmiar musi byc wiekszy od 0";
    }
    else
    {
        int* pi_table;
        pi_table = new int[iSize];
        for (int i = 0; i < iSize; i++)
        {
            pi_table[i] = i + 5;
            std::cout << pi_table[i] << " ";
        }
        delete pi_table;
    }// if (iSize <= 0)
    std::cout << std::endl;
}//v_alloc_table_add_5

bool b_alloc_table_2_dim(int*** piTable, int iSizeX, int iSizeY)
{
    if (iSizeX <= 0 && iSizeY <= 0)
    {
        std::cout << "Rozmiar musi byc wiekszy od 0";
    }
    else
    {
        int** pi_table;
        pi_table = new int* [iSizeX];
        for (int i = 0; i < iSizeX; i++)
        {
            pi_table[i] = new int[iSizeY];
            for (int ii = 0; ii < iSizeY; ii++) {
                pi_table[i][ii] = ii + i + 15;
                std::cout << pi_table[i][ii] << " ";
            }
            std::cout << std::endl;
            std::cout << std::endl;
        }
        *piTable = pi_table;
        return true;
    }//if (iSizeX >= 0 && iSizeY >= 0)
    return false;
}//b_alloc_table_2_dim(int *** piTable, int iSizeX, int iSizeY)

bool b_dealloc_table_2_dim(int*** piTable, int iSizeX, int iSizeY)
{
    for (int i = 0; i < iSizeX; i++)
    {
        delete (*piTable)[i];
    }//for (int i = 0; i < iSizeX; i++)
    delete *piTable;
    piTable = 0;
    if (piTable == NULL)
    {
        std::cout << "usuwam tablice" << std::endl;
        return true;
    }
    else
    {
        return false;
    }//if (piTable == NULL)
}//b_dealloc_table_2_dim(int ***piTable, int iSizeX, int iSizeY)

void v_mod_tab(CTable* pcTab, int iNewSize)
{
    pcTab->bSetNewSize(iNewSize);
    std::cout << "Po modufikacji: " << pcTab->getSizeTable() << std::endl;
}//v_mod_tab(CTable * pcTab, int iNewSize)

void v_mod_tab2(CTable cTab, int iNewSize)
{
    cTab.bSetNewSize(iNewSize);
    std::cout << "Po modufikacji: " << cTab.getSizeTable() << std::endl;
}//v_mod_tab2(CTable cTab, int iNewSize)
