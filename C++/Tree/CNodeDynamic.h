#pragma once

#include <vector>
#include <iostream>

template <typename T> class CNodeDynamic
{
public:
	CNodeDynamic();
	~CNodeDynamic();
	void vSetValue(T iNewVal);
	int iGetChildrenNumber();
	void vAddNewChild();
	CNodeDynamic<T>* pcGetChild(int iChildOffset);
	void vPrint();
	void vPrintAllBelow();

	void vAddNewChild(CNodeDynamic<T>* newChild);
	bool vRemoveChild();

private:
	std::vector<CNodeDynamic<T>*> v_children;
	CNodeDynamic<T>* pc_parent_node;
	T i_val;
};

template <typename T>
CNodeDynamic<T>::CNodeDynamic()
{
	i_val = 0;
	pc_parent_node = NULL;
}//CNodeDynamic::CNodeDynamic()

template <typename T>
void CNodeDynamic<T>::vSetValue(T iNewVal)
{
	i_val = iNewVal;
}//void CNodeDynamic::vSetValue(int i_new_val)

template <typename T>
int CNodeDynamic<T>::iGetChildrenNumber()
{
	return(v_children.size());
}//int CNodeDynamic::iGetChildrenNumber()

template <typename T>
void CNodeDynamic<T>::vAddNewChild()
{
	CNodeDynamic<T>* new_child = new CNodeDynamic<T>();
	new_child->pc_parent_node = this;
	v_children.push_back(new_child);
}//void CNodeDynamic::vAddNewChild()

template <typename T>
CNodeDynamic<T>* CNodeDynamic<T>::pcGetChild(int iChildOffset)
{
	if (iChildOffset < 0 && iChildOffset > iGetChildrenNumber())
	{
		return NULL;
	}//if (i_child_offset < 0 && i_child_offset > iGetChildrenNumber())
	return v_children.at(iChildOffset);
}//CNodeDynamic* CNodeDynamic::pcGetChild(int i_child_offset)

template <typename T>
void CNodeDynamic<T>::vPrint()
{
	std::cout << i_val;
}//void CNodeDynamic::vPrint()

template <typename T>
void CNodeDynamic<T>::vPrintAllBelow()
{
	vPrint();
	std::cout << " ";
	for (int i = 0; i < v_children.size(); i++)
	{
		v_children.at(i)->vPrintAllBelow();
	}//for (int i = 0; i < v_children.size(); i++)
}//void CNodeDynamic::vPrintAllBelow()

template <typename T>
void CNodeDynamic<T>::vAddNewChild(CNodeDynamic<T>* child)
{
	CNodeDynamic<T>* new_child = new CNodeDynamic<T>();
	new_child->i_val = child->i_val;
	new_child->pc_parent_node = this;

	for (int i = 0; i < child->iGetChildrenNumber(); i++)
	{
		new_child->v_children.push_back(child->pcGetChild(i));
	}//for (int i = 0; i < child->iGetChildrenNumber(); i++)
	v_children.push_back(new_child);
}//void CNodeDynamic::vAddNewChild(CNodeDynamic* child)

template <typename T>
bool CNodeDynamic<T>::vRemoveChild()
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

template <typename T>
CNodeDynamic<T>::~CNodeDynamic()
{
	for (int i = 0; i < iGetChildrenNumber(); i++)
	{
		delete v_children.at(i);
	}//for (int i = 0; i < iGetChildrenNumber(); i++)
}//CNodeDynamic::~CNodeDynamic()