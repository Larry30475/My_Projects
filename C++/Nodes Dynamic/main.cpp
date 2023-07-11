#include "CTreeStatic.h"
#include "CTreeDynamic.h"

int main()
{
	CTreeStatic staticTree;
	staticTree.pcGetRoot()->vSetValue(1);

	staticTree.pcGetRoot()->vAddNewChild();
	staticTree.pcGetRoot()->vAddNewChild();

	staticTree.pcGetRoot()->pcGetChild(0)->vSetValue(2);
	staticTree.pcGetRoot()->pcGetChild(1)->vSetValue(3);

	staticTree.pcGetRoot()->pcGetChild(0)->vAddNewChild();
	staticTree.pcGetRoot()->pcGetChild(0)->vAddNewChild();

	staticTree.pcGetRoot()->pcGetChild(0)->pcGetChild(0)->vSetValue(4);
	staticTree.pcGetRoot()->pcGetChild(0)->pcGetChild(1)->vSetValue(5);

	staticTree.pcGetRoot()->pcGetChild(1)->vAddNewChild();
	staticTree.pcGetRoot()->pcGetChild(1)->vAddNewChild();

	staticTree.pcGetRoot()->pcGetChild(1)->pcGetChild(0)->vSetValue(6);
	staticTree.pcGetRoot()->pcGetChild(1)->pcGetChild(1)->vSetValue(7);


	CTreeStatic staticTree2;
	staticTree2.pcGetRoot()->vSetValue(11);

	staticTree2.pcGetRoot()->vAddNewChild();
	staticTree2.pcGetRoot()->vAddNewChild();

	staticTree2.pcGetRoot()->pcGetChild(0)->vSetValue(12);
	staticTree2.pcGetRoot()->pcGetChild(1)->vSetValue(13);

	staticTree2.pcGetRoot()->pcGetChild(0)->vAddNewChild();
	staticTree2.pcGetRoot()->pcGetChild(0)->vAddNewChild();

	staticTree2.pcGetRoot()->pcGetChild(0)->pcGetChild(0)->vSetValue(14);
	staticTree2.pcGetRoot()->pcGetChild(0)->pcGetChild(1)->vSetValue(15);

	staticTree2.pcGetRoot()->pcGetChild(1)->vAddNewChild();
	staticTree2.pcGetRoot()->pcGetChild(1)->vAddNewChild();

	staticTree2.pcGetRoot()->pcGetChild(1)->pcGetChild(0)->vSetValue(16);
	staticTree2.pcGetRoot()->pcGetChild(1)->pcGetChild(1)->vSetValue(17);

	staticTree2.pcGetRoot()->pcGetChild(0)->pcGetChild(0)->vAddNewChild();
	staticTree2.pcGetRoot()->pcGetChild(0)->pcGetChild(0)->pcGetChild(0)->vSetValue(100);


	staticTree.vPrintTree();
	std::cout << std::endl;
	staticTree2.vPrintTree();
	std::cout << std::endl;

	staticTree.bMoveSubtree(staticTree.pcGetRoot()->pcGetChild(0)->pcGetChild(0), staticTree2.pcGetRoot()->pcGetChild(0));
	std::cout << "Po bMove" << std::endl;
	staticTree.vPrintTree();
	std::cout << std::endl;
	staticTree2.vPrintTree();
	std::cout << std::endl;




	CTreeDynamic dynamicTree;
	dynamicTree.pcGetRoot()->vSetValue(1);

	dynamicTree.pcGetRoot()->vAddNewChild();
	dynamicTree.pcGetRoot()->vAddNewChild();

	dynamicTree.pcGetRoot()->pcGetChild(0)->vSetValue(2);
	dynamicTree.pcGetRoot()->pcGetChild(1)->vSetValue(3);

	dynamicTree.pcGetRoot()->pcGetChild(0)->vAddNewChild();
	dynamicTree.pcGetRoot()->pcGetChild(0)->vAddNewChild();

	dynamicTree.pcGetRoot()->pcGetChild(0)->pcGetChild(0)->vSetValue(4);
	dynamicTree.pcGetRoot()->pcGetChild(0)->pcGetChild(1)->vSetValue(5);

	dynamicTree.pcGetRoot()->pcGetChild(1)->vAddNewChild();
	dynamicTree.pcGetRoot()->pcGetChild(1)->vAddNewChild();

	dynamicTree.pcGetRoot()->pcGetChild(1)->pcGetChild(0)->vSetValue(6);
	dynamicTree.pcGetRoot()->pcGetChild(1)->pcGetChild(1)->vSetValue(7);



	CTreeDynamic dynamicTree2;
	dynamicTree2.pcGetRoot()->vSetValue(11);

	dynamicTree2.pcGetRoot()->vAddNewChild();
	dynamicTree2.pcGetRoot()->vAddNewChild();

	dynamicTree2.pcGetRoot()->pcGetChild(0)->vSetValue(12);
	dynamicTree2.pcGetRoot()->pcGetChild(1)->vSetValue(13);

	dynamicTree2.pcGetRoot()->pcGetChild(0)->vAddNewChild();
	dynamicTree2.pcGetRoot()->pcGetChild(0)->vAddNewChild();

	dynamicTree2.pcGetRoot()->pcGetChild(0)->pcGetChild(0)->vSetValue(14);
	dynamicTree2.pcGetRoot()->pcGetChild(0)->pcGetChild(1)->vSetValue(15);

	dynamicTree2.pcGetRoot()->pcGetChild(1)->vAddNewChild();
	dynamicTree2.pcGetRoot()->pcGetChild(1)->vAddNewChild();

	dynamicTree2.pcGetRoot()->pcGetChild(1)->pcGetChild(0)->vSetValue(16);
	dynamicTree2.pcGetRoot()->pcGetChild(1)->pcGetChild(1)->vSetValue(17);

	dynamicTree2.pcGetRoot()->pcGetChild(0)->pcGetChild(0)->vAddNewChild();
	dynamicTree2.pcGetRoot()->pcGetChild(0)->pcGetChild(0)->pcGetChild(0)->vSetValue(100);


	dynamicTree.vPrintTree();
	std::cout << std::endl;
	dynamicTree2.vPrintTree();
	std::cout << std::endl;

	dynamicTree.bMoveSubtree(dynamicTree.pcGetRoot()->pcGetChild(0)->pcGetChild(0), dynamicTree2.pcGetRoot()->pcGetChild(0));
	std::cout << "Po bMove" << std::endl;
	dynamicTree.vPrintTree();
	std::cout << std::endl;
	dynamicTree2.vPrintTree();
	std::cout << std::endl;

	return 0;
}