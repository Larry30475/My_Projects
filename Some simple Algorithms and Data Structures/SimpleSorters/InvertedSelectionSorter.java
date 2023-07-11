public class InvertedSelectionSorter implements ISorter {
    private final IChecker checker;

    public InvertedSelectionSorter(IChecker checker) {
        this.checker = checker;
    }

    @Override
    public void sort(int[] values) {
        int temp;
        for (int i = 0; i < values.length; i++) {
            int index = 0;
            int maxVal = values[0];
            int sortIndex = values.length - 1 - i;
            for (int j = 0; j <= sortIndex; j++)
            {
                if (values[j] > maxVal)
                {
                    maxVal = values[j];
                    index = j;
                }
            }
            temp = values[sortIndex];
            values[sortIndex] = maxVal;
            values[index] = temp;
            checker.check(values);
        }
    }
}
