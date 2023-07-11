#include "CNodeDynamic.h"

CNodeDynamic::CNodeDynamic() 
{
	i_val = 0;
	pc_parent_node = NULL;
}//CNodeDynamic::CNodeDynamic()


void CNodeDynamic::vSetValue(int iNewVal) 
{
	i_val = iNewVal;
}//void CNodeDynamic::vSetValue(int i_new_val)

int CNodeDynamic::iGetChildrenNumber() 
{
	return(v_children.size());
}//int CNodeDynamic::iGetChildrenNumber()

void CNodeDynamic::vAddNewChild()
{
	CNodeDynamic* new_child = new CNodeDynamic();
	new_child->pc_parent_node = this;
	v_children.push_back(new_child);
}//void CNodeDynamic::vAddNewChild()

CNodeDynamic* CNodeDynamic::pcGetChild(int iChildOffset)
{
	if (iChildOffset < 0 && iChildOffset > iGetChildrenNumber()) 
	{
		return NULL;
	}//if (i_child_offset < 0 && i_child_offset > iGetChildrenNumber())
	return v_children.at(iChildOffset);
}//CNodeDynamic* CNodeDynamic::pcGetChild(int i_child_offset)

void CNodeDynamic::vPrint() 
{
	std::cout << i_val;
}//void CNodeDynamic::vPrint()

void CNodeDynamic::vPrintAllBelow()
{
	vPrint();
	std::cout << " ";
	for (int i = 0; i < v_children.size(); i++) 
	{
		v_children.at(i)->vPrintAllBelow();
	}//for (int i = 0; i < v_children.size(); i++)
}//void CNodeDynamic::vPrintAllBelow()

void CNodeDynamic::vAddNewChild(CNodeDynamic* child)
{
	CNodeDynamic* new_child = new CNodeDynamic;
	new_child->i_val = child->i_val;
	new_child->pc_parent_node = this;

	for (int i = 0; i < child->iGetChildrenNumber(); i++)
	{
		new_child->v_children.push_back(child->pcGetChild(i));
	}//for (int i = 0; i < child->iGetChildrenNumber(); i++)
	v_children.push_back(new_child);
}//void CNodeDynamic::vAddNewChild(CNodeDynamic* child)

bool CNodeDynamic::vRemoveChild() 
{
	int offset = -1;
	for (int i = 0; i < pc_parent_node->iGetChildrenNumber(); i++) 
	{
		if (this == pc_parent_node->v_children.at(i))
		{
			offset = i;
		}//if (this == pc_parent_node->v_children.at(i))
	}//for (int i = 0; i < pc_parent_node->iGetChildrenNumber(); i++)
	if (offset == -1 || this == NULL)
	{
		return false;
	}//if (offset == -1 || this == NULL)
	pc_parent_node->v_children.erase(pc_parent_node->v_children.begin() + offset);
	return true;
}//bool CNodeDynamic::vRemoveChild()

CNodeDynamic::~CNodeDynamic()
{
	for (int i = 0; i < iGetChildrenNumber(); i++)
	{
		delete v_children.at(i);
	}//for (int i = 0; i < iGetChildrenNumber(); i++)
}//CNodeDynamic::~CNodeDynamic()