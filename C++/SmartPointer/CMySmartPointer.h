#pragma once
#include "CRefCounter.h"

template <class T>
class CMySmartPointer
{
public:
	CMySmartPointer(T* pcPointer);
	CMySmartPointer(const CMySmartPointer& pcOther);
	~CMySmartPointer();
	T& operator*();
	T* operator->();
	CMySmartPointer<T>& operator=(const CMySmartPointer<T>& other);;
private:
	CRefCounter* pc_counter;
	T* pc_pointer;
};//class CMySmartPointer

template <class T>
CMySmartPointer<T>::CMySmartPointer(T* pcPointer)
{
	pc_pointer = pcPointer;
	pc_counter = new CRefCounter();
	pc_counter->iAdd();
}//CMySmartPointer<T>::CMySmartPointer(T* pcPointer)

template <class T>
CMySmartPointer<T>::CMySmartPointer(const CMySmartPointer& pcOther)
{
	pc_pointer = pcOther.pc_pointer;
	pc_counter = pcOther.pc_counter;
	pc_counter->iAdd();
}//CMySmartPointer<T>::CMySmartPointer(const CMySmartPointer& pcOther)

template<class T>
CMySmartPointer<T>::~CMySmartPointer()
{
	if (pc_counter->iDec() == 0)
	{
		delete pc_pointer;
		delete pc_counter;
	}//if (pc_counter->iDec())
}//CMySmartPointer<T>::~CMySmartPointer()

template <class T>
T& CMySmartPointer<T>::operator*()
{
	return(*pc_pointer);
}//T& CMySmartPointer<T>::operator*()

template <class T>
T* CMySmartPointer<T>::operator->()
{
	return(pc_pointer);
}//T* CMySmartPointer<T>::operator->()

template <class T>
CMySmartPointer<T>& CMySmartPointer<T>::operator=(const CMySmartPointer<T>& other)
{
	if (this != &other) {
		if (pc_counter->iDec() == 0) {
			delete pc_pointer;
			delete pc_counter;
		}

		pc_pointer = other.pc_pointer;
		pc_counter = other.pc_counter;
		pc_counter->iAdd();
	}

	return *this;
}