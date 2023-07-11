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
	CNodeStatic newChild;
	newChild.pc_parent_node = this;
	v_children.push_back(newChild);
}//void CNodeStatic::vAddNewChild()

CNodeStatic* CNodeStatic::pcGetChild(int iChildOffset)
{
	if (iChildOffset < 0 && iChildOffset > iGetChildrenNumber())
	{
		return NULL;
	}
	else
	{
		return &v_children.at(iChildOffset);
	}//if (iChildOffset < 0 && iChildOffset > iGetChildrenNumber())
}//CNodeStatic* CNodeStatic::pcGetChild(int iChildOffset)

void CNodeStatic::vPrint() 
{
	std::cout << " " << i_val;
}//void CNodeStatic::vPrint() 

void CNodeStatic::vPrintAllBelow()
{
	vPrint();
	std::cout << " ";
	for (int i = 0; i < iGetChildrenNumber(); i++)
	{
		v_children.at(i).vPrintAllBelow();
	}//for (int i = 0; i < iGetChildrenNumber(); i++)
}//void CNodeStatic::vPrintAllBelow()

void CNodeStatic::vPrintUp()
{
	vPrint();
	if (pc_parent_node != NULL)
	{
		pc_parent_node->vPrintUp();
	}//if (pc_parent_node != NULL)
}

void CNodeStatic::vAddNewChild(CNodeStatic* newChild) 
{
	CNodeStatic new_child;
	new_child.i_val = newChild->i_val;
	new_child.pc_parent_node = this;
	for (int i = 0; i < newChild->iGetChildrenNumber(); i++)
	{
		new_child.v_children.push_back(*(newChild->pcGetChild(i)));
	}//for (int i = 0; i < newChild->iGetChildrenNumber(); i++)
	v_children.push_back(new_child);
}//void CNodeStatic::vAddNewChild(CNodeStatic* newChild)

bool CNodeStatic::vRemoveChild() 
{
	int offset = -1;
	for (int i = 0; i < pc_parent_node->iGetChildrenNumber(); i++) 
	{
		if (this == &(pc_parent_node->v_children.at(i))) 
		{
			offset = i;
		}//if (this == &(pc_parent_node->v_children.at(i)))
	}//for (int i = 0; i < pc_parent_node->iGetChildrenNumber(); i++)
	if (this == NULL || offset == -1) 
	{
		return false;
	}//if (this == NULL || offset == -1)
	pc_parent_node->v_children.erase(pc_parent_node->v_children.begin() + offset);
	return true;
}//bool CNodeStatic::vRemoveChild()