#include "CTreeStatic.h"

CTreeStatic::CTreeStatic()
{
	c_root = CNodeStatic();
}//CTreeStatic::CTreeStatic()

CNodeStatic* CTreeStatic::pcGetRoot() 
{
	return &c_root;
}//CNodeStatic* CTreeStatic::pcGetRoot()

void CTreeStatic::vPrintTree()
{
	c_root.vPrintAllBelow();
}//void CTreeStatic::vPrintTree()

bool CTreeStatic::bMoveSubtree(CNodeStatic* pcParentNode, CNodeStatic* pcNewChildNode)
{
	if (pcParentNode == NULL || pcNewChildNode == NULL) 
	{
		return false;
	}//if (pcParentNode == NULL || pcNewChildNode == NULL)
	pcParentNode->vAddNewChild(pcNewChildNode);
	pcNewChildNode->vRemoveChild();
	return true;
}//bool CTreeStatic::bMoveSubtree(CNodeStatic* pcParentNode, CNodeStatic* pcNewChildNode)

CTreeStatic::~CTreeStatic()
{

}//CTreeStatic::~CTreeStatic()