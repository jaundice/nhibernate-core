﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iesi.Collections.Portable
{
    public interface ICloneable
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// 
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        object Clone();
    }
}