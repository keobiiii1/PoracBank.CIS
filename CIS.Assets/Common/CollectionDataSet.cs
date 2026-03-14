namespace CIS.Assets.Common
{
    public class CollectionDataSet<T>
    {
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; } = new List<T>();
    }
}