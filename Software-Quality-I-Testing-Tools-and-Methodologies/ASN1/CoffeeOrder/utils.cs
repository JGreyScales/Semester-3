public class Utils {
    public static string getClosetEnum(Enum enumList, byte input){
        // required to figure out what Enum we are working with
        Type enumType = enumList.GetType(); 
        // get an iterable list of values of our enum
        Array values = Enum.GetValues(enumType);
        // nullable closet value
        Enum? closetVal = null;
        byte closetByte = byte.MaxValue;

        foreach (Enum val in values){
            // make our value a known type
            byte enumByte = Convert.ToByte(val);
            // I like my bytes, I believe this would reduce stack usage once this goes
            // out of scope
            // knowing that the input must be a byte, this would never overflow
            byte dif = (byte)Math.Abs((int)enumByte - (int)input);

            if (dif < closetByte){
                closetByte = dif;
                closetVal = val;
            }
        }

        // if the input is equal to MaxValue this function will return null
        if (closetVal == null){
            return "";
        }
        return closetVal.ToString();
    }


    public static List<string> mergeStringLists(List<string> l1, List<string> l2){
        List<string> mergedList = new List<string>();
        if (l1 != null){
            foreach(string item in l1){mergedList.Add(item);}
        }
        if (l2 != null){
            foreach(string item in l2){mergedList.Add(item);}
        }
        return mergedList;
    }

    public static List<string> dedupeStringList(List<string> slist){
        List<string> deDupedStringList = new List<string>();
        
        if (slist != null){
            foreach(string item in slist){
                if (!deDupedStringList.Any(str => str.Contains(item))){
                    deDupedStringList.Add(item);
                }
            }
        }

        
        return deDupedStringList;
    }
}
