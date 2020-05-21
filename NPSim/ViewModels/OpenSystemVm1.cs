using System.Collections.ObjectModel;
using System.Collections.Specialized;
using NPSim.Domain.Builders;
using NPSim.Models;

namespace NPSim.ViewModels
{
    public class OpenSystemVm1 : INotifyCollectionChanged
    {
        private ObservableCollection<OpenSystemModel1> _openSystemModels;

        public ObservableCollection<OpenSystemModel1> OpenSystemModels
        {
            get => _openSystemModels;
            set
            {
                _openSystemModels = value;
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace));
            }
        }

        private readonly IOpenSystemBuilder _openSystemBuilder;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public OpenSystemVm1(IOpenSystemBuilder openSystemBuilder)
        {
            _openSystemBuilder = openSystemBuilder;

            OpenSystemModels = new ObservableCollection<OpenSystemModel1>();
        }

        public void AddOpenSystem()
        {
            var computer = _openSystemBuilder.BuildComputer();
            var openSystem = new OpenSystemModel1(computer);
            OpenSystemModels.Add(openSystem);
        }
    }
}
