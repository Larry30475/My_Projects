#pragma once

#include <vector>
#include <iostream>

class CNodeDynamic
{
public:
	CNodeDynamic();
	~CNodeDynamic();
	void vSetValue(int iNewVal);
	int iGetChildrenNumber();
	void vAddNewChild();
	CNodeDynamic* pcGetChild(int iChildOffset);
	void vPrint();
	void vPrintAllBelow();

	void vAddNewChild(CNodeDynamic* newChild);
	bool vRemoveChild();

private:
	std::vector<CNodeDynamic*> v_children;
	CNodeDynamic* pc_parent_node;
	int i_val;
};