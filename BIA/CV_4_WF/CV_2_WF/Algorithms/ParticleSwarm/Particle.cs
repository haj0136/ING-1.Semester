﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_4_WF.ParticleSwarm
{
    public class Particle
    {
        public List<float> X { get; set; }
        private List<float> speed;
        private List<float> pBest;

        public Particle()
        {
            X = new List<float>();
            speed = new List<float>();
            pBest = new List<float>();
        }

        public float[] ToFloatArray()
        {
            return X.ToArray();
        }
    }
}
