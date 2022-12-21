using System;
using Terminals;
using UnityEngine;
using Random = System.Random;

namespace DefaultNamespace
{
    public class PathGenerator : MonoBehaviour
    {
        public PathModel PathModel;
        private Vector3 _start;
        private Random _rnd;
        private Vector3 _nextPoint;

        private void Awake()
        {
            PathModel.Init();
            _rnd = new Random();
            _start = new Vector3(0, 0.5f, 5);
            CreatePath();
        }

        public void ContinuePath()
        {
            _nextPoint = ChooseYourWeapon(_nextPoint);
        }
        
        private void CreatePath()
        {
            new TripleBlock(PathModel).Create(_start + new Vector3(0, 0, 0));
            _nextPoint = ChooseYourWeapon(_start + new Vector3(0, 0, 1));
            int i = 0;
            while (i < 10)
            {
                _nextPoint = ChooseYourWeapon(_nextPoint);
                i++;
            }
        }
        

        private Vector3 ChooseYourWeapon(Vector3 vec)
        {
            Double rnd = _rnd.NextDouble();
            if (rnd <= 0.1)
            {
                return new UpStairs(PathModel).Create(vec);
            }

            if (rnd >= 0.9)
            {
                return new SingleBlock(PathModel).Create(vec);
            }
            if (rnd >= 0.8)
            {
                return new RightSweep(PathModel).Create(vec);
            }
            if (rnd >= 0.7)
            {
                return new LeftSweep(PathModel).Create(vec);
            }
            if (rnd >= 0.6)
            {
                return new SingleSpike(PathModel).Create(vec);
            }

            return new TripleBlock(PathModel).Create(vec);
        }
    }
}