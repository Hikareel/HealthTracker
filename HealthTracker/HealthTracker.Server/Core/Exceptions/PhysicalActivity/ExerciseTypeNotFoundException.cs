namespace HealthTracker.Server.Core.Exceptions.PhysicalActivity
{
    /// <summary>
    /// Represents an error when a exercise type cannot be found.
    /// </summary>
    public class ExerciseTypeNotFoundException : Exception
    {
        public ExerciseTypeNotFoundException() 
            :base("Exercise Type not found!") { }
        public ExerciseTypeNotFoundException(string message) 
            : base($"Exercise Type not found! {message}") { }
        public ExerciseTypeNotFoundException(int id)
            : base($"Exercise Type not found! Id: {id}") { }
    }
}
