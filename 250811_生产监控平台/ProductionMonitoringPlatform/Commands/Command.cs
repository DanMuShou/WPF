using System.Windows.Input;

namespace ProductionMonitoringPlatform.Commands;

public class Command(Action execute) : ICommand
{
    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        execute();
    }

    public event EventHandler? CanExecuteChanged;
}
