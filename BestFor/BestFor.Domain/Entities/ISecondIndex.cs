namespace BestFor.Domain.Entities
{
    public interface ISecondIndex
    {
        string SecondIndexKey { get; }

        int NumberOfEntries { get; set; }
    }
}
