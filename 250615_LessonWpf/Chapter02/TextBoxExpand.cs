using System.Windows.Controls;

namespace Chapter02;

internal class TextBoxExpand : TextBox
{
    // 依赖属性需求
    // 1. 要求支持绑定
    // 2. 自定义/扩展性属性 (一般要求支持绑定)
    // 3. 验证/强制回调
    // 4. 继承
    // 5. 附加属性 (一种特殊的依赖属性)
    public int TextNumber { get; set; }
}
