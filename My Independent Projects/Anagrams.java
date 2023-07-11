import java.util.Arrays;
import java.util.Scanner;

public class Main {

    static boolean isAnagram(String a, String b) {
        // Complete the function
        boolean anagr = true;
        String a2 = a.toLowerCase();
        String b2 = b.toLowerCase();
        char [] stra = a2.toCharArray();
        char [] strb = b2.toCharArray();
        char temp;
        if (stra.length == strb.length){
            String sorta = "";
            //Arrays.sort(stra);    The same thing as down here but withou using Arrays.sort() function
            for (int i =0; i<stra.length; i++){
                char mina = stra[i];
                for(int j = i+1; j<stra.length; j++) {
                    if (mina > stra[j]) {
                        temp = mina;
                        mina = stra[j];
                        stra[i] = mina;
                        stra[j] = temp;

                    }
                }
                sorta += stra[i];
            }
            String sortb = "";
            //Arrays.sort(strb);    The same thing as down here but withou using Arrays.sort() function
            for (int i =0; i<strb.length; i++){
                char minb = strb[i];
                for(int j = i+1; j<strb.length; j++) {
                    if (minb > strb[j]) {
                        temp = minb;
                        minb = strb[j];
                        strb[i] = minb;
                        strb[j] = temp;
                    }
                }
                sortb += strb[i];
            }
            System.out.println(sorta);
            System.out.println(sortb);
            for(int i = 0; i<stra.length; i++){
                if (stra[i] != strb[i]){
                    anagr = false;
                    break;
                }
            }
        } else {
            anagr = false;
        }
        return anagr;
    }

    public static void main(String[] args) {

        Scanner scan = new Scanner(System.in);
        String a = scan.next();
        String b = scan.next();
        scan.close();
        boolean ret = isAnagram(a, b);
        System.out.println( (ret) ? "Anagrams" : "Not Anagrams" );
    }
}
