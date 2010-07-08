﻿using System;

namespace common
{
    static public class FuncExtensions
    {
        static readonly object mutex = new object();

        static public Func<T> memorize<T>(this Func<T> item) where T : class
        {
            T the_implementation = null;
            return () =>
            {
                if (null == the_implementation)
                {
                    lock (mutex)
                    {
                        if (null == the_implementation)
                        {
                            the_implementation = item();
                        }
                    }
                }
                return the_implementation;
            };
        }
    }
}