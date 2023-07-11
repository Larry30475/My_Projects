import static org.junit.jupiter.api.Assertions.assertEquals;

public class MergerTests {
    @org.junit.jupiter.api.Test
    void mergeList1Then2() {
        var list1 = new OneWayLinkedList<Integer>();
        list1.add(1);
        list1.add(2);
        list1.add(3);

        var list2 = new OneWayLinkedList<Integer>();
        list2.add(4);
        list2.add(5);
        list2.add(6);

        var mergedList = Merger.merge(list1, list2);
        assertEquals(1, mergedList.get(0));
        assertEquals(2, mergedList.get(1));
        assertEquals(3, mergedList.get(2));
        assertEquals(4, mergedList.get(3));
        assertEquals(5, mergedList.get(4));
        assertEquals(6, mergedList.get(5));
    }

    @org.junit.jupiter.api.Test
    void mergeOneByOne() {
        var list1 = new OneWayLinkedList<Integer>();
        list1.add(1);
        list1.add(3);
        list1.add(5);

        var list2 = new OneWayLinkedList<Integer>();
        list2.add(2);
        list2.add(4);
        list2.add(6);

        var mergedList = Merger.merge(list1, list2);
        assertEquals(1, mergedList.get(0));
        assertEquals(2, mergedList.get(1));
        assertEquals(3, mergedList.get(2));
        assertEquals(4, mergedList.get(3));
        assertEquals(5, mergedList.get(4));
        assertEquals(6, mergedList.get(5));
    }

    @org.junit.jupiter.api.Test
    void mergeMyTest() {
        var list1 = new OneWayLinkedList<Integer>();
        list1.add(7);
        list1.add(3);
        list1.add(6);

        var list2 = new OneWayLinkedList<Integer>();
        list2.add(2);
        list2.add(8);
        list2.add(7);

        var mergedList = Merger.merge(list1, list2);
        assertEquals(2, mergedList.get(0));
        assertEquals(3, mergedList.get(1));
        assertEquals(6, mergedList.get(2));
        assertEquals(7, mergedList.get(3));
        assertEquals(7, mergedList.get(4));
        assertEquals(8, mergedList.get(5));
    }

    @org.junit.jupiter.api.Test
    void duplMerge() {
        var list1 = new OneWayLinkedList<Integer>();
        list1.add(1);
        list1.add(2);
        list1.add(3);
        list1.add(4);

        var list2 = new OneWayLinkedList<Integer>();
        list2.add(2);
        list2.add(3);
        list2.add(4);
        list2.add(5);

        var mergedList = Merger.findDupl(list1, list2);
        assertEquals(2, mergedList.get(0));
        assertEquals(3, mergedList.get(1));
        assertEquals(4, mergedList.get(2));
    }

    @org.junit.jupiter.api.Test
    void duplMerge2() {
        var list1 = new OneWayLinkedList<Integer>();
        list1.add(3);
        list1.add(5);
        list1.add(4);
        list1.add(2);

        var list2 = new OneWayLinkedList<Integer>();
        list2.add(2);
        list2.add(3);
        list2.add(4);
        list2.add(5);
        list2.add(1);

        var mergedList = Merger.findDupl(list1, list2);
        assertEquals(2, mergedList.get(0));
        assertEquals(3, mergedList.get(1));
        assertEquals(4, mergedList.get(2));
        assertEquals(5, mergedList.get(3));
    }
}
