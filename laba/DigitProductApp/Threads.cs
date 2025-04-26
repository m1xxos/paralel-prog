using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Thread01
{
    public class Class1
    {
        public static List<int> Result = new List<int>();
        private static Object locker = new object();

        public static void Op1(Object? param)
        {
            if (param is Params parameters)
            {
                int s = 0;
                for (int i = parameters.A; i <= parameters.B; i++)
                {
                    for (int j = 0; j <= 9; j++)
                    {
                        for (int k = 0; k <= 9; k++)
                        {
                            for (int l = 0; l <= 9; l++)
                            {
                                for (int ii = 0; ii <= 9; ii++)
                                {
                                    for (int jj = 0; jj <= 9; jj++)
                                    {
                                        for (int kk = 0; kk <= 9; kk++)
                                        {
                                            for (int ll = 0; ll <= 9; ll++)
                                            {
                                                if (i + j + k + l == ii + jj + kk + ll)
                                                {
                                                    s++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                lock (locker)
                {
                    Result.Add(s);
                }
            }
        }

        public static void ClearResult()
        {
            lock (locker)
            {
                Result.Clear();
            }
        }

        public static int GetResult()
        {
            return Result.Sum();
        }
    }
}
