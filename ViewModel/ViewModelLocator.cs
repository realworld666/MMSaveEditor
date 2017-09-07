/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MMSaveEditor"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace MMSaveEditor.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<TeamPrincipalViewModel>();
            SimpleIoc.Default.Register<EngineerViewModel>();
            SimpleIoc.Default.Register<DriverViewModel>();
            SimpleIoc.Default.Register<PlayerViewModel>();
            SimpleIoc.Default.Register<GameViewModel>();
            SimpleIoc.Default.Register<TeamViewModel>();
            SimpleIoc.Default.Register<MechanicViewModel>();
            SimpleIoc.Default.Register<ChairmanViewModel>();
            SimpleIoc.Default.Register<ChampionshipViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public TeamPrincipalViewModel TeamPrincipal
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TeamPrincipalViewModel>();
            }
        }

        public EngineerViewModel Engineer
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EngineerViewModel>();
            }
        }

        public MechanicViewModel Mechanic
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MechanicViewModel>();
            }
        }

        public PlayerViewModel Player
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlayerViewModel>();
            }
        }

        public GameViewModel Game
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GameViewModel>();
            }
        }

        public TeamViewModel Team
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TeamViewModel>();
            }
        }

        public DriverViewModel Driver
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DriverViewModel>();
            }
        }

        public ChairmanViewModel Chairman
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChairmanViewModel>();
            }
        }

        public ChampionshipViewModel Championship
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChampionshipViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
