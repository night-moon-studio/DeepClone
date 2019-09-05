using System;
using System.Collections;
using System.Collections.Generic;
using DeepClone;
using DeepClone.Template;
using DeepCloneUT.EqualityComparer;
using Xunit;

namespace DeepCloneUT
{
    public class ArrayTest
    {
        [Fact]
        public void IsArray()
        {
            var ins1 = new string[] { "Vito", "AzulX", "guodf", "wxn401", "myFirstway" };
            var ins2 = new int[] { 1, 3, 5, 2, 4, 6 };
            var ins3 = new float[][] {
                            new float[] { 1.2F,1.2F,1.3F,1.4F },
                            new float[] { 2.2F,2.2F,2.3F,2.4F }
                        };
            var ins4 = new decimal[3, 2, 4] {
                                        {
                                            {1M,2M,3M,4M},
                                            {5M,6M,7M,8M}
                                        },
                                        {
                                            {1M,2M,3M,4M},
                                            {5M,6M,7M,8M}
                                        },
                                        {
                                            {1M,2M,3M,4M},
                                            {5M,6M,7M,8M}
                                        }
                                    };

            var arrayTemp = new CloneArrayTemplate();

            Assert.True(arrayTemp.MatchType(ins1.GetType()));
            Assert.True(arrayTemp.MatchType(ins2.GetType()));
            Assert.True(arrayTemp.MatchType(ins3.GetType()));
            Assert.True(arrayTemp.MatchType(ins4.GetType()));
        }

        [Fact]
        public void NotArray()
        {
            var ins0 = new { Name = "Vito", Age = 17 };
            var ins1 = 1;
            var ins2 = 1L;
            var ins3 = "string";
            var ins4 = 1.3D;
            var ins5 = 1.4M;
            var ins6 = GenderEnum.Secrecy;
            var ins7 = new List<string>() { "Vito", "AzulX", "guodf", "wxn401", "myFirstway" };
            var ins8 = new List<int>() { 1, 3, 5, 2, 4, 6 };
            var ins9 = new List<int[,]>() {
                new int[,] { { 1, 2 }, { 2, 3 } },
                new int[,] { { 1, 2 }, { 2, 3 }, { 2, 3 } },
                new int[,] { { 1, 2, 3 }, { 3, 4, 5 } }
             };
            var ins10 = new ArrayList() { "Vito", "AzulX", "guodf", "wxn401", "myFirstway", 1, 3, 5, 2, 4, 6, new { Name = "Vito", Age = 17 } };
            var ins11 = new Dictionary<string, string>(){
                {"Key1","Value1"},
                {"Key2","Value2"}
            };

            var arrayTemp = new CloneArrayTemplate();
            Assert.False(arrayTemp.MatchType(ins0.GetType()));
            Assert.False(arrayTemp.MatchType(ins1.GetType()));
            Assert.False(arrayTemp.MatchType(ins2.GetType()));
            Assert.False(arrayTemp.MatchType(ins3.GetType()));
            Assert.False(arrayTemp.MatchType(ins4.GetType()));
            Assert.False(arrayTemp.MatchType(ins5.GetType()));
            Assert.False(arrayTemp.MatchType(ins6.GetType()));
            Assert.False(arrayTemp.MatchType(ins7.GetType()));
            Assert.False(arrayTemp.MatchType(ins8.GetType()));
            Assert.False(arrayTemp.MatchType(ins9.GetType()));
            Assert.False(arrayTemp.MatchType(ins10.GetType()));
            Assert.False(arrayTemp.MatchType(ins11.GetType()));
        }

        /// <summary>
        /// 1维度+简单: int[]
        /// </summary>
        [Fact]
        public void Clone1DimWithSimple()
        {
            string[] arrIns0 = null;
            var arrIns1 = new sbyte[] { 1, 2, 3, sbyte.MinValue, sbyte.MaxValue };
            var arrIns2 = new short[] { 4, 5, 6, short.MinValue, short.MaxValue };
            var arrIns3 = new int[] { 7, -8, 9, -10, int.MinValue, int.MaxValue };
            var arrIns4 = new long[] { 12345, 23456, long.MinValue, long.MaxValue };
            var arrIns5 = new byte[] { 123, 234, 56, byte.MinValue, byte.MaxValue };
            var arrIns6 = new ushort[] { 123, 567, ushort.MinValue, ushort.MaxValue };
            var arrIns7 = new uint[] { 678, 349, uint.MinValue, uint.MaxValue };
            var arrIns8 = new ulong[] { 789, 2345, ulong.MinValue, ulong.MaxValue };
            var arrIns9 = new float[] { 678.234F, 789, 234.000F, float.MinValue, float.MaxValue };
            var arrIns10 = new double[] { 567, 678.56789, double.MinValue, double.MaxValue };
            var arrIns11 = new decimal[] { 45678, 678.00M, 56789.234M, decimal.MinValue, decimal.MaxValue };
            var arrIns12 = new GenderEnum[] { GenderEnum.Female, GenderEnum.Secrecy };
            var arrIns13 = new bool[] { true, false, true };
            var arrIns14 = new char[] { '1', '2', 'a', 'Z' };
            var arrIns15 = new string[] { "Vito", "AzulX", "guodf", "wxn401", "myFirstway" };

            var arrIns0_Clone = CloneOperator.Clone(arrIns0);
            var arrIns1_Clone = CloneOperator.Clone(arrIns1);
            var arrIns2_Clone = CloneOperator.Clone(arrIns2);
            var arrIns3_Clone = CloneOperator.Clone(arrIns3);
            var arrIns4_Clone = CloneOperator.Clone(arrIns4);
            var arrIns5_Clone = CloneOperator.Clone(arrIns5);
            var arrIns6_Clone = CloneOperator.Clone(arrIns6);
            var arrIns7_Clone = CloneOperator.Clone(arrIns7);
            var arrIns8_Clone = CloneOperator.Clone(arrIns8);
            var arrIns9_Clone = CloneOperator.Clone(arrIns9);
            var arrIns10_Clone = CloneOperator.Clone(arrIns10);
            var arrIns11_Clone = CloneOperator.Clone(arrIns11);
            var arrIns12_Clone = CloneOperator.Clone(arrIns12);
            var arrIns13_Clone = CloneOperator.Clone(arrIns13);
            var arrIns14_Clone = CloneOperator.Clone(arrIns14);
            var arrIns15_Clone = CloneOperator.Clone(arrIns15);

            Assert.Equal(arrIns0_Clone, arrIns0);
            Assert.Equal(arrIns1_Clone, arrIns1);
            Assert.Equal(arrIns2_Clone, arrIns2);
            Assert.Equal(arrIns3_Clone, arrIns3);
            Assert.Equal(arrIns4_Clone, arrIns4);
            Assert.Equal(arrIns5_Clone, arrIns5);
            Assert.Equal(arrIns6_Clone, arrIns6);
            Assert.Equal(arrIns7_Clone, arrIns7);
            Assert.Equal(arrIns8_Clone, arrIns8);
            Assert.Equal(arrIns9_Clone, arrIns9);
            Assert.Equal(arrIns10_Clone, arrIns10);
            Assert.Equal(arrIns11_Clone, arrIns11);
            Assert.Equal(arrIns12_Clone, arrIns12);
            Assert.Equal(arrIns13_Clone, arrIns13);
            Assert.Equal(arrIns14_Clone, arrIns14);
            Assert.Equal(arrIns15_Clone, arrIns15);
        }

        /// <summary>
        /// 1维度+复杂: object[]
        /// </summary>
        [Fact]
        public void Clone1DimWithComplex()
        {
            var random = new Random();
            Member[] arrIns0 = null;
            // var arrIns1 = new object[] { new Member(), new Member(), new Member() };
            var arrIns2 = Mocker.MockArrayMember();
            var arrIns3 = new Dictionary<int, int>[] {
                Mocker.MockDict(),
                Mocker.MockDict()
            };
            var arrIns4 = new List<int>[] {
                Mocker.MockList(),
                Mocker.MockList()
            };
            // var arrIns5 = new dynamic[] { };

            var arrIns0_Clone = CloneOperator.Clone(arrIns0);
            // var arrIns1_Clone = CloneOperator.Clone(arrIns1);
            var arrIns2_Clone = CloneOperator.Clone(arrIns2);
            var arrIns3_Clone = CloneOperator.Clone(arrIns3);
            var arrIns4_Clone = CloneOperator.Clone(arrIns4);
            // var arrIns5_Clone = CloneOperator.Clone(arrIns5);

            Assert.Null(arrIns0_Clone);
            Assert.Equal(arrIns0_Clone, arrIns0);

            // Assert.Equal(arrIns1_Clone, arrIns1);

            Assert.NotNull(arrIns2_Clone);
            Assert.NotSame(arrIns2_Clone, arrIns2);
            Assert.True(arrIns2_Clone.Length == arrIns2.Length);
            Assert.Equal(arrIns2_Clone, arrIns2, new MemberArrayEqualityComparer());

            Assert.NotNull(arrIns3_Clone);
            Assert.NotSame(arrIns3_Clone, arrIns3);
            Assert.True(arrIns3_Clone.Length == arrIns3.Length);
            Assert.Equal(arrIns3_Clone, arrIns3, new DictArrayEqualityComparer());

            Assert.NotNull(arrIns4_Clone);
            Assert.NotSame(arrIns4_Clone, arrIns4);
            Assert.True(arrIns4_Clone.Length == arrIns4.Length);
            Assert.Equal(arrIns4_Clone, arrIns4, new ListArrayEqualityComparer());

            // Assert.Equal(arrIns5_Clone, arrIns5);
        }

        /// <summary>
        /// 多维+简单: int[,,]
        /// </summary>
        [Fact]
        public void CloneMultiDimWithSimple()
        {
            string[,,] arrIns0 = null;
            var arrIns1 = new sbyte[,,] {
                {
                    { 1, sbyte.MinValue, 0, 3, sbyte.MaxValue },
                    { 0, 2, sbyte.MaxValue, 23, sbyte.MinValue }
                },
                {
                    { 1, sbyte.MaxValue, 12, 3, sbyte.MinValue },
                    { sbyte.MinValue, 1, 41, 114, sbyte.MaxValue }
                }
            };
            var arrIns2 = new short[,,] {
                {
                    { 1, short.MinValue, 0, 3, short.MaxValue },
                    { 0, 2, short.MaxValue, 23, short.MinValue }
                },
                {
                    { 1, short.MaxValue, 12, 3, short.MinValue },
                    { short.MinValue, 1, 41, 114, short.MaxValue }
                }
            };
            var arrIns3 = new int[,,] {
                {
                    { 1, int.MinValue, 0, 3, int.MaxValue },
                    { 0, 2, int.MaxValue, 23, int.MinValue }
                },
                {
                    { 1, int.MaxValue, 12, 3, int.MinValue },
                    { int.MinValue, 1, 41, 114, int.MaxValue }
                }
            };
            var arrIns4 = new long[,,] {
                {
                    { 1, long.MinValue, 0, 3, long.MaxValue },
                    { 0, 2, long.MaxValue, 23, long.MinValue }
                },
                {
                    { 1, long.MaxValue, 12, 3, long.MinValue },
                    { long.MinValue, 1, 41, 114, long.MaxValue }
                }
            };
            var arrIns5 = new byte[,,] {
                {
                    { 1, byte.MinValue, 0, 3, byte.MaxValue },
                    { 0, 2, byte.MaxValue, 23, byte.MinValue }
                },
                {
                    { 1, byte.MaxValue, 12, 3, byte.MinValue },
                    { byte.MinValue, 1, 41, 114, byte.MaxValue }
                }
            };
            var arrIns6 = new ushort[,,] {
                {
                    { 1, ushort.MinValue, 0, 3, ushort.MaxValue },
                    { 0, 2, ushort.MaxValue, 23, ushort.MinValue }
                },
                {
                    { 1, ushort.MaxValue, 12, 3, ushort.MinValue },
                    { ushort.MinValue, 1, 41, 114, ushort.MaxValue }
                }
            };
            var arrIns7 = new uint[,,] {
                {
                    { 1, uint.MinValue, 0, 3, uint.MaxValue },
                    { 0, 2, uint.MaxValue, 23, uint.MinValue }
                },
                {
                    { 1, uint.MaxValue, 12, 3, uint.MinValue },
                    { uint.MinValue, 1, 41, 114, uint.MaxValue }
                }
            };
            var arrIns8 = new ulong[,,] {
                {
                    { 1, ulong.MinValue, 0, 3, ulong.MaxValue },
                    { 0, 2, ulong.MaxValue, 23, ulong.MinValue }
                },
                {
                    { 1, ulong.MaxValue, 12, 3, ulong.MinValue },
                    { ulong.MinValue, 1, 41, 114, ulong.MaxValue }
                }
            };
            var arrIns9 = new float[,,] {
                {
                    { 1, float.MinValue, 0, 3, float.MaxValue },
                    { 0, 2, float.MaxValue, 23, float.MinValue }
                },
                {
                    { 1, float.MaxValue, 12, 3, float.MinValue },
                    { float.MinValue, 1, 41, 114, float.MaxValue }
                }
            };
            var arrIns10 = new double[,,] {
                {
                    { 1, double.MinValue, 0, 3, double.MaxValue },
                    { 0, 2, double.MaxValue, 23, double.MinValue }
                },
                {
                    { 1, double.MaxValue, 12, 3, double.MinValue },
                    { double.MinValue, 1, 41, 114, double.MaxValue }
                }
            };
            var arrIns11 = new decimal[,,] {
                {
                    { 1, decimal.MinValue, 0, 3, decimal.MaxValue },
                    { 0, 2, decimal.MaxValue, 23, decimal.MinValue }
                },
                {
                    { 1, decimal.MaxValue, 12, 3, decimal.MinValue },
                    { decimal.MinValue, 1, 41, 114, decimal.MaxValue }
                }
            };
            var arrIns12 = new GenderEnum[,,] {
                {
                    { GenderEnum.Female,GenderEnum.Male,GenderEnum.Secrecy },
                    { GenderEnum.Male,GenderEnum.Secrecy,GenderEnum.Male },
                },
                {
                    { GenderEnum.Female,GenderEnum.Secrecy,GenderEnum.Secrecy },
                    { GenderEnum.Male,GenderEnum.Male,GenderEnum.Female },
                }
            };
            var arrIns13 = new bool[,,] {
                {
                    { true,false,true },
                    { false,true,true }
                },
                {
                    { false,false,true },
                    { false,true,false }
                }
            };
            var arrIns14 = new char[,,] {
                {
                    { '1','2','3' },
                    { 'a','c','.' }
                },
                {
                    { ')','+','@' },
                    { '/','%','$' }
                }
            };
            var arrIns15 = new string[,,] {
                {
                    {"wxn401", "AzulX", "guodf", "wxn401", "myFirstway"},
                    {"", "", "", "", ""},
                    {"Vito", "wxn401", "guodf", "AzulX", "myFirstway"}
                },
                {
                    {"Vito", "AzulX", "myFirstway", "wxn401", "myFirstway"},
                    {"myFirstway", "", "guodf", "wxn401", "myFirstway"},
                    {"Vito", "AzulX", "guodf", "AzulX", "AzulX"}
                }
            };

            var arrIns0_Clone = CloneOperator.Clone(arrIns0);
            var arrIns1_Clone = CloneOperator.Clone(arrIns1);
            var arrIns2_Clone = CloneOperator.Clone(arrIns2);
            var arrIns3_Clone = CloneOperator.Clone(arrIns3);
            var arrIns4_Clone = CloneOperator.Clone(arrIns4);
            var arrIns5_Clone = CloneOperator.Clone(arrIns5);
            var arrIns6_Clone = CloneOperator.Clone(arrIns6);
            var arrIns7_Clone = CloneOperator.Clone(arrIns7);
            var arrIns8_Clone = CloneOperator.Clone(arrIns8);
            var arrIns9_Clone = CloneOperator.Clone(arrIns9);
            var arrIns10_Clone = CloneOperator.Clone(arrIns10);
            var arrIns11_Clone = CloneOperator.Clone(arrIns11);
            var arrIns12_Clone = CloneOperator.Clone(arrIns12);
            var arrIns13_Clone = CloneOperator.Clone(arrIns13);
            var arrIns14_Clone = CloneOperator.Clone(arrIns14);
            var arrIns15_Clone = CloneOperator.Clone(arrIns15);

            Assert.Equal(arrIns0_Clone, arrIns0);
            Assert.Equal(arrIns1_Clone, arrIns1);
            Assert.Equal(arrIns2_Clone, arrIns2);
            Assert.Equal(arrIns3_Clone, arrIns3);
            Assert.Equal(arrIns4_Clone, arrIns4);
            Assert.Equal(arrIns5_Clone, arrIns5);
            Assert.Equal(arrIns6_Clone, arrIns6);
            Assert.Equal(arrIns7_Clone, arrIns7);
            Assert.Equal(arrIns8_Clone, arrIns8);
            Assert.Equal(arrIns9_Clone, arrIns9);
            Assert.Equal(arrIns10_Clone, arrIns10);
            Assert.Equal(arrIns11_Clone, arrIns11);
            Assert.Equal(arrIns12_Clone, arrIns12);
            Assert.Equal(arrIns13_Clone, arrIns13);
            Assert.Equal(arrIns14_Clone, arrIns14);
            Assert.Equal(arrIns15_Clone, arrIns15);
        }

        /// <summary>
        /// 多维+复杂: object[,,]
        /// </summary>
        [Fact]
        public void CloneMultiDimWithComplex()
        {
            Member[,,] arrIns0 = null;
            // var arrIns1 = new object[,,]{
            //     {
            //         {Mocker.MockMemberIns(),Mocker.MockMemberIns()},
            //         {Mocker.MockMemberIns(),Mocker.MockMemberIns()}
            //     },
            //     {
            //         {Mocker.MockMemberIns(),Mocker.MockMemberIns()},
            //         {Mocker.MockMemberIns(),Mocker.MockMemberIns()}
            //     }
            // };
            var arrIns2 = new Member[,,]{
                {
                    {Mocker.MockMemberIns(),Mocker.MockMemberIns()},
                    {Mocker.MockMemberIns(),Mocker.MockMemberIns()}
                },
                {
                    {Mocker.MockMemberIns(),Mocker.MockMemberIns()},
                    {Mocker.MockMemberIns(),Mocker.MockMemberIns()}
                }
            };
            var arrIns3 = new Dictionary<int, int>[,,]{
                {
                    {Mocker.MockDict(),Mocker.MockDict()},
                    {Mocker.MockDict(),Mocker.MockDict()}
                },
                {
                    {Mocker.MockDict(),Mocker.MockDict()},
                    {Mocker.MockDict(),Mocker.MockDict()}
                }
            };
            var arrIns4 = new List<int>[,,]{
                {
                    {Mocker.MockList(),Mocker.MockList()},
                    {Mocker.MockList(),Mocker.MockList()}
                },
                {
                    {Mocker.MockList(),Mocker.MockList()},
                    {Mocker.MockList(),Mocker.MockList()}
                }
            };
            // var arrIns5 = new dynamic[,,]{
            //     {
            //         {Mocker.MockMemberIns(),Mocker.MockMemberIns()},
            //         {Mocker.MockMemberIns(),Mocker.MockMemberIns()}
            //     },
            //     {
            //         {Mocker.MockMemberIns(),Mocker.MockMemberIns()},
            //         {Mocker.MockMemberIns(),Mocker.MockMemberIns()}
            //     }
            // };

            var arrIns0_Clone = CloneOperator.Clone(arrIns0);
            // var arrIns1_Clone = CloneOperator.Clone(arrIns1);
            var arrIns2_Clone = CloneOperator.Clone(arrIns2);
            var arrIns3_Clone = CloneOperator.Clone(arrIns3);
            var arrIns4_Clone = CloneOperator.Clone(arrIns4);
            // var arrIns5_Clone = CloneOperator.Clone(arrIns5);

            Assert.Null(arrIns0_Clone);
            Assert.Equal(arrIns0_Clone, arrIns0);

            // Assert.Equal(arrIns1_Clone, arrIns1);

            Assert.NotNull(arrIns2_Clone);
            Assert.NotSame(arrIns2_Clone, arrIns2);
            Assert.True(arrIns2_Clone.Length == arrIns2.Length);
            // Assert.Equal(arrIns2_Clone, arrIns2, new MemberArrayEqualityComparer());
            for (int i = 0; i < arrIns2.Length; i++)
                Assert.Equal(arrIns2_Clone, arrIns2);

            // Assert.AreEqual(arrIns2_Clone, arrIns2);

            Assert.NotNull(arrIns3_Clone);
            Assert.NotSame(arrIns3_Clone, arrIns3);
            Assert.True(arrIns3_Clone.Length == arrIns3.Length);
            // Assert.Equal(arrIns3_Clone, arrIns3, new DictArrayEqualityComparer());
            Assert.Equal(arrIns3_Clone, arrIns3);

            Assert.NotNull(arrIns4_Clone);
            Assert.NotSame(arrIns4_Clone, arrIns4);
            Assert.True(arrIns4_Clone.Length == arrIns4.Length);
            // Assert.Equal(arrIns4_Clone, arrIns4, new ListArrayEqualityComparer());
            Assert.Equal(arrIns4_Clone, arrIns4);

            // Assert.Equal(arrIns5_Clone, arrIns5);
        }

        /// <summary>
        /// 锯齿+简单: int[][][]
        /// </summary>
        [Fact]
        public void CloneJaggedWithSimple()
        {
            string[][][] arrIns0 = null;
            var arrIns1 = new sbyte[][][] {
                new sbyte[][]{
                    Mocker.MockArraySbyte(),
                    Mocker.MockArraySbyte(),
                    Mocker.MockArraySbyte()
                },
                new sbyte[][]{
                    Mocker.MockArraySbyte(),
                    Mocker.MockArraySbyte()
                }
            };
            var arrIns2 = new short[][][] {
                new short[][]{
                    Mocker.MockArrayShort(),
                    Mocker.MockArrayShort(),
                    Mocker.MockArrayShort()
                },
                new short[][]{
                    Mocker.MockArrayShort(),
                    Mocker.MockArrayShort()
                }
            };
            var arrIns3 = new int[][][] {
                new int[][]{
                    Mocker.MockArrayInt(),
                    Mocker.MockArrayInt(),
                    Mocker.MockArrayInt()
                },
                new int[][]{
                    Mocker.MockArrayInt(),
                    Mocker.MockArrayInt()
                }
            };
            var arrIns4 = new long[][][] {
                new long[][]{
                    Mocker.MockArrayLong(),
                    Mocker.MockArrayLong(),
                    Mocker.MockArrayLong()
                },
                new long[][]{
                    Mocker.MockArrayLong(),
                    Mocker.MockArrayLong()
                }
            };
            var arrIns5 = new byte[][][] {
                new byte[][]{
                    Mocker.MockArrayByte(),
                    Mocker.MockArrayByte(),
                    Mocker.MockArrayByte()
                },
                new byte[][]{
                    Mocker.MockArrayByte(),
                    Mocker.MockArrayByte()
                }
            };
            var arrIns6 = new ushort[][][] {
                new ushort[][]{
                    Mocker.MockArrayUShort(),
                    Mocker.MockArrayUShort(),
                    Mocker.MockArrayUShort()
                },
                new ushort[][]{
                    Mocker.MockArrayUShort(),
                    Mocker.MockArrayUShort()
                }
            };
            var arrIns7 = new uint[][][] {
                new uint[][]{
                    Mocker.MockArrayUInt(),
                    Mocker.MockArrayUInt(),
                    Mocker.MockArrayUInt()
                },
                new uint[][]{
                    Mocker.MockArrayUInt(),
                    Mocker.MockArrayUInt()
                }
            };
            var arrIns8 = new ulong[][][] {
                new ulong[][]{
                    Mocker.MockArrayULong(),
                    Mocker.MockArrayULong(),
                    Mocker.MockArrayULong()
                },
                new ulong[][]{
                    Mocker.MockArrayULong(),
                    Mocker.MockArrayULong()
                }
            };
            var arrIns9 = new float[][][] {
                new float[][]{
                    Mocker.MockArrayFloat(),
                    Mocker.MockArrayFloat(),
                    Mocker.MockArrayFloat()
                },
                new float[][]{
                    Mocker.MockArrayFloat(),
                    Mocker.MockArrayFloat()
                }
            };
            var arrIns10 = new double[][][] {
                new double[][]{
                    Mocker.MockArrayDouble(),
                    Mocker.MockArrayDouble(),
                    Mocker.MockArrayDouble()
                },
                new double[][]{
                    Mocker.MockArrayDouble(),
                    Mocker.MockArrayDouble()
                }
            };
            var arrIns11 = new decimal[][][] {
                new decimal[][]{
                    Mocker.MockArrayDecimal(),
                    Mocker.MockArrayDecimal(),
                    Mocker.MockArrayDecimal()
                },
                new decimal[][]{
                    Mocker.MockArrayDecimal(),
                    Mocker.MockArrayDecimal()
                }
            };
            var arrIns12 = new GenderEnum[][][] {
                new GenderEnum[][]{
                    Mocker.MockArrayGenderEnum(),
                    Mocker.MockArrayGenderEnum()
                },
                new GenderEnum[][]{
                    Mocker.MockArrayGenderEnum(),
                    Mocker.MockArrayGenderEnum(),
                    Mocker.MockArrayGenderEnum()
                }
            };
            var arrIns13 = new bool[][][] {
                new bool[][]{
                    Mocker.MockArrayBoolean(),
                    Mocker.MockArrayBoolean(),
                    Mocker.MockArrayBoolean()
                },
                new bool[][]{
                    Mocker.MockArrayBoolean(),
                    Mocker.MockArrayBoolean()
                }
            };
            var arrIns14 = new char[][][] {
                new char[][]{
                    Mocker.MockArrayChar(),
                    Mocker.MockArrayChar(),
                    Mocker.MockArrayChar()
                },
                new char[][]{
                    Mocker.MockArrayChar(),
                    Mocker.MockArrayChar()
                }
            };
            var arrIns15 = new string[][][] {
                new string[][]{
                    Mocker.MockArrayStr(),
                    Mocker.MockArrayStr(),
                    Mocker.MockArrayStr()
                },
                new string[][]{
                    Mocker.MockArrayStr(),
                    Mocker.MockArrayStr()
                }
            };


            var arrIns0_Clone = CloneOperator.Clone(arrIns0);
            var arrIns1_Clone = CloneOperator.Clone(arrIns1);
            var arrIns2_Clone = CloneOperator.Clone(arrIns2);
            var arrIns3_Clone = CloneOperator.Clone(arrIns3);
            var arrIns4_Clone = CloneOperator.Clone(arrIns4);
            var arrIns5_Clone = CloneOperator.Clone(arrIns5);
            var arrIns6_Clone = CloneOperator.Clone(arrIns6);
            var arrIns7_Clone = CloneOperator.Clone(arrIns7);
            var arrIns8_Clone = CloneOperator.Clone(arrIns8);
            var arrIns9_Clone = CloneOperator.Clone(arrIns9);
            var arrIns10_Clone = CloneOperator.Clone(arrIns10);
            var arrIns11_Clone = CloneOperator.Clone(arrIns11);
            var arrIns12_Clone = CloneOperator.Clone(arrIns12);
            var arrIns13_Clone = CloneOperator.Clone(arrIns13);
            var arrIns14_Clone = CloneOperator.Clone(arrIns14);
            var arrIns15_Clone = CloneOperator.Clone(arrIns15);

            Assert.Equal(arrIns0_Clone, arrIns0);
            Assert.Equal(arrIns1_Clone, arrIns1);
            Assert.Equal(arrIns2_Clone, arrIns2);
            Assert.Equal(arrIns3_Clone, arrIns3);
            Assert.Equal(arrIns4_Clone, arrIns4);
            Assert.Equal(arrIns5_Clone, arrIns5);
            Assert.Equal(arrIns6_Clone, arrIns6);
            Assert.Equal(arrIns7_Clone, arrIns7);
            Assert.Equal(arrIns8_Clone, arrIns8);
            Assert.Equal(arrIns9_Clone, arrIns9);
            Assert.Equal(arrIns10_Clone, arrIns10);
            Assert.Equal(arrIns11_Clone, arrIns11);
            Assert.Equal(arrIns12_Clone, arrIns12);
            Assert.Equal(arrIns13_Clone, arrIns13);
            Assert.Equal(arrIns14_Clone, arrIns14);
            Assert.Equal(arrIns15_Clone, arrIns15);
        }

        /// <summary>
        /// 锯齿+复杂: object[][][]
        /// </summary>
        [Fact]
        public void CloneJaggedWithComplex()
        {
            Member[][][] arrIns0 = null;
            // var arrIns1 = new object[][][] {
            //     new Member[][] {
            //         Mocker.MockArrayMember(),
            //         Mocker.MockArrayMember(),
            //         Mocker.MockArrayMember()
            //     },
            //     new Member[][] {
            //         Mocker.MockArrayMember()
            //     },
            //     new Member[][] {
            //         Mocker.MockArrayMember(),
            //         Mocker.MockArrayMember()
            //     }
            // };
            var arrIns2 = new Member[][][] {
                new Member[][] {
                    Mocker.MockArrayMember(),
                    Mocker.MockArrayMember(),
                    Mocker.MockArrayMember()
                },
                new Member[][] {
                    Mocker.MockArrayMember()
                },
                new Member[][] {
                    Mocker.MockArrayMember(),
                    Mocker.MockArrayMember()
                }
            };
            var arrIns3 = new Dictionary<int, int>[][][] {
                new Dictionary<int,int>[][] {
                    Mocker.MockArrayDict(),
                    Mocker.MockArrayDict(),
                    Mocker.MockArrayDict()
                },
                new Dictionary<int,int>[][] {
                    Mocker.MockArrayDict()
                },
                new Dictionary<int,int>[][] {
                    Mocker.MockArrayDict(),
                    Mocker.MockArrayDict()
                }
            };
            var arrIns4 = new List<int>[][][] {
                new List<int>[][] {
                    Mocker.MockArrayListInt(),
                    Mocker.MockArrayListInt(),
                    Mocker.MockArrayListInt()
                },
                new List<int>[][] {
                    Mocker.MockArrayListInt()
                },
                new List<int>[][] {
                    Mocker.MockArrayListInt(),
                    Mocker.MockArrayListInt()
                }
            };
            // var arrIns5 = new dynamic[][][] {
            //     new Member[][] {
            //         Mocker.MockArrayMember(),
            //         Mocker.MockArrayMember(),
            //         Mocker.MockArrayMember()
            //     },
            //     new Member[][] {
            //         Mocker.MockArrayMember()
            //     },
            //     new Member[][] {
            //         Mocker.MockArrayMember(),
            //         Mocker.MockArrayMember()
            //     }
            // };


            var arrIns0_Clone = CloneOperator.Clone(arrIns0);
            // var arrIns1_Clone = CloneOperator.Clone(arrIns1);
            var arrIns2_Clone = CloneOperator.Clone(arrIns2);
            var arrIns3_Clone = CloneOperator.Clone(arrIns3);
            var arrIns4_Clone = CloneOperator.Clone(arrIns4);
            // var arrIns5_Clone = CloneOperator.Clone(arrIns5);


            Assert.Null(arrIns0_Clone);
            Assert.Equal(arrIns0_Clone, arrIns0);

            // Assert.Equal(arrIns1_Clone, arrIns1);

            Assert.NotNull(arrIns2_Clone);
            Assert.NotSame(arrIns2_Clone, arrIns2);
            Assert.True(arrIns2_Clone.Length == arrIns2.Length);
            Assert.Equal(arrIns2_Clone, arrIns2);

            Assert.NotNull(arrIns3_Clone);
            Assert.NotSame(arrIns3_Clone, arrIns3);
            Assert.True(arrIns3_Clone.Length == arrIns3.Length);
            Assert.Equal(arrIns3_Clone, arrIns3);

            Assert.NotNull(arrIns4_Clone);
            Assert.NotSame(arrIns4_Clone, arrIns4);
            Assert.True(arrIns4_Clone.Length == arrIns4.Length);
            Assert.Equal(arrIns4_Clone, arrIns4);

            // Assert.Equal(arrIns5_Clone, arrIns5);
        }
    }
}