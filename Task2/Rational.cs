using System;
using System.Collections.Generic;
using System.Text;

namespace RationalNumberHandler
{

    public struct Rational : IComparable<Rational>, IEquatable<Rational>
    {
        private int denominator;

        public int Numerator { get; set; }
        public int Denominator
        {
            get
            {
                return denominator;
            }
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Denominator can't be zero");
                }
                denominator = value;
            }
        }

        public static Rational CreateInstance(int numerator, int _denominator)
        {
            if (_denominator == 0)
            {
                throw new ArgumentException("Denominator can't be zero");
            }
            else
            {
                return new Rational(numerator, _denominator);
            }
        }

        public Rational(int numerator, int _denominator) : this()
        {
            if (_denominator == 0)
            {
                throw new ArgumentException("Denominator can't be zero");
            }
             
            Numerator = numerator;
            Denominator = _denominator;
        }

        public override string ToString()
        {
            this.SimplifyRationalNumber();

            if (Denominator == 1)
            {
                return Numerator.ToString();
            }

            return $"{Numerator}r{Denominator}";
        }


        public bool Equals(Rational compareObject)
        {
            this.SimplifyRationalNumber();
            compareObject.SimplifyRationalNumber();

            if (Denominator == compareObject.Denominator &&
                Numerator == compareObject.Numerator)
            {
                return true;
            }
            return false;
        }

        /// <summary> Custom implementation of IComparable<T> interface
        /// obj1.CompareTo(obj2);
        /// 
        /// if obj1 > obj2 return -1  
        /// if obj1 = obj2 return  0 
        /// if obj1 < obj2 return  1 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(Rational obj)
        {

            if ((float)Numerator / Denominator > (float)obj.Numerator / obj.Denominator)
            {
                return -1;
            }
            if ((float)Numerator / Denominator == (float)obj.Numerator / obj.Denominator)
            {
                return 0;
            }

            return 1;
        }

        #region Operators

        public static Rational operator +(Rational firstObj, Rational secondObj)
        {
            int firstPartOfNumerator = firstObj.Numerator * (firstObj.CalculateLeastCommonMultiple(secondObj)
                                               / firstObj.Denominator);
            int secondOfNumerator = secondObj.Numerator * (firstObj.CalculateLeastCommonMultiple(secondObj)
                                               / secondObj.Denominator);

            int Denominator = firstObj.CalculateLeastCommonMultiple(secondObj);

            return new Rational(firstPartOfNumerator + secondOfNumerator, Denominator);
        }

        public static Rational operator -(Rational firstObj, Rational secondObj)
        {
            int firstPartOfNumerator = firstObj.Numerator * (firstObj.CalculateLeastCommonMultiple(secondObj)
                                             / firstObj.Denominator);
            int secondOfNumerator = secondObj.Numerator * (firstObj.CalculateLeastCommonMultiple(secondObj)
                                               / secondObj.Denominator);

            int Denominator = firstObj.CalculateLeastCommonMultiple(secondObj);

            return new Rational(firstPartOfNumerator - secondOfNumerator, Denominator);
        }

        public static Rational operator *(Rational firstObj, Rational secondObj)
        {
            return new Rational(firstObj.Numerator * secondObj.Numerator, firstObj.Denominator * secondObj.Denominator);
        }

        public static Rational operator /(Rational firstObj, Rational secondObj)
        {
            return firstObj * new Rational(secondObj.Denominator, secondObj.Numerator); // simply reversing and passing to multiplication operator
        }


        public static Rational operator !(Rational firstObj)
        {
            return new Rational(firstObj.Denominator, firstObj.Numerator);
        }

        #endregion
    }
}
