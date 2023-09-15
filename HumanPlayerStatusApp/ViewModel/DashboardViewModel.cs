using AzureCosmosDatabaseAccess;
using DataModelLayer.DataModels;
using HumanPlayerStatusApp.Store;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.Azure.Cosmos;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using System;
using System.Threading.Tasks;
using System.Windows;


namespace HumanPlayerStatusApp.ViewModel
{
    public class DashboardViewModel:ViewModelBase
    {
        public string StatTypeHeadingLabel { get; }

        public string StatValueHeadingLabel { get; }

        public string StatDebuffValueHeadingLabel { get;}

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


        private float str;

        public float Str
        {
            get { return str; }
            
            set 
            { 
                str = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Str));
            }
        }

        private float agi;

        public float Agi
        {
            get { return agi; }
            set 
            { 
                agi = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Agi));
            }
        }

        private float _int;

        public float Int
        {
            get { return _int; }
            set 
            {
                _int = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Int));
            }
        }

        private float comm;

        public float Comm
        {
            get { return comm; }
            set 
            {
                comm = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Comm));
            }
        }

        private float menstb;

        public float MenStb
        {
            get { return menstb; }
            set 
            {
                menstb = value;
                UpdateGraph();
                OnPropertyChanged(nameof(MenStb));
            }
        }

        private float crt;

        public float Crt
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

        private float hp;

        public float Hp
        {
            get { return hp; }
            set 
            {
                hp = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Hp));
            }
        }

        private float hyg;

        public float Hyg
        {
            get { return hyg; }
            set 
            {
                hyg = value;
                UpdateGraph();
                OnPropertyChanged(nameof(Hyg));
            }
        }

        private float[] debuffRates;

        public float[] DebuffRates
        {
            get { return debuffRates; }
            set
            {
                debuffRates = value;
                OnPropertyChanged(nameof(DebuffRates));
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

        private AzureDBContext dBContext { get; }

        public string? TraitsLabel { get; set; }

        private string? traits;

        public string? Traits
        {
            get { return traits; }
            set 
            { 
                traits = value;
                OnPropertyChanged(nameof(Traits));
            }
        }

        public string? DebuffsLabel { get; set; }

        private string? debuffs;

        public string? Debuffs
        {
            get { return debuffs; }
            set 
            { 
                debuffs = value;
                OnPropertyChanged(nameof(Debuffs));
            }
        }

        public DashboardViewModel()
        {
            StatTypeHeadingLabel = "Stats";

            StatValueHeadingLabel = "Values";

            StatDebuffValueHeadingLabel = "Debuff Rates";

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

            TraitsLabel = "TRAITS";

            DebuffsLabel = "DEBUFFS";

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
                Str = StatCalculator(_GetPlayerStats.STR, _GetPlayerStats.DebuffRates[0]);
                Agi = StatCalculator(_GetPlayerStats.AGI, _GetPlayerStats.DebuffRates[1]);
                Int = StatCalculator(_GetPlayerStats.INT, _GetPlayerStats.DebuffRates[2]);
                Comm = StatCalculator(_GetPlayerStats.COMM, _GetPlayerStats.DebuffRates[3]);
                MenStb = StatCalculator(_GetPlayerStats.MENSTB, _GetPlayerStats.DebuffRates[4]);
                Crt = StatCalculator(_GetPlayerStats.CRT, _GetPlayerStats.DebuffRates[5]);
                Hp = StatCalculator(_GetPlayerStats.HP, _GetPlayerStats.DebuffRates[6]);
                Hyg = StatCalculator(_GetPlayerStats.HYG, _GetPlayerStats.DebuffRates[7]);
                DebuffRates = _GetPlayerStats.DebuffRates;
                Traits = string.Join(", ", _GetPlayerStats.Traits);
                Debuffs = string.Join(", ", _GetPlayerStats.Debuffs);

                var TempExpPercent = (HumanPlayerExp * 100) / HumanPlayerLevelMaxExp;

                HumanPlayerExpPercent = TempExpPercent.ToString() + "%";
            }
            catch(CosmosException CosmosEx) 
            {
                
                MessageBoxResult res = MessageBox.Show(CosmosEx.Message + "\n Click Ok to Retry or Cancel to Exit Application!", "Error", MessageBoxButton.OKCancel);

                if (res == MessageBoxResult.OK)
                {
                    _ = MakeHumanPlayerStatReadAPICall();
                }
                else if (res == MessageBoxResult.Cancel)
                {
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex) 
            {
                MessageBoxResult res = MessageBox.Show(ex.Message + "\n Click Ok to Retry or Cancel to Exit Application!", "Error", MessageBoxButton.OKCancel);

                if (res == MessageBoxResult.OK)
                {
                    _ = MakeHumanPlayerStatReadAPICall();
                }
                else if (res == MessageBoxResult.Cancel)
                {
                    Application.Current.Shutdown();
                }
            }
        }

        private void UpdateGraph()
        {
            Series = new ISeries[] { new PolarLineSeries<float> {
                Values=new float[] { Str, Agi, Int, Comm, MenStb, Crt, Hp, Hyg },
                LineSmoothness=0,
                GeometrySize=0,
                Fill = new SolidColorPaint(SKColors.LightGreen.WithAlpha(90)),
                TooltipLabelFormatter = point =>{ return point.PrimaryValue.ToString("N2"); }
            }
            };

            Series[0].Name = "Stats Chart";

        }

        private float StatCalculator(float Stat, float _debuffrate)
        {
            float res = Stat - ((Stat * _debuffrate)/100.0F);

            return (float)Math.Round(res,2);
        }

    }
}
