import org.junit.jupiter.params.shadow.com.univocity.parsers.annotations.Convert;

import java.util.ArrayList;
import java.util.List;

public class BinarySearchTreeSorter {
    public static <T extends Comparable<T>> void sort(List<T> list) throws DuplicateElementException {
        var tree = new BinarySearchTree<T>();
        int size = list.size();

        for (int i = 0; i < list.size(); i++)
        {
            tree.add(list.get(i));
        }

        list.clear();

        ArrayList<T> result = tree.SortInOrder();

        for (int j = 0; j < size; j++)
        {
            list.add(result.get(j));
        }
    }
}
