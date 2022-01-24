
namespace QRCode
{
    public interface IWrapper<out T>
    {
        T Value { get; }
    }
}
