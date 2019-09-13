using System;
using System.Collections.Generic;

namespace DeepCloneUT
{
    public static class Mocker
    {
        private static Random random = new Random();
        private static int RandomInt => (int)random.Next(int.MinValue, int.MaxValue);
        public readonly static char[] Element = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };

        public static char[] MockArrayChar(int? len = null, bool notNull = false)
        {
            char[] result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            len = len ?? random.Next(1, 100);
            result = new char[len.Value];
            for (int i = 0; i < len; i++)
                result[i] = Element[random.Next(0, Element.Length)];

            return result;
        }

        public static string RandomStr(int? len = null, bool notNull = false)
        {
            var res = MockArrayChar(len);
            return res == null ? null : string.Join(string.Empty, res);
        }

        #region SimpleType

        public static List<bool> MockListBoolean(int? len = null, bool notNull = false)
        {
            List<bool> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<bool>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add(random.Next(0, 2) == 1);

            return result;
        }
        public static bool[] MockArrayBoolean(int? len = null, bool notNull = false) => MockListBoolean(len, notNull)?.ToArray();

        public static List<GenderEnum> MockListGenderEnum(int? len = null, bool notNull = false)
        {
            List<GenderEnum> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<GenderEnum>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add((GenderEnum)random.Next(0, 2));

            return result;
        }
        public static GenderEnum[] MockArrayGenderEnum(int? len = null, bool notNull = false) => MockListGenderEnum(len, notNull)?.ToArray();

        public static List<int> MockListInt(int? len = null, bool notNull = false)
        {
            List<int> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<int>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add((int)(random.NextDouble() * int.MaxValue) * (random.Next(0, 2) == 0 ? 1 : -1));

            return result;
        }

        public static List<int>[] MockArrayListInt(int? len = null, bool notNull = false)
        {
            List<int>[] result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            len = len ?? random.Next(1, 100);
            result = new List<int>[len.Value];
            for (int i = 0; i < len; i++)
                result[i] = MockListInt();

            return result;
        }

        public static int[] MockArrayInt(int? len = null, bool notNull = false) => MockListInt(len, notNull)?.ToArray();

        public static List<long> MockListLong(int? len = null, bool notNull = false)
        {
            List<long> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<long>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add((long)(random.NextDouble() * long.MaxValue) * (random.Next(0, 2) == 0 ? 1 : -1));

            return result;
        }

        public static long[] MockArrayLong(int? len = null, bool notNull = false) => MockListLong(len, notNull)?.ToArray();

        public static List<float> MockListFloat(int? len = null, bool notNull = false)
        {
            List<float> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<float>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add((float)(random.NextDouble() * float.MaxValue) * (random.Next(0, 2) == 0 ? 1 : -1));

            return result;
        }

        public static float[] MockArrayFloat(int? len = null, bool notNull = false) => MockListFloat(len, notNull)?.ToArray();

        public static List<double> MockListDouble(int? len = null, bool notNull = false)
        {
            List<double> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<double>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add((double)(random.NextDouble() * double.MaxValue) * (random.Next(0, 2) == 0 ? 1 : -1));

            return result;
        }

        public static double[] MockArrayDouble(int? len = null, bool notNull = false) => MockListDouble(len, notNull)?.ToArray();

        public static List<decimal> MockListDecimal(int? len = null, bool notNull = false)
        {
            List<decimal> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<decimal>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add((decimal)((decimal)random.NextDouble() * decimal.MaxValue) * (random.Next(0, 2) == 0 ? 1 : -1));

            return result;
        }

        public static decimal[] MockArrayDecimal(int? len = null, bool notNull = false) => MockListDecimal(len, notNull)?.ToArray();

        public static List<short> MockListShort(int? len = null, bool notNull = false)
        {
            List<short> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<short>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add((short)((random.NextDouble() * short.MaxValue) * (random.Next(0, 2) == 0 ? 1 : -1)));

            return result;
        }

        public static short[] MockArrayShort(int? len = null, bool notNull = false) => MockListShort(len, notNull)?.ToArray();

        public static List<sbyte> MockListSbyte(int? len = null, bool notNull = false)
        {
            List<sbyte> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<sbyte>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add((sbyte)((random.NextDouble() * sbyte.MaxValue) * (random.Next(0, 2) == 0 ? 1 : -1)));

            return result;
        }

        public static sbyte[] MockArraySbyte(int? len = null, bool notNull = false) => MockListSbyte(len, notNull)?.ToArray();

        public static List<byte> MockListByte(int? len = null, bool notNull = false)
        {
            List<byte> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<byte>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add((byte)(random.NextDouble() * byte.MaxValue));

            return result;
        }

        public static byte[] MockArrayByte(int? len = null, bool notNull = false) => MockListByte(len, notNull)?.ToArray();

        public static List<ushort> MockListUShort(int? len = null, bool notNull = false)
        {
            List<ushort> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<ushort>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add((ushort)(random.NextDouble() * ushort.MaxValue));

            return result;
        }

        public static ushort[] MockArrayUShort(int? len = null, bool notNull = false) => MockListUShort(len, notNull)?.ToArray();

        public static List<uint> MockListUInt(int? len = null, bool notNull = false)
        {
            List<uint> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<uint>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add((uint)(random.NextDouble() * uint.MaxValue));

            return result;
        }

        public static uint[] MockArrayUInt(int? len = null, bool notNull = false) => MockListUInt(len, notNull)?.ToArray();

        public static List<ulong> MockListULong(int? len = null, bool notNull = false)
        {
            List<ulong> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<ulong>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add((ulong)(random.NextDouble() * ulong.MaxValue));

            return result;
        }

        public static ulong[] MockArrayULong(int? len = null, bool notNull = false) => MockListULong(len, notNull)?.ToArray();

        #endregion


        private static Member _mockIns()
        {
            return new Member()
            {
                MemberType = (MemberType)random.Next(0, 2),
                FirstName = RandomStr(),
                MiddleName = RandomStr(),
                LastName = RandomStr(),
                Age = random.Next(1, 100),
                Birthday = DateTime.Now.AddDays(random.Next(-365, 365)).Date,
                AnnualIncome = random.Next(1, 1000000) / 13456.234M,
                Teacher = null
            };
        }
        public static List<Member> MockListMember(int? len = null, bool notNull = false)
        {
            List<Member> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<Member>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add(MockMemberIns());
            return result;
        }

        public static Member[] MockArrayMember(int? len = null, bool notNull = false) => MockListMember(len, notNull)?.ToArray();

        public static Member MockMemberIns()
        {
            Member teacher = null;
            if (random.Next(0, 2) == 1)
            {
                teacher = _mockIns();
                teacher.MemberType = MemberType.Teacher;
            }
            var result = _mockIns();
            result.Teacher = teacher;
            return result;
        }

        public static Dictionary<int, int> MockDict(int? len = null, bool notNull = false)
        {
            Dictionary<int, int> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new Dictionary<int, int>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
            {
                var key = random.Next(int.MinValue, int.MaxValue);
                if (!result.ContainsKey(key))
                    result.Add(key, random.Next(int.MinValue, int.MaxValue));
            }
            return result;
        }

        public static Dictionary<int, int>[] MockArrayDict(int? len = null, bool notNull = false)
        {
            Dictionary<int, int>[] result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            len = len ?? random.Next(1, 100);
            result = new Dictionary<int, int>[len.Value];
            for (int i = 0; i < len; i++)
                result[i] = MockDict();
            return result;
        }

        public static List<int> MockList(int? len = null, bool notNull = false)
        {
            List<int> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<int>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add(random.Next(int.MinValue, int.MaxValue));
            return result;
        }


        public static List<string> MockListStr(int? len = null, bool notNull = false)
        {
            List<string> result = null;
            if (!notNull)
                if (random.Next(0, 10) == 5)
                    return result;

            result = new List<string>();
            len = len ?? random.Next(1, 100);
            for (int i = 0; i < len; i++)
                result.Add(RandomStr());
            return result;
        }

        public static string[] MockArrayStr(int? len = null, bool notNull = false) => MockListStr(len, notNull)?.ToArray();
    }
}