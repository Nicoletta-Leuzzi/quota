using Quota.Core;
using Xunit;

namespace Quota.Core.Tests;

public class SplitterTests
{
    [Fact]
    public void SplitEqually_DistributesRemainderToFirstParticipants()
    {
        var parts = Splitter.SplitEqually(1000, 3);
        Assert.Equal(new long[] { 334, 333, 333 }, parts);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 3)]
    [InlineData(9999, 7)]
    [InlineData(123456, 11)]
    public void SplitEqually_PartsAlwaysSumToTotal(long total, int participants)
    {
        var parts = Splitter.SplitEqually(total, participants);
        Assert.Equal(participants, parts.Count);
        Assert.Equal(total, parts.Sum());
    }

    [Fact]
    public void SplitEqually_RejectsZeroParticipants()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Splitter.SplitEqually(100, 0));
    }

    [Fact]
    public void SplitEqually_RejectsNegativeTotal()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Splitter.SplitEqually(-1, 2));
    }
}
