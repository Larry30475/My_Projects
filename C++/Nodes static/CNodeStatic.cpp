#include "CNodeStatic.h"

CNodeStatic::CNodeStatic() 
{
	i_val = 0;
	pc_parent_node = NULL;
}//CNodeStatic::CNodeStatic()

CNodeStatic::~CNodeStatic()
{
	
}//CNodeStatic::~CNodeStatic()

void CNodeStatic::vSetValue(int iNewVal)
{
	i_val = iNewVal;
}//void CNodeStatic::vSetValue(int iNewVal)

int CNodeStatic::iGetChildrenNumber()
{
	return(v_children.size());
}//int CNodeStatic::iGetChildrenNumber()

void CNodeStatic::vAddNewChild()
{
	CNodeStatic new_child;
	new_child.pc_parent_node = this;
	v_children.push_back(new_child);
}//void CNodeStatic::vAddNewChild()

CNodeStatic* CNodeStatic::pcGetChild(int iChildOffset)
{
	if (iChildOffset < 0 && iChildOffset > iGetChildrenNumber())
	{
		return NULL;
	}
	return &v_children.at(iChildOffset);
}//CNodeStatic* CNodeStatic::pcGetChild(int iChildOffset)

void CNodeStatic::vPrint()
{
	std::cout << " " << i_val;
}//void CNodeStatic::vPrint() 

void CNodeStatic::vPrintAllBelow()
{
	vPrint();
	std::cout << " ";
	for (int i = 0; i < iGetChildrenNumber(); i++) {
		v_children.at(i).vPrintAllBelow();
	}
}//void CNodeStatic::vPrintAllBelow()