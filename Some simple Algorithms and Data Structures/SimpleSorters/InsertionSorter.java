public class InsertionSorter implements ISorter {
    private final IChecker checker;

    public InsertionSorter(IChecker checker) {
        this.checker = checker;
    }

    @Override
    public void sort(int[] values) {
        for (int j = 1; j < values.length; ++j)
        {
            int temp = values[j];
            int i;
            for (i = j; i > 0 && temp < values[i - 1]; i--)
            {
                values[i] = values[i - 1];
            }
            values[i] = temp;
            checker.check(values);
        }
    }
}
