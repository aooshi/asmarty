using System.Collections.Generic;
using System.Text;
using Sharpy.ViewEngine;

namespace Sharpy.Extensions
{
    internal static class StringExtensions
    {
        public static IList<ExpressionPart> ParseExpression(this string input)
        {
            var result = new List<ExpressionPart>();

            var current = new StringBuilder();
            bool escaped = false, doubleEscaped = false;
            var currentPosition = 0;
            char? currentQuote = null;
            while (currentPosition < input.Length)
            {
                if (currentQuote != null && input[currentPosition] == '\\')
                {
                    if (escaped)
                    {
                        if (doubleEscaped)
                        {
                            current.Append(input[currentPosition]);
                        }
                        doubleEscaped = true;
                    }
                    escaped = true;
                    currentPosition++;
                    continue;
                }

                if (input[currentPosition] == '\'' || input[currentPosition] == '"')
                {
                    if (escaped)
                    {
                        if (doubleEscaped)
                        {
                            // We should actually interpret one backslash and the quote
                            // If the quote is a " we should escape it again
                            current.Append('\\');
                            if (input[currentPosition] == '\'')
                            {
                                current.Append('"');
                            }
                            else
                            {
                                current.Append("\\\"");
                            }
                            currentQuote = null;
                        }
                        else
                        {
                            // This is an escaped quote - interpret the quote but not the backslash
                            // If the quote is a " we should escape it again
                            if (input[currentPosition] == '"')
                            {
                                current.Append("\\\"");
                            }
                            else
                            {
                                current.Append(input[currentPosition]);
                            }
                        }
                    }
                    else
                    {
                        if (currentQuote == input[currentPosition])
                        {
                            currentQuote = null;
                            current.Append('"');

                            result.Add(new ExpressionPart(current.ToString(), PartType.String));
                            current = new StringBuilder();
                        }
                        else
                        {
                            if (currentQuote == null)
                            {
                                currentQuote = input[currentPosition];
                                if (current.Length > 0)
                                {
                                    result.Add(new ExpressionPart(current.ToString(), PartType.Expression));
                                    current = new StringBuilder();
                                }

                                current.Append('"');
                            }
                            else
                            {
                                if (input[currentPosition] == '"')
                                {
                                    current.Append("\\\"");
                                }
                                else
                                {
                                    current.Append(input[currentPosition]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (escaped)
                    {
                        if (doubleEscaped)
                        {
                            // Just interpret two backslashes
                            current.Append(@"\\");
                        }
                        else
                        {
                            // Just interpret a single backslash
                            current.Append(@"\");
                        }
                    }

                    current.Append(input[currentPosition]);                        
                }

                escaped = false;
                doubleEscaped = false;
                currentPosition++;
            }

            if (current.Length > 0)
            {
                result.Add(new ExpressionPart(current.ToString(), PartType.Expression));
            }

            return result;
        }
    }
}
