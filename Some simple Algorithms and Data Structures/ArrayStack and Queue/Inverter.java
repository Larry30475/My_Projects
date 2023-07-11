import exceptions.EmptyQueueException;
import exceptions.FullQueueException;
import exceptions.FullStackException;

import java.util.Stack;

public class Inverter {
    public static <T> IQueue<T> invert(IQueue<T> queue) throws EmptyQueueException, FullQueueException, FullStackException {
        if (queue.isEmpty())
        {
            return queue;
        }
        else
            {
                TwoWayLinkedListQueue<T> result = new TwoWayLinkedListQueue<>(queue.size());
                ArrayStack<T> stos = new ArrayStack<>(queue.size());
                while(!queue.isEmpty())
                {
                    stos.push(queue.first());
                    queue.dequeue();
                }
                while(!stos.isEmpty())
                {
                    result.enqueue(stos.top());
                    stos.pop();
                }
                return result;
            }
    }
}

