namespace AddedControl
{
    public interface IActionInput
    {
        bool IsFirePressed();
        bool IsFirePress();
        bool IsSlowPressed();
        bool IsRebootPressed();
        bool IsDashPressed();
        bool IsJumpPressed();
    }
}