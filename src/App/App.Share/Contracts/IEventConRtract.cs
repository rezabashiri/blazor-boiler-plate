namespace App.Share.Contracts;
public interface IEventContract
{
    public int Id { get; init; }

    public string Name { get; init; }

    public string AppKey { get; init; }
    DateTime DateStart { get; init; }
    DateTime DateEnd { get; init; }
}