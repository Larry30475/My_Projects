public class Merger {
    public static OneWayLinkedList<Integer> findDupl(OneWayLinkedList<Integer> list1, OneWayLinkedList<Integer> list2)
    {
        OneWayLinkedList<Integer> result = new OneWayLinkedList<>();
        int index1 = 0;
        int index2 = 0;
        OneWayLinkedList<Integer> sortedList1 = sortList(list1);
        OneWayLinkedList<Integer> sortedList2 = sortList(list2);
        if (sortedList1.size() <= sortedList2.size())
        {
            while (index1 < sortedList1.size())
            {
                if (sortedList1.get(index1) == sortedList2.get(index2))
                {
                    result.add(sortedList1.get(index1));
                    index2++;
                    index1++;
                }
                else
                    {
                        if (sortedList1.get(index1) < sortedList2.get(index2))
                        {
                            index1++;
                        }
                        else
                            {
                                index2++;
                            }
                    }
            }
        }
        else
            {
                while (index2 < sortedList2.size())
                {
                    if (sortedList1.get(index1) == sortedList2.get(index2))
                    {
                        result.add(sortedList1.get(index1));
                        index2++;
                        index1++;
                    }
                    else
                        {
                            if (sortedList1.get(index1) > sortedList2.get(index2))
                            {
                                index2++;
                            }
                            else
                            {
                                index1++;
                            }
                        }
                }
            }
        return result;
    }

    public static OneWayLinkedList<Integer> sortList(OneWayLinkedList<Integer> list)
    {
        OneWayLinkedList<Integer> result = new OneWayLinkedList<>();
        int size = list.size();
        for(int i = 0; i < size; i++)
        {
            int min = list.get(0);
            for (int j = 0; j < list.size(); j++)
            {
                if (min > list.get(j))
                {
                    min = list.get(j);
                }
            }
            result.add(min);
            list.remove(min);
        }
        return result;
    }
    public static OneWayLinkedList<Integer> merge(
            OneWayLinkedList<Integer> list1,
            OneWayLinkedList<Integer> list2)
    {
        OneWayLinkedList<Integer> result = new OneWayLinkedList<>();
        int index1 = 0;
        int index2 = 0;
        OneWayLinkedList<Integer> sortedList1 = sortList(list1);
        OneWayLinkedList<Integer> sortedList2 = sortList(list2);
        while(index1 < sortedList1.size() || index2 < sortedList2.size())
        {
            if (sortedList1.size() == index1 && sortedList2.size() > index2)
            {
                while(index2 < sortedList2.size())
                {
                    result.add(sortedList2.get(index2));
                    index2++;
                }
            }
            else if (sortedList1.size() > index1 && sortedList2.size() == index2)
            {
                while(index1 < sortedList1.size())
                {
                    result.add(sortedList1.get(index1));
                    index1++;
                }
            }
            else
                {
                    if (sortedList1.get(index1) < sortedList2.get(index2)) {
                        result.add(sortedList1.get(index1));
                        index1++;
                    } else if (sortedList1.get(index1) > sortedList2.get(index2)) {
                        result.add(sortedList2.get(index2));
                        index2++;
                    } else {
                        result.add(sortedList1.get(index1));
                        result.add(sortedList2.get(index2));
                        index1++;
                        index2++;
                    }
                }
        }
        return result;
    }
}
