using System;
using System.Collections;
using System.Collections.Generic;

namespace StringToStronglyTyped.Test.Helpers
{
    public static class TestDataHelper
    {
        public static Hashtable GetTestDataValidHashtable()
        {
            var hashtable = new Hashtable();
            hashtable.Add("String", "some string");
            hashtable.Add("Int16", "123");
            hashtable.Add("Int32", "1");
            hashtable.Add("Int64", "98764");
            hashtable.Add("Decimal", "0.123");
            hashtable.Add("Double", "1.123");
            hashtable.Add("Single", "3.12");
            hashtable.Add("Boolean", "true");
            hashtable.Add("Int16Nullable", "321");
            hashtable.Add("Int32Nullable", "18");
            hashtable.Add("Int64Nullable", "3213");
            hashtable.Add("DecimalNullable", "0.123");
            hashtable.Add("DoubleNullable", "2.213");
            hashtable.Add("SingleNullable", "3.213");
            hashtable.Add("BooleanNullable", "true");
            hashtable.Add("Int16Nullable2", "");
            hashtable.Add("Int32Nullable2", "");
            hashtable.Add("Int64Nullable2", "");
            hashtable.Add("DecimalNullable2", "");
            hashtable.Add("DoubleNullable2", "");
            hashtable.Add("SingleNullable2", "");
            hashtable.Add("BooleanNullable2", "");
            hashtable.Add("StringList", "a|b|c");
            hashtable.Add("IntegerArray", "1|2|3|5");
            hashtable.Add("ArrayList", "z|y|x");
            hashtable.Add("Enumeration", "outbound");
            hashtable.Add("StringCustomLoadMethod", "127.0.0.1:80");
            hashtable.Add("UInt16", "1231");
            hashtable.Add("UInt32", "12");
            hashtable.Add("UInt64", "987");
            hashtable.Add("UInt16Nullable", "2132");
            hashtable.Add("UInt32Nullable", "325");
            hashtable.Add("UInt64Nullable", "12");
            hashtable.Add("UInt16Nullable2", "");
            hashtable.Add("UInt32Nullable2", "");
            hashtable.Add("UInt64Nullable2", "");
            hashtable.Add("EnumerableStringList", "q|w|e|r|t|y");
            hashtable.Add("Dictionary", "id=1|name=Chupacabra");

            return hashtable;
        }

        public static Dictionary<string, string> GetTestDataValidGenericDictionary()
        {
            Hashtable hashtable = GetTestDataValidHashtable();
            var dictionary = new Dictionary<string, string>();

            foreach (DictionaryEntry item in hashtable)
            {
                dictionary.Add(item.Key.ToString(), item.Value.ToString());
            }

            return dictionary;
        }

        public static Hashtable GetTestDataInvalidHashtable()
        {
            var hashtable = new Hashtable();
            hashtable.Add("String", "");
            hashtable.Add("Int16", "123");

            return hashtable;
        }
    }

    public class TestModel
    {
        [StronglyTypedMetadata]
        public string String { get; private set; }

        [StronglyTypedMetadata]
        public short Int16 { get; private set; }

        [StronglyTypedMetadata]
        public int Int32 { get; private set; }

        [StronglyTypedMetadata]
        public long Int64 { get; private set; }

        [StronglyTypedMetadata]
        public decimal Decimal { get; private set; }

        [StronglyTypedMetadata]
        public double Double { get; private set; }

        [StronglyTypedMetadata]
        public float Single { get; private set; }

        [StronglyTypedMetadata]
        public bool Boolean { get; private set; }

        [StronglyTypedMetadata]
        public short? Int16Nullable { get; private set; }

        [StronglyTypedMetadata]
        public int? Int32Nullable { get; private set; }

        [StronglyTypedMetadata]
        public long? Int64Nullable { get; private set; }

        [StronglyTypedMetadata]
        public decimal? DecimalNullable { get; private set; }

        [StronglyTypedMetadata]
        public double? DoubleNullable { get; private set; }

        [StronglyTypedMetadata]
        public float? SingleNullable { get; private set; }

        [StronglyTypedMetadata]
        public bool? BooleanNullable { get; private set; }

        [StronglyTypedMetadata(IsRequired = true)]
        public short? Int16Nullable2 { get; private set; }

        [StronglyTypedMetadata(IsRequired = true)]
        public int? Int32Nullable2 { get; private set; }

        [StronglyTypedMetadata(IsRequired = true)]
        public long? Int64Nullable2 { get; private set; }

        [StronglyTypedMetadata(IsRequired = true)]
        public decimal? DecimalNullable2 { get; private set; }

        [StronglyTypedMetadata(IsRequired = true)]
        public double? DoubleNullable2 { get; private set; }

        [StronglyTypedMetadata(IsRequired = true)]
        public float? SingleNullable2 { get; private set; }

        [StronglyTypedMetadata(IsRequired = true)]
        public bool? BooleanNullable2 { get; private set; }

        [StronglyTypedMetadata]
        public List<string> StringList { get; private set; }

        [StronglyTypedMetadata]
        public int[] IntegerArray { get; private set; }

        [StronglyTypedMetadata]
        public ArrayList ArrayList { get; private set; }

        [StronglyTypedMetadata]
        public Direction Enumeration { get; private set; }

        [StronglyTypedMetadata(CustomLoadMethod = "SplitStringCustomLoadMethod")]
        private string StringCustomLoadMethod { get; set; }

        public string Ip { get; private set; }

        public string Port { get; private set; }

        private void SplitStringCustomLoadMethod()
        {
            var array = StringCustomLoadMethod.Split(':');
            if (array.Length == 2)
            {
                Ip = array[0];
                Port = array[1];
            }
            else
            {
                throw new ArgumentException("Ip:Port = " + StringCustomLoadMethod + " is not correct (ip:port)");
            }
        }

        [StronglyTypedMetadata]
        public ushort UInt16 { get; private set; }

        [StronglyTypedMetadata]
        public uint UInt32 { get; private set; }

        [StronglyTypedMetadata]
        public ulong UInt64 { get; private set; }

        [StronglyTypedMetadata]
        public ushort? UInt16Nullable { get; private set; }

        [StronglyTypedMetadata]
        public uint? UInt32Nullable { get; private set; }

        [StronglyTypedMetadata]
        public ulong? UInt64Nullable { get; private set; }

        [StronglyTypedMetadata]
        public ushort? UInt16Nullable2 { get; private set; }

        [StronglyTypedMetadata]
        public uint? UInt32Nullable2 { get; private set; }

        [StronglyTypedMetadata]
        public ulong? UInt64Nullable2 { get; private set; }

        [StronglyTypedMetadata]
        public IEnumerable<string> EnumerableStringList { get; private set; }

        [StronglyTypedMetadata(IsRequired = false)]
        public IList<string> StringIList { get; private set; }

        [StronglyTypedMetadata]
        public Dictionary<string, string> Dictionary { get; protected set; }
    }

    public enum Direction
    {
        Outbound,
        Inbound
    }
}