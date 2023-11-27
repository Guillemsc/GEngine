using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GEngine.Utils.Extensions
{
    public static class StringPluralizationExtensions
    {
        public static readonly Regex FunctionRegex = new Regex(@"\$\[(?<functionName>\w+)\((?<params>.*?)\)\]");

        public static string Pluralize(string value, IReadOnlyList<object> numericParameters) => ParseAndRunFunction(value, numericParameters, HandlePluralFunction);

        public static string HandlePluralFunction(string functionName, float numeric, IReadOnlyList<string> parameters)
            => functionName switch
            {
                "plural_english" => HandlePluralDual(numeric, parameters),
                "plural_spanish" => HandlePluralDual(numeric, parameters),
                _ => string.Empty
            };

        public static string HandlePluralDual(float numeric, IReadOnlyList<string> parameters)
        {
            if (parameters.Count == 1)
            {
                if (numeric == 1f)
                {
                    return string.Empty;
                }

                return parameters[0];
            }

            if (parameters.Count == 2)
            {
                if (numeric == 1)
                {
                    return parameters[0];
                }

                return parameters[1];
            }

            throw new InvalidOperationException("Could not get the plural");
        }

        public static string ParseAndRunFunction(string input, IReadOnlyList<object> numericParameters, Func<string, float, IReadOnlyList<string>, string> function)
        {
            return FunctionRegex.Replace(input, match =>
            {
                var functionName = match.Groups["functionName"].Value;
                var paramString = match.Groups["params"].Value;

                var allParams = paramString.Split(',');
                // Split parameters by comma and trim each parameter to handle spaces
                var parameters = allParams.Skip(1).ToArray();
                for(int i = 0; i < parameters.Length; i++)
                {
                    parameters[i] = parameters[i];
                }

                int numericIndex = int.Parse(allParams[0]);
                float numericParameter = Convert.ToSingle(numericParameters[numericIndex]);
                return function(functionName, numericParameter, parameters);
            });
        }
    }
}
