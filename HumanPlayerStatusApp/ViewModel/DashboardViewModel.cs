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

        private int str;

        public int Str
        {
            get { return str; }
            
            set 
            { 
                str = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Str));
            }
        }

        private int agi;

        public int Agi
        {
            get { return agi; }
            set 
            { 
                agi = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Agi));
            }
        }

        private int _int;

        public int Int
        {
            get { return _int; }
            set 
            {
                _int = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Int));
            }
        }

        private int comm;

        public int Comm
        {
            get { return comm; }
            set 
            {
                comm = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Comm));
            }
        }
        private int menstb;

        public int MenStb
        {
            get { return menstb; }
            set 
            {
                menstb = value;
                UpdateGraph();
                OnPropertyChanged(nameof(MenStb));
            }
        }

        private int crt;

        public int Crt
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

        private int hp;

        public int Hp
        {
            get { return hp; }
            set 
            {
                hp = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Hp));
            }
        }

        private int hyg;

        public int Hyg
        {
            get { return hyg; }
            set 
            {
                hyg = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Hyg));
            }
        }
        private ISeries[] series;

        public ISeries[] Series
        {
            get { return series; }
            set 
            { 
                series = value;
                OnPropertyChanged(nameof(Series));
            }
        }



        private PolarAxis[] angleAxes;

        public PolarAxis[] AngleAxes
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
            Strength = "STR : ";

            Agility = "AGI : ";

            Intelligence = "INT : ";

            Communication = "COMM : ";

            MentalStability = "MENSTB : ";

            Creativity = "CRT : ";

            HealthPoints = "HP : ";

            Hygine = "HYG : ";

            dBContext = new AzureDBContext();

            _ = MakeHumanPlayerStatReadAPICall();

            AngleAxes = new PolarAxis[] { new PolarAxis {
                LabelsRotation = LiveCharts.TangentAngle,
                Labels = new string[]{ "STR","AGI","INT","COMM","MENSTB","CRT","HP","HYG" }
            } 
            };
        }

        private async Task MakeHumanPlayerStatReadAPICall() 
        {
            PlayerStats _GetPlayerStats = await dBContext.GetPlayerStats(IDStore.UniqueId, IDStore.PartitionKey);

            Str = _GetPlayerStats.STR;
            Agi = _GetPlayerStats.AGI;
            Int = _GetPlayerStats.INT;
            Comm = _GetPlayerStats.COMM;
            MenStb = _GetPlayerStats.MENSTB;
            Crt = _GetPlayerStats.CRT;
            Hp = _GetPlayerStats.HP;
            Hyg = _GetPlayerStats.HYG;
        }

        private void UpdateGraph()
        {
            Series = new ISeries[] { new PolarLineSeries<int> {
                Values=new[]{Str,Agi,Int,Comm,MenStb,Crt,Hp,Hyg },
                LineSmoothness=0,
                GeometrySize=0,
                Fill = new SolidColorPaint(SKColors.LightGreen.WithAlpha(90))
            } 
            };


        }

    }
}
