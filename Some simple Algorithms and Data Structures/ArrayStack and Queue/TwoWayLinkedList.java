import java.util.Iterator;
import java.util.NoSuchElementException;

public class TwoWayLinkedList<T> implements IList<T> {
    Element sentinel;
    int capacity;

    public TwoWayLinkedList(int capacity)
    {
        sentinel = new Element(null);
        sentinel.setNext(sentinel);
        sentinel.setPrev(sentinel);
        this.capacity = capacity;
    }

    public TwoWayLinkedList()
    {
        sentinel = new Element(null);
        sentinel.setNext(sentinel);
        sentinel.setPrev(sentinel);
    }

    private Element getElement(int index)
    {
        //if (index < 0) throw new NoSuchElementException();
        Element elem = sentinel.getNext();
        int counter = 0;
        while(elem != sentinel && counter < index)
        {
            counter++;
            elem = elem.getNext();
        }
        //if(elem == sentinel) throw new NoSuchElementException();
        return elem;
    }

    private Element getElement(T value)
    {
        Element elem = sentinel.getNext();
        while(elem != sentinel && !value.equals(elem.getValue()))
        {
            elem = elem.getNext();
        }
        if(elem == sentinel)
        {
            return null;
        }
        return elem;
    }

    @Override
    public void add(T value)
    {
        if (!isFull())
        {
            Element newElem = new Element(value);
            sentinel.insertBefore(newElem);
        }
    }

    @Override
    public void addAt(int index, T value) //throws NoSuchElementException
    {
        if(!isFull()) {
            Element newElem = new Element(value);
            if (index == 0) {
                sentinel.insertAfter(newElem);
            } else {
                Element elem = getElement(index - 1);
                elem.insertAfter(newElem);
            }
        }
    }

    @Override
    public void clear()
    {
        sentinel.setNext(sentinel);
        sentinel.setPrev(sentinel);
    }

    @Override
    public boolean contains(T value)
    {
        return indexOf(value) != -1;
    }

    @Override
    public T get(int index) //throws NoSuchElementException
    {
        Element elem = getElement(index);
        return elem.getValue();
    }

    @Override
    public void set(int index, T value) //throws NoSuchElementException
    {
        Element elem = getElement(index);
        elem.setValue(value);
    }

    @Override
    public int indexOf(T value)
    {
        Element elem = sentinel.getNext();
        int counter = 0;
        while (elem != sentinel && !elem.getValue().equals(value))
        {
            counter++;
            elem = elem.getNext();
        }
        if(elem == sentinel)
        {
            return -1;
        }
        return counter;
    }

    public boolean isFull()
    {
        return size() == capacity;
    }

    @Override
    public boolean isEmpty()
    {
        return sentinel.getNext() == sentinel;
    }

    @Override
    public T removeAt(int index) //throws NoSuchElementException
    {
        Element elem = getElement(index);
        elem.remove();
        return elem.getValue();
    }

    @Override
    public boolean remove(T value)
    {
        Element elem = getElement(value);
        if(elem == null)
        {
            return false;
        }
        elem.remove();
        return true;
    }

    @Override
    public int size()
    {
        Element elem = sentinel.getNext();
        int counter = 0;
        while(elem != sentinel && counter < capacity)
        {
            counter++;
            elem = elem.getNext();
        }
        return counter;
    }

    @Override
    public void print() {
        Element elem = sentinel.getNext();
        while(elem != sentinel)
        {
            System.out.print(elem.getValue() + " ");
            elem = elem.getNext();
        }
    }

    @Override
    public Iterator<T> iterator()
    {
        return new TwoWayLinkedListIterator();
    }

    private class Element
    {
        private T value;
        private Element next;
        private Element prev;

        public T getValue()
        {
            return value;
        }
        public void setValue(T value)
        {
            this.value = value;
        }
        public Element getNext()
        {
            return next;
        }
        public void setNext(Element next)
        {
            this.next = next;
        }
        public Element getPrev()
        {
            return prev;
        }
        public void setPrev(Element prev)
        {
            this.prev = prev;
        }
        Element(T data)
        {
            this.value = data;
        }

        public void insertAfter(Element elem)
        {
            elem.setNext(this.getNext());
            elem.setPrev(this);
            this.getNext().setPrev(elem);
            this.setNext(elem);
        }

        public void insertBefore(Element elem)
        {
            elem.setNext(this);
            elem.setPrev(this.getPrev());
            this.getPrev().setNext(elem);
            this.setPrev(elem);
        }

        public void remove()
        {
            this.getNext().setPrev(this.getPrev());
            this.getPrev().setNext(this.getNext());
        }
    }

    private class TwoWayLinkedListIterator implements Iterator<T>
    {
        Element current = sentinel;

        @Override
        public boolean hasNext()
        {
            return current.getNext() != sentinel;
        }

        @Override
        public T next()
        {
            /*if (hasNext())
            {*/
                current = current.getNext();
                return current.getValue();
            /*}
            else
                {
                    throw new NoSuchElementException();
                }*/
        }
    }
}
