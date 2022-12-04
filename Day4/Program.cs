using System.Globalization;

using Common;

"input.txt".WriteSum((line, index) =>
{
    // Custom Deconstructor. Check Shared.Extensions.
    var (firstStart, firstEnd, secondStart, secondEnd) = line
        .Split(new[] { ',', '-' })
        .Select(i => int.Parse(i, CultureInfo.InvariantCulture));

    // 1,2,3,4,5,6,7
    //     3,4,5,6
    //     1 >= 3 && 7 <= 6 (first in range of second: false)
    // ||  3 >= 1 && 6 <= 7 (second in range of first: true)
    var first = (firstStart >= secondStart && firstEnd <= secondEnd)
        || (secondStart >= firstStart && secondEnd <= firstEnd);

    // 1,2,3,4
    //     3,4,5,6
    //    1 >= 3 && 1 <= 6 (first start in range of second: false)
    // || 4 >= 3 && 4 <= 6 (first end in range of second: true)
    var second = first
        || (firstStart >= secondStart && firstStart <= secondEnd)
        || (firstEnd >= secondStart && firstEnd <= secondEnd);

    return (first ? 1 : 0, second ? 1 : 0);
});
