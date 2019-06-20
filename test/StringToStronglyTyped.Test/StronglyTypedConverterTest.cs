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
using System.Linq;
using StringToStronglyTyped.Test.Helpers;
using Xunit;

namespace StringToStronglyTyped.Test
{
    public class StronglyTypedConverterTest
    {
        [Fact]
        public void Should_Convert_String_ToType()
        {
            string @string = StronglyTypedConverter.ToType<string>("some string");
            Assert.Equal("some string", @string);

            int @int = StronglyTypedConverter.ToType<int>("123");
            Assert.Equal(123, @int);

            decimal @decimal = StronglyTypedConverter.ToType<decimal>("123.456");
            Assert.Equal(123.456m, @decimal);
        }

        [Fact]
        public void Should_Convert_Hashtable_ToType()
        {
            var hashtable = TestDataHelper.GetTestDataValidHashtable();
            var testModel = StronglyTypedConverter.ToType<TestModel>(hashtable);

            Assert.Equal((string)hashtable["String"], testModel.String);
            Assert.Equal(Convert.ToInt16(hashtable["Int16"]), testModel.Int16);
            Assert.Equal(Convert.ToInt32(hashtable["Int32"]), testModel.Int32);
            Assert.Equal(Convert.ToInt64(hashtable["Int64"]), testModel.Int64);
            Assert.Equal(Convert.ToDecimal(hashtable["Decimal"]), testModel.Decimal);
            Assert.Equal(Convert.ToDouble(hashtable["Double"]), testModel.Double);
            Assert.Equal(Convert.ToSingle(hashtable["Single"]), testModel.Single);
            Assert.Equal(Convert.ToBoolean(hashtable["Boolean"]), testModel.Boolean);

            Assert.Equal(Convert.ToInt16(hashtable["Int16Nullable"]), testModel.Int16Nullable);
            Assert.Equal(Convert.ToInt32(hashtable["Int32Nullable"]), testModel.Int32Nullable);
            Assert.Equal(Convert.ToInt64(hashtable["Int64Nullable"]), testModel.Int64Nullable);
            Assert.Equal(Convert.ToDecimal(hashtable["DecimalNullable"]), testModel.DecimalNullable);
            Assert.Equal(Convert.ToDouble(hashtable["DoubleNullable"]), testModel.DoubleNullable);
            Assert.Equal(Convert.ToSingle(hashtable["SingleNullable"]), testModel.SingleNullable);
            Assert.Equal(Convert.ToBoolean(hashtable["BooleanNullable"]), testModel.BooleanNullable);

            Assert.False(testModel.Int16Nullable2.HasValue);
            Assert.False(testModel.Int32Nullable2.HasValue);
            Assert.False(testModel.Int64Nullable2.HasValue);
            Assert.False(testModel.DecimalNullable2.HasValue);
            Assert.False(testModel.DoubleNullable2.HasValue);
            Assert.False(testModel.SingleNullable2.HasValue);
            Assert.False(testModel.BooleanNullable2.HasValue);

            Assert.Equal(3, testModel.StringList.Count);
            Assert.Equal("a", testModel.StringList[0]);
            Assert.Equal("b", testModel.StringList[1]);
            Assert.Equal("c", testModel.StringList[2]);

            Assert.Equal(3, testModel.ArrayList.Count);
            Assert.Equal("z", testModel.ArrayList[0]);
            Assert.Equal("y", testModel.ArrayList[1]);
            Assert.Equal("x", testModel.ArrayList[2]);

            Assert.Equal(Direction.Outbound, testModel.Enumeration);
            Assert.Equal("127.0.0.1", testModel.Ip);
            Assert.Equal("80", testModel.Port);

            Assert.Equal(Convert.ToUInt16(hashtable["UInt16"]), testModel.UInt16);
            Assert.Equal(Convert.ToUInt32(hashtable["UInt32"]), testModel.UInt32);
            Assert.Equal(Convert.ToUInt64(hashtable["UInt64"]), testModel.UInt64);

            Assert.Equal(Convert.ToUInt16(hashtable["UInt16Nullable"]), testModel.UInt16Nullable);
            Assert.Equal(Convert.ToUInt32(hashtable["UInt32Nullable"]), testModel.UInt32Nullable);
            Assert.Equal(Convert.ToUInt64(hashtable["UInt64Nullable"]), testModel.UInt64Nullable);

            Assert.False(testModel.UInt16Nullable2.HasValue);
            Assert.False(testModel.UInt32Nullable2.HasValue);
            Assert.False(testModel.UInt64Nullable2.HasValue);

            var enumerableStringList = testModel.EnumerableStringList.ToList();
            string qwerty = "qwerty";

            Assert.Equal(qwerty.Length, enumerableStringList.Count);
            for (int i = 0; i < qwerty.Length; i++)
            {
                Assert.Equal(qwerty[i].ToString(), enumerableStringList[i]);
            }

            Assert.Equal(2, testModel.Dictionary.Count);
            Assert.Equal("1", testModel.Dictionary["id"]);
            Assert.Equal("Chupacabra", testModel.Dictionary["name"]);
        }

        [Fact]
        public void Should_Convert_GenericDictionary_ToType()
        {
            var dictionary = TestDataHelper.GetTestDataValidGenericDictionary();
            var testModel = StronglyTypedConverter.ToType<TestModel>(dictionary);

            Assert.Equal((string)dictionary["String"], testModel.String);
            Assert.Equal(Convert.ToInt16(dictionary["Int16"]), testModel.Int16);
            Assert.Equal(Convert.ToInt32(dictionary["Int32"]), testModel.Int32);
            Assert.Equal(Convert.ToInt64(dictionary["Int64"]), testModel.Int64);
            Assert.Equal(Convert.ToDecimal(dictionary["Decimal"]), testModel.Decimal);
            Assert.Equal(Convert.ToDouble(dictionary["Double"]), testModel.Double);
            Assert.Equal(Convert.ToSingle(dictionary["Single"]), testModel.Single);
            Assert.Equal(Convert.ToBoolean(dictionary["Boolean"]), testModel.Boolean);

            Assert.Equal(Convert.ToInt16(dictionary["Int16Nullable"]), testModel.Int16Nullable);
            Assert.Equal(Convert.ToInt32(dictionary["Int32Nullable"]), testModel.Int32Nullable);
            Assert.Equal(Convert.ToInt64(dictionary["Int64Nullable"]), testModel.Int64Nullable);
            Assert.Equal(Convert.ToDecimal(dictionary["DecimalNullable"]), testModel.DecimalNullable);
            Assert.Equal(Convert.ToDouble(dictionary["DoubleNullable"]), testModel.DoubleNullable);
            Assert.Equal(Convert.ToSingle(dictionary["SingleNullable"]), testModel.SingleNullable);
            Assert.Equal(Convert.ToBoolean(dictionary["BooleanNullable"]), testModel.BooleanNullable);

            Assert.False(testModel.Int16Nullable2.HasValue);
            Assert.False(testModel.Int32Nullable2.HasValue);
            Assert.False(testModel.Int64Nullable2.HasValue);
            Assert.False(testModel.DecimalNullable2.HasValue);
            Assert.False(testModel.DoubleNullable2.HasValue);
            Assert.False(testModel.SingleNullable2.HasValue);
            Assert.False(testModel.BooleanNullable2.HasValue);

            Assert.Equal(3, testModel.StringList.Count);
            Assert.Equal("a", testModel.StringList[0]);
            Assert.Equal("b", testModel.StringList[1]);
            Assert.Equal("c", testModel.StringList[2]);

            Assert.Equal(3, testModel.ArrayList.Count);
            Assert.Equal("z", testModel.ArrayList[0]);
            Assert.Equal("y", testModel.ArrayList[1]);
            Assert.Equal("x", testModel.ArrayList[2]);

            Assert.Equal(Direction.Outbound, testModel.Enumeration);
            Assert.Equal("127.0.0.1", testModel.Ip);
            Assert.Equal("80", testModel.Port);

            Assert.Equal(Convert.ToUInt16(dictionary["UInt16"]), testModel.UInt16);
            Assert.Equal(Convert.ToUInt32(dictionary["UInt32"]), testModel.UInt32);
            Assert.Equal(Convert.ToUInt64(dictionary["UInt64"]), testModel.UInt64);

            Assert.Equal(Convert.ToUInt16(dictionary["UInt16Nullable"]), testModel.UInt16Nullable);
            Assert.Equal(Convert.ToUInt32(dictionary["UInt32Nullable"]), testModel.UInt32Nullable);
            Assert.Equal(Convert.ToUInt64(dictionary["UInt64Nullable"]), testModel.UInt64Nullable);

            Assert.False(testModel.UInt16Nullable2.HasValue);
            Assert.False(testModel.UInt32Nullable2.HasValue);
            Assert.False(testModel.UInt64Nullable2.HasValue);

            var enumerableStringList = testModel.EnumerableStringList.ToList();
            string qwerty = "qwerty";

            Assert.Equal(qwerty.Length, enumerableStringList.Count);
            for (int i = 0; i < qwerty.Length; i++)
            {
                Assert.Equal(qwerty[i].ToString(), enumerableStringList[i]);
            }

            Assert.Equal(2, testModel.Dictionary.Count);
            Assert.Equal("1", testModel.Dictionary["id"]);
            Assert.Equal("Chupacabra", testModel.Dictionary["name"]);
        }

        [Fact]
        public void Should_Fail_Convert()
        {
            var expectedErrors = new[]
            {
                @"String - Value cannot be blank/empty",
                "Int32 - Value cannot be null.",
                "Parameter name: Int32 is not found in the configuration (Expected type: System.Int32)",
                "Int64 - Value cannot be null.",
                "Parameter name: Int64 is not found in the configuration (Expected type: System.Int64)",
                "Decimal - Value cannot be null.",
                "Parameter name: Decimal is not found in the configuration (Expected type: System.Decimal)",
                "Double - Value cannot be null.",
                "Parameter name: Double is not found in the configuration (Expected type: System.Double)",
                "Single - Value cannot be null.",
                "Parameter name: Single is not found in the configuration (Expected type: System.Single)",
                "Boolean - Value cannot be null.",
                "Parameter name: Boolean is not found in the configuration (Expected type: System.Boolean)",
                "Int16Nullable - Value cannot be null.",
                "Parameter name: Int16Nullable is not found in the configuration (Expected type: System.Nullable`1[System.Int16])",
                "Int32Nullable - Value cannot be null.",
                "Parameter name: Int32Nullable is not found in the configuration (Expected type: System.Nullable`1[System.Int32])",
                "Int64Nullable - Value cannot be null.",
                "Parameter name: Int64Nullable is not found in the configuration (Expected type: System.Nullable`1[System.Int64])",
                "DecimalNullable - Value cannot be null.",
                "Parameter name: DecimalNullable is not found in the configuration (Expected type: System.Nullable`1[System.Decimal])",
                "DoubleNullable - Value cannot be null.",
                "Parameter name: DoubleNullable is not found in the configuration (Expected type: System.Nullable`1[System.Double])",
                "SingleNullable - Value cannot be null.",
                "Parameter name: SingleNullable is not found in the configuration (Expected type: System.Nullable`1[System.Single])",
                "BooleanNullable - Value cannot be null.",
                "Parameter name: BooleanNullable is not found in the configuration (Expected type: System.Nullable`1[System.Boolean])",
                "Int16Nullable2 - Value cannot be null.",
                "Parameter name: Int16Nullable2 is not found in the configuration (Expected type: System.Nullable`1[System.Int16])",
                "Int32Nullable2 - Value cannot be null.",
                "Parameter name: Int32Nullable2 is not found in the configuration (Expected type: System.Nullable`1[System.Int32])",
                "Int64Nullable2 - Value cannot be null.",
                "Parameter name: Int64Nullable2 is not found in the configuration (Expected type: System.Nullable`1[System.Int64])",
                "DecimalNullable2 - Value cannot be null.",
                "Parameter name: DecimalNullable2 is not found in the configuration (Expected type: System.Nullable`1[System.Decimal])",
                "DoubleNullable2 - Value cannot be null.",
                "Parameter name: DoubleNullable2 is not found in the configuration (Expected type: System.Nullable`1[System.Double])",
                "SingleNullable2 - Value cannot be null.",
                "Parameter name: SingleNullable2 is not found in the configuration (Expected type: System.Nullable`1[System.Single])",
                "BooleanNullable2 - Value cannot be null.",
                "Parameter name: BooleanNullable2 is not found in the configuration (Expected type: System.Nullable`1[System.Boolean])",
                "StringList - Value cannot be null.",
                "Parameter name: StringList is not found in the configuration (Expected type: System.Collections.Generic.List`1[System.String])",
                "IntegerArray - Value cannot be null.",
                "Parameter name: IntegerArray is not found in the configuration (Expected type: System.Int32[])",
                "ArrayList - Value cannot be null.",
                "Parameter name: ArrayList is not found in the configuration (Expected type: System.Collections.ArrayList)",
                "Enumeration - Value cannot be null.",
                "Parameter name: Enumeration is not found in the configuration (Expected type: StringToStronglyTyped.Test.Helpers.Direction)",
                "StringCustomLoadMethod - Value cannot be null.",
                "Parameter name: StringCustomLoadMethod is not found in the configuration (Expected type: System.String)",
                "UInt16 - Value cannot be null.",
                "Parameter name: UInt16 is not found in the configuration (Expected type: System.UInt16)",
                "UInt32 - Value cannot be null.",
                "Parameter name: UInt32 is not found in the configuration (Expected type: System.UInt32)",
                "UInt64 - Value cannot be null.",
                "Parameter name: UInt64 is not found in the configuration (Expected type: System.UInt64)",
                "UInt16Nullable - Value cannot be null.",
                "Parameter name: UInt16Nullable is not found in the configuration (Expected type: System.Nullable`1[System.UInt16])",
                "UInt32Nullable - Value cannot be null.",
                "Parameter name: UInt32Nullable is not found in the configuration (Expected type: System.Nullable`1[System.UInt32])",
                "UInt64Nullable - Value cannot be null.",
                "Parameter name: UInt64Nullable is not found in the configuration (Expected type: System.Nullable`1[System.UInt64])",
                "UInt16Nullable2 - Value cannot be null.",
                "Parameter name: UInt16Nullable2 is not found in the configuration (Expected type: System.Nullable`1[System.UInt16])",
                "UInt32Nullable2 - Value cannot be null.",
                "Parameter name: UInt32Nullable2 is not found in the configuration (Expected type: System.Nullable`1[System.UInt32])",
                "UInt64Nullable2 - Value cannot be null.",
                "Parameter name: UInt64Nullable2 is not found in the configuration (Expected type: System.Nullable`1[System.UInt64])",
                "EnumerableStringList - Value cannot be null.",
                "Parameter name: EnumerableStringList is not found in the configuration (Expected type: System.Collections.Generic.IEnumerable`1[System.String])",
                "Dictionary - Value cannot be null.",
                "Parameter name: Dictionary is not found in the configuration (Expected type: System.Collections.Generic.Dictionary`2[System.String,System.String])"
            };

            var hashtable = TestDataHelper.GetTestDataInvalidHashtable();
            try
            {
                var testModel = StronglyTypedConverter.ToType<TestModel>(hashtable);

                throw new Exception("Errors must be thrown!");
            }
            catch (Exception ex)
            {
                Assert.Equal(expectedErrors.Length, ex.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Length);

                foreach (var error in expectedErrors)
                {
                    Assert.True(ex.Message.Contains(error), "Error should be thrown: " + error);
                }

                return;
            }
        }
    }
}
