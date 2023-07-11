#pragma once

#include <vector>
#include <iostream>

template <typename T> class CNodeStatic
{
public:
	CNodeStatic();
	~CNodeStatic();
	void vSetValue(T iNewVal);
	int iGetChildrenNumber();
	void vAddNewChild();
	CNodeStatic<T>* pcGetChild(int iChildOffset);
	void vPrint();
	void vPrintAllBelow();
	void vPrintUp();

	void vAddNewChild(CNodeStatic<T>* newChild);
	bool vRemoveChild();

private:
	std::vector<CNodeStatic<T>> v_children;
	CNodeStatic<T>* pc_parent_node;
	T i_val;
};

template <typename T>
CNodeStatic<T>::CNodeStatic()
{
	if (T == std::string)
	{
		i_val = "";
	}
	else
	{
		i_val = 0;
	}
	pc_parent_node = NULL;
}//CNodeStatic::CNodeStatic()

template <typename T>
CNodeStatic<T>::~CNodeStatic()
{

}//CNodeStatic::~CNodeStatic()

template <typename T>
void CNodeStatic<T>::vSetValue(T iNewVal)
{
	i_val = iNewVal;
}//void CNodeStatic::vSetValue(int iNewVal)

template <typename T>
int CNodeStatic<T>::iGetChildrenNumber()
{
	return(v_children.size());
}//int CNodeStatic::iGetChildrenNumber()

template <typename T>
void CNodeStatic<T>::vAddNewChild()
{
	CNodeStatic<T> newChild;
	newChild.pc_parent_node = this;
	v_children.push_back(newChild);
}//void CNodeStatic::vAddNewChild()

template <typename T>
CNodeStatic<T>* CNodeStatic<T>::pcGetChild(int iChildOffset)
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

template <typename T>
void CNodeStatic<T>::vPrint()
{
	std::cout << " " << i_val;
}//void CNodeStatic::vPrint() 

template <typename T>
void CNodeStatic<T>::vPrintAllBelow()
{
	vPrint();
	std::cout << " ";
	for (int i = 0; i < iGetChildrenNumber(); i++)
	{
		v_children.at(i).vPrintAllBelow();
	}//for (int i = 0; i < iGetChildrenNumber(); i++)
}//void CNodeStatic::vPrintAllBelow()

template <typename T>
void CNodeStatic<T>::vPrintUp()
{
	vPrint();
	if (pc_parent_node != NULL)
	{
		pc_parent_node->vPrintUp();
	}//if (pc_parent_node != NULL)
}

template <typename T>
void CNodeStatic<T>::vAddNewChild(CNodeStatic<T>* newChild)
{
	CNodeStatic<T> new_child;
	new_child.i_val = newChild->i_val;
	new_child.pc_parent_node = this;
	for (int i = 0; i < newChild->iGetChildrenNumber(); i++)
	{
		new_child.v_children.push_back(*(newChild->pcGetChild(i)));
	}//for (int i = 0; i < newChild->iGetChildrenNumber(); i++)
	v_children.push_back(new_child);
}//void CNodeStatic::vAddNewChild(CNodeStatic* newChild)

template <typename T>
bool CNodeStatic<T>::vRemoveChild()
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