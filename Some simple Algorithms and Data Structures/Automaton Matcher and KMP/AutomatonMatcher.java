import java.util.*;

public class AutomatonMatcher implements IStringMatcher
{
    static int getState(char[] pattern, int patSize, int state, int sign)
    {
        if(state < patSize && sign == pattern[state])
            return state + 1;

        int i;
        for (int nextState = state; nextState > 0; nextState--)
        {
            if (pattern[nextState - 1] == sign)
            {
                for (i = 0; i < nextState - 1; i++) {
                    if (pattern[i] != pattern[state - nextState + 1 + i]) {
                        break;
                    }
                }
                if (i == nextState - 1) {
                    return nextState;
                }
            }
        }

        return 0;
    }

    static void computeTF(char[] pattern, int patSize, int[][] TofS)
    {
        for (int state = 0; state <= patSize; ++state) {
            for (int sign = 0; sign < 256; ++sign) {
                TofS[state][sign] = getState(pattern, patSize, state, sign);
            }
        }
    }

    @Override
    public List<Integer> validShifts(String textToSearch, String patternToFind)
    {
        char [] text = textToSearch.toCharArray();
        char [] pattern = patternToFind.toCharArray();
        List<Integer> result = new ArrayList<>();

        int[][] TofS = new int[pattern.length + 1][256];

        computeTF(pattern, pattern.length, TofS);

        int state = 0;
        for (int i = 0; i < text.length; i++)
        {
            state = TofS[state][text[i]];
            if (state == pattern.length)
            {
                result.add(i - pattern.length + 1);
            }
        }

        return result;
    }
}
