namespace Chapter04;

public class Country
{
    /// <summary>
    /// 国家ID
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// 国家名称
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 国家代码
    /// </summary>
    public required string Code { get; set; }

    public override string ToString()
    {
        return $"Id:{Id},Name:{Name},Code:{Code}";
    }
}
