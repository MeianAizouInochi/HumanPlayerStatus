using AzureCosmosDatabaseAccess;
using DataModelLayer.DataModels;
using HumanPlayerStatusApp.Store;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.Azure.Cosmos;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using Microsoft.Azure.Cosmos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace HumanPlayerStatusApp.ViewModel
{
    public class DashboardViewModel:ViewModelBase
    {
        public string Strength { get; }

        public string Agility { get;}

        public string Intelligence { get;  }
        
        public string Communication { get;  }
        
        public string MentalStability { get; }
        
        public string Creativity { get;  }
        
        public string HealthPoints { get;  }
        
        public string Hygine { get;  }
        
        public string HumanPlayerNameLabel { get; set; }
        
        public string HumanPlayerName { get; set; }
        
        public string HumanPlayerLevelLabel { get; set; }

        private long _humanPlayerLevel;

        public long HumanPlayerLevel
        {
            get { return _humanPlayerLevel; }
            set 
            { 
                _humanPlayerLevel = value;
                OnPropertyChanged(nameof(HumanPlayerLevel));
            }
        }

        private long _humanPlayerLevelMaxExp;

        public long HumanPlayerLevelMaxExp
        {
            get { return _humanPlayerLevelMaxExp; }
            set 
            { 
                _humanPlayerLevelMaxExp = value;

                OnPropertyChanged(nameof(HumanPlayerLevelMaxExp));
            }
        }

        private long _humanPlayerExp;

        public long HumanPlayerExp
        {
            get { return _humanPlayerExp; }
            set 
            { 
                _humanPlayerExp = value;
                OnPropertyChanged(nameof(HumanPlayerExp));
            }
        }

        private string? _humanPlayerExpPercent;

        public string? HumanPlayerExpPercent
        {
            get { return _humanPlayerExpPercent; }
            set 
            { 
                _humanPlayerExpPercent = value;
                OnPropertyChanged(nameof(HumanPlayerExpPercent));
            }
        }


        private double str;

        public double Str
        {
            get { return str; }
            
            set 
            { 
                str = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Str));
            }
        }

        private double agi;

        public double Agi
        {
            get { return agi; }
            set 
            { 
                agi = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Agi));
            }
        }

        private double _int;

        public double Int
        {
            get { return _int; }
            set 
            {
                _int = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Int));
            }
        }

        private double comm;

        public double Comm
        {
            get { return comm; }
            set 
            {
                comm = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Comm));
            }
        }

        private double menstb;

        public double MenStb
        {
            get { return menstb; }
            set 
            {
                menstb = value;
                UpdateGraph();
                OnPropertyChanged(nameof(MenStb));
            }
        }

        private double crt;

        public double Crt
        {
            get { return crt; }
            set 
            {
                crt = value;
                UpdateGraph();
                UpdateGraph();
                OnPropertyChanged(nameof(Crt));
            }
        }

        private double hp;

        public double Hp
        {
            get { return hp; }
            set 
            {
                hp = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Hp));
            }
        }

        private double hyg;

        public double Hyg
        {
            get { return hyg; }
            set 
            {
                hyg = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Hyg));
            }
        }

        private ISeries[]? series;

        public ISeries[]? Series
        {
            get { return series; }
            set 
            { 
                series = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        private PolarAxis[]? angleAxes;

        public PolarAxis[]? AngleAxes
        {
            get { return angleAxes; }
            set
            {
                angleAxes = value;
                OnPropertyChanged(nameof(AngleAxes));
            }
        }

        private AzureDBContext? dBContext { get; }

        public PlayerStats? _playerStats { get; }

        public DashboardViewModel()
        {
            HumanPlayerNameLabel = "Name: ";

            HumanPlayerName = "Meian";

            Strength = "STR : ";

            Agility = "AGI : ";

            Intelligence = "INT : ";

            Communication = "COMM : ";

            MentalStability = "MENSTB : ";

            Creativity = "CRT : ";

            HealthPoints = "HP : ";

            Hygine = "HYG : ";

            HumanPlayerLevelLabel = "Lv: ";

            dBContext = new AzureDBContext();

            _ = MakeHumanPlayerStatReadAPICall();

            AngleAxes = new PolarAxis[] { 
                new PolarAxis {
                LabelsRotation = LiveCharts.TangentAngle,
                Labels = new string[]{ "STR","AGI","INT","COMM","MENSTB","CRT","HP","HYG" }
            } 
            };
        }

        private async Task MakeHumanPlayerStatReadAPICall() 
        {
            try
            {
                PlayerStats _GetPlayerStats = await dBContext.GetPlayerStats(IDStore.StatsUniqueId, IDStore.StatsPartitionKey);

                HumanPlayerExp = _GetPlayerStats.HumanPlayerExp;
                HumanPlayerLevelMaxExp = _GetPlayerStats.HumanPlayerLevelMaxExp;
                HumanPlayerLevel = _GetPlayerStats.HumanPlayerLevel;
                Str = _GetPlayerStats.STR;
                Agi = _GetPlayerStats.AGI;
                Int = _GetPlayerStats.INT;
                Comm = _GetPlayerStats.COMM;
                MenStb = _GetPlayerStats.MENSTB;
                Crt = _GetPlayerStats.CRT;
                Hp = _GetPlayerStats.HP;
                Hyg = _GetPlayerStats.HYG;

                var TempExpPercent = (HumanPlayerExp * 100) / HumanPlayerLevelMaxExp;

                HumanPlayerExpPercent = TempExpPercent.ToString() + "%";
            }
            catch(CosmosException CosmosEx) 
            {
                //TODO: Add a functionality to restart this function.
                MessageBox.Show(CosmosEx.Message);
            }
            catch (Exception ex) 
            {
                //TODO: Add a functionality to restart this function.
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateGraph()
        {
            Series = new ISeries[] { new PolarLineSeries<double> {
                Values=new[]{Str,Agi,Int,Comm,MenStb,Crt,Hp,Hyg },
                LineSmoothness=0,
                GeometrySize=0,
                Fill = new SolidColorPaint(SKColors.LightGreen.WithAlpha(90))
            } 
            };

        }

    }
}
