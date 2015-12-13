using BestFor.Data;

namespace GeneralTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dataContext = new BestDataContext();
            dataContext.DebugCallOnConfiguring();
        }
    }
}
