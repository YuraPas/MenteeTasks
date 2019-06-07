using System;
using System.Collections.Generic;
using System.Text;

namespace RationalNumberHandler
{

    public struct Rational : IComparable<Rational>, IEquatable<Rational>
    {

        public int numerator { get; set; }
        public int denominator { get; set; }

        public static Rational CreateInstance(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Denominator can't be zero");
            else
            {
                return new Rational(numerator, denominator);
            }
        }

        public Rational(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Denominator can't be zero");


            this.numerator = numerator;
            this.denominator = denominator;
        }

        public override string ToString()
        {
            this.SimplifyRationalNumber();

            if (denominator == 1)
                return numerator.ToString();
            return $"{numerator}r{denominator}" ;
        }


        public bool Equals(Rational other)
        {
            this.SimplifyRationalNumber();
            other.SimplifyRationalNumber();

            if (this.denominator == other.denominator &&
                this.numerator == other.numerator)
                return true;

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
            
            if ((float)this.numerator / this.denominator > (float)obj.numerator / obj.denominator)
                return -1;
            if ((float) this.numerator / this.denominator == (float)obj.numerator / obj.denominator)
                return 0;

            return 1;
        }

        #region Operators

        public static Rational operator +(Rational firstObj, Rational secondObj)
        {
            int firstPartOfNumerator = firstObj.numerator * (firstObj.CalculateLeastCommonMultiple(secondObj)
                                               / firstObj.denominator);
            int secondOfNumerator = secondObj.numerator * (firstObj.CalculateLeastCommonMultiple(secondObj)
                                               / secondObj.denominator);

            int denominator = firstObj.CalculateLeastCommonMultiple(secondObj);

            return new Rational(firstPartOfNumerator + secondOfNumerator, denominator);
        }

        public static Rational operator -(Rational firstObj, Rational secondObj)
        {
            int firstPartOfNumerator = firstObj.numerator * (firstObj.CalculateLeastCommonMultiple(secondObj)
                                             / firstObj.denominator);
            int secondOfNumerator = secondObj.numerator * (firstObj.CalculateLeastCommonMultiple(secondObj)
                                               / secondObj.denominator);

            int denominator = firstObj.CalculateLeastCommonMultiple(secondObj);

            return new Rational(firstPartOfNumerator - secondOfNumerator, denominator);
        }

        public static Rational operator *(Rational firstObj, Rational secondObj)
        {
            return new Rational(firstObj.numerator * secondObj.numerator, firstObj.denominator * secondObj.denominator);
        }

        //public static Rational operator /(Rational firstObj, Rational secondObj)
        //{

        //}

        ////що саме заперечувати? до чого задіювати цей оператор ?
        //public static Rational operator !(Rational firstObj)
        //{

        //}

        #endregion
    }
}
