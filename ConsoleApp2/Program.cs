// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var x = GenericRange.Range.Create(1, 2);
var y = GenericRange.Range.Create(0, 1);
var z = x.Intersect(y);
var k = x.Overlaps(y);
var result = new List<GenericRange.Range<double>>();
var rangesLeft = new List<GenericRange.Range<double>>();
rangesLeft.Add(GenericRange.Range.Create(0d, 254d));

var rangesRight = new List<GenericRange.Range<double>>();
rangesRight.Add(GenericRange.Range.Create(0d, 250d));
rangesRight.Add(GenericRange.Range.Create(251d, 255d));
var dimension = 1;

var newRanges = new List<GenericRange.Range<double>>();
foreach (var rangeRight in rangesRight)
{
    if (rangesLeft.Count == 0)
    {
        var exist = newRanges.FirstOrDefault(x => x.Overlaps(rangeRight));
        if (exist is null)
            newRanges.Add(rangeRight);
        else
            exist = exist.Union(rangeRight);
    }
    else
    {
        foreach (var rangeLeft in rangesLeft)
        {
            if (rangeRight.Overlaps(rangeLeft))
            {
                var union = rangeRight.Union(rangeLeft);
                if (!newRanges.Contains(union))
                {
                    var exist = newRanges.FirstOrDefault(x => x.Overlaps(union));
                    if (exist is null)
                        newRanges.Add(union);
                    else
                        exist = exist.Union(union);
                }
            }
            else
            {
                var exist = newRanges.FirstOrDefault(x => x.Overlaps(rangeRight));
                if (exist is null)
                    newRanges.Add(rangeRight);
                else
                    exist = exist.Union(rangeRight);
            }

        }
    }
}


foreach (var rangeLeft in rangesLeft)
{
    var isOutOfRange = true;

    foreach (var rangeRight in rangesRight)
    {
        var dests = new List<GenericRange.Range<double>>();

        if (rangeLeft.Overlaps(rangeRight))
        {
            isOutOfRange = false;
            var and = rangeLeft.Intersect(rangeRight);

            // lower
            if (rangeLeft.LowerBound <= and.LowerBound - dimension)
                dests.Add(GenericRange.Range.Create(rangeLeft.LowerBound, and.LowerBound - dimension));

            // upper
            if (and.UpperBound + dimension <= rangeLeft.UpperBound)
                dests.Add(GenericRange.Range.Create(and.UpperBound + dimension, rangeLeft.UpperBound));
        }

        if (result.Count == 0)
            result.AddRange(dests);
        else
        {
            var srcs = new List<GenericRange.Range<double>>();
            srcs.AddRange(result);
            if (dests.Count != 0) result.Clear();

            foreach (var src in srcs)
            {
                foreach (var dest in dests)
                {
                    if (src.Overlaps(dest))
                    {
                        var inter = src.Intersect(dest);
                        if (!result.Contains(inter))
                            result.Add(inter);
                    }
                    else
                    {
                        if (!result.Contains(src))
                            result.Add(src);
                        if (!result.Contains(dest))
                            result.Add(dest);
                    }

                }
            }
        }
    }

    if (isOutOfRange)
        result.Add(rangeLeft);
}

var ranges = new List<GenericRange.Range<double>>();
var defaultRange = GenericRange.Range.Create(-20d, 20d);
ranges.Add(GenericRange.Range.Create(1d, 4d));
ranges.Add(GenericRange.Range.Create(8d, 10d));

result.Clear();
foreach (var range in ranges)
{
    var dests = new List<GenericRange.Range<double>>();

    if (defaultRange.Overlaps(range))
    {
        var and = defaultRange.Intersect(range);

        // lower
        if (defaultRange.LowerBound <= and.LowerBound - dimension)
            dests.Add(GenericRange.Range.Create(defaultRange.LowerBound, and.LowerBound - dimension));

        // upper
        if (and.UpperBound + dimension <= defaultRange.UpperBound)
            dests.Add(GenericRange.Range.Create(and.UpperBound + dimension, defaultRange.UpperBound));
    }

    if (result.Count == 0)
        result.AddRange(dests);
    else
    {
        var srcs = new List<GenericRange.Range<double>>();
        srcs.AddRange(result);
        if (dests.Count != 0) result.Clear();

        foreach (var src in srcs)
        {
            foreach (var dest in dests)
            {
                if (src.Overlaps(dest))
                {
                    var inter = src.Intersect(dest);
                    if (!result.Contains(inter))
                        result.Add(inter);
                }
                else
                {
                    if (!result.Contains(src))
                        result.Add(src);
                    if (!result.Contains(dest))
                        result.Add(dest);
                }

            }
        }
    }
}
Console.WriteLine("Finish");
