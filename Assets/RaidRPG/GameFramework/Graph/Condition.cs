using System;
using System.Collections.Generic;

namespace RaidRPG.GameFramework.Graph
{
    public  class Condition : Node
    {
        public ExecuteMode ExecuteMode;
        private List<ConditionTask> _conditions;
        private Result _result;

        public Condition()
        {
            _conditions = new List<ConditionTask>();
        }

        public override Result Execute()
        {
            _result = Result.None;
            bool success = ExecuteMode switch
            {
                ExecuteMode.Parallel => Parallel(),
                ExecuteMode.Sequence => Sequence(),
                _ => throw new ArgumentOutOfRangeException()
            };

            Finish(success);
            return _result;
        }
        

        private void Finish(bool success)
        {
            _result = success ? Result.Success : Result.Failure;
        }

        private bool Parallel()
        {
            bool success = true;
            for (int index = 0; index < _conditions.Count; index++)
            {
                ConditionTask condition = _conditions[index];
                if (!condition.Check())
                {
                    success = false;
                }
            }

            return success;
        }

        private bool Sequence()
        {
            bool success = true;
            for (int index = 0; index < _conditions.Count; index++)
            {
                ConditionTask condition = _conditions[index];
                if (!condition.Check())
                {
                    success = false;
                    break;
                }
            }

            return success;
        }
    }
}