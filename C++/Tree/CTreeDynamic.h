#pragma once
#include "CNodeDynamic.h"

template <typename T> class CTreeDynamic
{
public:
	CTreeDynamic();
	~CTreeDynamic();
	CNodeDynamic<T>* pcGetRoot();
	void vPrintTree();
	bool bMoveSubtree(CNodeDynamic<T>* parentNode, CNodeDynamic<T>* newChildNode);
private:
	CNodeDynamic<T>* pc_root;
};

template <typename T>
CTreeDynamic<T>::CTreeDynamic()
{
	pc_root = new CNodeDynamic<T>();
}//CTreeDynamic::CTreeDynamic()

template <typename T>
CNodeDynamic<T>* CTreeDynamic<T>::pcGetRoot()
{
	return pc_root;
}//CNodeDynamic* CTreeDynamic::pcGetRoot()

template <typename T>
void CTreeDynamic<T>::vPrintTree()
{
	pc_root->vPrintAllBelow();
}//void CTreeDynamic::vPrintTree()

template <typename T>
bool CTreeDynamic<T>::bMoveSubtree(CNodeDynamic<T>* pcParentNode, CNodeDynamic<T>* pcNewChildNode)
{
	if (pcParentNode == NULL || pcNewChildNode == NULL)
	{
		return false;
	}//if (pcParentNode == NULL || pcNewChildNode == NULL)
	pcParentNode->vAddNewChild(pcNewChildNode);
	pcNewChildNode->vRemoveChild();
	return true;
}//bool CTreeDynamic::bMoveSubtree(CNodeDynamic* pcParentNode, CNodeDynamic* pcNewChildNode)

template <typename T>
CTreeDynamic<T>::~CTreeDynamic()
{
	delete pc_root;
}//CTreeDynamic::~CTreeDynamic()
