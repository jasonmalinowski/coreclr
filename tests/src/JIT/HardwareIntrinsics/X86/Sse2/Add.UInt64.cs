// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\X86\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace JIT.HardwareIntrinsics.X86
{
    public static partial class Program
    {
        private static void AddUInt64()
        {
            var test = new SimpleBinaryOpTest__AddUInt64();

            if (test.IsSupported)
            {
                // Validates basic functionality works
                test.RunBasicScenario();

                // Validates calling via reflection works
                test.RunReflectionScenario();

                // Validates passing a static member works
                test.RunClsVarScenario();

                // Validates passing a local works
                test.RunLclVarScenario();

                // Validates passing the field of a local works
                test.RunLclFldScenario();

                // Validates passing an instance member works
                test.RunFldScenario();
            }
            else
            {
                // Validates we throw on unsupported hardware
                test.RunUnsupportedScenario();
            }

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class SimpleBinaryOpTest__AddUInt64
    {
        private const int VectorSize = 16;
        private const int ElementCount = VectorSize / sizeof(UInt64);

        private static UInt64[] _data1 = new UInt64[ElementCount];
        private static UInt64[] _data2 = new UInt64[ElementCount];

        private static Vector128<UInt64> _clsVar1;
        private static Vector128<UInt64> _clsVar2;

        private Vector128<UInt64> _fld1;
        private Vector128<UInt64> _fld2;

        private SimpleBinaryOpTest__DataTable<UInt64> _dataTable;

        static SimpleBinaryOpTest__AddUInt64()
        {
            var random = new Random();

            for (var i = 0; i < ElementCount; i++) { _data1[i] = (ulong)(random.Next(0, int.MaxValue)); _data2[i] = (ulong)(random.Next(0, int.MaxValue)); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<UInt64>, byte>(ref _clsVar1), ref Unsafe.As<UInt64, byte>(ref _data2[0]), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<UInt64>, byte>(ref _clsVar2), ref Unsafe.As<UInt64, byte>(ref _data1[0]), VectorSize);
        }

        public SimpleBinaryOpTest__AddUInt64()
        {
            Succeeded = true;

            var random = new Random();

            for (var i = 0; i < ElementCount; i++) { _data1[i] = (ulong)(random.Next(0, int.MaxValue)); _data2[i] = (ulong)(random.Next(0, int.MaxValue)); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<UInt64>, byte>(ref _fld1), ref Unsafe.As<UInt64, byte>(ref _data1[0]), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<UInt64>, byte>(ref _fld2), ref Unsafe.As<UInt64, byte>(ref _data2[0]), VectorSize);

            for (var i = 0; i < ElementCount; i++) { _data1[i] = (ulong)(random.Next(0, int.MaxValue)); _data2[i] = (ulong)(random.Next(0, int.MaxValue)); }
            _dataTable = new SimpleBinaryOpTest__DataTable<UInt64>(_data1, _data2, new UInt64[ElementCount]);
        }

        public bool IsSupported => Sse2.IsSupported;

        public bool Succeeded { get; set; }

        public void RunBasicScenario()
        {
            var result = Sse2.Add(
                Unsafe.Read<Vector128<UInt64>>(_dataTable.inArray1Ptr),
                Unsafe.Read<Vector128<UInt64>>(_dataTable.inArray2Ptr)
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1, _dataTable.inArray2, _dataTable.outArray);
        }

        public void RunReflectionScenario()
        {
            var result = typeof(Sse2).GetMethod(nameof(Sse2.Add), new Type[] { typeof(Vector128<UInt64>), typeof(Vector128<UInt64>) })
                                     .Invoke(null, new object[] {
                                        Unsafe.Read<Vector128<UInt64>>(_dataTable.inArray1Ptr),
                                        Unsafe.Read<Vector128<UInt64>>(_dataTable.inArray2Ptr)
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<UInt64>)(result));
            ValidateResult(_dataTable.inArray1, _dataTable.inArray2, _dataTable.outArray);
        }

        public void RunClsVarScenario()
        {
            var result = Sse2.Add(
                _clsVar1,
                _clsVar2
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_clsVar1, _clsVar2, _dataTable.outArray);
        }

        public void RunLclVarScenario()
        {
            var left = Unsafe.Read<Vector128<UInt64>>(_dataTable.inArray1Ptr);
            var right = Unsafe.Read<Vector128<UInt64>>(_dataTable.inArray2Ptr);
            var result = Sse2.Add(left, right);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(left, right, _dataTable.outArray);
        }

        public void RunLclFldScenario()
        {
            var test = new SimpleBinaryOpTest__AddUInt64();
            var result = Sse2.Add(test._fld1, test._fld2);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld1, test._fld2, _dataTable.outArray);
        }

        public void RunFldScenario()
        {
            var result = Sse2.Add(_fld1, _fld2);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_fld1, _fld2, _dataTable.outArray);
        }

        public void RunUnsupportedScenario()
        {
            Succeeded = false;

            try
            {
                RunBasicScenario();
            }
            catch (PlatformNotSupportedException)
            {
                Succeeded = true;
            }
        }

        private void ValidateResult(Vector128<UInt64> left, Vector128<UInt64> right, UInt64[] result, [CallerMemberName] string method = "")
        {
            UInt64[] inArray1 = new UInt64[ElementCount];
            UInt64[] inArray2 = new UInt64[ElementCount];

            Unsafe.Write(Unsafe.AsPointer(ref inArray1[0]), left);
            Unsafe.Write(Unsafe.AsPointer(ref inArray2[0]), right);

            ValidateResult(inArray1, inArray2, result, method);
        }

        private void ValidateResult(UInt64[] left, UInt64[] right, UInt64[] result, [CallerMemberName] string method = "")
        {
            if ((ulong)(left[0] + right[0]) != result[0])
            {
                Succeeded = false;
            }
            else
            {
                for (var i = 1; i < left.Length; i++)
                {
                    if ((ulong)(left[i] + right[i]) != result[i])
                    {
                        Succeeded = false;
                        break;
                    }
                }
            }

            if (!Succeeded)
            {
                Console.WriteLine($"{nameof(Sse2)}.{nameof(Sse2.Add)}<UInt64>: {method} failed:");
                Console.WriteLine($"    left: ({string.Join(", ", left)})");
                Console.WriteLine($"   right: ({string.Join(", ", right)})");
                Console.WriteLine($"  result: ({string.Join(", ", result)})");
                Console.WriteLine();
            }
        }
    }
}
