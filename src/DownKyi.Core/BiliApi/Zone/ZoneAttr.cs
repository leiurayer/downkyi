namespace DownKyi.Core.BiliApi.Zone
{
    public class ZoneAttr
    {
        public int Id { get; }
        public string Type { get; }
        public string Name { get; }
        public int ParentId { get; }

        public ZoneAttr(int id, string type, string name, int parentId = 0)
        {
            Id = id;
            Type = type;
            Name = name;
            ParentId = parentId;
        }

    }
}
