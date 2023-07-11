#pragma once
#include "CNodeDynamic.h"

class CTreeDynamic
{
public:
	CTreeDynamic();
	~CTreeDynamic();
	CNodeDynamic* pcGetRoot();
	void vPrintTree();
	bool bMoveSubtree(CNodeDynamic* parentNode, CNodeDynamic* newChildNode);
private:
	CNodeDynamic* pc_root;
};//class CTreeDynamic