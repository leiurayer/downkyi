using Newtonsoft.Json;

namespace Core.entity2
{
    public abstract class BaseEntity
    {
        public string ToString(string format = "")
        {
            // 设置为去掉null
            var jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            
            switch (format)
            {
                case "":
                    return JsonConvert.SerializeObject(this);
                case "F":
                    // 整理json格式
                    return JsonConvert.SerializeObject(this, Formatting.Indented);
                case "N":
                    // 去掉null后，转换为json字符串
                    return JsonConvert.SerializeObject(this, Formatting.None, jsonSetting);
                case "FN":
                case "NF":
                    return JsonConvert.SerializeObject(this, Formatting.Indented, jsonSetting);
                default:
                    return ToString();
            }
        }
    }
}
