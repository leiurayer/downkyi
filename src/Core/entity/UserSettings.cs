namespace Core.entity
{
    // https://space.bilibili.com/ajax/settings/getSettings?mid=42018135
    public class UserSettings
    {
        public UserSettingsData data { get; set; }
        public bool status { get; set; }
    }

    public class UserSettingsData
    {
        // ……

        public UserSettingsDataToutu toutu { get; set; }
    }


    public class UserSettingsDataToutu
    {
        public string android_img { get; set; }
        public long expire { get; set; }
        public string ipad_img { get; set; }
        public string iphone_img { get; set; }
        public string l_img { get; set; }
        public int platform { get; set; }
        public string s_img { get; set; }
        public int sid { get; set; }
        public string thumbnail_img { get; set; }
    }

}
