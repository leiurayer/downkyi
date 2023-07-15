namespace Downkyi.Core.Bili.Utils;

public static class BvId
{
    private const string tableStr = "fZodR9XQDSUm21yCkr6zBqiveYah8bt4xsWpHnJE7jL5VG3guMTKNPAwcF"; //码表
    private static readonly char[] table = tableStr.ToCharArray();

    private static readonly char[] tr = new char[124]; //反查码表
    private const ulong Xor = 177451812; //固定异或值
    private const ulong add = 8728348608; //固定加法值
    private static readonly int[] s = { 11, 10, 3, 8, 4, 6 }; //位置编码表

    static BvId()
    {
        Tr_init();
    }

    //初始化反查码表
    private static void Tr_init()
    {
        for (int i = 0; i < 58; i++)
            tr[table[i]] = (char)i;
    }

    /// <summary>
    /// bvid转avid
    /// </summary>
    /// <param name="bvid"></param>
    /// <returns></returns>
    public static ulong Bv2Av(string bvid)
    {
        char[] bv = bvid.ToCharArray();

        ulong r = 0;
        ulong av;
        for (int i = 0; i < 6; i++)
            r += tr[bv[s[i]]] * (ulong)Math.Pow(58, i);
        av = (r - add) ^ Xor;
        return av;
    }

    /// <summary>
    /// avid转bvid
    /// </summary>
    /// <param name="av"></param>
    /// <returns></returns>
    public static string Av2Bv(ulong av)
    {
        //编码结果
        string res = "BV1  4 1 7  ";
        char[] result = res.ToCharArray();

        av = (av ^ Xor) + add;
        for (int i = 0; i < 6; i++)
            result[s[i]] = table[av / (ulong)Math.Pow(58, i) % 58];
        var bv = new string(result);
        return bv;
    }

}