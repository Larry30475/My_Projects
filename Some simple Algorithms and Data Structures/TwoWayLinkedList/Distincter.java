public class Distincter {
    public static TwoWayLinkedList<Integer> distinct(TwoWayLinkedList<Integer> list)
    {
        TwoWayLinkedList<Integer> result = new TwoWayLinkedList<>();
        for(int i = 0; i < list.size(); i++)
        {
            for(int j = 0; j < list.size(); j++)
            {
                if(list.get(i) == list.get(j))
                {
                    i = j;
                }
            }
            result.add(list.get(i));
        }
        return result;
    }
}