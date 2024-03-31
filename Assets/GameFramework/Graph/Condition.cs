using System;
using System.Collections.Generic;

namespace GameFramework.Graph
{
    public abstract  class Condition : Node
    {
        public ExecuteMode ExecuteMode;
        private readonly List<ConditionTask> _conditions;
        private Result _result;
        
        protected Condition(ConditionData data = default) : base(data)
        {
            if (ReferenceEquals(data, null))
            {
                _conditions = new List<ConditionTask>();
                ExecuteMode = ExecuteMode.Sequence;
                
            }
            else
            {
                _conditions = data.Conditions;
                ExecuteMode = data.ExecuteMode;
            }
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
            
            success = Check() && success;

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
        
        protected virtual bool Check() { return true; }
    }
    
    public abstract class Condition<T> : Condition
    {
        protected T Agent;
        
        public void SetAgent(T agent)
        {
            Agent = agent;
        }

      
    }
}