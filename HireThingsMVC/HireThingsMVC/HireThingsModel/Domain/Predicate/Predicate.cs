using System;
using System.ComponentModel;

namespace ASOL.HireThings.Model
{
    public enum PredSpecialType : byte
    {
        None = 0,
        CustomFormat = 1
    }

    public class Predicate<T> : IPredicate, IPredicateBase<T>
    {
        private string _name;
        private T _value;
       
        private Operator _operator;
        private PredSpecialType _specialTp;

        protected Predicate(string name, T value, Operator op, PredSpecialType specialOp = PredSpecialType.None)
        {
            Type tp = value.GetType();
            if (tp != typeof(int) && tp != typeof(long) && tp != typeof(byte) && tp != typeof(short) && tp != typeof(string) && tp != typeof(DateTime) && tp != typeof(float) && tp != typeof(decimal))
            {
                throw new Exception(string.Format("{0} Type is not supported to create a predicate", tp));
            }

            _name = name;
            _value = value;
            _operator = op;
            _specialTp = specialOp;
        }
        protected Predicate(string name, T value, T value2, Operator op, PredSpecialType specialOp = PredSpecialType.None)
        {
            Type tp = value.GetType();
           
            if (tp != typeof(int) && tp != typeof(long) && tp != typeof(byte) && tp != typeof(short) && tp != typeof(string) && tp != typeof(DateTime) && tp != typeof(float) && tp != typeof(decimal))
            {
                throw new Exception(string.Format("{0} Type is not supported to create a predicate", tp));
            }
            

            _name = name;
            _value = value;
            
            _operator = op;
            _specialTp = specialOp;
        }
        public string Name { get { return _name; } }
        public T Value { get { return _value; } }
        public Operator Operator { get { return _operator; } }
        public PredSpecialType SpecialType { get { return _specialTp; } }

        public static IPredicate GetPredicate(string name, T value, Operator op, PredSpecialType type = PredSpecialType.None)
        {
            IPredicate pred = new Predicate<T>(name, value, op, type);
            return pred;
        }
        
        public virtual string Serialize(bool escapeSQL = false)
        {
            string val = "";
            string retVal = "";
            string escChar = "''";

            if (escapeSQL == false)
            {
                escChar = "'";
            }

            Type tp = _value.GetType();

            if (tp == typeof(string) && (!(_operator == Model.Operator.LeftLike || _operator == Model.Operator.Like || _operator == Model.Operator.RightLike)))
            {
                val = _value.ToString(); 
                if (_specialTp == PredSpecialType.None)
                {
                    val = val.Replace("'", "''");
                    val = string.Format("{0}{1}{2}", escChar, val.Trim(), escChar);
                }
            }
            else if (tp == typeof(DateTime))
            {
                DateTime dt = Convert.ToDateTime(_value);

                if (!(_operator == Model.Operator.LeftLike || _operator == Model.Operator.Like || _operator == Model.Operator.RightLike))
                    val = string.Format("{0}{1}{2}", escChar, dt.ToString("MM/dd/yyyy h:mm tt"), escChar);
                else
                    val = string.Format("{0}", dt.ToString("MM/dd/yyyy h:mm tt"));
            }
            else
            {
                val = _value.ToString().Replace("'", "''");
            }


            switch (this._operator)
            {
                case Operator.Like:
                    retVal = string.Format("{0} {1} {2}%{3}%{4}", Name, PublicFunctions.GetDescription(_operator), escChar, val, escChar);
                    break;
                case Operator.LeftLike:
                    retVal = string.Format("{0} {1} {2}%{3}{4}", Name, PublicFunctions.GetDescription(_operator), escChar, val, escChar);
                    break;
                case Operator.RightLike:
                    retVal = string.Format("{0} {1} {2}{3}%{4}", Name, PublicFunctions.GetDescription(_operator), escChar, val, escChar);
                    break;
                case Operator.IsNull:
                    retVal = string.Format("{0} {1}", Name, PublicFunctions.GetDescription(_operator));
                    break;
                case Operator.NotIsNull:
                    retVal = string.Format("{0} {1}", Name, PublicFunctions.GetDescription(_operator), val);
                    break;
                case Operator.Equal:
                case Operator.Greater:
                case Operator.GreaterEqual:
                case Operator.Lesser:
                case Operator.LesserEqual:
                case Operator.NotEqual:
                    retVal = string.Format("{0} {1} {2}", Name, PublicFunctions.GetDescription(_operator), val);
                    break;

                default:
                    throw new Exception("Operator not defined");
            }


            return retVal;
        }
    }

    public enum LogicalOperator
    {
        [Description(" AND ")]
        AND,

        [Description(" OR ")]
        OR,
    }

    public enum Operator
    {
        [Description("=")]
        Equal,

        [Description(">")]
        Greater,

        [Description("<")]
        Lesser,

        [Description(">=")]
        GreaterEqual,

        [Description("<=")]
        LesserEqual,

        [Description("!=")]
        NotEqual,

        [Description("NOT IS NULL")]
        NotIsNull,

        [Description("IS NULL")]
        IsNull,

        [Description("LIKE")]
        Like,

        [Description("LIKE")]
        LeftLike,

        [Description("LIKE")]
        RightLike,

        [Description("BETWEEN")]
        Between
    }
}
