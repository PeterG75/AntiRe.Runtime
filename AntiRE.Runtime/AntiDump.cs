using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AntiRE.Runtime
{
    public class AntiDump
    {
        /// <summary>
        /// Prevent assembly being dumped from memory (not stable may cause some problems)
        /// </summary>
        public static void Parse(Type type)
        {
            unsafe
            {
                Module module = type.Module;
                byte* ptr = (byte*)((void*)Marshal.GetHINSTANCE(module));
                byte* ptr2 = ptr + 60;
                ptr2 = ptr + *(uint*)ptr2;
                ptr2 += 6;
                ushort num = *(ushort*)ptr2;
                ptr2 += 14;
                ushort num2 = *(ushort*)ptr2;
                ptr2 = ptr2 + 4 + num2;
                byte* ptr3 = stackalloc byte[(int)(UIntPtr)11];
                bool flag = module.FullyQualifiedName[0] != '<';
                if (flag)
                {
                    byte* ptr4 = ptr + *(uint*)(ptr2 - 16);
                    bool flag2 = *(uint*)(ptr2 - 120) > 0u;
                    uint num3 = 0;
                    if (flag2)
                    {
                        byte* ptr5 = ptr + *(uint*)(ptr2 - 120);
                        byte* ptr6 = ptr + *(uint*)ptr5;
                        byte* ptr7 = ptr + *(uint*)(ptr5 + 12);
                        byte* ptr8 = ptr + *(uint*)ptr6 + 2;

                        VM.VM936799001(ptr7, 11, 64u, ref num3);
                        *(int*)ptr3 = 1818522734;
                        *(int*)(ptr3 + 4) = 1818504812;
                        *(short*)(ptr3 + (int)(IntPtr)4 * 2) = 108;
                        ptr3[10] = 0;
                        for (int i = 0; i < 11; i++)
                        {
                            ptr7[i] = ptr3[i];
                        }

                        VM.VM936799001(ptr8, 11, 64u, ref num3);
                        *(int*)ptr3 = 1866691662;
                        *(int*)(ptr3 + 4) = 1852404846;
                        *(short*)(ptr3 + (int)(IntPtr)4 * 2) = 25973;
                        ptr3[10] = 0;
                        for (int j = 0; j < 11; j++)
                        {
                            ptr8[j] = ptr3[j];
                        }
                    }
                    for (int k = 0; k < (int)num; k++)
                    {

                        VM.VM936799001(ptr2, 8, 64u, ref num3);
                        Marshal.Copy(new byte[8], 0, (IntPtr)((void*)ptr2), 8);
                        ptr2 += 40;
                    }

                    VM.VM936799001(ptr4, 72, 64u, ref num3);
                    byte* ptr9 = ptr + *(uint*)(ptr4 + 8);
                    *(int*)ptr4 = 0;
                    *(int*)(ptr4 + 4) = 0;
                    *(int*)(ptr4 + (int)(IntPtr)2 * 4) = 0;
                    *(int*)(ptr4 + (int)(IntPtr)3 * 4) = 0;

                    VM.VM936799001(ptr9, 4, 64u, ref num3);
                    *(int*)ptr9 = 0;
                    ptr9 += 12;
                    ptr9 += *(uint*)ptr9;
                    ptr9 += (7L & -4L);
                    ptr9 += 2;
                    ushort num4 = (ushort)(*ptr9);
                    ptr9 += 2;
                    for (int l = 0; l < (int)num4; l++)
                    {

                        VM.VM936799001(ptr9, 8, 64u, ref num3);
                        ptr9 += 4;
                        ptr9 += 4;
                        for (int m = 0; m < 8; m++)
                        {

                            VM.VM936799001(ptr9, 4, 64u, ref num3);
                            *ptr9 = 0;
                            ptr9++;
                            bool flag3 = *ptr9 == 0;
                            if (flag3)
                            {
                                ptr9 += 3;
                                break;
                            }
                            *ptr9 = 0;
                            ptr9++;
                            bool flag4 = *ptr9 == 0;
                            if (flag4)
                            {
                                ptr9 += 2;
                                break;
                            }
                            *ptr9 = 0;
                            ptr9++;
                            bool flag5 = *ptr9 == 0;
                            if (flag5)
                            {
                                ptr9++;
                                break;
                            }
                            *ptr9 = 0;
                            ptr9++;
                        }
                    }
                }
                else
                {
                    uint num5 = *(uint*)(ptr2 - 16);
                    uint num6 = *(uint*)(ptr2 - 120);
                    uint[] array = new uint[(int)num];
                    uint[] array2 = new uint[(int)num];
                    uint[] array3 = new uint[(int)num];
                    uint num3 = 0;
                    for (int n = 0; n < (int)num; n++)
                    {

                        VM.VM936799001(ptr2, 8, 64u, ref num3);
                        Marshal.Copy(new byte[8], 0, (IntPtr)((void*)ptr2), 8);
                        array[n] = *(uint*)(ptr2 + 12);
                        array2[n] = *(uint*)(ptr2 + 8);
                        array3[n] = *(uint*)(ptr2 + 20);
                        ptr2 += 40;
                    }
                    bool flag6 = num6 > 0u;
                    if (flag6)
                    {
                        for (int num7 = 0; num7 < (int)num; num7++)
                        {
                            bool flag7 = array[num7] <= num6 && num6 < array[num7] + array2[num7];
                            if (flag7)
                            {
                                num6 = num6 - array[num7] + array3[num7];
                                break;
                            }
                        }
                        byte* ptr10 = ptr + num6;
                        uint num8 = *(uint*)ptr10;
                        for (int num9 = 0; num9 < (int)num; num9++)
                        {
                            bool flag8 = array[num9] <= num8 && num8 < array[num9] + array2[num9];
                            if (flag8)
                            {
                                num8 = num8 - array[num9] + array3[num9];
                                break;
                            }
                        }
                        byte* ptr11 = ptr + num8;
                        uint num10 = *(uint*)(ptr10 + 12);
                        for (int num11 = 0; num11 < (int)num; num11++)
                        {
                            bool flag9 = array[num11] <= num10 && num10 < array[num11] + array2[num11];
                            if (flag9)
                            {
                                num10 = num10 - array[num11] + array3[num11];
                                break;
                            }
                        }
                        uint num12 = *(uint*)ptr11 + 2u;
                        for (int num13 = 0; num13 < (int)num; num13++)
                        {
                            bool flag10 = array[num13] <= num12 && num12 < array[num13] + array2[num13];
                            if (flag10)
                            {
                                num12 = num12 - array[num13] + array3[num13];
                                break;
                            }
                        }

                        VM.VM936799001(ptr + num10, 11, 64u, ref num3);
                        *(int*)ptr3 = 1818522734;
                        *(int*)(ptr3 + 4) = 1818504812;
                        *(short*)(ptr3 + (int)(IntPtr)4 * 2) = 108;
                        ptr3[10] = 0;
                        for (int num14 = 0; num14 < 11; num14++)
                        {
                            (ptr + num10)[num14] = ptr3[num14];
                        }

                        VM.VM936799001(ptr + num12, 11, 64u, ref num3);
                        *(int*)ptr3 = 1866691662;
                        *(int*)(ptr3 + 4) = 1852404846;
                        *(short*)(ptr3 + (int)(IntPtr)4 * 2) = 25973;
                        ptr3[10] = 0;
                        for (int num15 = 0; num15 < 11; num15++)
                        {
                            (ptr + num12)[num15] = ptr3[num15];
                        }
                    }
                    for (int num16 = 0; num16 < (int)num; num16++)
                    {
                        bool flag11 = array[num16] <= num5 && num5 < array[num16] + array2[num16];
                        if (flag11)
                        {
                            num5 = num5 - array[num16] + array3[num16];
                            break;
                        }
                    }
                    byte* ptr12 = ptr + num5;

                    VM.VM936799001(ptr12, 72, 64u, ref num3);
                    uint num17 = *(uint*)(ptr12 + 8);
                    for (int num18 = 0; num18 < (int)num; num18++)
                    {
                        bool flag12 = array[num18] <= num17 && num17 < array[num18] + array2[num18];
                        if (flag12)
                        {
                            num17 = num17 - array[num18] + array3[num18];
                            break;
                        }
                    }
                    *(int*)ptr12 = 0;
                    *(int*)(ptr12 + 4) = 0;
                    *(int*)(ptr12 + (int)(IntPtr)2 * 4) = 0;
                    *(int*)(ptr12 + (int)(IntPtr)3 * 4) = 0;
                    byte* ptr13 = ptr + num17;

                    VM.VM936799001(ptr13, 4, 64u, ref num3);
                    *(int*)ptr13 = 0;
                    ptr13 += 12;
                    ptr13 += *(uint*)ptr13;
                    ptr13 += (7L & -4L);
                    ptr13 += 2;
                    ushort num19 = (ushort)(*ptr13);
                    ptr13 += 2;
                    for (int num20 = 0; num20 < (int)num19; num20++)
                    {

                        VM.VM936799001(ptr13, 8, 64u, ref num3);
                        ptr13 += 4;
                        ptr13 += 4;
                        for (int num21 = 0; num21 < 8; num21++)
                        {

                            VM.VM936799001(ptr13, 4, 64u, ref num3);
                            *ptr13 = 0;
                            ptr13++;
                            bool flag13 = *ptr13 == 0;
                            if (flag13)
                            {
                                ptr13 += 3;
                                break;
                            }
                            *ptr13 = 0;
                            ptr13++;
                            bool flag14 = *ptr13 == 0;
                            if (flag14)
                            {
                                ptr13 += 2;
                                break;
                            }
                            *ptr13 = 0;
                            ptr13++;
                            bool flag15 = *ptr13 == 0;
                            if (flag15)
                            {
                                ptr13++;
                                break;
                            }
                            *ptr13 = 0;
                            ptr13++;
                        }
                    }
                }
            }
        }
    }
}
