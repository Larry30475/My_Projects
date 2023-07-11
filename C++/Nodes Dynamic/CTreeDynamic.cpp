#include "CTreeDynamic.h"

CTreeDynamic::CTreeDynamic()
{
	pc_root = new CNodeDynamic();
}//CTreeDynamic::CTreeDynamic()

CNodeDynamic* CTreeDynamic::pcGetRoot()
{
	return pc_root;
}//CNodeDynamic* CTreeDynamic::pcGetRoot()

void CTreeDynamic::vPrintTree()
{
	pc_root->vPrintAllBelow();
}//void CTreeDynamic::vPrintTree()

bool CTreeDynamic::bMoveSubtree(CNodeDynamic* pcParentNode, CNodeDynamic* pcNewChildNode) 
{
	if (pcParentNode == NULL || pcNewChildNode == NULL) 
	{
		return false;
	}//if (pcParentNode == NULL || pcNewChildNode == NULL)
	pcParentNode->vAddNewChild(pcNewChildNode);
	pcNewChildNode->vRemoveChild();
	return true;
}//bool CTreeDynamic::bMoveSubtree(CNodeDynamic* pcParentNode, CNodeDynamic* pcNewChildNode)

CTreeDynamic::~CTreeDynamic()
{
	delete pc_root;
}//CTreeDynamic::~CTreeDynamic()
