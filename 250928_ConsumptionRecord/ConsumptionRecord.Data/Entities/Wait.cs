namespace ConsumptionRecord.Data.Entities;

public class Wait
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreateTime { get; set; }
}
