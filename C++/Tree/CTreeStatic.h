#pragma once
#include "CNodeStatic.h"

template <typename T> class CTreeStatic
{
public:
	CTreeStatic();
	~CTreeStatic();
	CNodeStatic<T>* pcGetRoot();
	void vPrintTree();
	bool bMoveSubtree(CNodeStatic<T>* pcParentNode, CNodeStatic<T>* pcNewChildNode);
private:
	CNodeStatic<T> c_root;
};//class CTreeStatic

template <typename T>
CTreeStatic<T>::CTreeStatic()
{
	c_root = CNodeStatic<T>();
}//CTreeStatic::CTreeStatic()

template <typename T>
CNodeStatic<T>* CTreeStatic<T>::pcGetRoot()
{
	return &c_root;
}//CNodeStatic* CTreeStatic::pcGetRoot()

template <typename T>
void CTreeStatic<T>::vPrintTree()
{
	c_root.vPrintAllBelow();
}//void CTreeStatic::vPrintTree()

template <typename T>
bool CTreeStatic<T>::bMoveSubtree(CNodeStatic<T>* pcParentNode, CNodeStatic<T>* pcNewChildNode)
{
	if (pcParentNode == NULL || pcNewChildNode == NULL)
	{
		return false;
	}//if (pcParentNode == NULL || pcNewChildNode == NULL)
	pcParentNode->vAddNewChild(pcNewChildNode);
	pcNewChildNode->vRemoveChild();
	return true;
}//bool CTreeStatic::bMoveSubtree(CNodeStatic* pcParentNode, CNodeStatic* pcNewChildNode)

template <typename T>
CTreeStatic<T>::~CTreeStatic()
{

}//CTreeStatic::~CTreeStatic()