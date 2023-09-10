namespace Superkatten.Katministratie.Domain;

public class Superkat
{
    public int Number { get; }
    public DateTime Entered { get; }

    public Superkat(int number, DateTime entered)
    {
        if (number <= 0 || number > 500)
        {
            throw new ArgumentOutOfRangeException(nameof(number), number, "Nummer moet groter dan 1 en kleiner dan 500 zijn.");
        }

        Number = number;
        Entered = entered;
    }
}