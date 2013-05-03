using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class Weapon : Item
    {
        //Attributes
        protected string type;
        protected string name;
        protected int accuracy;
        protected int weight;
        protected int attackPower;
        protected int reloadSpeed;
        protected int clipCapacity;
        protected int fireRate;

        //Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Accuracy
        {
            get { return accuracy; }
            set { accuracy = value; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public int AttackPower
        {
            get { return attackPower; }
            set { attackPower = value; }
        }

        public int ReloadSpeed
        {
            get { return reloadSpeed; }
            set { reloadSpeed = value; }
        }

        public int ClipCapacity
        {
            get { return clipCapacity; }
            set { clipCapacity = value; }
        }

        public int FireRate
        {
            get { return fireRate; }
            set { fireRate = value; }
        }

        public string Type
        {
            get
            {
                if (type == null) return "Pistol";
                return type;
            }
            set { type = value; }
        }
    }
}
