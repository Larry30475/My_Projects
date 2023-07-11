#pragma once

#include "CTable.h"
#include <stdio.h>
#include <string>
#include <iostream>

void v_alloc_table_add_5(int iSize);

bool b_alloc_table_2_dim(int*** piTable, int iSizeX, int iSizeY);

bool b_dealloc_table_2_dim(int*** piTable, int iSizeX, int iSizeY);

void v_mod_tab(CTable* pcTab, int iNewSize);

void v_mod_tab2(CTable cTab, int iNewSize);