import java.util.ArrayList;
import java.util.List;

public class KMPMatcher implements IStringMatcher {
    void computeLPSArray(String pat, int patSize, int[] lps)
    {
        int len = 0;
        int i = 1;
        lps[0] = 0;

        while (i < patSize)
        {
            if (pat.charAt(i) == pat.charAt(len))
            {
                len++;
                lps[i] = len;
                i++;
            }
            else
            {
                if (len != 0)
                {
                    len = lps[len - 1];
                }
                else
                {
                    lps[i] = len;
                    i++;
                }
            }
        }
    }

    @Override
    public List<Integer> validShifts(String textToSearch, String patternToFind) {
        List<Integer> result = new ArrayList<>();
        int [] lps = new int[patternToFind.length()];
        int patIndex = 0;

        computeLPSArray(patternToFind, patternToFind.length(), lps);

        int textIndex = 0;
        while (textIndex < textToSearch.length())
        {
            if (patternToFind.charAt(patIndex) == textToSearch.charAt(textIndex))
            {
                patIndex++;
                textIndex++;
            }

            if (patIndex == patternToFind.length())
            {
                result.add(textIndex - patIndex);
                patIndex = lps[patIndex - 1];
            }
            else if (textIndex < textToSearch.length() && patternToFind.charAt(patIndex) != textToSearch.charAt(textIndex))
            {
                if (patIndex == 0)
                {
                    textIndex = textIndex + 1;
                }
                else
                {
                    patIndex = lps[patIndex - 1];
                }
            }
        }

        return result;
    }
}
