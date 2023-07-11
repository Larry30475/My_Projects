#include <iostream>
#include <string>

#include "CMySmartPointer.h"
#include "CTable.h"

int main()
{

    //dzialanie move

    CTable c_tab_0, c_tab_1;
    c_tab_0.bSetNewSize(6);
    c_tab_1.bSetNewSize(4);
    /* initialize table */
    c_tab_0 = std::move(c_tab_1);
    c_tab_0.vSetValueAt(2, 123);
    c_tab_0.vPrint();
    c_tab_1.vPrint();

    CTable c_tab_2;
    c_tab_2.bSetNewSize(5);
    c_tab_2 + std::move(c_tab_0);
    c_tab_2.vPrint();

    //dzialanie Smart Pointera

    int* piTable = new int [5];
    int* piTable2 = new int[5];
    
    for (int i = 0; i < 5; i++)
    {
        piTable[i] = 10 + i;
        piTable2[i] = 20 + i;
    }

    std::cout << "pierwsza tablica" << std::endl;

    for (int i = 0; i < 5; i++)
    {
        std::cout << piTable[i] << " ";
    }
    std::cout << std::endl;

    std::cout << "druga tablica" << std::endl;

    for (int i = 0; i < 5; i++)
    {
        std::cout << piTable2[i] << " ";
    }
    std::cout << std::endl;

    std::cout << "przyrownanie wskaznikow piTable = piTable2" << std::endl;

    piTable = piTable2;

    std::cout << "pierwsza tablica" << std::endl;

    for (int i = 0; i < 5; i++)
    {
        std::cout << piTable[i] << " ";
    }
    std::cout << std::endl;

    std::cout << "druga tablica" << std::endl;

    for (int i = 0; i < 5; i++)
    {
        std::cout << piTable2[i] << " ";
    }
    std::cout << std::endl;

    return 0;
}