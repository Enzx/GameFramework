namespace GameFramework.Graph
{
    public abstract class ActionTask
    {
        public string ErrorMessage { get; private set; }

        protected virtual void ReportError(string message, object context = null)
        {
            ErrorMessage = context != null ? $"{context}: {message}" : $"{message}";
        }
        
        public void Reset()
        {
            ErrorMessage = null;
        }
        
        public virtual void Update(float deltaTime)
        {
        }

        public abstract Result Execute();
    }

    public abstract class ActionTask<TAgent> : ActionTask
    {
        protected TAgent Agent;


        public void SetAgent(TAgent agent)
        {
            Agent = agent;
        }

        protected override void ReportError(string message, object context = null)
        {
            base.ReportError(message, Agent);
        }
    }
}