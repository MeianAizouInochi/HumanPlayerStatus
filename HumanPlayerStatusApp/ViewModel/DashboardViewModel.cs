using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanPlayerStatusApp.ViewModel
{
    public class DashboardViewModel:ViewModelBase
    {
        public string Strength { get; }
        public string Agility { get;}
        public string Intelligence { get; set; }
        public string Communication { get; set; }
        public string MentalStability { get; set; }
        public string Creativity { get; set; }
        public string HealthPoints { get; set; }
        public string Hygine { get; set; }

        private int str;

        public int Str
        {
            get { return str; }
            set { str = value; }
        }

        private int agi;

        public int Agi
        {
            get { return agi; }
            set { agi = value; }
        }

        private int _int;

        public int Int
        {
            get { return _int; }
            set { _int = value; }
        }

        private int comm;

        public int Comm
        {
            get { return comm; }
            set { comm = value; }
        }
        private int menstb;

        public int MenStb
        {
            get { return menstb; }
            set { menstb = value; }
        }

        private int crt;

        public int Crt
        {
            get { return crt; }
            set { crt = value; }
        }

        private int hp;

        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        private int hyg;

        public int Hyg
        {
            get { return hyg; }
            set { hyg = value; }
        }


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



        }
    }
}
