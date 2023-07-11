import java.util.ArrayList;

public class Queue<T> {
    ArrayList<T> array = new ArrayList<>();

    public boolean isEmpty()
    {
        return array.isEmpty();
    }

    public void enqueue(T value){
        array.add(value);
    }

    public T dequeue(){
        if(array.size() == 0) return null;
        return array.remove(0);
    }
}
