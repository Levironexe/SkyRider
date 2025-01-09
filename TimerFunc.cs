using System.Security.Cryptography;
using System;

    public class TimeFunc
    {

        private int _count;
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }        
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
            _count = value;
            }
        }

        public TimeFunc(string name, int count)
        {
            _name = name;
            _count = count;
        }

        public void Decrement()
        {
            _count--;

        }

        public void Reset()
        {
            _count = 0;
        }

        public int Ticks
        {
            get { return _count; }
        }



    }
