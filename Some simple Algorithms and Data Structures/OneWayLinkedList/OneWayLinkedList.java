import java.util.Iterator;
import java.util.NoSuchElementException;

public class OneWayLinkedList<T> implements IList<T> {
    Element head = null;

    private Element getElement(int index){
        if(index < 0) throw new NoSuchElementException();
        Element actElem = head;
        while(index > 0 && actElem != null)
        {
            index--;
            actElem = actElem.getNext();
        }
        if (actElem == null) throw new NoSuchElementException();
        return actElem;
    }

    @Override
    public void add(T value) {
        Element newElem = new Element(value);
        if(head == null)
        {
            head = newElem;
            head.setNext(null);
        }
        Element tail = head;
        while (tail.getNext() != null) {
            tail = tail.getNext();
        }
        tail.setNext(newElem);
        tail = tail.getNext();
        tail.setNext(null);
    }

    @Override
    public void addAt(int index, T value) throws NoSuchElementException {
        if (index<0) throw new NoSuchElementException();
        Element newElem = new Element(value);
        if(index == 0)
        {
            newElem.setNext(head);
            head = newElem;
        }
        else
            {
                Element actElem = getElement(index-1);
                newElem.setNext(actElem.getNext());
                actElem.setNext(newElem);
            }
    }

    @Override
    public void clear()
    {
        head = null;
    }

    @Override
    public boolean contains(T value) {
        return indexOf(value) >= 0;
    }

    @Override
    public T get(int index) throws NoSuchElementException {
        Element actElem = getElement(index);
        return actElem.getValue();
    }

    @Override
    public void set(int index, T value) throws NoSuchElementException {
        Element actElem = getElement(index);
        actElem.setValue(value);
    }

    @Override
    public int indexOf(T value) {
        int pos=0;
        Element actElem = head;
        while(actElem != null)
        {
            if(actElem.getValue().equals(value))
            {
                return pos;
            }
            pos++;
            actElem = actElem.getNext();
        }
        return -1;
    }

    @Override
    public boolean isEmpty()
    {
        return head == null;
    }

    @Override
    public T removeAt(int index) throws NoSuchElementException {
        if (index < 0 || head == null) throw new NoSuchElementException();
        if(index == 0)
        {
            T retValue = head.getValue();
            head = head.getNext();
            return retValue;
        }
        Element actElem = getElement(index-1);
        if(actElem.getNext() == null) throw new NoSuchElementException();
        T retValue = actElem.getNext().getValue();
        actElem.setNext(actElem.getNext().getNext());
        return retValue;
    }

    @Override
    public boolean remove(T value) {
        if(head == null)
        {
            return false;
        }
        if(head.getValue().equals(value))
        {
            head = head.getNext();
            return true;
        }
        Element actElem = head;
        while(actElem.getNext() != null && !actElem.getNext().getValue().equals(value))
        {
            actElem = actElem.getNext();
        }
        if(actElem.getNext()==null)
        {
            return false;
        }
        actElem.setNext(actElem.getNext().getNext());
        return true;
    }

    @Override
    public int size() {
        int pos = 0;
        Element actElem = head;
        while(actElem != null)
        {
            pos++;
            actElem = actElem.getNext();
        }
        return pos;
    }

    @Override
    public void print() {
        Element actElem = head;
        while(actElem != null)
        {
            System.out.print(actElem.getValue() + " ");
            actElem = actElem.getNext();
        }
    }

    @Override
    public Iterator<T> iterator() {
        return new OneWayLinkedListIterator();
    }

    private class Element{
        private T value;
        private Element next;

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

        Element(T data)
        {
            this.value = data;
        }
    }

    private class OneWayLinkedListIterator implements Iterator<T> {
        Element actElem;
        public OneWayLinkedListIterator()
        {
            actElem = head;
        }

        @Override
        public boolean hasNext()
        {
            return actElem != null;
        }
        @Override
        public T next()
        {
            if (hasNext())
            {
                T value = actElem.getValue();
                actElem = actElem.getNext();
                return value;
            }
            else
                {
                    throw new NoSuchElementException();
                }
        }
    }
}
