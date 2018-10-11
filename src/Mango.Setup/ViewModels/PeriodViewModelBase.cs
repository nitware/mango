using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.Interfaces;
using Mango.Setup.Services;
using Mango.Infrastructure.Models;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mango.Infrastructure.Events;
using Microsoft.Practices.Prism.Events;

namespace Mango.Setup.ViewModels
{
    public class PeriodViewModelBase : SetupViewModelBase<Period>
    {
        private ICollectionView types;
        private PeriodType type;
        private Value year;
        private ICollectionView years;
        private PeriodTypeService periodTypeService;
        private const int yearStartsFrom = 2010;

        public PeriodViewModelBase(ISetupService<Period> _service, IEventAggregator _eventAggregator)
            : base(_service)
        {
            periodTypeService = new PeriodTypeService();
            //LoadAllPeriodTypeCompleted();
            //periodTypeService.LoadAll();
            
            //PopulateYears(yearStartsFrom);

            //_eventAggregator.GetEvent<SetupEvent>().Subscribe(OnInitialise);

            OnInitialise("");
        }

        public void OnInitialise(string refresh)
        {
            LoadAllPeriodTypeCompleted();
            periodTypeService.LoadAll();

            PopulateYears(yearStartsFrom);
        }

        public ICollectionView Types
        {
            get { return types; }
            set
            {
                types = value;
                base.OnPropertyChanged("Types");
            }
        }
        public PeriodType Type
        {
            get { return type; }
            set
            {
                type = value;
                base.OnPropertyChanged("Type");
            }
        }
        public ICollectionView Years
        {
            get { return years; }
            set
            {
                years = value;
                base.OnPropertyChanged("Years");
            }
        }
        public Value Year
        {
            get { return year; }
            set
            {
                year = value;
                base.OnPropertyChanged("Year");
            }
        }

        private void LoadAllPeriodTypeCompleted()
        {
            EventHandler handler = null;

            try
            {
                handler = (s, e) =>
                {
                    LoadAllPeriodTypeCompletedHelper();
                    periodTypeService.GetAllModelsCompleted -= handler;
                };

                periodTypeService.GetAllModelsCompleted += handler;
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }
        private void LoadAllPeriodTypeCompletedHelper()
        {
            try
            {
                dispatcher.BeginInvoke
                           (() =>
                           {
                               if (Utility.FaultExist(periodTypeService.Fault))
                               {
                                   return;
                               }

                               if (periodTypeService.Models != null && periodTypeService.Models.Count > 0)
                               {
                                   periodTypeService.Models.Insert(0, new PeriodType() { Id = 0, Name = "<< Select Type >>" });

                                   Types = new PagedCollectionView(periodTypeService.Models);
                                   Types.MoveCurrentToFirst();
                                   Types.CurrentChanged += (s, e) =>
                                   {
                                       Type = Types.CurrentItem as PeriodType;
                                   };
                               }
                           });
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

        private void PopulateYears(int beginYearFrom)
        {
            try
            {
                //dispatcher.BeginInvoke
                //           (() =>
                //           {
                ObservableCollection<Value> years = Utility.CreateYearListFrom(beginYearFrom);

                if (years != null && years.Count > 0)
                {
                    //years.Insert(0, new Value() { Id = 0, Name = "<< Select Year >>" });

                    Years = new PagedCollectionView(years);
                    Years.MoveCurrentToFirst();
                    Years.CurrentChanged += (s, e) =>
                    {
                        Year = Years.CurrentItem as Value;
                    };
                }
                //});
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }

    }


}
