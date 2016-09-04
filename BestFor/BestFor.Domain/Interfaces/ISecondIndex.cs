namespace BestFor.Domain.Interfaces
{
    public interface ISecondIndex
    {
        string SecondIndexKey { get; }

        int NumberOfEntries { get; set; }
    }
}
