public class IterativeMergeSorter implements ISorter {
    private final IChecker checker;

    public IterativeMergeSorter(IChecker checker) {
        this.checker = checker;
    }

    static void merge(int [] val, int left, int mid, int right)
    {
        int k = left;
        int l = mid + 1;
        int [] result = new int[val.length];
        int index = left;
        while (k <= mid && l <= right)
        {
            if (val[k] < val[l])
            {
                result[index++] = val[k++];
            }
            else
            {
                result[index++] = val[l++];
            }
        }

        while (k <= mid && k < result.length) {
            result[index++] = val[k++];
        }

        while (l <= right && l < result.length)
        {
            result[index++] = val[l++];
        }

        for (k = left; k <= right; k++) {
            val[k] = result[k];
        }
    }

    @Override
    public void sort(int[] values) {
        for (int subArr = 1; subArr <= values.length - 1; subArr = 2 * subArr)
        {
            for (int i = 0; i < values.length - 1; i += 2 * subArr)
            {
                int end = Math.min(i + 2 * subArr - 1, values.length - 1);
                merge(values, i, i + subArr - 1, end);
            }
            checker.check(values);
        }
    }
}