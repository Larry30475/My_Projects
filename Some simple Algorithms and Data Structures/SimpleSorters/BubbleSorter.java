public class BubbleSorter implements ISorter {
    private final IChecker checker;

    public BubbleSorter(IChecker checker) {
        this.checker = checker;
    }

    @Override
    public void sort(int[] values) {
        int temp;
        for (int j = 0; j < values.length; j++)
        {
            for (int i = 0; i < values.length - 1; i++)
            {
                if (values[i] > values[i + 1])
                {
                    temp = values[i];
                    values[i] = values[i + 1];
                    values[i + 1] = temp;
                }
            }
            checker.check(values);
        }
    }
}
