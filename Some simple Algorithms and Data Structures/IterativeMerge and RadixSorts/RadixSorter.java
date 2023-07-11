public class RadixSorter implements ISorter {
    private final IChecker checker;

    public RadixSorter(IChecker checker) {
        this.checker = checker;
    }

    void countSort(int [] val, int place) {
        int[] result = new int[val.length + 1];
        int max = getMax(val);
        int[] count = new int[max + 1];

        /*
        for(int i = 0; i < count.length; i++)
        {
            count[i] = 0;
        }
        */

        for (int i = 0; i < val.length; i++) {
            count[(val[i] / place) % 10]++;
        }

        for (int i = 1; i <= 10; i++) {
            count[i] += count[i - 1];
        }

        for (int i = val.length - 1; i >= 0; i--) {
            result[count[(val[i] / place) % 10] - 1] = val[i];
            count[(val[i] / place) % 10]--;
        }

        for (int i = 0; i < val.length; i++) {
            val[i] = result[i];
        }
    }

    int getMax(int [] val) {
        int max = val[0];
        for (int i = 1; i < val.length; i++)
            if (val[i] > max)
                max = val[i];
        return max;
    }

    @Override
    public void sort(int[] values) {
        int max = getMax(values);

        for (int place = 1; max / place > 0; place *= 10) {
            countSort(values, place);
            checker.check(values);
        }
    }
}