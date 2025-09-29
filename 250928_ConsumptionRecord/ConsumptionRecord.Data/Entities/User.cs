namespace ConsumptionRecord.Data.Entities;

public class User
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    /// <summary>
    /// 是否为管理员
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// 用户账户创建时间
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// 创建该用户账户的用户标识符
    /// </summary>
    public Guid? CreatedBy { get; set; }
}
