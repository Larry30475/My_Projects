#pragma once


#include <iostream>
#include <vector>

class CNodeStatic
{
public:
	CNodeStatic();
	~CNodeStatic();
	void vSetValue(int iNewVal);
	int iGetChildrenNumber();
	void vAddNewChild();
	CNodeStatic* pcGetChild(int iChildOffset);
	void vPrint();
	void vPrintAllBelow();
private:
	std::vector<CNodeStatic> v_children;
	CNodeStatic* pc_parent_node;
	int i_val;
};//class CNodeStatic