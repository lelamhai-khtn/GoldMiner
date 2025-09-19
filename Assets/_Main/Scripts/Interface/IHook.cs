using UnityEngine;

public interface IHook 
{
    bool IsSwinging { get; }
    bool IsExtending { get; }
    bool IsRetracting { get; }

    void Swing();
    void Extending();    
    void Retracting();
}
