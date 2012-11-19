// -----------------------------------------------------------------------
// <copyright file="IntPtrExtensionTests.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;

namespace Knuckleball.Tests
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class IntPtrExtensionTests
    {
        IntPtr pointer;

        [SetUp]
        public void SetUp()
        {
            this.pointer = Marshal.AllocHGlobal(1024);
        }

        [TearDown]
        public void TearDown()
        {
            Marshal.FreeHGlobal(this.pointer);
        }

        [Test]
        public void ReadIntShouldReturnNullForZeroPointer()
        {
            Assert.IsNull(IntPtr.Zero.ReadInt());
        }

        [Test]
        public void ReadIntShouldReturnIntForPointerToInt()
        {
            Marshal.WriteInt32(pointer, 2222);
            Assert.AreEqual(2222, pointer.ReadInt());
        }

        [Test]
        public void ReadLongShouldReturnNullForZeroPointer()
        {
            Assert.IsNull(IntPtr.Zero.ReadLong());
        }

        [Test]
        public void ReadLongShouldReturnLongForPointerToLong()
        {
            Marshal.WriteInt64(pointer, 2222L);
            Assert.AreEqual(2222L, pointer.ReadLong());
        }

        [Test]
        public void ReadShortShouldReturnNullForZeroPointer()
        {
            Assert.IsNull(IntPtr.Zero.ReadShort());
        }

        [Test]
        public void ReadShortShouldReturnShortForPointerToShort()
        {
            Marshal.WriteInt16(pointer, 2222);
            Assert.AreEqual(2222, pointer.ReadShort());
        }

        [Test]
        public void ReadBooleanShouldReturnNullForZeroPointer()
        {
            Assert.IsNull(IntPtr.Zero.ReadBoolean());
        }

        [Test]
        public void ReadBooleanShouldReturnFalseForPointerToZero()
        {
            Marshal.WriteByte(pointer, 0);
            Assert.IsFalse(pointer.ReadBoolean().Value);
        }

        [Test]
        public void ReadBooleanShouldReturnTrueForPointerToNonZero()
        {
            Marshal.WriteByte(pointer, 1);
            Assert.IsTrue(pointer.ReadBoolean().Value);
        }

        [Test]
        public void ReadByteShouldReturnNullForZeroPointer()
        {
            Assert.IsNull(IntPtr.Zero.ReadByte());
        }

        [Test]
        public void ReadByteShouldReturnByteForPointerToByte()
        {
            Marshal.WriteByte(pointer, 240);
            Assert.AreEqual(240, pointer.ReadByte());
        }

        private enum TestEnum
        {
            NotSet = -1,
            FirstValue = 0,
            SecondValue = 1
        }

        [Test]
        public void ReadEnumShouldReturnDefaultValueForZeroPointer()
        {
            Assert.AreEqual(TestEnum.NotSet, IntPtr.Zero.ReadEnumValue<TestEnum>(TestEnum.NotSet));
        }

        [Test]
        public void ReadEnumShouldReturnEnumValueForPointerToEnum()
        {
            Marshal.WriteInt32(pointer, (int)TestEnum.SecondValue);
            Assert.AreEqual(TestEnum.SecondValue, pointer.ReadEnumValue<TestEnum>(TestEnum.NotSet));
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TestStruct
        {
            public int Foo;
            public string Bar;
        }
 
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadStructureShouldThrowExceptionForZeroPointer()
        {
            Assert.IsNull(IntPtr.Zero.ReadStructure<TestStruct>());
        }

        [Test]
        public void ReadStructureShouldReturnStructureForPointerToStructure()
        {
            TestStruct structValue = new TestStruct();
            structValue.Foo = 1;
            structValue.Bar = "Hello";
            Marshal.StructureToPtr(structValue, pointer, true);
            Assert.AreEqual(1, pointer.ReadStructure<TestStruct>().Foo);
            Assert.AreEqual("Hello", pointer.ReadStructure<TestStruct>().Bar);
        }
   }
}
