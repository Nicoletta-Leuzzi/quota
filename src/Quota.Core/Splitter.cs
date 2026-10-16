namespace Quota.Core;

/// <summary>
/// Splits amounts among participants. All amounts are integer cents,
/// so the parts always add up exactly to the total.
/// </summary>
public static class Splitter
{
    /// <summary>
    /// Splits <paramref name="totalCents"/> equally among <paramref name="participants"/>.
    /// Leftover cents go one each to the first participants.
    /// </summary>
    /// <example>SplitEqually(1000, 3) returns [334, 333, 333].</example>
    public static IReadOnlyList<long> SplitEqually(long totalCents, int participants)
    {
        if (participants <= 0)
            throw new ArgumentOutOfRangeException(nameof(participants), "At least one participant is required.");
        if (totalCents < 0)
            throw new ArgumentOutOfRangeException(nameof(totalCents), "Total must not be negative.");

        long baseShare = totalCents / participants;
        long remainder = totalCents % participants;

        var parts = new long[participants];
        for (int i = 0; i < participants; i++)
            parts[i] = baseShare + (i < remainder ? 1 : 0);

        return parts;
    }
}
