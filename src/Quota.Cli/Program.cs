using System.Globalization;
using Quota.Core;

// Usage: quota split <amount> <participants>
if (args.Length == 3 && args[0] == "split"
    && decimal.TryParse(args[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var amount)
    && int.TryParse(args[2], out var participants))
{
    long cents = (long)Math.Round(amount * 100m, MidpointRounding.AwayFromZero);
    var parts = Splitter.SplitEqually(cents, participants);

    for (int i = 0; i < parts.Count; i++)
        Console.WriteLine($"Person {i + 1}: {(parts[i] / 100m).ToString("0.00", CultureInfo.InvariantCulture)}");

    return 0;
}

Console.Error.WriteLine("Usage: quota split <amount> <participants>");
Console.Error.WriteLine("Example: quota split 100.00 3");
return 1;
