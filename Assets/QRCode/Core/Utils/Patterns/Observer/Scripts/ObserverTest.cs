using QRCode;
using UnityEngine;

/// <summary>
/// OBSERVER EXAMPLE.
/// </summary>
public interface IObservableMessage : IObservable
{

}

/// <summary>
/// OBSERVER EXAMPLE.
/// </summary>
public class ObserverTest : Observer<IObservableMessage>
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Notify();
        }
    }
}
