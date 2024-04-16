namespace HealthTracker.Server.Core.Exceptions.PhysicalActivity
{
    /// <summary>
    /// Represents an error when a goaltype cannot be found.
    /// </summary>
    public class GoalTypeNotFoundException : Exception
    {
        public GoalTypeNotFoundException() 
            :base("GoalType not found!") { }

        public GoalTypeNotFoundException(string message) 
            : base($"GoalType not found! {message}") { }
        public GoalTypeNotFoundException(int id)
            : base($"GoalType not found! Id: {id}") { }
    }
}
