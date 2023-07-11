import java.util.NoSuchElementException;

public class Inserter {
    public static TwoWayLinkedList<String> insert(TwoWayLinkedList<String> list1, TwoWayLinkedList<String> list2, int beforeIndex) throws NoSuchElementException
    {
        for (int i = list2.size() - 1; i > -1; i--)
        {
            list1.addAt(beforeIndex, list2.get(i));
        }
        return list1;
    }

    public static TwoWayLinkedList<String> insert(TwoWayLinkedList<String> list1, TwoWayLinkedList<String> list2, String beforeElement) throws NoSuchElementException
    {
        int beforeIndex = -1;
        for (int i = 0; i < list1.size(); i++)
        {
            if (list1.get(i).equals(beforeElement))
            {
                beforeIndex = i;
            }
        }
        for (int i = list2.size() - 1; i > -1; i--)
        {
            list1.addAt(beforeIndex, list2.get(i));
        }
        return list1;
    }
}

