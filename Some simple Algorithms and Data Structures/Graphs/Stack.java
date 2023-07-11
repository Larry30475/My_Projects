import java.util.ArrayList;
import java.util.EmptyStackException;

public class Stack <T>{
    ArrayList<T> array = new ArrayList<>();
    int topIndex = 0;

    public boolean isEmpty() {
        return topIndex == 0;
    }

    public T top() throws EmptyStackException {
        if(array.isEmpty()) throw new EmptyStackException();
        return array.get(topIndex - 1);
    }

    public T pop() throws EmptyStackException
    {
        if(array.isEmpty()) throw new EmptyStackException();
        T temp = array.get(--topIndex);
        array.remove(temp);
        return temp;
    }

    public void push(T value)
    {
        array.add(topIndex++, value);
    }
}
