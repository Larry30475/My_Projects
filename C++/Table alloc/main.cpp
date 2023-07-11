#include "CTable.h"
#include "Zad1.h"

int main()
{
   
    CTable c_tab_0, c_tab_1;
    c_tab_0.bSetNewSize(6);
    c_tab_1.bSetNewSize(4);
    c_tab_0 = c_tab_1;

    return 0;
} //int main()
