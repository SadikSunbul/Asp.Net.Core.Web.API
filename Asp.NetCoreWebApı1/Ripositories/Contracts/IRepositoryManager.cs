﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ripositories.Contracts
{
    public interface IRepositoryManager
    {
        IBookRepository Book { get; }
        Task SaveAsync(); //Task<void> hatalıdır ->Task yazılır
    }
}
