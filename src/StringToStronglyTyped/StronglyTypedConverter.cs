// Copyright (c) 2014 Conrad Yacat
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StringToStronglyTyped
{
    public static class StronglyTypedConverter
    {
        private const char LIST_DELIMITER = '|';

        public static object ToType(string value, Type toType)
        {
            if (toType == typeof(string))
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Value cannot be blank/empty");

                return value;
            }
            else if (toType == typeof(int))
            {
                if (!int.TryParse(value, out int i))
                    throw new ArgumentException("Value must be numeric/number (Int32 type)");
                return i;
            }
            else if (toType == typeof(uint))
            {
                if (!uint.TryParse(value, out uint i))
                    throw new ArgumentException("Value must be non-negative numeric/number (UInt32 type)");
                return i;
            }
            else if (toType == typeof(long))
            {
                if (!long.TryParse(value, out long i))
                    throw new ArgumentException("Value must be numeric/number (Int64 type)");
                return i;
            }
            else if (toType == typeof(ulong))
            {
                if (!ulong.TryParse(value, out ulong i))
                    throw new ArgumentException("Value must be non-negative numeric/number (UInt64 type)");
                return i;
            }
            else if (toType == typeof(short))
            {
                if (!short.TryParse(value, out short i))
                    throw new ArgumentException("Value must be numeric/number (Int16 type)");
                return i;
            }
            else if (toType == typeof(ushort))
            {
                if (!ushort.TryParse(value, out ushort i))
                    throw new ArgumentException("Value must be non-negative numeric/number (UInt16 type)");
                return i;
            }
            else if (toType == typeof(decimal))
            {
                if (!decimal.TryParse(value, out decimal i))
                    throw new ArgumentException("Value must be numeric/number (Decimal type)");
                return i;
            }
            else if (toType == typeof(double))
            {
                if (!double.TryParse(value, out double i))
                    throw new ArgumentException("Value must be numeric/number (Double type)");
                return i;
            }
            else if (toType == typeof(float))
            {
                if (!float.TryParse(value, out float i))
                    throw new ArgumentException("Value must be numeric/number (Single type)");
                return i;
            }
            else if (toType == typeof(bool))
            {
                if (!new[] { "true", "1", "false", "0" }.Any(x => StringInvariantCultureIgnoreCaseEquals(x, value)))
                    throw new ArgumentException("Value must be a Boolean type. Case-insensitive allowed values: ([True = true, 1], [False = false, 0])");

                return StringInvariantCultureIgnoreCaseEquals(value, "true") || StringInvariantCultureIgnoreCaseEquals(value, "1");
            }
            else if (toType == typeof(int?))
            {
                return int.TryParse(value, out int i) ? i : (int?)null;
            }
            else if (toType == typeof(uint?))
            {
                return uint.TryParse(value, out uint i) ? i : (uint?)null;
            }
            else if (toType == typeof(long?))
            {
                return long.TryParse(value, out long i) ? i : (long?)null;
            }
            else if (toType == typeof(ulong?))
            {
                return ulong.TryParse(value, out ulong i) ? i : (ulong?)null;
            }
            else if (toType == typeof(short?))
            {
                return short.TryParse(value, out short i) ? i : (short?)null;
            }
            else if (toType == typeof(ushort?))
            {
                return ushort.TryParse(value, out ushort i) ? i : (ushort?)null;
            }
            else if (toType == typeof(decimal?))
            {
                return decimal.TryParse(value, out decimal i) ? i : (decimal?)null;
            }
            else if (toType == typeof(double?))
            {
                return double.TryParse(value, out double i) ? i : (double?)null;
            }
            else if (toType == typeof(float?))
            {
                return float.TryParse(value, out float i) ? i : (float?)null;
            }
            else if (toType == typeof(bool?))
            {
                if (string.IsNullOrEmpty(value))
                    return null;

                if (!new[] { "true", "1", "false", "0" }.Any(x => StringInvariantCultureIgnoreCaseEquals(x, value)))
                    throw new ArgumentException("Value must be a Boolean type. Case-insensitive allowed values: ([True = true, 1], [False = false, 0])");

                return StringInvariantCultureIgnoreCaseEquals(value, "true") || StringInvariantCultureIgnoreCaseEquals(value, "1");
            }
            else if (toType.IsGenericType &&
                (toType.GetGenericTypeDefinition() == typeof(List<>) ||
                toType.GetGenericTypeDefinition() == typeof(IList<>) ||
                toType.GetGenericTypeDefinition() == typeof(IReadOnlyList<>) ||
                toType.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                toType.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>) ||
                toType.GetGenericTypeDefinition() == typeof(ICollection<>)))
            {
                var parts = Split(value, LIST_DELIMITER);
                Type genType = toType.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(new Type[] { genType });
                var list = Activator.CreateInstance(listType) as IList;

                foreach (var p in parts.Where(x => x.Trim().Length > 0))
                {
                    list.Add(ToType(p, genType));
                }
                return list;
            }
            else if (toType.IsGenericType &&
                (toType.GetGenericTypeDefinition() == typeof(Dictionary<,>) ||
                toType.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
            {
                var parts = Split(value, LIST_DELIMITER);
                Type genKeyType = toType.GetGenericArguments()[0];
                Type genValueType = toType.GetGenericArguments()[1];
                var dictionaryType = typeof(Dictionary<,>).MakeGenericType(new Type[] { genKeyType, genValueType });
                var dictionary = Activator.CreateInstance(dictionaryType) as IDictionary;

                foreach (var p in parts.Where(x => x.Trim().Length > 0))
                {
                    var kvp = p.Split(new[] { '=' });
                    if (string.IsNullOrWhiteSpace(kvp[0]))
                        throw new ArgumentException("Key-value pair must be delimited by = sign (Dictionary type)");

                    dictionary.Add(ToType(kvp[0], genKeyType), ToType(kvp[1], genValueType));
                }
                return dictionary;
            }
            else if (toType.IsArray)
            {
                var parts = Split(value, LIST_DELIMITER).Where(x => x.Trim().Length > 0).ToList();
                var array = Array.CreateInstance(toType.GetElementType(), parts.Count);

                for (var i = 0; i < parts.Count; i++)
                {
                    if (parts[i].Trim().Length > 0)
                        array.SetValue(ToType(parts[i], toType.GetElementType()), i);
                }
                return array;
            }
            else if (typeof(ArrayList).IsAssignableFrom(toType))
            {
                var parts = Split(value, LIST_DELIMITER);
                var arrayList = new ArrayList();

                foreach (var p in parts.Where(x => x.Trim().Length > 0))
                {
                    arrayList.Add(p);
                }
                return arrayList;
            }
            else if (toType.IsEnum)
            {
                // perform case-insensitive search
                var names = Enum.GetNames(toType);
                foreach (var name in names)
                {
                    if (StringInvariantCultureIgnoreCaseEquals(name, value))
                        return Enum.Parse(toType, name);
                }

                throw new ArgumentException($"{value} is not defined in {toType} enumeration. Valid values: {string.Join(",", names)}");
            }

            return value;
        }

        public static T ToType<T>(string value)
        {
            return (T)ToType(value, typeof(T));
        }

        public static T ToType<T>(IDictionary dictionary) where T : class, new()
        {
            var type = typeof(T);
            var t = new T();
            var properties = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            var errors = new List<string>();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<StronglyTypedMetadataAttribute>(true);

                if (attribute == null)
                    continue;

                string key = property.Name;

                try
                {
                    if (dictionary.Contains(key))
                    {
                        // set the config entry value
                        property.SetValue(t, ToType(dictionary[key].ToString(), property.PropertyType), null);
                    }
                    else
                    {
                        // throw an exception required but no default value
                        if (attribute.IsRequired && attribute.DefaultValue == null)
                            throw new ArgumentNullException(key + " is not found in the configuration (Expected type: " + property.PropertyType + ")");

                        // skip if not required and no default value and no custom load method specified
                        if (attribute.DefaultValue == null && string.IsNullOrEmpty(attribute.CustomLoadMethod))
                        {
                            continue;
                        }
                        else if (attribute.DefaultValue != null)
                        {
                            // set the default value
                            property.SetValue(t, ToType(attribute.DefaultValue.ToString(), property.PropertyType), null);
                        }
                    }

                    // invoke custom loading method
                    if (!string.IsNullOrEmpty(attribute.CustomLoadMethod))
                    {
                        try
                        {
                            t.GetType().InvokeMember(attribute.CustomLoadMethod, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, t, null);
                        }
                        catch (TargetInvocationException ex)
                        {
                            var msg = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                            throw new TargetInvocationException($"Error on CustomLoadMethod: {attribute.CustomLoadMethod} ({msg})", ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.Add(property.Name + " - " + ex.Message);
                }
            }

            if (errors.Count > 0)
                throw new Exception(string.Join(Environment.NewLine, errors.ToArray()));

            return t;
        }

        private static bool StringInvariantCultureIgnoreCaseEquals(string text1, string text2)
        {
            return string.Compare(text1, text2, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        private static string[] Split(string value, char delimiter)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be blank/empty", nameof(value));

            var parts = value.Split(delimiter);
            if (parts.Length == 0)
                throw new ArgumentException($"Unable to split '{value}' using '{delimiter}'");

            return parts;
        }
    }
}