public interface IHook 
{
    HookState state { get; set; }
}

public enum HookState 
{ 
    Swing, Extending, Retracting 
}