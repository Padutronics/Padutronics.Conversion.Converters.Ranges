using Padutronics.Formatting.Ranges;
using Padutronics.Ranges;
using System;
using System.Text;

namespace Padutronics.Conversion.Converters.Ranges;

public sealed class RangeToStringConverter<T> : IConverter<Range<T>, string>
    where T : IComparable<T>
{
    private readonly IConverter<T, string> boundConverter;
    private readonly RangeFormatOptions formatOptions;

    public RangeToStringConverter(RangeFormatOptions formatOptions, IConverter<T, string> boundConverter)
    {
        this.boundConverter = boundConverter;
        this.formatOptions = formatOptions;
    }

    public string Convert(Range<T> value)
    {
        string lowerBound = boundConverter.Convert(value.LowerBound);
        string upperBound = boundConverter.Convert(value.UpperBound);

        var stringBuilder = new StringBuilder();
        stringBuilder.Append(value.IsLowerBoundIncluded ? formatOptions.OpeningInclusiveCap : formatOptions.OpeningExclusiveCap);
        if (formatOptions.InsertSpaceWithinCaps)
        {
            stringBuilder.Append(' ');
        }
        stringBuilder.Append(lowerBound);
        if (formatOptions.InsertSpaceBeforeDelimiter)
        {
            stringBuilder.Append(' ');
        }
        stringBuilder.Append(formatOptions.Delimiter);
        if (formatOptions.InsertSpaceAfterDelimiter)
        {
            stringBuilder.Append(' ');
        }
        stringBuilder.Append(upperBound);
        if (formatOptions.InsertSpaceWithinCaps)
        {
            stringBuilder.Append(' ');
        }
        stringBuilder.Append(value.IsUpperBoundIncluded ? formatOptions.ClosingInclusiveCap : formatOptions.ClosingExclusiveCap);

        return stringBuilder.ToString();
    }
}