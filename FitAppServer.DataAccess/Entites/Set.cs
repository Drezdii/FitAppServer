namespace FitAppServer.DataAccess.Entites
{
    public class Set
    {
        public int ID { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }
        public bool Completed { get; set; }
        public Exercise Exercise { get; set; }

    }
}
