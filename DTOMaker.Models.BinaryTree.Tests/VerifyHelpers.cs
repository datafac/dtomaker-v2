using System;
using System.Text;

namespace DTOMaker.Models.BinaryTree.Tests;

public static class VerifyHelpers
{
    public static string ToDisplay(this ReadOnlyMemory<byte> buffer)
    {
        var result = new StringBuilder();
        int i = 0;
        foreach (byte b in buffer.Span)
        {
            if (i % 32 == 0)
            {
                if (i > 0) result.AppendLine();
            }
            else
            {
                result.Append('-');
            }
            result.Append(b.ToString("X2"));
            i++;
        }
        result.AppendLine();
        return result.ToString();
    }
}
