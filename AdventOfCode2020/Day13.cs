namespace AdventOfCode2020
{
    internal class Day13 : IDay
    {
        public string FirstStar(string[] inputLines)
        {
            var earliestDepartureTime = int.Parse(inputLines[0]);
            var busIds = inputLines[1].Split(',').Where(busId => busId != "x").Select(busId => int.Parse(busId));
            var departureTimeOffset = 0;
            while (true)
            {
                foreach (var busId in busIds)
                {
                    if ((earliestDepartureTime + departureTimeOffset) % busId == 0)
                    {
                        return (departureTimeOffset * busId).ToString();
                    }
                }
                departureTimeOffset++;
            }
        }

        public string SecondStar(string[] inputLines)
        {
            var busIdsAndOffsets = inputLines[1].Split(',')
                                    .Select((busId, i) => new { Id = busId, Offset = i })
                                    .Where(item => item.Id != "x")
                                    .Select(item => new { Id = long.Parse(item.Id), Offset = item.Offset })
                                    .ToList();

            var period = busIdsAndOffsets[0].Id;
            long departureTime = 0;
            for (int i = 1; i < busIdsAndOffsets.Count; i++)
            {
                var item = busIdsAndOffsets[i];
                // Find the time where the condition (offset between bus i-1 and bus i is the given offset.
                while (!((departureTime + item.Offset) % item.Id == 0))
                {
                    departureTime += period;
                }
                // The duration from the start (t0) to the first occurence of the fulfilled condition (t1 - t0)
                // is not the same as the one of the condition being fulfilled next time (tn - tn-1).
                var firstTimeOccurence = departureTime;

                if (i == busIdsAndOffsets.Count - 1)
                {
                    // No need to calculate the occurence of the fulfilled condition for the last item.
                    return firstTimeOccurence.ToString();
                }

                do
                {
                    departureTime += period;
                }
                while (!((departureTime + item.Offset) % item.Id == 0));
                // The condition has been fulfilled again, calculate the period.
                period = departureTime - firstTimeOccurence;
                // Next time, start at the time of the first occurence of the fulfilled condition.
                departureTime = firstTimeOccurence;
            }
            return departureTime.ToString();
        }
    }
}
