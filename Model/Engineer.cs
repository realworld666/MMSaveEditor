using System.Collections.Generic;
using FullSerializer;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using MMSaveEditor.ViewModel;
using MMSaveEditor.View;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Engineer : Person
{
    public EngineerStats stats = new EngineerStats();
    public EngineerStats lastAccumulatedStats = new EngineerStats();
    public float improvementRate;
    public List<CarPartComponent> availableComponents = new List<CarPartComponent>();
    private readonly float negativeImprovementHQScalar = 0.9f;
    private readonly float negativeImprovementHQOverallScalar = 0.03f;
    private readonly float negativeMaxImprovementHQ = 0.75f;
    private readonly float learnNewComponentChancePerAbilityStar = 0.8f;

    public EngineerStats Stats
    {
        get { return stats; }
    }

    public RelayCommand<Engineer> ViewDriver { get; private set; }

    public Engineer()
    {
        ViewDriver = new RelayCommand<Engineer>(_viewDriver);
    }
    private void _viewDriver(Engineer d)
    {
        var driverVM = SimpleIoc.Default.GetInstance<EngineerViewModel>();
        driverVM.SetModel(this);
        MainWindow.Instance.SwitchToTab(MainWindow.TabPage.Engineer);
    }

    public override bool IsReplacementPerson()
    {
        return Game.instance.engineerManager.IsReplacementPerson(this);
    }
}
