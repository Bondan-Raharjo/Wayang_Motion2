using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace open1
{
    public class Info
    {
        private string kepala, tubuh, LKiA, LKiB, LKaA, LKaB, KKiA, KKiB, KKaA, KKaB;
        public string[] scenePath;

        public string[] ScenePath
        {
            get { return scenePath; }
            set { scenePath = value; }
        }

        public string kepalaPath
        {
            get { return kepala; }
            set { kepala = value; }
        }

        public string tubuhPath
        {
            get { return tubuh; }
            set { tubuh = value; }
        }

        public string LKiAPath
        {
            get { return LKiA; }
            set { LKiA = value; }
        }

        public string LKiBPath
        {
            get { return LKiB; }
            set { LKiB = value; }
        }

        public string LKaAPath
        {
            get { return LKaA; }
            set { LKaA = value; }
        }

        public string LKaBPath
        {
            get { return LKaB; }
            set { LKaB = value; }
        }

        public string KKiAPath
        {
            get { return KKiA; }
            set { KKiA = value; }
        }

        public string KKiBPath
        {
            get { return KKiB; }
            set { KKiB = value; }
        }

        public string KKaAPath
        {
            get { return KKaA; }
            set { KKaA = value; }
        }

        public string KKaBPath
        {
            get { return KKaB; }
            set { KKaB = value; }
        }
    }
}
