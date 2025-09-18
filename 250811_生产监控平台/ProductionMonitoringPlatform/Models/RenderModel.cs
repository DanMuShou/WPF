using ProductionMonitoringPlatform.Models.Base;

namespace ProductionMonitoringPlatform.Models;

public class RenderModel : NotifyBaseModel
{
    private string _itemName = "Null";

    public string ItemName
    {
         get => _itemName;
         set => SetField(ref _itemName, value);
    }
    
    private string _itemValue = "Null";
    
    public string ItemValue
    {
         get => _itemValue;
         set => SetField(ref _itemValue, value);
    }
}