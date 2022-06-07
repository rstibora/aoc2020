using System.Collections;

namespace AdventOfCode2020;

internal class Day05 : IDay
{
    private int boardingPassId(string boardingPassString)
    {
        var columnBitArray = new BitArray(boardingPassString[7..].Select(character => character == 'R').Reverse().ToArray());
        var rowBitArray = new BitArray(boardingPassString[..7].Select(character => character == 'B').Reverse().ToArray());

        int[] array = new int[1];
        columnBitArray.CopyTo(array, 0);
        var column = array[0];

        rowBitArray.CopyTo(array, 0);
        var row = array[0];
        return row * 8 + column;
    }
    public string FirstStar(string[] inputLines)
    {
        return inputLines.Select(boardingPassId).Max().ToString();
    }

    public string SecondStar(string[] inputLines)
    {
        var allSeatIds = inputLines.Select(boardingPassId).ToHashSet();
        for (var seatId = 0; seatId < allSeatIds.Max(); seatId++)
            if (!allSeatIds.Contains(seatId) && allSeatIds.Contains(seatId - 1) && allSeatIds.Contains(seatId + 1))
                return seatId.ToString();
        throw new Exception("No such seat.");
    }
}
