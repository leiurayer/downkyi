namespace Downkyi.Core.Bili.Utils;

public static class DanmakuSender
{
    private const uint CRCPOLYNOMIAL = 0xEDB88320;
    private static readonly uint[] crctable = new uint[256];

    static DanmakuSender()
    {
        CreateTable();
    }

    private static void CreateTable()
    {
        for (int i = 0; i < 256; i++)
        {
            uint crcreg = (uint)i;

            for (int j = 0; j < 8; j++)
            {
                if ((crcreg & 1) != 0)
                {
                    crcreg = CRCPOLYNOMIAL ^ (crcreg >> 1);
                }
                else
                {
                    crcreg >>= 1;
                }
            }
            crctable[i] = crcreg;
        }
    }

    private static uint Crc32(string userId)
    {
        uint crcstart = 0xFFFFFFFF;
        for (int i = 0; i < userId.Length; i++)
        {
            uint index = (uint)(crcstart ^ (int)userId[i]) & 255;
            crcstart = (crcstart >> 8) ^ crctable[index];
        }
        return crcstart;
    }

    private static uint Crc32LastIndex(string userId)
    {
        uint index = 0;
        uint crcstart = 0xFFFFFFFF;
        for (int i = 0; i < userId.Length; i++)
        {
            index = (uint)((crcstart ^ (int)userId[i]) & 255);
            crcstart = (crcstart >> 8) ^ crctable[index];
        }
        return index;
    }

    private static int GetCrcIndex(long t)
    {
        for (int i = 0; i < 256; i++)
        {
            if ((crctable[i] >> 24) == t)
            {
                return i;
            }
        }
        return -1;
    }

    private static object[] DeepCheck(int i, int[] index)
    {
        object[] resultArray = new object[2];

        string result = "";
        uint tc;// = 0x00;
        var hashcode = Crc32(i.ToString());
        tc = (uint)(hashcode & 0xff ^ index[2]);

        if (!(tc <= 57 && tc >= 48))
        {
            resultArray[0] = 0;
            return resultArray;
        }

        result += (tc - 48).ToString();
        hashcode = crctable[index[2]] ^ (hashcode >> 8);
        tc = (uint)(hashcode & 0xff ^ index[1]);

        if (!(tc <= 57 && tc >= 48))
        {
            resultArray[0] = 0;
            return resultArray;
        }

        result += (tc - 48).ToString();
        hashcode = crctable[index[1]] ^ (hashcode >> 8);
        tc = (uint)(hashcode & 0xff ^ index[0]);

        if (!(tc <= 57 && tc >= 48))
        {
            resultArray[0] = 0;
            return resultArray;
        }

        result += (tc - 48).ToString();
        //hashcode = crctable[index[0]] ^ (hashcode >> 8);

        resultArray[0] = 1;
        resultArray[1] = result;
        return resultArray;
    }

    /// <summary>
    /// 查询弹幕发送者
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static string FindDanmakuSender(string userId)
    {
        object[] deepCheckData = new object[2];

        int[] index = new int[4];
        uint ht = (uint)Convert.ToInt32($"0x{userId}", 16);
        ht ^= 0xffffffff;

        int i;
        for (i = 3; i > -1; i--)
        {
            index[3 - i] = GetCrcIndex(ht >> (i * 8));
            uint snum = crctable[index[3 - i]];
            ht ^= snum >> ((3 - i) * 8);
        }
        for (i = 0; i < 100000000; i++)
        {
            uint lastindex = Crc32LastIndex(i.ToString());
            if (lastindex == index[3])
            {
                deepCheckData = DeepCheck(i, index);
                if ((int)deepCheckData[0] != 0)
                {
                    break;
                }
            }
        }
        if (i == 100000000)
        {
            return "-1";
        }
        return $"{i}{deepCheckData[1]}";
    }

}