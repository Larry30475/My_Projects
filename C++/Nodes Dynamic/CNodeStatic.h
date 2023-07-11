#pragma once

#include <vector>
#include <iostream>

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
	void vPrintUp();

	void vAddNewChild(CNodeStatic* newChild);
	bool vRemoveChild();

private:
	std::vector<CNodeStatic> v_children;
	CNodeStatic* pc_parent_node;
	int i_val;
};