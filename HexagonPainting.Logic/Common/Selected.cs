namespace HexagonPainting.Logic.Common;

public class Selected<T> : ISelectedValueProvider<T>
{
    public Selected(T defaultValue)
    {
        Value = defaultValue;
    }
    public T Value { get; set; }
}
