import exceptions.*;
import java.util.LinkedList;
import java.util.Queue;

public class TwoWayLinkedListQueue<T> implements IQueue<T> {
    TwoWayLinkedList<T> list;

    public TwoWayLinkedListQueue(int capacity)
    {
        list = new TwoWayLinkedList<T>(capacity);
    }

    @Override
    public boolean isEmpty()
    {
        return list.isEmpty();
    }

    @Override
    public boolean isFull()
    {
        return size() == list.capacity;
    }

    @Override
    public void enqueue(T value) throws FullQueueException {
        if (isFull()) throw new FullQueueException();
        list.add(value);
    }

    @Override
    public T first() throws EmptyQueueException {
        T value = list.get(0);
        if(value == null) throw new EmptyQueueException();
        return value;
    }

    @Override
    public T dequeue() throws EmptyQueueException {
        T value = list.removeAt(0);
        if(value == null) throw new EmptyQueueException();
        return value;
    }

    @Override
    public int size() {
        return list.size();
    }
}