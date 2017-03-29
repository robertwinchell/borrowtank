using System;

namespace ASOL.HireThings.Model
{
    public class CompoundPredicate : IPredicate
    {
        private IPredicate[] _predicateList;
        private LogicalOperator _logic;

        public static IPredicate GetCompoundPredicate(IPredicate fPredicate, IPredicate sPredicate, LogicalOperator op)
        {
            IPredicate pred = new CompoundPredicate(fPredicate, sPredicate, op);
            return pred;
        }

        public static IPredicate GetCompoundPredicate(LogicalOperator op, params IPredicate[] args)
        {
            IPredicate pred = new CompoundPredicate(op, args);
            return pred;
        }

        public string Serialize(bool escapeSQL = false)
        {
            string operat = PublicFunctions.GetDescription(_logic);
            string retVal = _predicateList[0].Serialize(escapeSQL);

            for (int i = 1; i < _predicateList.Length; i++)
            {
                retVal = string.Format("{0} {1} {2}", retVal, operat, _predicateList[i].Serialize(escapeSQL));
            }

            retVal = string.Format("( {0} )", retVal);
            return retVal;
        }

        private CompoundPredicate(IPredicate fPredicate, IPredicate sPredicate, LogicalOperator op)
        {
            _predicateList = new IPredicate[2];
            _predicateList[0] = sPredicate;
            _predicateList[1] = fPredicate;

            _logic = op;
        }

        private CompoundPredicate(LogicalOperator op, params IPredicate[] preds)
        {
            if (preds == null || preds.Length < 1)
                throw new Exception("No predicate provided for making compound predicate");

            _predicateList = preds;
            _logic = op;
        }

    }
}
