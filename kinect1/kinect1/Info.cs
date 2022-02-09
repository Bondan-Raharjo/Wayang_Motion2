using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinect1
{
    public class Info
    {
        private string kepala, tubuh, LKiA, LKiB, LKaA, LKaB, KKiA, KKiB, KKaA, KKaB;
        private string[] scenePath;

        public struct Player1Config
        {
            public int kepala1Rasio, tubuh1Rasio, lKi1Rasio, tKi1Rasio, lKa1Rasio, tKa1Rasio, pKi1Rasio, kKi1Rasio, pKa1Rasio, kKa1Rasio;
            public int HeadToCShoulderCal1, CShoulderToCHipCal1, CShoulderToRShoulderCal1, RShoulderToRElbowCal1, RElbowToRHandCal1, CShoulderToLShoulderCal1, LShoulderToLElbowCal1, LElbowToLHandCal1, CHipToRHipCal1, RHipToRKneeCal1, RKneeToRAnkleCal1, CHipToLHipCal1, LHipToLKneeCal1, LKneeToLAnkleCal1;
        }

        public struct Player2Config
        {
            public int kepala2Rasio, tubuh2Rasio, lKi2Rasio, tKi2Rasio, lKa2Rasio, tKa2Rasio, pKi2Rasio, kKi2Rasio, pKa2Rasio, kKa2Rasio;
            public int HeadToCShoulderCal2, CShoulderToCHipCal2, CShoulderToRShoulderCal2, RShoulderToRElbowCal2, RElbowToRHandCal2, CShoulderToLShoulderCal2, LShoulderToLElbowCal2, LElbowToLHandCal2, CHipToRHipCal2, RHipToRKneeCal2, RKneeToRAnkleCal2, CHipToLHipCal2, LHipToLKneeCal2, LKneeToLAnkleCal2;
        }

        Player1Config p1con;
        Player2Config p2con;

        public Player1Config P1Con
        {
            get { return p1con; }
            set { p1con = value; }
        }

        public Player2Config P2Con
        {
            get { return p2con; }
            set { p2con = value; }
        }

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
