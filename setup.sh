#!/usr/bin/env bash
# Creates the .NET solution and projects around the existing source files,
# builds, runs the tests and makes the first git commit.
# Run once from the repository folder:  bash setup.sh
set -euo pipefail

command -v dotnet >/dev/null || { echo "Install the .NET 10 SDK first: https://dotnet.microsoft.com/download"; exit 1; }
command -v git >/dev/null || { echo "Install git first."; exit 1; }

FW=net10.0

dotnet new sln -n Quota --force
dotnet new classlib -n Quota.Core -o src/Quota.Core -f $FW --force
dotnet new console  -n Quota.Cli  -o src/Quota.Cli  -f $FW --force
dotnet new xunit    -n Quota.Core.Tests -o tests/Quota.Core.Tests -f $FW --force

# Remove template placeholders (our own files are kept)
rm -f src/Quota.Core/Class1.cs tests/Quota.Core.Tests/UnitTest1.cs

# Templates overwrite Program.cs: restore ours from this script's copy
cat > src/Quota.Cli/Program.cs <<'EOF'
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
EOF

dotnet sln Quota.sln add src/Quota.Core src/Quota.Cli tests/Quota.Core.Tests
dotnet add src/Quota.Cli reference src/Quota.Core
dotnet add tests/Quota.Core.Tests reference src/Quota.Core

dotnet build
dotnet test
dotnet run --project src/Quota.Cli -- split 100.00 3

if [ ! -d .git ]; then
  git init -b main
fi
git add .
git commit -m "Initial project structure: core library, CLI and tests"

echo ""
echo "Done. Now create an empty repo named 'quota' on GitHub and push (see the instructions)."
