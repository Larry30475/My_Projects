public class SelectionSorter implements ISorter {
    private final IChecker checker;

    public SelectionSorter(IChecker checker) {
        this.checker = checker;
    }

    @Override
    public void sort(int[] values) {
        int temp;
        int sortIndex = 0;
        for (int i = values.length - 1; i >= 0; i--) {
            int index = values.length - 1;
            int minVal = values[values.length - 1];
            sortIndex = values.length - 1 - i;
            for (int j = values.length - 1; j >= sortIndex; j--)
            {
                if (values[j] < minVal)
                {
                    minVal = values[j];
                    index = j;
                }
            }
            temp = values[sortIndex];
            values[sortIndex] = minVal;
            values[index] = temp;
            checker.check(values);
        }
    }
}
