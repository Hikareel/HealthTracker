namespace HealthTracker.Server.Core.Exceptions.PhysicalActivity
{
    /// <summary>
    /// Represents an error when a goal cannot be found.
    /// </summary>
    public class GoalNotFoundException : Exception
    {
        public GoalNotFoundException() 
            :base("Goal not found!") { }

        public GoalNotFoundException(string message) 
            : base($"Goal not found! {message}") { }
        public GoalNotFoundException(int id)
            : base($"Goal not found! Id: {id}") { }
    }
}
